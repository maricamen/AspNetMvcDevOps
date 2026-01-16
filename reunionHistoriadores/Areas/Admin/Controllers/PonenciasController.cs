using reunionhistoriadores2025.AuthorizationPolicies;
using reunionhistoriadores2025.Data;
using reunionhistoriadores2025.Localization;
using reunionhistoriadores2025.Models;
using reunionhistoriadores2025.Services.Email;
using reunionhistoriadores2025.Services.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using X.PagedList;
using reunionhistoriadores2025.Services.ErrorLog;
using reunionhistoriadores2025.Areas.Admin.ViewModels.Ponencias;
using System.IO.Compression;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;

namespace reunionhistoriadores2025.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PonenciasController : Controller
    {
        private readonly IErrorLog _errorlog;
        private readonly RegistroContext _context;
        private readonly GlobalOptions _opciones;
        private readonly IStringLocalizer<PonenciasController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IEmail _email;
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public PonenciasController(IErrorLog errorlog, RegistroContext context, IOptionsSnapshot<GlobalOptions> opciones, IStringLocalizer<PonenciasController> localizer, IStringLocalizer<SharedResource> sharedLocalizer, IEmail email, SignInManager<CustomIdentityUser> signInManager, UserManager<CustomIdentityUser> userManager)
        {
            _errorlog = errorlog;
            _context = context;
            _opciones = opciones.Value;
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
            _email = email;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //public async Task<IActionResult> Index(string buscar, string filtroRelacion, int? page)
        public async Task<IActionResult> Index(string filtroRelacion, int? page)
        {

            int pageSize = 10;
            int pageNumber = page ?? 1;

            var query = _context.Ponencias
                .Include(p => p.Expositores)
                .Include(p => p.Archivos)
                .Include(p => p.LineaTematica)
                .Include(p => p.Regitros)
                    .ThenInclude(p => p.RegistroFolio)
                .Where(s => s.Visible)
                .AsQueryable();

            ViewBag.Total = query.Count().ToString();

            //  Filtro por texto 
            //if (!string.IsNullOrEmpty(buscar))
            //{
            //    query = query.Where(p => p.Titulo.Contains(buscar) ||
            //                             p.Expositores.Nombre.Contains(buscar));
            //}

            // Filtro por relación
            if (!string.IsNullOrEmpty(filtroRelacion))
            {
                if (filtroRelacion == "Mesas")
                {
                    query = query.Where(p => p.MesaId != null);
                }
                else if (filtroRelacion == "Simposios")
                {
                    query = query.Where(p => p.SimposioId != null);
                }
            }

            var ponencias = await query
                .Select(p => new PonenciaListadoViewModel
                {
                    Id = p.Id,
                    Folio = p.Regitros.RegistroFolio.Clave,
                    Titulo = p.Titulo,
                    ExpositorId = p.Expositores != null ? p.Expositores.Id : null,
                    ExpositorNombre = p.Expositores != null ? p.Expositores.Nombre : "(Sin expositor)",
                    ExpositorCorreo = p.Expositores.Correo,
                    LineaTematica = p.LineaTematica.Nombre ?? "-",
                    ArchivoNombre = p.Archivos != null && p.Archivos.Any()
                        ? p.Archivos.FirstOrDefault().Nombre
                        : "(Sin archivo)",
                    Fecha = p.Regitros.Fecha.ToString("dd/MM/yyyy"),
                })
                .OrderBy(p => p.Folio)
                .ToPagedListAsync(pageNumber, pageSize);

            ViewBag.FiltroRelacion = filtroRelacion;
            return View(ponencias);
        }
        public async Task<IActionResult> Detalles(int id)
        {

            var ponencia = _context.Ponencias
                .Include(e => e.Simposios)
                    .ThenInclude(e => e.LineaTematica)
                .Include(e => e.Mesas)
                    .ThenInclude(e => e.LineaTematica)
                .Include(p => p.Expositores)
                    .ThenInclude(e => e.Genero)
                .Include(p => p.Expositores)
                    .ThenInclude(e => e.Paises)
                .Include(e => e.Regitros.RegistroFolio)
                .Include(e => e.Archivos)
                .Include(e => e.LineaTematica)
                .Where(e => e.Id == id).FirstOrDefault();

            if(ponencia.LineaTematicaId == null)
            {
                if(ponencia.SimposioId == null)
                {
                    ViewData["Tema"] = "Ponencia de la mesa: " +  ponencia.Mesas.Nombre + " con el tema: " +ponencia.Mesas.LineaTematica.Nombre;
                }
                else
                {
                    ViewData["Tema"] = "Ponencia del simposio: " + ponencia.Simposios.Nombre + " con el tema: " + ponencia.Simposios.LineaTematica.Nombre;
                }
            }
            else if(ponencia.LineaTematicaId == 9)
            {
                ViewData["Tema"] = "Otro "+ " - " + ponencia.LineaTematicaOtro;
            }
            else if (ponencia.LineaTematicaId != 9 && ponencia.LineaTematicaId != null)
            {
                ViewData["Tema"] = ponencia.LineaTematica.Nombre;
            }

            return View(ponencia);
        }

        public async Task<IActionResult> DescargarArchivo(int id)
        {
            var archivo = await _context.Archivos.FirstOrDefaultAsync(a => a.PonenciaId == id);

            if (archivo == null || archivo.Contenido == null)
                return NotFound();

            return File(archivo.Contenido, archivo.Mime, archivo.Nombre);
        }
        public async Task<IActionResult> ExportarExcel()
        {
            var ponencias = await _context.Ponencias
                .Include(p => p.LineaTematica)
                .Include(p => p.Expositores)
                    .ThenInclude(p => p.Genero)
                .Include(p => p.Archivos)
                .Include(p => p.Regitros)
                    .ThenInclude(p => p.RegistroFolio)
                .Select(p => new
                {
                    Fecha = p.Regitros.Fecha,
                    Folio = p.Regitros.RegistroFolio.Clave,
                    Ponencia = p.Titulo,
                    Tema = p.LineaTematicaId == null ? (p.SimposioId != null ? "Tema de simposio" : "Tema de mesa") : p.LineaTematica.Nombre,
                    Expositor =  p.Expositores.Nombre,
                    GradoAcademico = p.Expositores.GradoAcademico,
                    Genero = p.Expositores.Genero.Nombre,
                    //Autodescripcion = p.Expositores.Autodescripcion,
                    Correo = p.Expositores.Correo,
                    Institucion = p.Expositores.Institucion,
                    Pais = p.Expositores.Paises.Nombre,
                    Archivo = p.Archivos != null && p.Archivos.Any()
                        ? p.Archivos.FirstOrDefault().Nombre
                        : "(Sin archivo)"
                })
                .ToListAsync();

            using (var memoryStream = new MemoryStream())
            {
                using (var spreadsheet = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
                {
                    var workbookPart = spreadsheet.AddWorkbookPart();
                    workbookPart.Workbook = new Workbook();

                    var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    var sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    var sheets = spreadsheet.WorkbookPart.Workbook.AppendChild(new Sheets());
                    var sheet = new Sheet()
                    {
                        Id = spreadsheet.WorkbookPart.GetIdOfPart(worksheetPart),
                        SheetId = 1,
                        Name = "Ponencias"
                    };
                    sheets.Append(sheet);

                    // Encabezados
                    var headerRow = new Row();
                    headerRow.Append(
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("Fecha") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("Folio") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("TítuloPonencia") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("Tema") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("Expositor") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("GradoAcadémico") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("Género") },
                        //new Cell { DataType = CellValues.String, CellValue = new CellValue("Autodescripción") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("Institución") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("Correo") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("País") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("Archivo") }
                    );
                    sheetData.AppendChild(headerRow);

                    // Datos
                    foreach (var p in ponencias)
                    {
                        var row = new Row();
                        row.Append(
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(p.Fecha.ToString("dd-MM-yyyy")) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(p.Folio) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(p.Ponencia) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(p.Tema) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(p.Expositor) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(p.GradoAcademico) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(p.Genero) },
                            //new Cell { DataType = CellValues.String, CellValue = new CellValue(p.Autodescripcion) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(p.Institucion) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(p.Correo) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(p.Pais) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(p.Archivo) }
                        );
                        sheetData.AppendChild(row);
                    }

                    workbookPart.Workbook.Save();
                }

                memoryStream.Position = 0;

                return File(
                    memoryStream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "ListadoPonencias.xlsx"
                );
            }
        }
        //private string SanitizeFileName(string name)
        //{
        //    foreach (var c in Path.GetInvalidFileNameChars())
        //    {
        //        name = name.Replace(c, '_');
        //    }
        //    return name;
        //}

        public async Task<IActionResult> DescargarZip()
        {
            var ponenciasConArchivos = await _context.Ponencias
                .Include(p => p.Archivos)
                .Include(p => p.Regitros)
                .ThenInclude(p => p.RegistroFolio)
                .ToListAsync();

            if (ponenciasConArchivos == null || ponenciasConArchivos.Count == 0)
            {
                TempData["Mensaje"] = "No hay archivos para descargar.";
                return RedirectToAction(nameof(Index));
            }

            using (var memoryStream = new MemoryStream())
            {
                using (var zip = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var ponencia in ponenciasConArchivos)
                    {
                        if (ponencia.Archivos != null && ponencia.Archivos.Any())
                        {
                            foreach (var archivo in ponencia.Archivos)
                            {
                                if (archivo.Contenido == null)
                                    continue;

                                // Nombre dentro del ZIP - "TituloPonencia/NombredelArchivo.pdf"
                                //var entryName = $"{SanitizeFileName(ponencia.Titulo)}/{archivo.Nombre}";
                                string nombreArchivo = archivo.Ponencia.Regitros.RegistroFolio.Clave + "-" + archivo.Nombre;

                                var zipEntry = zip.CreateEntry(nombreArchivo, CompressionLevel.Fastest);
                                using (var entryStream = zipEntry.Open())
                                {
                                    await entryStream.WriteAsync(archivo.Contenido, 0, archivo.Contenido.Length);
                                }
                            }
                        }
                    }
                }

                memoryStream.Position = 0;

                return File(
                    memoryStream.ToArray(),
                    "application/zip",
                    "Archivos-Ponencias.zip"
                );
            }
        }        

        [Authorize(Policy = nameof(AdminRolePolicies.AdministradorGeneralPolicy))]
        public async Task<IActionResult> ArchivosAsync(int id, string sortOrder, string currentFilter, string currentStatus, int? pageNumber)
        {
            var registro = await _context.Registros.Include(r => r.RegistroFolio).Include(r => r.RegistroTipo).Include(r => r.RegistroStatus).AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            ViewData["SubTitle"] = registro.RegistroFolio.Clave;

            if (registro == null)
            {
                return NotFound();
            }

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = currentFilter;
            ViewData["CurrentStatus"] = currentStatus;
            ViewData["PageNumber"] = pageNumber;

            return View(registro);
        }

        private string CuerpoCorreo(int Id, string Mensaje)
        {
            var registro = _context.Registros.Include(r => r.RegistroFolio).AsNoTracking().FirstOrDefault(r => r.Id == Id);
            Mensaje = $"<p>{Mensaje}</p>";

            if (registro != null)
            {
                if (registro.RegistroStatusId == (int)RegistroStatusId.ACEPTADO)
                {
                    Mensaje += $"<p>{ViewData["SiteSubTitle"]} agradece el envío de su participación:</p>";

                    switch (registro.RegistroTipoId)
                    {
                        case 1:
                            var registro1 = _context.Ponencias.AsNoTracking().FirstOrDefault(r => r.Id == Id);
                            Mensaje += $"<p>{registro1.Titulo}</p>";
                            break;
                    }

                    Mensaje += "<p>Mismo que ha sido revisado y dictaminado a doble ciego considerando que cumple con los requisitos de formato establecido en la convocatoria. Una vez evaluado se ha decidido:</p>";
                    Mensaje += "<p>ACEPTAR su participación</p>";
                    Mensaje += "<p>Sin otro particular, reciba un cordial saludo de nuestra parte.</p>";
                }

                Mensaje += $"Correo registrado: {registro.Correo}<br />";
                Mensaje += $"Folio: {registro.RegistroFolio.Clave}<br />";
            }

            Mensaje += "<p>Agredecemos su participación.</p>";
            Mensaje += "<br /><br /><br /><div>-----------------------------------------------------------------</div>";
            Mensaje += "<div>Mensaje enviado automáticamente. Favor de no responder al remitente de este mensaje.</div>";

            return Mensaje;
        }

    }
}

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using reunionhistoriadores2025.Areas.Admin.Controllers;
using reunionhistoriadores2025.Data;
using reunionhistoriadores2025.Localization;
using reunionhistoriadores2025.Models;
using reunionhistoriadores2025.Services.Email;
using reunionhistoriadores2025.Services.ErrorLog;
using reunionhistoriadores2025.Services.Options;
using reunionhistoriadores2025.ViewModels;
using System.IO.Compression;
using X.PagedList;

namespace reunionHistoriadores2025.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SimposiosController : Controller
    {
        private readonly IErrorLog _errorlog;
        private readonly RegistroContext _context;
        private readonly GlobalOptions _opciones;
        private readonly IStringLocalizer<SimposiosController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IEmail _email;
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly UserManager<CustomIdentityUser> _userManager;
        public SimposiosController(IErrorLog errorlog, RegistroContext context, IOptionsSnapshot<GlobalOptions> opciones, IStringLocalizer<SimposiosController> localizer, IStringLocalizer<SharedResource> sharedLocalizer, IEmail email, SignInManager<CustomIdentityUser> signInManager, UserManager<CustomIdentityUser> userManager)
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
        public async Task<IActionResult> Index(string buscar, int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var query = _context.Simposios
                .Include(l => l.LineaTematica)
                .Include(s => s.Regitros)
                    .ThenInclude(r => r.RegistroFolio)
                .Where(s => s.Visible)
                .AsQueryable();

            ViewBag.Total = query.Count().ToString();

            //if (!string.IsNullOrEmpty(buscar))
            //{
            //    buscar = buscar.Trim().ToLower();
            //    query = query.Where(s =>
            //        s.Regitros.Correo.ToLower().Contains(buscar) ||
            //        s.Regitros.RegistroFolio.Clave.ToLower().Contains(buscar)
            //    );
            //}

            var simposios = await query
                .OrderBy(p => p.Regitros.RegistroFolio.Clave)
                .ToPagedListAsync(pageNumber, pageSize);

            //ViewBag.Buscar = buscar; // Para mantener el texto del input
            return View(simposios);
        }

        public async Task<IActionResult> Detalles(int id)
        {
            var simposio = await _context.Simposios
            .Include(s => s.LineaTematica)
            .Include(s => s.Mesas)
            .Include(s => s.Ponencias)
                .ThenInclude(s => s.Expositores)
                .ThenInclude(s => s.Genero)
            .Include(s => s.Ponencias)
                .ThenInclude(s => s.Expositores)
                .ThenInclude(s => s.Paises)
            .Include(s => s.Regitros.RegistroFolio)
            .Include(s => s.Archivos)
            .FirstOrDefaultAsync(s => s.Id == id);

            if (simposio == null)
            {
                return NotFound();
            }

            ViewBag.SimposioArchivo = simposio.Archivos?.FirstOrDefault()?.Nombre;
            ViewBag.SimposioId = simposio.Id;

            ViewData["Tema"] = simposio.LineaTematica.Nombre;
            ViewData["Folio"] = simposio.Regitros.RegistroFolio.Clave;
            ViewData["SimposioNombre"] = simposio.Nombre;
            ViewData["NombreCoordinador"] = simposio.CoordinadorNombre;
            ViewData["CorreoCoordinador"] = simposio.CoordinadorCorreo;
            ViewData["InstitucionCoordinador"] = simposio.CoordinadorInstitucion;

            return View(simposio);
        }

        public async Task<IActionResult> DescargarArchivo(int id)
        {
            var archivo = await _context.Archivos.FirstOrDefaultAsync(a => a.Id == id);

            if (archivo == null || archivo.Contenido == null)
                return NotFound();

            return File(archivo.Contenido, archivo.Mime, archivo.Nombre);
        }

        public async Task<IActionResult> ExportarExcel()
        {
            var simposios = await _context.Simposios
                .Include(p => p.LineaTematica)
                .Include(p => p.Archivos)
                .Include(p => p.Regitros)
                .Include(p => p.Mesas)
                .Include(p => p.Ponencias)
                    .ThenInclude(p => p.Expositores)
                    .ThenInclude(p => p.Genero)
               .Include(p => p.Ponencias)
                    .ThenInclude(p => p.Expositores)
                    .ThenInclude(p => p.Paises)
                .Select(p => new
                {
                    Fecha = p.Regitros.Fecha,
                    Folio = p.Regitros.RegistroFolio.Clave,
                    Simposio = p.Nombre,
                    Tema = p.LineaTematica.Nombre,
                    SCoordinadorNombre = p.CoordinadorNombre,
                    SCoordinadorCorreo = p.CoordinadorCorreo,
                    Archivo = p.Archivos != null && p.Archivos.Any()
                        ? p.Archivos.FirstOrDefault().Nombre
                        : "(Sin archivo)",
                    Mesas = p.Mesas.Select(p => new
                    {
                        MCoordinadorNombre = p.CoordinadorNombre,
                        MCoordinadorCorreo = p.CoordinadorCorreo,
                        MArchivo = p.Archivos != null && p.Archivos.Any()
                        ? p.Archivos.FirstOrDefault().Nombre
                        : "(Sin archivo)",
                        Ponencias = p.Ponencias.Select(p => new
                        {
                            PTitulo = p.Titulo,
                            PExpositor = p.Expositores.Nombre,
                            PCorreoExpositor = p.Expositores.Correo,
                            PInstitucionExpositor = p.Expositores.Institucion,
                            PGenero = p.Expositores.Genero.Nombre,
                            PPais = p.Expositores.Paises.Nombre
                        }).ToList()
                    }).ToList()
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
                        Name = "Simposios"
                    };
                    sheets.Append(sheet);

                    // Encabezados
                    var headerRow = new Row();
                    headerRow.Append(
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("Fecha") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("Folio") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("NombreSimposio") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("Tema") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("NombreCoordinadorSimposio") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("CorreoCoordinadorSimposio") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("ArchivoSimposio") }
                    );
                    for (int i = 1; i <= 3; i++)
                    {
                        headerRow.Append(
                                new Cell { DataType = CellValues.String, CellValue = new CellValue($"Mesa {i} - NombreCoordinadorMesa") },
                                new Cell { DataType = CellValues.String, CellValue = new CellValue($"Mesa {i} - CorreoCoordinadorMesa") },
                                new Cell { DataType = CellValues.String, CellValue = new CellValue($"Mesa {i} - ArchivoMesa") }
                        );
                        for (int j = 1; j <= 4; j++)
                        {
                            headerRow.Append(
                                new Cell { DataType = CellValues.String, CellValue = new CellValue($"Mesa {i} - Ponencia {j} - Título") },
                                new Cell { DataType = CellValues.String, CellValue = new CellValue($"Mesa {i} - Ponencia {j} - Expositor") },
                                new Cell { DataType = CellValues.String, CellValue = new CellValue($"Mesa {i} - Ponencia {j} - CorreoExpositor") },
                                new Cell { DataType = CellValues.String, CellValue = new CellValue($"Mesa {i} - Ponencia {j} - InstituciónAdscripción") },
                                new Cell { DataType = CellValues.String, CellValue = new CellValue($"Mesa {i} - Ponencia {j} - Género") },
                                new Cell { DataType = CellValues.String, CellValue = new CellValue($"Mesa {i} - Ponencia {j} - País") }
                            );
                        }
                    }
                    sheetData.AppendChild(headerRow);

                    // Datos
                    foreach (var s in simposios)
                    {
                        var row = new Row();
                        row.Append(
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(s.Fecha.ToString("dd-MM-yyyy")) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(s.Folio) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(s.Simposio) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(s.Tema) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(s.SCoordinadorNombre) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(s.SCoordinadorCorreo) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(s.Archivo) }
                        );

                        //

                        // Mesas (hasta 3)
                        for (int mIndex = 0; mIndex < 3; mIndex++)
                        {
                            if (mIndex < s.Mesas.Count)
                            {
                                var mesa = s.Mesas[mIndex];
                                row.Append(
                                    new Cell { DataType = CellValues.String, CellValue = new CellValue(mesa.MCoordinadorNombre) },
                                    new Cell { DataType = CellValues.String, CellValue = new CellValue(mesa.MCoordinadorCorreo) },
                                    new Cell { DataType = CellValues.String, CellValue = new CellValue(mesa.MArchivo) }
                                );

                                // Ponencias (hasta 4)
                                for (int pIndex = 0; pIndex < 4; pIndex++)
                                {
                                    if (pIndex < mesa.Ponencias.Count)
                                    {
                                        var pon = mesa.Ponencias[pIndex];
                                        row.Append(
                                            new Cell { DataType = CellValues.InlineString, InlineString = new InlineString(new Text(pon.PTitulo)) },
                                            new Cell { DataType = CellValues.InlineString, InlineString = new InlineString(new Text(pon.PExpositor)) },
                                            new Cell { DataType = CellValues.InlineString, InlineString = new InlineString(new Text(pon.PCorreoExpositor)) },
                                            new Cell { DataType = CellValues.InlineString, InlineString = new InlineString(new Text(pon.PInstitucionExpositor)) },
                                            new Cell { DataType = CellValues.InlineString, InlineString = new InlineString(new Text(pon.PGenero)) },
                                            new Cell { DataType = CellValues.InlineString, InlineString = new InlineString(new Text(pon.PPais)) }
                                        );
                                    }
                                }
                            }
                        }

                        //
                        sheetData.AppendChild(row);
                    }

                    workbookPart.Workbook.Save();
                }

                memoryStream.Position = 0;

                return File(
                    memoryStream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "ListadoSimposios.xlsx"
                );
            }
        }

        public async Task<IActionResult> DescargarZip()
        {
            var simposios = await _context.Simposios
                .Include(m => m.Archivos)
                .Include(p => p.Regitros)
                .ThenInclude(p => p.RegistroFolio)
                .ToListAsync();

            var archivos = simposios
                .Where(m => m.Archivos != null)
                .SelectMany(m => m.Archivos)
                .ToList();

            if (archivos == null || archivos.Count == 0)
            {
                TempData["Mensaje"] = "No hay archivos para descargar.";
                return RedirectToAction(nameof(Index));
            }

            using (var memoryStream = new MemoryStream())
            {
                using (var zip = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var archivo in archivos)
                    {
                        if (archivo.Contenido != null && archivo.Contenido.Length > 0)
                        {
                            string nombreArchivo = archivo.Simposio.Regitros.RegistroFolio.Clave + "-" + archivo.Nombre;

                            var zipEntry = zip.CreateEntry(nombreArchivo, CompressionLevel.Fastest);

                            using (var entryStream = zipEntry.Open())
                            {
                                entryStream.Write(archivo.Contenido, 0, archivo.Contenido.Length);
                            }
                        }
                    }
                }

                memoryStream.Position = 0;
                return File(memoryStream.ToArray(), "application/zip", "Archivos-Simposios.zip");
            }
        }

    }
}

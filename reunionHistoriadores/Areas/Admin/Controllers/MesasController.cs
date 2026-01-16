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
    public class MesasController : Controller
    {
        private readonly IErrorLog _errorlog;
        private readonly RegistroContext _context;
        private readonly GlobalOptions _opciones;
        private readonly IStringLocalizer<MesasController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IEmail _email;
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly UserManager<CustomIdentityUser> _userManager;
        public MesasController(IErrorLog errorlog, RegistroContext context, IOptionsSnapshot<GlobalOptions> opciones, IStringLocalizer<MesasController> localizer, IStringLocalizer<SharedResource> sharedLocalizer, IEmail email, SignInManager<CustomIdentityUser> signInManager, UserManager<CustomIdentityUser> userManager)
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
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var mesas = await _context.Mesas
                .Include(p => p.LineaTematica)
                .Include(p => p.Regitros)
                .ThenInclude(p => p.RegistroFolio)
                .Where(s => s.Visible)
                .OrderBy(p => p.Regitros.RegistroFolio.Clave)
                .ToPagedListAsync(pageNumber, pageSize);

            ViewBag.Total = mesas.Count().ToString();

            return View(mesas);
        }

        public async Task<IActionResult> Detalles(int id)
        {
            var mesa = await _context.Mesas
                .Include(e => e.Simposios)
                    .ThenInclude(e => e.LineaTematica)
                .Include(m => m.LineaTematica)
                .Include(e => e.Regitros.RegistroFolio)
                .Include(e => e.Archivos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mesa == null)
            {
                return NotFound();
            }

            ViewBag.MesaArchivo = mesa.Archivos?.FirstOrDefault()?.Nombre;
            ViewBag.MesaId = mesa.Id;

            // ponencias relacionadas
            var ponencias = await _context.Ponencias
                .Include(p => p.Expositores)
                    .ThenInclude(g => g.Genero)
                .Include(p => p.Expositores)
                    .ThenInclude(g => g.Paises)
                .Include(p => p.Archivos)
                .Where(p => p.MesaId == id)
                .ToListAsync();

            if (mesa.LineaTematicaId == null)
            {
                ViewData["Tema"] = "Mesa del simposio: " + mesa.Simposios.Nombre + " con el tema: " + mesa.Simposios.LineaTematica.Nombre;
            }
            else if (mesa.LineaTematicaId == 9)
            {
                ViewData["Tema"] = "Otro " + " - " + mesa.LineaTematicaOtro;
            }
            else if (mesa.LineaTematicaId != 9 && mesa.LineaTematicaId != null)
            {
                ViewData["Tema"] = mesa.LineaTematica.Nombre;
            }

            ViewData["MesaNombre"] = mesa.Nombre;
            ViewData["NombreCoordinador"] = mesa.CoordinadorNombre;
            ViewData["CorreoCoordinador"] = mesa.CoordinadorCorreo;
            ViewData["Folio"] = mesa.Regitros.RegistroFolio.Clave;

            return View(ponencias);
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
            var mesas = await _context.Mesas
                .Include(p => p.LineaTematica)
                .Include(p => p.Archivos)
                .Include(p => p.Regitros)
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
                    Mesa = p.Nombre,
                    Tema = p.LineaTematicaId != null ? p.LineaTematica.Nombre : "Mesa de simposio",
                    CoordinadorNombre = p.CoordinadorNombre,
                    CoordinadorCorreo = p.CoordinadorCorreo,
                    Archivo = p.Archivos != null && p.Archivos.Any()
                        ? p.Archivos.FirstOrDefault().Nombre
                        : "(Sin archivo)",
                    Ponencias = p.Ponencias.Select(p => new
                    {
                        Titulo = p.Titulo,
                        Expositor = p.Expositores.Nombre,
                        CorreoExpositor = p.Expositores.Correo,
                        InstitucionExpositor = p.Expositores.Institucion,
                        Genero = p.Expositores.Genero.Nombre,
                        Pais = p.Expositores.Paises.Nombre
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
                        Name = "Mesas"
                    };
                    sheets.Append(sheet);

                    //Encabezados
                    var headerRow = new Row();
                    headerRow.Append(
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("Fecha") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("Folio") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("NombreMesa") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("Tema") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("NombreCoordinador") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("CorreoCoordinador") },
                        new Cell { DataType = CellValues.String, CellValue = new CellValue("Archivo") }
                    );
                    for (int i = 1; i <= 4; i++)
                    {
                        headerRow.Append(
                            new Cell { DataType = CellValues.String, CellValue = new CellValue($"Ponencia {i} - Título") },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue($"Ponencia {i} - Expositor") },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue($"Ponencia {i} - CorreoExpositor") },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue($"Ponencia {i} - InstituciónAdscripción") },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue($"Ponencia {i} - Género") },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue($"Ponencia {i} - País") }
                        );
                    }
                    sheetData.AppendChild(headerRow);

                    //Datos
                    foreach (var m in mesas)
                    {
                        var row = new Row();
                        row.Append(
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(m.Fecha.ToString("dd-MM-yyyy")) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(m.Folio) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(m.Mesa) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(m.Tema) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(m.CoordinadorNombre) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(m.CoordinadorCorreo) },
                            new Cell { DataType = CellValues.String, CellValue = new CellValue(m.Archivo) }
                        );
                        for (int i = 0; i < 4; i++)
                        {
                            if (i < m.Ponencias.Count)
                            {
                                var p = m.Ponencias[i];
                                row.Append(
                                    new Cell { DataType = CellValues.String, CellValue = new CellValue(p.Titulo) },
                                    new Cell { DataType = CellValues.String, CellValue = new CellValue(p.Expositor) },
                                    new Cell { DataType = CellValues.String, CellValue = new CellValue(p.CorreoExpositor) },
                                    new Cell { DataType = CellValues.String, CellValue = new CellValue(p.InstitucionExpositor) },
                                    new Cell { DataType = CellValues.String, CellValue = new CellValue(p.Genero) },
                                    new Cell { DataType = CellValues.String, CellValue = new CellValue(p.Pais) }
                                );
                            }
                        }
                        sheetData.AppendChild(row);
                    }

                    workbookPart.Workbook.Save();
                }

                memoryStream.Position = 0;

                return File(
                    memoryStream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "ListadoMesas.xlsx"
                );
            }
        }

        public async Task<IActionResult> DescargarZip()
        {
            var mesas = await _context.Mesas
                .Include(m => m.Archivos)
                .Include(p => p.Regitros)
                .ThenInclude(p => p.RegistroFolio)
                .ToListAsync();

            var archivos = mesas
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
                            string nombreArchivo = archivo.Mesa.Regitros.RegistroFolio.Clave + "-" + archivo.Nombre;

                            var zipEntry = zip.CreateEntry(nombreArchivo, CompressionLevel.Fastest);

                            using (var entryStream = zipEntry.Open())
                            {
                                entryStream.Write(archivo.Contenido, 0, archivo.Contenido.Length);
                            }
                        }
                    }
                }

                memoryStream.Position = 0;
                return File(memoryStream.ToArray(), "application/zip", "Archivos-Mesas.zip");
            }
        }
    }
}

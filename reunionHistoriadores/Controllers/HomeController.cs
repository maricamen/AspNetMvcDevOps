using reunionhistoriadores2025.Data;
using reunionhistoriadores2025.Localization;
using reunionhistoriadores2025.Models;
using reunionhistoriadores2025.Services.Email;
using reunionhistoriadores2025.Services.Options;
using reunionhistoriadores2025.ViewModels;
using reunionhistoriadores2025.ViewModels.Home;
using reunionhistoriadores2025.ViewModels.Shared;
using reunionhistoriadores2025.Services.ErrorLog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using NuGet.Protocol.Core.Types;
using System.Collections.Generic;
using System.Diagnostics;
using static reunionhistoriadores2025.ViewModels.Home.PonenciaViewModel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace reunionhistoriadores2025.Controllers
{
    public class HomeController : Controller
    {
        private readonly IErrorLog _errorlog;
        private readonly RegistroContext _context;
        private readonly GlobalOptions _opciones;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IEmail _email;
        private readonly bool _registroEnabled;
        private readonly string _correoEvento;
        private readonly string _urlPortal;

        public HomeController(IErrorLog errorlog, RegistroContext context, IOptionsSnapshot<GlobalOptions> opciones, IStringLocalizer<HomeController> localizer, IStringLocalizer<SharedResource> sharedLocalizer, IEmail email)
        {
            _errorlog = errorlog;
            _context = context;
            _opciones = opciones.Value;
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
            _email = email;

            // Revisa la vigencia del registro
            _registroEnabled = DateTime.Compare(Convert.ToDateTime(_opciones.FechaCierre), DateTime.Now) >= 0;
            _correoEvento = _opciones.CorreoEvento;
            _urlPortal = _opciones.UrlPortal;

        }

        public async Task<IActionResult> IndexAsync()
        {
            if (_registroEnabled)
                return RedirectToAction("Inicio");

            ViewData["RegistroEnabled"] = _registroEnabled;
            ViewData["CorreoEvento"] = _correoEvento;
            ViewData["UrlPortal"] = _urlPortal;

            return View(await _context.RegistroTipos.Where(t => t.FechaCierre > DateTime.Now).AsNoTracking().ToListAsync());
        }

        public ActionResult Inicio()
        {
            ViewData["RegistroEnabled"] = _registroEnabled;
            ViewData["CorreoEvento"] = _correoEvento;
            ViewData["UrlPortal"] = _urlPortal;

            return View();
        }

        public async Task<IActionResult> Ponencia()
        {
            if (!_registroEnabled)
                return RedirectToAction("Index");

            int id = 1;
            if (!ExisteTipoRegistroVigente(id))
                return NotFound();

            ViewData["RegistroEnabled"] = _registroEnabled;
            ViewData["CorreoEvento"] = _correoEvento;
            ViewData["UrlPortal"] = _urlPortal;
            ViewData["TipoRegistroUnico"] = TipoRegistroUnico();
            ViewData["Temas"] = new SelectList(await _context.LineasTematicas.Select(r => new { r.Id, r.Nombre }).ToListAsync(), "Id", "Nombre");
            ViewData["Generos"] = new SelectList(await _context.Generos.Select(r => new { r.Id, r.Nombre }).ToListAsync(), "Id", "Nombre");
            ViewData["Paises"] = new SelectList(await _context.Paises.OrderBy(p => p.Orden).Select(r => new { r.Id, r.Nombre }).ToListAsync(), "Id", "Nombre");

            var RegistroTipo = await _context.RegistroTipos.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

            var model = new PonenciaViewModel
            {
                RegistroTipo = RegistroTipo
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ponencia(int id, PonenciaViewModel model)
        {
            if (!ExisteTipoRegistroVigente(id))
                return NotFound();

            var RegistroTipo = await _context.RegistroTipos.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

            try
            {
                if (ModelState.IsValid)
                {
                    var registro = new Registro
                    {
                        RegistroFolioId = await _context.RegistroFolios.Where(r => r.Id > 0 && r.Usada == false).Select(r => r.Id).FirstOrDefaultAsync(),
                        RegistroStatusId = (int)RegistroStatusId.RECIBIDO,
                        RegistroTipoId = RegistroTipo.Id,
                        IpAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
                        Fecha = DateTime.Now,
                        Correo = model.Correo,
                        ExposicionTipo = 1, //1-Tipo Ponencia 
                    };
                    _context.Registros.Add(registro);
                    _context.SaveChanges();

                    var expositor = new Expositor
                    {
                        Nombre = model.Nombre,
                        GradoAcademico = model.GradoAcademico,
                        GeneroId = model.GeneroId,
                        PaisId = model.PaisId,
                        //Autodescripcion = model.Autodescripcion,
                        Institucion = model.InstituciónAdscripción,
                        Correo = model.Correo,
                    };
                    _context.Expositores.Add(expositor);
                    _context.SaveChanges();

                    var itemToCreate = new Ponencia
                    {
                        Titulo = model.Titulo,
                        LineaTematicaOtro = model.LineaTematicaOtro,
                        LineaTematicaId = model.LineaTematicaId,
                        ExpositorId = expositor.Id,
                        RegistroId = registro.Id,
                        Visible = true,
                        Archivos = new List<Archivo>()
                    };

                    // Archivo
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.Resumen.CopyToAsync(memoryStream);

                        var file = new Archivo
                        {
                            Contenido = memoryStream.ToArray(),
                            ArchivoTipoId = TipoIdentificacion.Id,
                            Mime = model.Resumen.ContentType,
                            Nombre = model.Resumen.FileName,
                            Fecha = DateTime.Now,
                            Size = model.Resumen.Length
                        };
                        itemToCreate.Archivos.Add(file);
                    }

                    // Agrega el nuevo registro
                    _context.Add(itemToCreate);
                    // Marca la referencia como usada
                    _context.RegistroFolios.FirstOrDefault(f => f.Id == registro.RegistroFolioId).Usada = true;
                    // Bitacora
                    _context.Bitacora.Add(new Bitacora() { Message = $"Envío de registro con Folio: {registro.RegistroFolioId} y Correo: {model.Correo}", TimeStamp = DateTime.Now, UserName = model.Correo });
                    // Guarda los cambios
                    await _context.SaveChangesAsync();
                    // Envía correo
                    //await _email.EnviaCorreoAsync($"[{ViewData["SiteShortTitle"]}] {_localizer["Registro"]} {ViewData["SiteTitle"]}", model.Correo, null, null, CuerpoCorreo(registro.Id));

                    return RedirectToAction(nameof(Success));
                }
            }
            catch (DbUpdateException ex)
            {
                await _errorlog.ErrorLogAsync(ex.Message);
                ModelState.AddModelError("", _localizer["No ha sido posible enviar el registro. Inténtelo nuevamente."]);
            }

            // Se inicia el modelo en caso de error
            ViewData["RegistroEnabled"] = _registroEnabled;
            ViewData["CorreoEvento"] = _correoEvento;
            ViewData["UrlPortal"] = _urlPortal;
            ViewData["TipoRegistroUnico"] = TipoRegistroUnico();
            ViewData["Temas"] = new SelectList(await _context.LineasTematicas.Select(r => new { r.Id, r.Nombre }).ToListAsync(), "Id", "Nombre");
            ViewData["Generos"] = new SelectList(await _context.Generos.Select(r => new { r.Id, r.Nombre }).ToListAsync(), "Id", "Nombre");
            ViewData["Paises"] = new SelectList(await _context.Paises.OrderBy(p => p.Orden).Select(r => new { r.Id, r.Nombre }).ToListAsync(), "Id", "Nombre");

            model.RegistroTipo = await _context.RegistroTipos.Where(t => t.Id == id).AsNoTracking().FirstOrDefaultAsync();

            return View(model);
        }

        public IActionResult Success()
        {
            ViewData["RegistroEnabled"] = _registroEnabled;
            ViewData["CorreoEvento"] = _correoEvento;
            ViewData["UrlPortal"] = _urlPortal;

            return View();
        }

        public string CuerpoCorreo(int Id)
        {
            var registro = _context.Registros
                .Include(r => r.RegistroFolio)
                .Include(r => r.RegistroTipo)
                .AsNoTracking().FirstOrDefault(r => r.Id == Id);

            string Mensaje = "<p>Se ha registrado correctamente su propuesta para la XVII Reunión Internacional de Historiadores de México: “Las crisis en la historia de México”, que se celebrará del 24 al 27 de septiembre de 2026 en la Universidad Veracruzana, Veracruz, México.</p>";
            
            if (registro != null)
            {
                Mensaje += $"Correo registrado: {registro.Correo}<br />";
                Mensaje += $"Folio: {registro.RegistroFolio.Clave}<br />";
            }

            Mensaje += "<p>No responda a este correo, ya que fue generado de manera automática.</p>";
            Mensaje += "<p>Cualquier duda o comentario será atendido exclusivamente a través del correo comiteIIHS@uv.mx</p>";

            Mensaje += "<p>Atentamente,</p>";
            Mensaje += "<p>Comité Organizador</p>";
            Mensaje += "<p>XVII Reunión Internacional de Historiadores de México</p>";
            Mensaje += "<p>https://www.uv.mx/xvii-reunion-historiadores/</p>";
            Mensaje += "<br /><br /><br /><div>-----------------------------------------------------------------</div>";

            Mensaje += "<p>Your proposal has been successfully registered for the XVII International Meeting of Historians of Mexico: “Crises in the History of Mexico,” to be held from September 24 to 27, 2026, at the Universidad Veracruzana, Veracruz, Mexico.</p>";

            if (registro != null)
            {
                Mensaje += $"Registered email: {registro.Correo}<br />";
                Mensaje += $"Folio: {registro.RegistroFolio.Clave}<br />";
            }

            Mensaje += "<p>Please do not reply to this email, as it was automatically generated.</p>";
            Mensaje += "<p>Any questions or comments will be handled exclusively through the following email address: comiteIIHS@uv.mx</p>";

            Mensaje += "<p>Organizing Committee</p>";
            Mensaje += "<p>XVII International Meeting of Historians of Mexico</p>";
            Mensaje += "<p>https://www.uv.mx/xvii-reunion-historiadores/</p>";
            Mensaje += "<br /><br /><br /><div>-----------------------------------------------------------------</div>";


            return Mensaje;
        }

        public bool ExisteTipoRegistroVigente(int id)
        {
            if (_context.RegistroTipos.Any(a => a.Id == id && a.FechaCierre > DateTime.Now))
                return true;
            return false;
        }

        public bool TipoRegistroUnico()
        {
            if (_context.RegistroTipos.AsNoTracking().Count() == 1)
                return true;
            return false;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
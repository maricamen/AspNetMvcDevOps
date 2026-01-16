using reunionhistoriadores2025.AuthorizationPolicies;
using reunionhistoriadores2025.Data;
using reunionhistoriadores2025.Localization;
using reunionhistoriadores2025.Models;
using reunionhistoriadores2025.Services.ErrorLog;
using reunionhistoriadores2025.Services.Options;
using reunionhistoriadores2025.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace reunionhistoriadores2025.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = nameof(AdminRolePolicies.AdministradorGeneralPolicy))]
    public class ConfiguracionController : Controller
    {
        private readonly IErrorLog _errorlog;
        private readonly RegistroContext _context;
        private readonly GlobalOptions _opciones;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public ConfiguracionController(IErrorLog errorlog, RegistroContext context, IOptionsSnapshot<GlobalOptions> opciones, IStringLocalizer<HomeController> localizer, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _errorlog = errorlog;
            _context = context;
            _opciones = opciones.Value;
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = from r in _context.Opciones
                        select r;
            return View(await model.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> EditarAsync(int id)
        {
            var model = await _context.Opciones.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            ViewData["UsaSelect"] = false;
            if (model.Nombre.Contains("Habilita"))
            {
                ViewData["UsaSelect"] = true;
                var boleanoSelect = new List<ControlBoleanoViewModel>()
                {
                    new ControlBoleanoViewModel { Id = "true", Texto = "Sí" },
                    new ControlBoleanoViewModel { Id = "false", Texto = "No" }
                };
                ViewData[model.Nombre] = new SelectList(boleanoSelect, "Id", "Texto", model.Valor);
            }

            return View(model);
        }

        [HttpPost, ActionName("Editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarPostAsync(int id)
        {
            var opcionToUpdate = await _context.Opciones.FirstOrDefaultAsync(r => r.Id == id);

            if (opcionToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Models.Opciones>(opcionToUpdate, "", o => o.Valor))
            {
                try
                {
                    // Bitacora
                    _context.Bitacora.Add(new Bitacora() { Message = $"Edita configuración con Nombre: {opcionToUpdate.Nombre} y Valor: {opcionToUpdate.Valor}.", TimeStamp = DateTime.Now, UserName = User.Identity.Name });
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    await _errorlog.ErrorLogAsync(ex.Message);
                    ModelState.AddModelError("", _localizer["No ha sido posible editar la opción. Inténtelo nuevamente."]);
                }
            }

            // Se inicia el modelo en caso de error
            var model = await _context.Opciones.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            ViewData["UsaSelect"] = false;
            if (model.Nombre.Contains("Habilita"))
            {
                ViewData["UsaSelect"] = true;
                var boleanoSelect = new List<ControlBoleanoViewModel>()
                {
                    new ControlBoleanoViewModel { Id = "true", Texto = "Sí" },
                    new ControlBoleanoViewModel { Id = "false", Texto = "No" }
                };
                ViewData[model.Nombre] = new SelectList(boleanoSelect, "Id", "Texto", model.Valor);
            }

            return View(model);
        }
    }
}

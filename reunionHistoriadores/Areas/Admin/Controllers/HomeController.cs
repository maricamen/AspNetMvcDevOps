using reunionhistoriadores2025.Areas.Admin.ViewModels.Home;
using reunionhistoriadores2025.AuthorizationPolicies;
using reunionhistoriadores2025.Data;
using reunionhistoriadores2025.Helpers;
using reunionhistoriadores2025.Localization;
using reunionhistoriadores2025.Models;
using reunionhistoriadores2025.Services.ErrorLog;
using reunionhistoriadores2025.Services.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace reunionhistoriadores2025.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = nameof(AdminRolePolicies.JuradosPolicy))]
    public class HomeController : Controller
    {
        private readonly IErrorLog _errorlog;
        private readonly RegistroContext _context;
        private readonly GlobalOptions _opciones;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public HomeController(IErrorLog errorlog, RegistroContext context, IOptionsSnapshot<GlobalOptions> opciones, IStringLocalizer<HomeController> localizer, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _errorlog = errorlog;
            _context = context;
            _opciones = opciones.Value;
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<IActionResult> IndexAsync()
        {

            var RegistroStatus = await _context.RegistroStatus.AsNoTracking().ToListAsync();
            List<IndexViewModel> model = new();
            foreach (var item in await _context.RegistroTipos.AsNoTracking().ToListAsync())
            {
                var listado = _context.Ponencias.Where(c => c.Regitros.RegistroTipoId == item.Id).AsNoTracking();

                IndexViewModel itemToAdd = new()
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    Descripcion = item.Descripcion,
                    ClaseCss = item.ClaseCss,
                    FechaCierre = item.FechaCierre,
                    IconoCss = item.IconoCss,
                    Total = listado.Count(),
                };
                itemToAdd.SinAtenderPorcentaje = itemToAdd.Total == 0 ? "0" : (itemToAdd.SinAtender * 100.0 / itemToAdd.Total).ToString("0");
                itemToAdd.AceptadosPorcentaje = itemToAdd.Total == 0 ? "0" : (itemToAdd.Aceptados * 100.0 / itemToAdd.Total).ToString("0");
                itemToAdd.RechazadosPorcentaje = itemToAdd.Total == 0 ? "0" : (itemToAdd.Rechazados * 100.0 / itemToAdd.Total).ToString("0");
                itemToAdd.EliminadosPorcentaje = itemToAdd.Total == 0 ? "0" : (itemToAdd.Eliminados * 100.0 / itemToAdd.Total).ToString("0");
                model.Add(itemToAdd);
            }
            return View(model);
        }

        public ActionResult Inicio()
        {
            return View();
        }


        [Authorize(Policy = nameof(AdminRolePolicies.AdministradorGeneralPolicy))]
        public IActionResult Crear(int id)
        {
            ViewData["ControlBoleano"] = PopulateItems.GetBoleanoSelect();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = nameof(AdminRolePolicies.AdministradorGeneralPolicy))]
        public async Task<IActionResult> CrearAsync(int id, RegistroTipo itemToCreate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(itemToCreate);

                    // Bitacora
                    _context.Bitacora.Add(new Bitacora() { Message = $"Crear tipo de registro con Nombre: {itemToCreate.Nombre} y Descripción: {itemToCreate.Descripcion}", TimeStamp = DateTime.Now, UserName = User.Identity.Name });

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    await _errorlog.ErrorLogAsync(ex.Message);
                    ModelState.AddModelError("", _localizer["No ha sido posible crear el tipo de registro. Inténtelo nuevamente."]);
                }
            }

            // Se inicia el modelo en caso de error
            ViewData["ControlBoleano"] = PopulateItems.GetBoleanoSelect();
            return View(itemToCreate);
        }

        [Authorize(Policy = nameof(AdminRolePolicies.AdministradorGeneralPolicy))]
        public async Task<IActionResult> EditarAsync(int id)
        {
            var itemToEdit = await _context.RegistroTipos.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            if (itemToEdit == null)
            {
                return NotFound();
            }

            ViewData["ControlBoleano"] = PopulateItems.GetBoleanoSelect();
            return View(itemToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = nameof(AdminRolePolicies.AdministradorGeneralPolicy))]
        public async Task<IActionResult> EditarAsync(int id, RegistroTipo itemToEdit)
        {
            if (id != itemToEdit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemToEdit);

                    // Bitacora
                    _context.Bitacora.Add(new Bitacora() { Message = $"Edita tipo de registro con Id: {itemToEdit.Id} y Nombre: {itemToEdit.Nombre}", TimeStamp = DateTime.Now, UserName = User.Identity.Name });

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    await _errorlog.ErrorLogAsync(ex.Message);
                    ModelState.AddModelError("", _localizer["No ha sido posible editar el tipo de registro. Inténtelo nuevamente."]);
                }
            }

            // Se inicia el modelo en caso de error
            ViewData["ControlBoleano"] = PopulateItems.GetBoleanoSelect();
            return View(itemToEdit);
        }
    }
}

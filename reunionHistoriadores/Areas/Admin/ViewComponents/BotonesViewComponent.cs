using reunionhistoriadores2025.Areas.Admin.Controllers;
using reunionhistoriadores2025.Areas.Admin.ViewModels.Registros;
using reunionhistoriadores2025.AuthorizationPolicies;
using reunionhistoriadores2025.Data;
using reunionhistoriadores2025.Localization;
using reunionhistoriadores2025.Models;
using reunionhistoriadores2025.Services.Options;
using reunionhistoriadores2025.Services.ErrorLog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace reunionhistoriadores2025.Areas.Admin.ViewComponents
{
    public class BotonesViewComponent : ViewComponent
    {
        private readonly IErrorLog _errorlog;
        private readonly RegistroContext _context;
        private readonly GlobalOptions _opciones;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public BotonesViewComponent(IErrorLog errorlog, RegistroContext context, IOptionsSnapshot<GlobalOptions> opciones, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _errorlog = errorlog;
            _context = context;
            _opciones = opciones.Value;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var registro = await _context.Ponencias.AsNoTracking().FirstOrDefaultAsync(registro => registro.Id == id);
            List<BotonViewModel> model = new();

            if (User.IsInRole(AdminRolePolicies.AdministradorRole) || User.IsInRole(AdminRolePolicies.AdministradorGeneralRole))
            {
                model.Add(new BotonViewModel() { Id = id, Nombre = _sharedLocalizer["DETALLES"], Controlador = "Registros", Action = "Detalles" });

                if (User.IsInRole(AdminRolePolicies.AdministradorGeneralRole))
                    model.Add(new BotonViewModel() { Id = id, Nombre = _sharedLocalizer["ARCHIVOS"], Controlador = "Registros", Action = "Archivos" });
            }

            return View(model);
        }
    }
}

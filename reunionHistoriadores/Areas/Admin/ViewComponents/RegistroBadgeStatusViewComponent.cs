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
using System.Security.Claims;

namespace reunionhistoriadores2025.Areas.Admin.ViewComponents
{
    public class RegistroBadgeStatusViewComponent : ViewComponent
    {
        private readonly IErrorLog _errorlog;
        private readonly RegistroContext _context;
        private readonly GlobalOptions _opciones;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public RegistroBadgeStatusViewComponent(IErrorLog errorlog, RegistroContext context, IOptionsSnapshot<GlobalOptions> opciones, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _errorlog = errorlog;
            _context = context;
            _opciones = opciones.Value;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var itemToDisplay = await _context.Ponencias.Include(r => r.Regitros.RegistroStatus).AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            return View(new StatusViewModel() { Id = id, Nombre = itemToDisplay.Regitros.RegistroStatus.Nombre, NombreLargo = itemToDisplay.Regitros.RegistroStatus.NombreLargo, ClaseCss = itemToDisplay.Regitros.RegistroStatus.ClaseCss, Visible = itemToDisplay.Regitros.RegistroStatus.Visible });
        }
    }
}

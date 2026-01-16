using reunionhistoriadores2025.Areas.Admin.Controllers;
using reunionhistoriadores2025.Areas.Admin.ViewModels.Registros;
using reunionhistoriadores2025.AuthorizationPolicies;
using reunionhistoriadores2025.Data;
using reunionhistoriadores2025.Localization;
using reunionhistoriadores2025.Models;
using reunionhistoriadores2025.Services.Options;
using reunionhistoriadores2025.Services.ErrorLog;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace reunionhistoriadores2025.Areas.Admin.ViewComponents
{
    public class ArchivosViewComponent : ViewComponent
    {
        private readonly IErrorLog _errorlog;
        private readonly RegistroContext _context;
        private readonly GlobalOptions _opciones;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public ArchivosViewComponent(IErrorLog errorlog, RegistroContext context, IOptionsSnapshot<GlobalOptions> opciones, IStringLocalizer<SharedResource> sharedLocalizer, UserManager<CustomIdentityUser> userManager)
        {
            _errorlog = errorlog;
            _context = context;
            _opciones = opciones.Value;
            _sharedLocalizer = sharedLocalizer;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id, ArchivosListadoVista vista = ArchivosListadoVista.NORMAL)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            ViewData["CardsViewEnabled"] = user.CardsViewEnabled;

            //var model = await _context.Archivos.Include(a => a.ArchivoTipo).Where(a => a.RegistroId == id).AsNoTracking().ToListAsync();
            List<ArchivoViewModel> model = await _context.Archivos.Include(a => a.ArchivoTipo).Where(a => a.PonenciaId == id).AsNoTracking()
                .Select(a => new ArchivoViewModel { Id = a.Id, Nombre = a.Nombre, ArchivoTipoId = a.ArchivoTipoId, Fecha = a.Fecha, Size = a.Size, Mime = a.Mime, PonenciaId = a.PonenciaId, ArchivoTipo = a.ArchivoTipo }).ToListAsync();

            if ( vista == ArchivosListadoVista.TABLA && User.IsInRole(AdminRolePolicies.AdministradorGeneralRole))
            {
                return View("Tabla", model);
            }

            return View(model);
        }        
    }

    public enum ArchivosListadoVista
    {
        NORMAL = 1, TABLA
    }
}

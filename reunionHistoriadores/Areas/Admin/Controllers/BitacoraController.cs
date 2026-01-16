using reunionhistoriadores2025.AuthorizationPolicies;
using reunionhistoriadores2025.Data;
using reunionhistoriadores2025.Localization;
using reunionhistoriadores2025.Services.ErrorLog;
using reunionhistoriadores2025.Services.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using X.PagedList;

namespace reunionhistoriadores2025.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = nameof(AdminRolePolicies.AdministradoresPolicy))]
    public class BitacoraController : Controller
    {
        private readonly IErrorLog _errorlog;
        private readonly RegistroContext _context;
        private readonly GlobalOptions _opciones;
        private readonly IStringLocalizer<PonenciasController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IConfiguration _configuration;

        public BitacoraController(IErrorLog errorlog, RegistroContext context, IOptionsSnapshot<GlobalOptions> opciones, IStringLocalizer<PonenciasController> localizer, IStringLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration)
        {
            _errorlog = errorlog;
            _context = context;
            _opciones = opciones.Value;
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
            _configuration = configuration;
        }

        public IActionResult Index(int? pageNumber)
        {
            // Paginación
            ViewData["PageNumber"] = pageNumber;
            int pageSize = Convert.ToInt32(_opciones.PaginacionSize);
            var model = from r in _context.Bitacora
                        orderby r.Id descending
                        select r;

            return View(model.AsNoTracking().ToPagedList(pageNumber ?? 1, pageSize));
        }
    }
}

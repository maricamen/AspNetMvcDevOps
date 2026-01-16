using reunionhistoriadores2025.Areas.Admin.Controllers;
using reunionhistoriadores2025.Data;
using reunionhistoriadores2025.Localization;
using reunionhistoriadores2025.Services.Options;
using reunionhistoriadores2025.Services.ErrorLog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace reunionhistoriadores2025.Areas.Admin.ViewComponents
{
    public class RegistroDetallesViewComponent : ViewComponent
    {
        private readonly IErrorLog _errorlog;
        private readonly RegistroContext _context;
        private readonly GlobalOptions _opciones;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public RegistroDetallesViewComponent(IErrorLog errorlog, RegistroContext context, IOptionsSnapshot<GlobalOptions> opciones, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _errorlog = errorlog;
            _context = context;
            _opciones = opciones.Value;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var itemToDisplay = await _context.Ponencias.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);

            var model = await _context.Ponencias.Include(r => r.Regitros.RegistroFolio).Include(r => r.Regitros.RegistroStatus).Include(r => r.Regitros.RegistroTipo)
            //.Include(r => r.LineaTematica).ThenInclude(r => r.LineaTematicaGrupo)
            .AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            return View(model);

        }
    }
}

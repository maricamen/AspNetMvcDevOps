using reunionhistoriadores2025.Localization;
using reunionhistoriadores2025.Services.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace reunionhistoriadores2025.Filters
{
    public class ViewDataActionFilter : ActionFilterAttribute
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly GlobalOptions _opciones;

        public ViewDataActionFilter(IStringLocalizer<SharedResource> sharedLocalizer, IOptionsSnapshot<GlobalOptions> opciones)
        {
            _sharedLocalizer = sharedLocalizer;
            _opciones = opciones.Value;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Do something before the action executes.

            Controller controller = (Controller)context.Controller;
            controller.ViewData["SiteTitle"] = _sharedLocalizer[_opciones.SiteTitle];
            controller.ViewData["SiteSubTitle"] = _sharedLocalizer[_opciones.SiteSubTitle];
            controller.ViewData["SiteShortTitle"] = _sharedLocalizer[_opciones.SiteShortTitle];

            // next() calls the action method.
            _ = await next();
            // resultContext.Result is set.
            // Do something after the action executes.
        }
    }
}

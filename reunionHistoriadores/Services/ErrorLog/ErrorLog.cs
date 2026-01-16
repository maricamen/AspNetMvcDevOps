using reunionhistoriadores2025.Data;
using reunionhistoriadores2025.Models;
using reunionhistoriadores2025.Services.Options;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.DirectoryServices;
using System.Security.Claims;

namespace reunionhistoriadores2025.Services.ErrorLog
{
    public class ErrorLog : IErrorLog
    {
        private readonly GlobalOptions _opciones;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ErrorLog(IOptionsSnapshot<GlobalOptions> opciones, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _opciones = opciones.Value;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task ErrorLogAsync(string Mensaje)
        {

            try
            {
                string webRootPath = _webHostEnvironment.WebRootPath;

                string path = "";
                path = Path.Combine(webRootPath, "log");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Retreive server/local IP address
                var feature = _httpContextAccessor.HttpContext.Features.Get<IHttpConnectionFeature>();
                string LocalIPAddr = feature?.LocalIpAddress?.ToString();

                // Write the specified text asynchronously to a new file named "WriteTextAsync.txt".
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, "log.txt"), true))
                {
                    await outputFile.WriteLineAsync(Mensaje + " - " + _httpContextAccessor.HttpContext.User.Identity.Name + " - " + LocalIPAddr + " - " + DateTime.Now.ToString());
                }
            }
            catch
            {
                //No hace nada
            }
        }
    }
}

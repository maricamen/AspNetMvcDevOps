using reunionhistoriadores2025.Data;
using reunionhistoriadores2025.Models;
using Microsoft.Extensions.Options;

namespace reunionhistoriadores2025.Services.Options
{
    public class GlobalConfigureOptions : IConfigureOptions<GlobalOptions>
    {
        private readonly RegistroContext _context;
        private List<Opciones> _opciones;

        public GlobalConfigureOptions(RegistroContext context)
        {
            _context = context;
        }

        public void Configure(GlobalOptions options)
        {
            // Generales
            options.SiteTitle = ObtenOpcion(nameof(options.SiteTitle));
            options.SiteSubTitle = ObtenOpcion(nameof(options.SiteSubTitle));
            options.SiteShortTitle = ObtenOpcion(nameof(options.SiteShortTitle));
            options.CorreoEvento = ObtenOpcion(nameof(options.CorreoEvento));
            options.HabilitaProduccion = ObtenOpcion(nameof(options.HabilitaProduccion));
            options.HabilitaJurado = ObtenOpcion(nameof(options.HabilitaJurado));
            options.HabilitaJuradoLineaTematica = ObtenOpcion(nameof(options.HabilitaJuradoLineaTematica));
            options.HabilitaIdioma = ObtenOpcion(nameof(options.HabilitaIdioma));
            options.HabilitaArchivoAdicional = ObtenOpcion(nameof(options.HabilitaArchivoAdicional));
            options.HabilitaPago = ObtenOpcion(nameof(options.HabilitaPago));
            options.UrlPortal = ObtenOpcion(nameof(options.UrlPortal));
            options.FechaCierre = ObtenOpcion(nameof(options.FechaCierre));

            // AD y Correo
            options.LdapUrlString = ObtenOpcion(nameof(options.LdapUrlString));
            options.LdapUser = ObtenOpcion(nameof(options.LdapUser));
            options.LdapPwd = ObtenOpcion(nameof(options.LdapPwd));
            options.SmtpServer = ObtenOpcion(nameof(options.SmtpServer));
            options.SmtpUser = ObtenOpcion(nameof(options.SmtpUser));
            options.SmtpPwd = ObtenOpcion(nameof(options.SmtpPwd));

            // Dropzone
            options.PaginacionSize = ObtenOpcion(nameof(options.PaginacionSize));
        }

        public string ObtenOpcion(string nombre)
        {
            return OpcionesList.Find(f => f.Nombre == nombre).Valor;
        }

        public List<Opciones> OpcionesList
        {
            get
            {
                if (_opciones == null)
                {
                    _opciones = _context.Opciones.ToList();
                }
                return _opciones;
            }
        }
    }
}

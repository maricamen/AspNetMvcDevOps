using reunionhistoriadores2025.Data;
using reunionhistoriadores2025.Models;

namespace reunionhistoriadores2025.Services.Options
{
    public class GlobalOptions
    {
        // Generales
        public string SiteTitle { get; set; } = string.Empty;
        public string SiteSubTitle { get; set; } = string.Empty;
        public string SiteShortTitle { get; set; } = string.Empty;
        public string CorreoEvento { get; set; } = string.Empty;
        public string HabilitaProduccion { get; set; } = string.Empty;
        public string HabilitaJurado { get; set; } = string.Empty;
        public string HabilitaJuradoLineaTematica { get; set; } = string.Empty;
        public string HabilitaIdioma { get; set; } = string.Empty;
        public string HabilitaArchivoAdicional { get; set; } = string.Empty;
        public string HabilitaPago { get; set; } = string.Empty;
        public string UrlPortal { get; set; } = string.Empty;
        public string FechaCierre { get; set; } = string.Empty;

        // Correo
        public string LdapUrlString { get; set; } = string.Empty;
        public string LdapUser { get; set; } = string.Empty;
        public string LdapPwd { get; set; } = string.Empty;
        public string SmtpServer { get; set; } = string.Empty;
        public string SmtpUser { get; set; } = string.Empty;
        public string SmtpPwd { get; set; } = string.Empty;

        // Despliegue
        public string PaginacionSize { get; set; } = string.Empty;
    }
}

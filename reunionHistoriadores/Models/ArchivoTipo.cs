using System.ComponentModel.DataAnnotations;

namespace reunionhistoriadores2025.Models
{
    public class ArchivoTipo
    {
        public int Id { get; set; }

        [Display(Name = "Tipo")]
        public string Nombre { get; set; }
        public string IconoCss { get; set; }
    }

    public static class TipoDocumento
    {
        public const int Id = 1;
        public const string Nombre = "Participación";
        public const string ExtensionesValidas = ".pdf,.doc,.docx";
        public const int MaxFileSizeInMB = 2;
        public const string IconoCss = "bi bi-file-earmark-text";
    }

    public static class TipoImagen
    {
        public const int Id = 2;
        public const string Nombre = "Fotografía";
        public const string ExtensionesValidas = ".jpg,.png";
        public const int MaxFileSizeInMB = 2;
        public const string IconoCss = "bi bi-file-earmark-text";
    }

    public static class TipoPresentacion
    {
        public const int Id = 3;
        public const string Nombre = "Presentación";
        public const string ExtensionesValidas = ".ppt,.pptx";
        public const int MaxFileSizeInMB = 2;
        public const string IconoCss = "bi bi-file-earmark-text";
    }    

    public static class TipoPago
    {
        public const int Id = 4;
        public const string Nombre = "Pago";
        public const string ExtensionesValidas = ".pdf,.doc,.docx,.jpg,.png";
        public const int MaxFileSizeInMB = 2;
        public const string IconoCss = "bi bi-cash-coin";
    }

    public static class TipoCartaAceptacion
    {
        public const int Id = 5;
        public const string Nombre = "Aceptación";
        public const string ExtensionesValidas = ".pdf,.doc,.docx";
        public const int MaxFileSizeInMB = 2;
        public const string IconoCss = "bi bi-file-earmark-medical";
    }

    public static class TipoIdentificacion
    {
        public const int Id = 6;
        public const string Nombre = "Identificación";
        public const string ExtensionesValidas = ".pdf,.doc,.docx";
        public const int MaxFileSizeInMB = 2;
        public const string IconoCss = "bi bi-postcard";
    }
}

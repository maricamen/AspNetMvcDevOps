using System.ComponentModel.DataAnnotations;

namespace reunionhistoriadores2025.Models
{
    public class RegistroStatus
    {
        public int Id { get; set; }

        [Display(Name = "Estado")]
        public string Nombre { get; set; }
        public string NombreLargo { get; set; }
        public string ClaseCss { get; set; }
        public bool Visible { get; set; }
    }

    public enum RegistroStatusId
    {
        RECIBIDO = 1, ACEPTADO, RECHAZADO, ELIMINADO, ELIMINADODEFINITIVAMENTE
    }
}

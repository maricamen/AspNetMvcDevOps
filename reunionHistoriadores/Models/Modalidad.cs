using System.ComponentModel.DataAnnotations;

namespace reunionhistoriadores2025.Models
{
    public class Modalidad
    {
        public int Id { get; set; }

        [Display(Name = "Modalidad de Participación")]
        public string Nombre { get; set; }        
    }
}

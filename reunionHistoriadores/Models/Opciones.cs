using reunionhistoriadores2025.DataAnnotationsValidators;

namespace reunionhistoriadores2025.Models
{
    public class Opciones
    {
        public int Id { get; set; }
        
        public string Nombre { get; set; }

        [CustomRequired]
        public string Valor { get; set; }
    }
}

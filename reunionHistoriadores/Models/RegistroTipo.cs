using System.ComponentModel.DataAnnotations;

namespace reunionhistoriadores2025.Models
{
    public class RegistroTipo
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ClaseCss { get; set; }
        public string IconoCss { get; set; }

        [Range(0, 5)]
        public int MaxMesas { get; set; } = 0;

        [DataType(DataType.DateTime)]
        public DateTime FechaCierre { get; set; }
    }
}

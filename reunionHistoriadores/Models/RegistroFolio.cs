using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reunionhistoriadores2025.Models
{
    public class RegistroFolio
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Display(Name = "Folio")]
        public string Clave { get; set; }
        public bool Usada { get; set; }
    }
}

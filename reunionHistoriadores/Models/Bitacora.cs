using System.ComponentModel.DataAnnotations;

namespace reunionhistoriadores2025.Models
{
    public class Bitacora
    {
        public int Id { get; set; }

        [Display(Name = "Mensaje")]
        public string Message { get; set; }

        [Display(Name = "Fecha")]
        public DateTime TimeStamp { get; set; }
        public string UserName { get; set; }
    }
}

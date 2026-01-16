using reunionhistoriadores2025.Models;
using System.ComponentModel.DataAnnotations;

namespace reunionhistoriadores2025.Areas.Admin.ViewModels.Registros
{
    public class StatusViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreLargo { get; set; }
        public bool Visible { get; set; }
        public string ClaseCss { get; set; }
    }
}

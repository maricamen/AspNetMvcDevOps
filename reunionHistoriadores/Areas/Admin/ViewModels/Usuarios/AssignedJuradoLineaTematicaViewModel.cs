using System.ComponentModel.DataAnnotations;
using reunionhistoriadores2025.DataAnnotationsValidators;
using Microsoft.AspNetCore.Mvc;

namespace reunionhistoriadores2025.Areas.Admin.ViewModels.Usuarios
{
    public class AssignedJuradoLineaTematicaViewModel
    {
        public int LineaTematicaId { get; set; }

        [Display(Name = "Eje temático")]
        public string Nombre { get; set; }

        public bool Assigned { get; set; }
    }
}

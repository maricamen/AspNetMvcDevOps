using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace reunionhistoriadores2025.Models
{
    public class CustomIdentityUser : IdentityUser
    {
        //public int JuradoCalificacionId { get; set; }

        [PersonalData]
        [Display(Name = "Nombre")]
        public string Nombrecompleto { get; set; }

        [PersonalData]
        [Display(Name = "Adscripción")]
        public string? Adscripcion { get; set; }

        [Display(Name = "Usuario UV")]
        public bool ActiveDirectoryEnabled { get; set; }

        [Display(Name = "Vista tarjetas")]
        public bool CardsViewEnabled { get; set; }

        public bool Deleted { get; set; }

    }
}

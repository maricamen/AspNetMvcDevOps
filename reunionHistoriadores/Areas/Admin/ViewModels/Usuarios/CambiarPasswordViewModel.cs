using System.ComponentModel.DataAnnotations;
using reunionhistoriadores2025.DataAnnotationsValidators;
using Microsoft.AspNetCore.Mvc;

namespace reunionhistoriadores2025.Areas.Admin.ViewModels.Usuarios
{
    [BindProperties]
    public class CambiarPasswordViewModel
    {
        public string? Id { get; set; }

        [Display(Name = "Correo")]
        public string? UserName { get; set; }

        [Display(Name = "Correo alterno")]
        public string? Email { get; set; }

        [CustomRequired]
        [PasswordValidation(6)]
        //[DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }        

        [Display(Name = "Nombre")]
        public string? Nombrecompleto { get; set; }

        [Display(Name = "Usuario UV")]
        public bool ActiveDirectoryEnabled { get; set; }

        [Display(Name = "Vista tarjetas")]
        public bool CardsViewEnabled { get; set; }

        [Display(Name = "Rol")]
        public string? Rol { get; set; }
    }
}

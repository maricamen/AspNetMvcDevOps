using System.ComponentModel.DataAnnotations;
using reunionhistoriadores2025.AuthorizationPolicies;
using reunionhistoriadores2025.DataAnnotationsValidators;
using Microsoft.AspNetCore.Mvc;

namespace reunionhistoriadores2025.Areas.Admin.ViewModels.Usuarios
{
    [BindProperties]
    public class EditarViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Correo")]
        public string? UserName { get; set; }

        [CustomRequired]
        [Display(Name = "Correo alterno")]
        //[EmailAddress(ErrorMessage = "Valida EmailAddress")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        public string Email { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        [CustomRequired]
        [Display(Name = "Nombre")]
        public string Nombrecompleto { get; set; }

        [CustomRequired]
        [Display(Name = "Adscripción")]
        public string Adscripcion { get; set; }

        [Display(Name = "Usuario UV")]
        public bool ActiveDirectoryEnabled { get; set; }

        [CustomRequired]
        [Display(Name = "Vista tarjetas")]
        public bool CardsViewEnabled { get; set; }

        [CustomRequired]
        //[MuestraBloqueIfValores(bloqueId: "BloqueLineasTematicas", AdminRolePolicies.CoordinadorMesaRole + "," + AdminRolePolicies.JuradoRole)]
        [Display(Name = "Rol")]
        public string Rol { get; set; }
    }
}

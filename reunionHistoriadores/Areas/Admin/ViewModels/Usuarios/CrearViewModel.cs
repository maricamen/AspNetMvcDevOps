using System.ComponentModel.DataAnnotations;
using reunionhistoriadores2025.AuthorizationPolicies;
using reunionhistoriadores2025.DataAnnotationsValidators;
using Microsoft.AspNetCore.Mvc;

namespace reunionhistoriadores2025.Areas.Admin.ViewModels.Usuarios
{
    [BindProperties]
    public class CrearViewModel
    {
        [CustomRequired]
        //[EmailAddress(ErrorMessage = "Valida EmailAddress")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        [Display(Name = "Correo")]
        public string UserName { get; set; }

        [CustomRequired]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        //[EmailAddress(ErrorMessage = "Valida EmailAddress")]
        [Display(Name = "Correo alterno")]
        public string Email { get; set; }

        [RequiredIfValor(otherProperty: "ActiveDirectoryEnabled", targetValue: "false")]
        [PasswordValidation(6)]
        //[DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string? Password { get; set; }

        [CustomRequired]
        [Display(Name = "Nombre")]
        public string Nombrecompleto { get; set; }

        [CustomRequired]
        [Display(Name = "Adscripción")]
        public string Adscripcion { get; set; }

        [CustomRequired]
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

using System.ComponentModel.DataAnnotations;
using reunionhistoriadores2025.DataAnnotationsValidators;
using Microsoft.AspNetCore.Mvc;

namespace reunionhistoriadores2025.Areas.Admin.ViewModels.Account
{
    [BindProperties]
    public class LoginViewModel
    {
        [CustomRequired]
        //[RequiredCountChars(10, MinimumLength = 3)]
        //[EmailAddress(ErrorMessage = "Valida EmailAddress")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        //[RequiredCountWords(10, MinimumLength = 2)]
        //[RequiredIfValor(nameof(Password), 1)]
        //[RequiredIfValores(nameof(Password), "1,2")]
        [Display(Name = "Correo")]
        public string Correo { get; set; }

        [CustomRequired]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [RequiredCheckBox]
        [Display(Name = "He leído y aceptado el aviso de privacidad")]
        public bool Declarativa { get; set; }
    }
}

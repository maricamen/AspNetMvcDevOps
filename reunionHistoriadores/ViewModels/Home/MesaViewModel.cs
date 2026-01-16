using reunionhistoriadores2025.DataAnnotationsValidators;
using reunionhistoriadores2025.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace reunionhistoriadores2025.ViewModels.Home
{
    public class MesaViewModel
    {
        public RegistroTipo? RegistroTipo { get; set; }

        [CustomRequired]
        [Display(Name = "Nombre de la mesa")]
        public string NombreMesa { get; set; }

        [CustomRequired]
        [Display(Name = "Tema")]
        public int LineaTematicaId { get; set; }

        [CustomRequired]
        [Display(Name = "Nombre(s) y apellidos del coordinador(a) de la mesa")]
        public string NombreCoordinador { get; set; }

        [CustomRequired]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        //[EmailAddress(ErrorMessage = "Valida EmailAddress")]
        [Display(Name = "Correo electrónico del coordinador(a) de la mesa")]
        public string CorreoCoordinador { get; set; }

        [Display(Name = "Nuevo tema (si selecciona 'Otro')")]
        public string? LineaTematicaOtro { get; set; }

        [CustomRequired]
        [MaxFileSize(TipoIdentificacion.MaxFileSizeInMB)]
        [AllowedExtensions(Extensions = TipoIdentificacion.ExtensionesValidas)]
        [DataType(DataType.Upload)]
        [Display(Name = "Resumen general y de ponencias")]
        public IFormFile Resumen { get; set; }

        [RequiredCheckBox]
        [Display(Name = "Acepto las condiciones de participación y confirmo lectura del Aviso de privacidad.")]
        public bool Declarativa { get; set; }

        public List<PonenciaMesaViewModel> Ponencias { get; set; }

    }
}

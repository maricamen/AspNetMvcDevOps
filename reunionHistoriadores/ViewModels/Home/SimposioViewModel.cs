using reunionhistoriadores2025.DataAnnotationsValidators;
using reunionhistoriadores2025.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace reunionhistoriadores2025.ViewModels.Home
{
    public class SimposioViewModel
    {
        public RegistroTipo? RegistroTipo { get; set; }

        [CustomRequired]
        [Display(Name = "Nombre del simposio")]
        public string NombreSimposio { get; set; }

        [CustomRequired]
        [Display(Name = "Tema")]
        public int LineaTematicaId { get; set; }

        [CustomRequired]
        [Display(Name = "Nombre(s) y apellidos del coordinador(a) del simposio")]
        public string NombreCoordinador { get; set; }

        [CustomRequired]
        [Display(Name = "Grado académico del coordinador(a) del simposio")]
        public string GradoAcademicoCoordinador { get; set; }

        [CustomRequired]
        [Display(Name = "Institución de adscripción del coordinador(a) del simposio")]
        public string InstitucionCoordinador { get; set; }

        [CustomRequired]
        //[EmailAddress(ErrorMessage = "Valida EmailAddress")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        [Display(Name = "Correo electrónico del coordinador(a) del simposio")]
        public string CorreoCoordinador { get; set; }

        [Display(Name = "Nuevo tema (si selecciona 'Otro')")]
        public string? LineaTematicaOtro { get; set; }

        [CustomRequired]
        [MaxFileSize(TipoIdentificacion.MaxFileSizeInMB)]
        [AllowedExtensions(Extensions = TipoIdentificacion.ExtensionesValidas)]
        [DataType(DataType.Upload)]
        [Display(Name = "Resumen general de Simposio")]
        public IFormFile Resumen { get; set; }

        [RequiredCheckBox]
        [Display(Name = "Acepto las condiciones de participación y confirmo lectura del Aviso de privacidad.")]
        public bool Declarativa { get; set; }

        public int? MaxMesas { get; set; } = 0;
        public List<RegistroMesaViewModel> Mesas { get; set; }

    }
}

using reunionhistoriadores2025.DataAnnotationsValidators;
using reunionhistoriadores2025.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using reunionHistoriadores2025.ViewModels.Home;

namespace reunionhistoriadores2025.ViewModels.Home
{
    public class RegistroMesaViewModel
    {
        [Display(Name = "Nombre(s) y apellidos del coordinador(a) de la mesa")]
        public string? NombreCoordinador { get; set; }

        //[EmailAddress(ErrorMessage = "Valida EmailAddress")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        [Display(Name = "Correo electrónico del coordinador(a) de la mesa")]
        public string? CorreoCoordinador { get; set; }

        [MaxFileSize(TipoIdentificacion.MaxFileSizeInMB)]
        [AllowedExtensions(Extensions = TipoIdentificacion.ExtensionesValidas)]
        [DataType(DataType.Upload)]
        [Display(Name = "Resumen general de Mesa")]
        public IFormFile? Resumen { get; set; }
        public List<PonenciaSimposioViewModel> Ponencias { get; set; }
    }
}

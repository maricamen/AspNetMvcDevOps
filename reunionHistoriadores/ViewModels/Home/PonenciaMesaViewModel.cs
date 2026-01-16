using reunionhistoriadores2025.DataAnnotationsValidators;
using reunionhistoriadores2025.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace reunionhistoriadores2025.ViewModels.Home
{
    public class PonenciaMesaViewModel
    {

        [CustomRequired]
        [Display(Name = "Título de la ponencia")]
        public string Titulo { get; set; }

        [CustomRequired]
        [Display(Name = "Nombre(s) y apellidos")]
        public string Nombre { get; set; }

        [CustomRequired]
        [Display(Name = "Grado académico")]
        public string GradoAcademico { get; set; }

        [CustomRequired]
        [Display(Name = "Género")]
        public int GeneroId { get; set; }

        //[Display(Name = "Prefiero autodescribir")]
        //public string? Autodescripcion { get; set; }

        [CustomRequired]
        [Display(Name = "Institución de adscripción ")]
        public string InstituciónAdscripción { get; set; }

        [CustomRequired]
        //[EmailAddress(ErrorMessage = "Valida EmailAddress")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }

        [CustomRequired]
        [Display(Name = "País")]
        public int PaisId { get; set; }
    }
}

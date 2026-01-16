using reunionhistoriadores2025.DataAnnotationsValidators;
using System.ComponentModel.DataAnnotations;

namespace reunionHistoriadores2025.ViewModels.Home
{
    public class PonenciaSimposioViewModel
    {
 
        [Display(Name = "Título de la ponencia")]
        public string? Titulo { get; set; }

        [Display(Name = "Nombre(s) y apellidos")]
        public string? Nombre { get; set; }

        [Display(Name = "Grado académico")]
        public string? GradoAcademico { get; set; }

        [Display(Name = "Género")]
        public int? GeneroId { get; set; }

        //[Display(Name = "Prefiero autodescribir")]
        //public string? Autodescripcion { get; set; }
 
        [Display(Name = "Institución de adscripción ")]
        public string? InstituciónAdscripción { get; set; }

        //[EmailAddress(ErrorMessage = "Valida EmailAddress")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        [Display(Name = "Correo electrónico")]
        public string? Correo { get; set; }

        [Display(Name = "País")]
        public int? PaisId { get; set; }
    }
}

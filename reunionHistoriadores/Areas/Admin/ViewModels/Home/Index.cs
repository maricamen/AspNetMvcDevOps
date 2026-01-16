using System.ComponentModel.DataAnnotations;
using reunionhistoriadores2025.DataAnnotationsValidators;
using reunionhistoriadores2025.Models;
using Microsoft.AspNetCore.Mvc;

namespace reunionhistoriadores2025.Areas.Admin.ViewModels.Home
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ClaseCss { get; set; }
        public string IconoCss { get; set; }
        public DateTime FechaCierre { get; set; }

        [Display(Name = "Sin atender")]
        public int SinAtender { get; set; }
        public string SinAtenderPorcentaje { get; set; }
        public string SinAtenderCss { get; set; }
        public int Aceptados { get; set; }
        public string AceptadosPorcentaje { get; set; }
        public string AceptadosCss { get; set; }
        public int Rechazados { get; set; }
        public string RechazadosPorcentaje { get; set; }
        public string RechazadosCss { get; set; }
        public int Eliminados { get; set; }
        public string EliminadosPorcentaje { get; set; }
        public string EliminadosCss { get; set; }
        public int SinEvaluar { get; set; }
        public string SinEvaluarPorcentaje { get; set; }
        public string SinEvaluarCss { get; set; }
        public int Evaluados { get; set; }
        public string EvaluadosPorcentaje { get; set; }
        public string EvaluadosCss { get; set; }

        [Display(Name = "Total")]
        public int Total { get; set; }
    }
}

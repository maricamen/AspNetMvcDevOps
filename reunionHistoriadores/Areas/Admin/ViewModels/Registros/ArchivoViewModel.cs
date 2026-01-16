using System.ComponentModel.DataAnnotations;
using reunionhistoriadores2025.DataAnnotationsValidators;
using reunionhistoriadores2025.Models;
using Microsoft.AspNetCore.Mvc;

namespace reunionhistoriadores2025.Areas.Admin.ViewModels.Registros
{
    public class ArchivoViewModel
    {
        public int Id { get; set; }
        public int? PonenciaId { get; set; }
        public int ArchivoTipoId { get; set; }
        public string Nombre { get; set; }
        public string Mime { get; set; }

        [Display(Name = "Tamaño")]
        public long Size { get; set; }
        public DateTime Fecha { get; set; }

        public ArchivoTipo ArchivoTipo { get; set; }        
    }
}

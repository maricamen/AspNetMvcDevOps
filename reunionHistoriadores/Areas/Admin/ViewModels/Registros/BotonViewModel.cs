using System.ComponentModel.DataAnnotations;
using reunionhistoriadores2025.DataAnnotationsValidators;
using reunionhistoriadores2025.Models;
using Microsoft.AspNetCore.Mvc;

namespace reunionhistoriadores2025.Areas.Admin.ViewModels.Registros
{
    public class BotonViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Controlador { get; set; }
        public string Action { get; set; }
    }
}

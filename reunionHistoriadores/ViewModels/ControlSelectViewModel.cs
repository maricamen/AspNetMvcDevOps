using System.ComponentModel.DataAnnotations;
using reunionhistoriadores2025.DataAnnotationsValidators;
using reunionhistoriadores2025.Models;
using Microsoft.AspNetCore.Mvc;

namespace reunionhistoriadores2025.ViewModels
{
    public class ControlSelectViewModel
    {
        public int Id { get; set; }
        public string Texto { get; set; }
    }
}

using reunionhistoriadores2025.Models;
using reunionhistoriadores2025.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace reunionhistoriadores2025.Helpers
{
    public static class PopulateItems
    {
        public static SelectList GetBoleanoSelect(object selectedItem = null)
        {
            var boleanoSelect = new List<ControlBoleanoViewModel>()
            {
                new ControlBoleanoViewModel { Id = "true", Texto = "Sí" },
                new ControlBoleanoViewModel { Id = "false", Texto = "No" }
            };
            return new SelectList(boleanoSelect, "Id", "Texto", selectedItem);
        }
    }
}

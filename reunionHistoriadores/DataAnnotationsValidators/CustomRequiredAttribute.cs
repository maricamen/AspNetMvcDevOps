using System.ComponentModel.DataAnnotations;

namespace reunionhistoriadores2025.DataAnnotationsValidators
{
    public class CustomRequiredAttribute : RequiredAttribute
    {
        private readonly string _defaultErrorMessage = "Campo requerido";
        public CustomRequiredAttribute()
        {
            // Si no escribimos el error, tomamos el default
            ErrorMessage = string.IsNullOrEmpty(ErrorMessage) ? _defaultErrorMessage : ErrorMessage;
        }
    }
}

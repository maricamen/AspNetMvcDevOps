using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace reunionhistoriadores2025.DataAnnotationsValidators
{
    public class RequiredCheckBoxAttribute : ValidationAttribute
    {
        private readonly string _defaultErrorMessage = "Campo requerido";

        public RequiredCheckBoxAttribute()
        {
            // Si no escribimos el error, tomamos el default
            ErrorMessage = string.IsNullOrEmpty(ErrorMessage) ? _defaultErrorMessage : ErrorMessage;
        }

        public override bool IsValid(object value)
        {
            if (value is bool boolean)
                return boolean;
            else
                return true;
        }
    }

    public class RequiredCheckBoxAttributeAdapter : AttributeAdapterBase<RequiredCheckBoxAttribute>
    {
        private readonly IStringLocalizer? _localizer;

        public RequiredCheckBoxAttributeAdapter(RequiredCheckBoxAttribute attribute, IStringLocalizer? stringLocalizer) : base(attribute, stringLocalizer)
        {
            _localizer = stringLocalizer;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var errorMessage = GetErrorMessage(context);
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-required", String.Format(_localizer["Valida Required"], context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-checkboxrequired", errorMessage);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName());
        }
    }
}

using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace reunionhistoriadores2025.DataAnnotationsValidators
{
    public class RequiredCountCharsAttribute : StringLengthAttribute
    {
        private readonly string _defaultErrorMessage = "Valida CountChars";
        public RequiredCountCharsAttribute(int maximumLength) : base(maximumLength)
        {
            // Si no escribimos el error, tomamos el default
            ErrorMessage = string.IsNullOrEmpty(ErrorMessage) ? _defaultErrorMessage : ErrorMessage;
        }
    }

    public class RequiredCountCharsAttributeAdapter : AttributeAdapterBase<RequiredCountCharsAttribute>
    {
        private readonly RequiredCountCharsAttribute _attribute;
        private readonly IStringLocalizer? _localizer;

        public RequiredCountCharsAttributeAdapter(RequiredCountCharsAttribute attribute, IStringLocalizer? stringLocalizer) : base(attribute, stringLocalizer)
        {
            _attribute = attribute;
            _localizer = stringLocalizer;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var errorMessage = GetErrorMessage(context);

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-required", String.Format(_localizer["Valida Required"], context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-requiredcountchars", errorMessage);
            MergeAttribute(context.Attributes, "data-val-requiredcountchars-minvalue", _attribute.MinimumLength.ToString());
            MergeAttribute(context.Attributes, "data-val-requiredcountchars-maxvalue", _attribute.MaximumLength.ToString());
            MergeAttribute(context.Attributes, "data-val-requiredcountchars-archivocaracteresescritas", "Caracteres escritos:");
            MergeAttribute(context.Attributes, "data-val-requiredcountchars-archivocaracteresrestantes", "Caracteres restantes:");
            MergeAttribute(context.Attributes, "data-val-requiredcountchars-archivominimo1", "El contenido debe tener un mínimo de");
            MergeAttribute(context.Attributes, "data-val-requiredcountchars-archivominimo2", "caractere(s)");
            MergeAttribute(context.Attributes, "data-val-requiredcountchars-archivoexceder1", "El contenido no debe exceder de");
            MergeAttribute(context.Attributes, "data-val-requiredcountchars-archivoexceder2", "caracteres");
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName(), _attribute.MaximumLength, _attribute.MinimumLength);
        }
    }
}

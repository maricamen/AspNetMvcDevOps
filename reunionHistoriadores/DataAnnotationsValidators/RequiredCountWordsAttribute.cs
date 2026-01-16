using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace reunionhistoriadores2025.DataAnnotationsValidators
{
    public class RequiredCountWordsAttribute : ValidationAttribute
    {
        private readonly string _defaultErrorMessage = "Valida CountWords";
        public int MaximumLength { get; private set; }
        public int MinimumLength { get; set; } = 0;

        public RequiredCountWordsAttribute(int maximumLength) : base()
        {
            MaximumLength = maximumLength;

            // Si no escribimos el error, tomamos el default
            ErrorMessage = string.IsNullOrEmpty(ErrorMessage) ? _defaultErrorMessage : ErrorMessage;
        }

        public override bool IsValid(object value)
        {
            // Check the lengths for legality
            EnsureLegalLengths();

            // Automatically pass if value is null. RequiredAttribute should be used to assert a value is not null.
            // We expect a cast exception if a non-string was passed in.
            int length = value == null ? 0 : Regex.Split(value.ToString().Trim(), @"\s+").Length;
            return value == null || (length >= this.MinimumLength && length <= this.MaximumLength);
        }

        private void EnsureLegalLengths()
        {
            if (MaximumLength < 0)
            {
                throw new InvalidOperationException("El parámetro MaximumLength debe ser un número entero no negativo.");
            }

            if (MaximumLength < this.MinimumLength)
            {
                throw new InvalidOperationException(String.Format("El valor máximo '{0}' debe ser más grande o igual al valor mínimo '{1}'.", this.MaximumLength, this.MinimumLength));
            }
        }
    }

    public class RequiredCountWordsAttributeAdapter : AttributeAdapterBase<RequiredCountWordsAttribute>
    {
        private readonly RequiredCountWordsAttribute _attribute;
        private readonly IStringLocalizer? _localizer;

        public RequiredCountWordsAttributeAdapter(RequiredCountWordsAttribute attribute, IStringLocalizer? stringLocalizer) : base(attribute, stringLocalizer)
        {
            _attribute = attribute;
            _localizer = stringLocalizer;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var errorMessage = GetErrorMessage(context);

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-required", String.Format(_localizer["Valida Required"], context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-requiredcountwords", errorMessage);
            MergeAttribute(context.Attributes, "data-val-requiredcountwords-minvalue", _attribute.MinimumLength.ToString());
            MergeAttribute(context.Attributes, "data-val-requiredcountwords-maxvalue", _attribute.MaximumLength.ToString());
            MergeAttribute(context.Attributes, "data-val-requiredcountwords-archivopalabrasescritas", "Palabras escritas:");
            MergeAttribute(context.Attributes, "data-val-requiredcountwords-archivopalabrasrestantes", "Palabras restantes:");
            MergeAttribute(context.Attributes, "data-val-requiredcountwords-archivominimo1", "El contenido debe tener un mínimo de");
            MergeAttribute(context.Attributes, "data-val-requiredcountwords-archivominimo2", "palabra(s)");
            MergeAttribute(context.Attributes, "data-val-requiredcountwords-archivoexceder1", "El contenido no debe exceder de");
            MergeAttribute(context.Attributes, "data-val-requiredcountwords-archivoexceder2", "palabras");
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName(), _attribute.MaximumLength, _attribute.MinimumLength);
        }
    }
}

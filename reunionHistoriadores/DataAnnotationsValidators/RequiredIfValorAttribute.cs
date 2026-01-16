using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace reunionhistoriadores2025.DataAnnotationsValidators
{
    public class RequiredIfValorAttribute : ValidationAttribute
    {
        private readonly string _defaultErrorMessage = "Valida RequiredIfValor";
        public string OtherProperty { get; private set; }
        public string TargetValue { get; private set; }

        public RequiredIfValorAttribute(string otherProperty, int targetValue) : base()
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException(nameof(otherProperty));
            }
            if (string.IsNullOrEmpty(targetValue.ToString()))
            {
                throw new ArgumentNullException(nameof(targetValue));
            }

            OtherProperty = otherProperty;
            TargetValue = targetValue.ToString();

            // Si no escribimos el error, tomamos el default
            ErrorMessage = string.IsNullOrEmpty(ErrorMessage) ? _defaultErrorMessage : ErrorMessage;
        }

        public RequiredIfValorAttribute(string otherProperty, string targetValue) : base()
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException(nameof(otherProperty));
            }
            if (string.IsNullOrEmpty(targetValue.ToString()))
            {
                throw new ArgumentNullException(nameof(targetValue));
            }

            OtherProperty = otherProperty;
            TargetValue = targetValue.ToString();

            // Si no escribimos el error, tomamos el default
            ErrorMessage = string.IsNullOrEmpty(ErrorMessage) ? _defaultErrorMessage : ErrorMessage;
        }        

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            if (otherPropertyInfo == null)
                return new ValidationResult(String.Format("No se pudo encontrar una propiedad con el nombre {0}.", OtherProperty));

            object otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (otherPropertyValue == null)
                return ValidationResult.Success;

            if (otherPropertyValue.ToString() == TargetValue)
            {
                if (value == null)
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

                if (string.IsNullOrEmpty(value.ToString()))
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }

    public class RequiredIfValorAttributeAdapter : AttributeAdapterBase<RequiredIfValorAttribute>
    {
        private readonly RequiredIfValorAttribute _attribute;
        private readonly IStringLocalizer? _localizer;

        public RequiredIfValorAttributeAdapter(RequiredIfValorAttribute attribute, IStringLocalizer? stringLocalizer) : base(attribute, stringLocalizer)
        {
            _attribute = attribute;
            _localizer = stringLocalizer;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var errorMessage = GetErrorMessage(context);

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-required", String.Format(_localizer["Valida Required"], context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-requiredifvalor", errorMessage);
            MergeAttribute(context.Attributes, "data-val-requiredifvalor-otherproperty", _attribute.OtherProperty);
            MergeAttribute(context.Attributes, "data-val-requiredifvalor-targetvalue", _attribute.TargetValue);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName());
        }
    }
}

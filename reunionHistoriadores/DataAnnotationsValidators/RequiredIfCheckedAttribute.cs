using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace reunionhistoriadores2025.DataAnnotationsValidators
{
    public class RequiredIfCheckedAttribute : ValidationAttribute
    {
        private readonly string _defaultErrorMessage = "Valida RequiredIfValor";
        public string OtherProperty { get; private set; }

        public RequiredIfCheckedAttribute(string otherProperty) : base()
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException(nameof(otherProperty));
            }

            OtherProperty = otherProperty;

            // Si no escribimos el error, tomamos el default
            ErrorMessage = string.IsNullOrEmpty(ErrorMessage) ? _defaultErrorMessage : ErrorMessage;
        }

        public RequiredIfCheckedAttribute(string otherProperty, string targetValue) : base()
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

            if ((bool)otherPropertyValue == true)
            {
                if (value == null)
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

                if (string.IsNullOrEmpty(value.ToString()))
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }

    public class RequiredIfCheckedAttributeAdapter : AttributeAdapterBase<RequiredIfCheckedAttribute>
    {
        private readonly RequiredIfCheckedAttribute _attribute;
        private readonly IStringLocalizer? _localizer;

        public RequiredIfCheckedAttributeAdapter(RequiredIfCheckedAttribute attribute, IStringLocalizer? stringLocalizer) : base(attribute, stringLocalizer)
        {
            _attribute = attribute;
            _localizer = stringLocalizer;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var errorMessage = GetErrorMessage(context);

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-required", String.Format(_localizer["Valida Required"], context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-requiredifchecked", errorMessage);
            MergeAttribute(context.Attributes, "data-val-requiredifchecked-otherproperty", _attribute.OtherProperty);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName());
        }
    }
}

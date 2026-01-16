using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace reunionhistoriadores2025.DataAnnotationsValidators
{
    public class RequiredIfValoresAttribute : ValidationAttribute
    {
        private readonly string _defaultErrorMessage = "Valida RequiredIfValores";
        public string OtherProperty { get; private set; }
        public string ListOfTargetValues { get; private set; }

        public RequiredIfValoresAttribute(string otherProperty, string listOfTargetValues) : base()
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException(nameof(otherProperty));
            }
            if (string.IsNullOrEmpty(listOfTargetValues))
            {
                throw new ArgumentNullException(nameof(listOfTargetValues));
            }

            OtherProperty = otherProperty;
            ListOfTargetValues = listOfTargetValues;

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

            if (ListOfTargetValues.Split(',').Contains(otherPropertyValue.ToString()))
            {
                if (value == null)
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

                if (string.IsNullOrEmpty(value.ToString()))
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;

        }
    }

    public class RequiredIfValoresAttributeAdapter : AttributeAdapterBase<RequiredIfValoresAttribute>
    {
        private readonly RequiredIfValoresAttribute _attribute;
        private readonly IStringLocalizer? _localizer;

        public RequiredIfValoresAttributeAdapter(RequiredIfValoresAttribute attribute, IStringLocalizer? stringLocalizer) : base(attribute, stringLocalizer)
        {
            _attribute = attribute;
            _localizer = stringLocalizer;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var errorMessage = GetErrorMessage(context);

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-required", String.Format(_localizer["Valida Required"], context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-requiredifvalores", errorMessage);
            MergeAttribute(context.Attributes, "data-val-requiredifvalores-otherproperty", _attribute.OtherProperty);
            MergeAttribute(context.Attributes, "data-val-requiredifvalores-listoftargetvalues", _attribute.ListOfTargetValues);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName());
        }
    }
}

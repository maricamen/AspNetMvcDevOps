using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace reunionhistoriadores2025.DataAnnotationsValidators
{
    public class RequiredIfGroupEmptyAttribute : ValidationAttribute
    {
        private readonly string _defaultErrorMessage = "Valida RequiredIfGroupEmpty";
        public string ListOfProperties { get; private set; }

        public RequiredIfGroupEmptyAttribute(string listOfProperties) : base()
        {
            if (string.IsNullOrEmpty(listOfProperties))
            {
                throw new ArgumentNullException(nameof(listOfProperties));
            }

            ListOfProperties = listOfProperties;

            // Si no escribimos el error, tomamos el default
            ErrorMessage = string.IsNullOrEmpty(ErrorMessage) ? _defaultErrorMessage : ErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            int countChecked = 0;

            foreach (string item in ListOfProperties.Split(','))
            {
                PropertyInfo property = type.GetProperty(item);
                object propertyValue = property.GetValue(instance);

                if ((bool)propertyValue == true)
                    countChecked++;
            }

            // Si no ha sido checado ningun otro checkbox
            if (countChecked == 0)
            {
                if (value == null)
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

                if ((bool)value == false)
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }

    public class RequiredIfGroupEmptyAttributeAdapter : AttributeAdapterBase<RequiredIfGroupEmptyAttribute>
    {
        private readonly RequiredIfGroupEmptyAttribute _attribute;
        private readonly IStringLocalizer? _localizer;

        public RequiredIfGroupEmptyAttributeAdapter(RequiredIfGroupEmptyAttribute attribute, IStringLocalizer? stringLocalizer) : base(attribute, stringLocalizer)
        {
            _attribute = attribute;
            _localizer = stringLocalizer;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var errorMessage = GetErrorMessage(context);

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-required", String.Format(_localizer["Valida Required"], context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-requiredifgroupempty", errorMessage);
            MergeAttribute(context.Attributes, "data-val-requiredifgroupempty-listofproperties", _attribute.ListOfProperties);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName());
        }
    }
}

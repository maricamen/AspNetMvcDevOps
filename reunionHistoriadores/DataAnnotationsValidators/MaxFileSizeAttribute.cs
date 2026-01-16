using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace reunionhistoriadores2025.DataAnnotationsValidators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly string _defaultErrorMessage = "Valida MaxFileSize";
        public int MaxFileSizeInMB { get; private set; }

        public MaxFileSizeAttribute(int maxFileSizeInMB) : base()
        {
            MaxFileSizeInMB = maxFileSizeInMB;

            // Si no escribimos el error, tomamos el default
            ErrorMessage = string.IsNullOrEmpty(ErrorMessage) ? _defaultErrorMessage : ErrorMessage;
        }

        public override bool IsValid(object value)
        {
            // Check the lengths for legality
            EnsureLegalSize();

            // Automatically pass if value is null. RequiredAttribute should be used to assert a value is not null.
            // We expect a cast exception if the passed value was not an IFormFile.
            var length = value == null ? 0 : ((IFormFile)value).Length;

            return value == null || (length <= (MaxFileSizeInMB * 1024 * 1024));
        }

        private void EnsureLegalSize()
        {
            if (MaxFileSizeInMB < 0)
            {
                throw new InvalidOperationException("El parámetro maxFileSize debe ser un número entero no negativo.");
            }
        }
    }

    public class MaxFileSizeAttributeAdapter : AttributeAdapterBase<MaxFileSizeAttribute>
    {
        private readonly MaxFileSizeAttribute _attribute;
        private readonly IStringLocalizer? _localizer;

        public MaxFileSizeAttributeAdapter(MaxFileSizeAttribute attribute, IStringLocalizer? stringLocalizer) : base(attribute, stringLocalizer)
        {
            _attribute = attribute;
            _localizer = stringLocalizer;
        }
        public override void AddValidation(ClientModelValidationContext context)
        {
            var errorMessage = GetErrorMessage(context);

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-required", String.Format(_localizer["Valida Required"], context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-maxfilesize", errorMessage);
            MergeAttribute(context.Attributes, "data-val-maxfilesize-maxvalue", (_attribute.MaxFileSizeInMB * 1024 * 1024).ToString());
            MergeAttribute(context.Attributes, "data-val-maxfilesize-pesomaximo", $"El peso máximo permitido es {_attribute.MaxFileSizeInMB}MB.");

        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName(), _attribute.MaxFileSizeInMB);
        }
    }
}

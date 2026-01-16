using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace reunionhistoriadores2025.DataAnnotationsValidators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string _defaultErrorMessage = "Valida FileExtensions";
        private string _extensions;

        public string Extensions
        {
            get
            {
                // Default file extensions
                return string.IsNullOrEmpty(_extensions) ? ".pdf,.doc,.docx" : _extensions;
            }
            set
            {
                _extensions = value;
            }
        }

        private string ExtensionsNormalized
        {
            get
            {
                return Extensions.Replace(" ", "", StringComparison.Ordinal).Replace(".", "", StringComparison.Ordinal).ToLowerInvariant();
            }
        }

        public AllowedExtensionsAttribute() : base()
        {
            // Si no escribimos el error, tomamos el default
            ErrorMessage = string.IsNullOrEmpty(ErrorMessage) ? _defaultErrorMessage : ErrorMessage;
        }

        public override bool IsValid(object value)
        {
            // Automatically pass if value is null. RequiredAttribute should be used to assert a value is not null.
            if (value == null)
            {
                return true;
            }

            // We expect a cast exception if the passed value was not an IFormFile.
            return ExtensionsNormalized.Split(",").Contains(((IFormFile)value).FileName.Split('.').LastOrDefault().ToLowerInvariant());
        }
    }

    public class AllowedExtensionsAttributeAdapter : AttributeAdapterBase<AllowedExtensionsAttribute>
    {
        private readonly AllowedExtensionsAttribute _attribute;
        private readonly IStringLocalizer? _localizer;

        public AllowedExtensionsAttributeAdapter(AllowedExtensionsAttribute attribute, IStringLocalizer? stringLocalizer) : base(attribute, stringLocalizer)
        {
            _attribute = attribute;
            _localizer = stringLocalizer;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var errorMessage = GetErrorMessage(context);

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-required", String.Format(_localizer["Valida Required"], context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-allowedextensions", errorMessage);
            // Para valida en javascript requiere quitar el punto
            MergeAttribute(context.Attributes, "data-val-allowedextensions-extensions", _attribute.Extensions.Replace(".", "", StringComparison.Ordinal).ToLowerInvariant());
            // Para abrir el dialogo de selección de archivo
            MergeAttribute(context.Attributes, "data-val-allowedextensions-accept", _attribute.Extensions.ToLowerInvariant());
            // Escribe el texto descriptivo con espacio después del punto
            MergeAttribute(context.Attributes, "data-val-allowedextensions-extensionespermitidas", $"Las extensiones permitidas son ({_attribute.Extensions.Replace(",.", ", .", StringComparison.Ordinal)}).");
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName(), _attribute.Extensions.Replace(",.", ", .", StringComparison.Ordinal));
        }
    }
}

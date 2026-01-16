using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace reunionhistoriadores2025.DataAnnotationsValidators
{
    public class MuestraBloqueIfValoresAttribute : ValidationAttribute
    {
        private readonly string _defaultErrorMessage = "Valida MuestraBloqueIfValores";
        public string BloqueId { get; private set; }
        public string ListOfTargetValues { get; private set; }

        public MuestraBloqueIfValoresAttribute(string bloqueId, string listOfTargetValues) : base()
        {
            if (string.IsNullOrEmpty(bloqueId))
            {
                throw new ArgumentNullException(nameof(bloqueId));
            }
            if (string.IsNullOrEmpty(listOfTargetValues))
            {
                throw new ArgumentNullException(nameof(listOfTargetValues));
            }

            BloqueId = bloqueId;
            ListOfTargetValues = listOfTargetValues;

            // Si no escribimos el error, tomamos el default
            ErrorMessage = string.IsNullOrEmpty(ErrorMessage) ? _defaultErrorMessage : ErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Siempre va a ser válido porque solo queremos mostrar/ocultar un div
            return ValidationResult.Success;
        }
    }

    public class MuestraBloqueIfValoresAttributeAdapter : AttributeAdapterBase<MuestraBloqueIfValoresAttribute>
    {
        private readonly MuestraBloqueIfValoresAttribute _attribute;
        private readonly IStringLocalizer? _localizer;

        public MuestraBloqueIfValoresAttributeAdapter(MuestraBloqueIfValoresAttribute attribute, IStringLocalizer? stringLocalizer) : base(attribute, stringLocalizer)
        {
            _attribute = attribute;
            _localizer = stringLocalizer;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var errorMessage = GetErrorMessage(context);

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-muestrabloqueifvalores", errorMessage);
            MergeAttribute(context.Attributes, "data-val-muestrabloqueifvalores-bloqueid", _attribute.BloqueId);
            MergeAttribute(context.Attributes, "data-val-muestrabloqueifvalores-listoftargetvalues", _attribute.ListOfTargetValues);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName());
        }
    }
}

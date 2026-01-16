using reunionhistoriadores2025.DataAnnotationsValidators;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace reunionhistoriadores2025.Services.DataAnnotationsValidators
{
    public class CustomValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();
        public IAttributeAdapter? GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer? stringLocalizer)
        {
            return attribute switch
            {
                RequiredCheckBoxAttribute checkBoxRequiredAttribute =>
                    new RequiredCheckBoxAttributeAdapter(checkBoxRequiredAttribute, stringLocalizer),
                PasswordValidationAttribute passwordValidationAttribute =>
                    new PasswordValidationAttributeAdapter(passwordValidationAttribute, stringLocalizer),
                RequiredCountCharsAttribute requiredCountCharsAttribute =>
                    new RequiredCountCharsAttributeAdapter(requiredCountCharsAttribute, stringLocalizer),
                RequiredCountWordsAttribute requiredCountWordsAttribute =>
                    new RequiredCountWordsAttributeAdapter(requiredCountWordsAttribute, stringLocalizer),
                RequiredIfGroupEmptyAttribute requiredIfGroupEmptyAttribute =>
                    new RequiredIfGroupEmptyAttributeAdapter(requiredIfGroupEmptyAttribute, stringLocalizer),
                RequiredIfValorAttribute requiredIfValorAttribute =>
                    new RequiredIfValorAttributeAdapter(requiredIfValorAttribute, stringLocalizer),
                RequiredIfValoresAttribute requiredIfValoresAttribute =>
                    new RequiredIfValoresAttributeAdapter(requiredIfValoresAttribute, stringLocalizer),
                MuestraBloqueIfValoresAttribute muestraBloqueIfValoresAttribute =>
                    new MuestraBloqueIfValoresAttributeAdapter(muestraBloqueIfValoresAttribute, stringLocalizer),
                RequiredIfCheckedAttribute requiredIfCheckedAttribute =>
                    new RequiredIfCheckedAttributeAdapter(requiredIfCheckedAttribute, stringLocalizer),
                MaxFileSizeAttribute maxFileSizeAttribute =>
                    new MaxFileSizeAttributeAdapter(maxFileSizeAttribute, stringLocalizer),
                AllowedExtensionsAttribute allowedExtensionsAttribute =>
                    new AllowedExtensionsAttributeAdapter(allowedExtensionsAttribute, stringLocalizer),
                _ => _baseProvider.GetAttributeAdapter(attribute, stringLocalizer)
            };
        }
    }
}

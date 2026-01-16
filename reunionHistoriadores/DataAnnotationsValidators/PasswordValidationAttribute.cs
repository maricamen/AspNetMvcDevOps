using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace reunionhistoriadores2025.DataAnnotationsValidators
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        private readonly string _defaultErrorMessage = "Valida Password";
        public bool AllowEmptyString { get; private set; }
        public int MinLength { get; private set; }
        public bool RequireDigit { get; private set; }
        public bool RequireLowercase { get; private set; }
        public bool RequireUppercase { get; private set; }
        public bool RequireNonLetterOrDigit { get; private set; }

        public PasswordValidationAttribute(int MinLength = 6, bool AllowEmptyString = true, bool RequireDigit = true, bool RequireLowercase = true, bool RequireUppercase = true, bool RequireNonLetterOrDigit = true) : base()
        {
            if (string.IsNullOrEmpty(MinLength.ToString()))
            {
                throw new ArgumentNullException(nameof(MinLength));
            }

            this.AllowEmptyString = AllowEmptyString;
            this.MinLength = MinLength;
            this.RequireDigit = RequireDigit;
            this.RequireLowercase = RequireLowercase;
            this.RequireUppercase = RequireUppercase;
            this.RequireNonLetterOrDigit = RequireNonLetterOrDigit;

            // Si no escribimos el error, tomamos el default
            ErrorMessage = string.IsNullOrEmpty(ErrorMessage) ? _defaultErrorMessage : ErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (AllowEmptyString && value == null)
                return ValidationResult.Success;

            string password = value.ToString();

            if (password.Length < MinLength)
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            int counter = 0;
            List<string> patterns = new();

            if (RequireLowercase)
                patterns.Add(@"[a-z]"); // lowercase
            if (RequireLowercase)
                patterns.Add(@"[A-Z]"); // uppercase
            if (RequireDigit)
                patterns.Add(@"[0-9]"); // digits
            if (RequireNonLetterOrDigit)
                patterns.Add(@"[!@#$%^&*\)\(]"); // special symbols

            // count type of different chars in password  
            foreach (string p in patterns)
            {
                if (Regex.IsMatch(password, p))
                {
                    counter++;
                }
            }
            if (counter < patterns.Count)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }        
    }

    public class PasswordValidationAttributeAdapter : AttributeAdapterBase<PasswordValidationAttribute>
    {
        private readonly PasswordValidationAttribute _attribute;
        private readonly IStringLocalizer? _localizer;

        public PasswordValidationAttributeAdapter(PasswordValidationAttribute attribute, IStringLocalizer? stringLocalizer) : base(attribute, stringLocalizer)
        {
            _attribute = attribute;
            _localizer = stringLocalizer;
        }
        public override void AddValidation(ClientModelValidationContext context)
        {
            var errorMessage = GetErrorMessage(context);

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-required", String.Format(_localizer["Valida Required"], context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-custompasswordvalidator", errorMessage);
            MergeAttribute(context.Attributes, "data-val-custompasswordvalidator-allowemptystring", _attribute.AllowEmptyString.ToString());
            MergeAttribute(context.Attributes, "data-val-custompasswordvalidator-minlength", _attribute.MinLength.ToString());
            MergeAttribute(context.Attributes, "data-val-custompasswordvalidator-requiredigit", _attribute.RequireDigit.ToString());
            MergeAttribute(context.Attributes, "data-val-custompasswordvalidator-requirelowercase", _attribute.RequireLowercase.ToString());
            MergeAttribute(context.Attributes, "data-val-custompasswordvalidator-requireuppercase", _attribute.RequireUppercase.ToString());
            MergeAttribute(context.Attributes, "data-val-custompasswordvalidator-requirenonletterordigit", _attribute.RequireNonLetterOrDigit.ToString());
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName());
        }
    }
}

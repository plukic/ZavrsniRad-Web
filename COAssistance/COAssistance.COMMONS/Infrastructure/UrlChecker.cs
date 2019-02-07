using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace COAssistance.COMMONS.Infrastructure
{
    /// <summary>
    /// Enables url validation
    /// </summary>
    /// <remarks>
    /// Since this is a custom validation attribute, it's not supported by jquery unobtrusive validation.
    /// To enable client side validation, implement IClientValidatable(<see cref="IClientValidatable"/>) interface and define
    /// needed .js logic.
    /// </remarks>
    public class UrlChecker : ValidationAttribute
    {
        #region Methods

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var url = (string)value;

            if (Uri.TryCreate(url, UriKind.Absolute, out Uri uri) && (uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeHttp))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(base.ErrorMessage);
        }

        #endregion Methods
    }
}
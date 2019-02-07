using COAssistance.COMMONS.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace COAssistance.COMMONS.Infrastructure
{
    /// <summary>
    /// Validates file selection
    /// </summary>
    /// <remarks>
    /// Since this is a custom validation attribute, it's not supported by jquery unobtrusive validation.
    /// To enable client side validation, implement IClientValidatable(<see cref="IClientValidatable"/>) interface and define
    /// needed .js logic.
    /// </remarks>
    public class FileRequired : ValidationAttribute
    {
        #region Fields

        private readonly int _minWidth;
        private readonly int _minHeight;

        #endregion Fields

        #region Constructor

        public FileRequired(int minWidth, int minHeight)
        {
            _minWidth = minWidth;
            _minHeight = minHeight;
        }

        #endregion Constructor

        #region Methods

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                // If value is null, return "FileRequired" validation message;
                return new ValidationResult(Resource.FileRequired);
            }

            var file = (HttpPostedFileBase)value;

            if (file != null && file.ContentLength > 0)
            {
                // Validate dimensions
                return ValidationResult.Success;
            }

            // Here use message that says selected file is invalid;
            return new ValidationResult(base.ErrorMessage);
        }

        #endregion Methods
    }
}
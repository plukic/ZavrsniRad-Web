using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace COAssistance.COMMONS.Infrastructure
{
    /// <summary>
    /// Validates if selected number is less than allowed
    /// </summary>
    /// <remarks>
    /// Since this is a custom validation attribute, it's not supported by jquery unobtrusive validation.
    /// To enable client side validation, implement IClientValidatable(<see cref="IClientValidatable"/>) interface and define
    /// needed .js logic.
    /// </remarks>
    public class MinNumberAttribute : ValidationAttribute
    {
        #region Fields

        private readonly int _minNumber;

        #endregion Fields

        #region Constructors

        public MinNumberAttribute(string errorMessage)
            : base(errorMessage)
        {
        }

        public MinNumberAttribute(int minNumber)
        {
            _minNumber = minNumber;
        }

        #endregion Constructors

        #region Methods

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var number = (int)value;

            if (number < _minNumber)
            {
                return new ValidationResult(base.ErrorMessage);
            }

            return ValidationResult.Success;
        }

        #endregion Methods
    }
}
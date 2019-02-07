using COAssistance.COMMONS.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace COAssistance.WEB.Extensions
{
    /// <summary>
    /// Enum extensions
    /// </summary>
    public static class EnumExtensions
    {
        #region Methods

        /// <summary>
        /// Returns [Name] value from Display attribute
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="value">Enum instance</param>
        /// <param name="isLocalized">Value to determine if should value be localized</param>
        /// <returns>String value</returns>
        public static string GetName<TEnum>(this Enum value, bool isLocalized = true)
        {
            if (value == null)
            {
                return "/";
            }

            var attribute = typeof(TEnum).GetMember(value.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DisplayAttribute>();

            if (attribute != null)
            {
                if (isLocalized)
                {
                    return Resource.ResourceManager.GetString(attribute.Name);
                }

                return attribute.Name;
            }

            return "/";
        }

        /// <summary>
        /// Returns [Description] value from Display attribute
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="value">Enum instance</param>
        /// <returns>String value</returns>
        public static string GetDescription<TEnum>(this Enum value)
        {
            if (value == null)
            {
                return "/";
            }

            var attribute = typeof(TEnum).GetMember(value.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DisplayAttribute>();

            return attribute?.Description;
        }

        #endregion Methods
    }
}
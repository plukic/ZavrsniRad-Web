using System;

namespace COAssistance.COMMONS.Extensions
{
    /// <summary>
    /// Generic extension methods
    /// </summary>
    public static class GenericExtensions
    {
        #region Methods

        /// <summary>
        /// Checks if value for current type equals to its default value.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="value">Value</param>
        /// <returns>Boolean</returns>
        public static bool HasDefaultValue<T>(this T value)
            where T : IEquatable<T>
        {
            return value.Equals(default(T));
        }

        #endregion Methods
    }
}
using COAssistance.COMMONS.Extensions;
using System;
using System.Configuration;
using System.Linq;

namespace COAssistance.WEB.Extensions
{
    /// <summary>
    /// Extends existing ConfigurationManager methods
    /// </summary>
    /// <remarks>
    /// Cannot extend with instance methods since ConfigurationManager(<see cref="ConfigurationManager"/>) is static class.
    /// </remarks>
    public static class ConfigurationManagerWrapper
    {
        #region Methods

        /// <summary>
        /// Checks if AppSettings collection contains values with passed keys
        /// </summary>
        /// <param name="keys">Keys to check</param>
        /// <returns>Boolean result</returns>
        public static bool EntryExists(params string[] keys)
        {
            return keys.All(k => ConfigurationManager.AppSettings
                .AllKeys
                .Contains(k));
        }

        /// <summary>
        /// Gets and converts value to specified type
        /// </summary>
        /// <typeparam name="T">Destination type</typeparam>
        /// <param name="key">Key</param>
        /// <returns>Converted or default value</returns>
        public static T GetConvertedValue<T>(string key) where T : IConvertible
        {
            var value = ConfigurationManager.AppSettings[key];

            if (value.IsNullOrEmpty())
                return default(T);

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }

        #endregion Methods
    }
}
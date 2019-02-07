using COAssistance.COMMONS.Resources;

namespace COAssistance.COMMONS.Extensions
{
    /// <summary>
    /// Extends existing int methods
    /// </summary>
    public static class IntExtensions
    {
        #region Methods

        public static string ToStringDays(this int number)
        {
            if (number == 1 || number % 10 == 0 || number % 2 == 0)
            {
                return $"{number} {Resource.Days.ToLower()}";
            }

            return $"{number} {Resource.Day.ToLower()}";
        }

        #endregion Methods
    }
}
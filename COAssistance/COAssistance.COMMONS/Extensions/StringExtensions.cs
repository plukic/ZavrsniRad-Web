using System;
using System.Linq;

namespace COAssistance.COMMONS.Extensions
{
    /// <summary>
    /// Extends existing string methods
    /// </summary>
    public static class StringExtensions
    {
        #region Methods

        /// <summary>
        /// Substrings words from text
        /// </summary>
        /// <param name="value">Text to substring</param>
        /// <param name="wordCount">Words to substring from text</param>
        /// <param name="postfixCharacter">Character to append at the end of text</param>
        /// <returns>String</returns>
        public static string SubstringWords(this string value, int wordCount, string postfixCharacter = "...")
        {
            if (string.IsNullOrEmpty(value)) return value;
            if (wordCount <= 0) throw new InvalidOperationException("Word count has to be greater than zero.");

            var segments = value.Split(' ').Take(wordCount);

            var joined = string.Join(" ", segments);

            // Do not append postfix character if last element contains dot, or
            // if value of wordCount argument is grater than length of segments.
            if (segments.Last().Contains(".") || wordCount > segments.Count())
            {
                return joined.ToString();
            }

            return $"{joined.ToString()} {postfixCharacter}";
        }

        /// <summary>
        /// Shorthand for checking if string is null or empty
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Boolean</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Replaces all characters from value
        /// </summary>
        /// <param name="value">Value to edit</param>
        /// <param name="replacementChar">Replacement character</param>
        /// <param name="charsToReplace">Characters to replace</param>
        /// <returns>String</returns>
        public static string Replace(this string value, string replacementChar, params string[] charsToReplace)
        {
            foreach (var character in charsToReplace)
            {
                value.Replace(character, replacementChar);
            }

            return value;
        }

        #endregion Methods
    }
}
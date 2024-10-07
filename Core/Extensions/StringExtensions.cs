using Core.Helpers;

namespace Core.Extensions
{
    /// <summary>
    /// A static class that provides extension methods for processing strings with dynamic values.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Replaces dynamic values in a string based on specific prefixes ("AppSettings" or "Random").
        /// </summary>
        /// <param name="input">The input string potentially containing dynamic values.</param>
        /// <returns>
        /// A modified string where dynamic values are replaced:
        /// - If the string starts with "AppSettings", the corresponding application setting value is retrieved.
        /// - If the string starts with "Random", a random value is generated.
        /// - Otherwise, the original string is returned unchanged.
        /// </returns>
        public static string ReplaceDynamicValues(this string input)
        {
            // Check if the string starts with "AppSettings" and replace with corresponding app settings value
            if (input.StartsWith("AppSettings"))
            {
                return ValueHelper.GetAppSettingsValue(input);
            }

            // Check if the string starts with "Random" and replace with a generated random value
            if (input.StartsWith("Random"))
            {
                return ValueHelper.GenerateRandomValue(input);
            }

            // If no dynamic value is detected, return the original input
            return input;
        }
    }
}

using System.Globalization;

namespace Core.Helpers
{
    /// <summary>
    /// A static helper class for parsing price values from string representations.
    /// </summary>
    public static class PriceHelper
    {
        /// <summary>
        /// Parses a string representation of a price and returns the corresponding decimal value.
        /// </summary>
        /// <param name="priceString">The string representation of the price (e.g., "₴1000").</param>
        /// <returns>
        /// A decimal value representing the price. If the input string is "Безкоштовно", it returns 0.
        /// </returns>
        /// <exception cref="FormatException">
        /// Thrown if the price string is in an invalid format that cannot be parsed to a decimal.
        /// </exception>
        public static decimal ParsePrice(string priceString)
        {
            if (priceString == "Безкоштовно")
                return 0;
            else
            {
                return decimal.Parse(priceString
                            .Replace("₴", "")       // Remove currency symbol
                            .Replace(" ", "")       // Remove spaces
                            .Replace(",", ".")      // Replace comma with dot for decimal point
                            .Trim(),
                            CultureInfo.InvariantCulture);  // Parse using invariant culture to ensure correct format
            }
        }
    }
}

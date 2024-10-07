using Core.Extensions;
using NUnit.Framework;

namespace Core.Helpers
{
    /// <summary>
    /// A static helper class for parsing and handling age values and their corresponding age units.
    /// </summary>
    public static class AgeUnitHelper
    {
        /// <summary>
        /// Parses a string representation of an age unit and returns the corresponding <see cref="Enums.AgeUnits"/> enum value.
        /// </summary>
        /// <param name="unitString">The string representation of the age unit (e.g., "Months").</param>
        /// <returns>
        /// The corresponding <see cref="Enums.AgeUnits"/> enum value, or the default value if no match is found.
        /// </returns>
        public static Enums.AgeUnits ParseAgeUnit(string unitString)
        {
            // Iterate through all values in the AgeUnits enum and find the one whose string value matches the input
            return Enum.GetValues(typeof(Enums.AgeUnits))
                       .Cast<Enums.AgeUnits>()
                       .FirstOrDefault(unit => unit.GetStringValue()
                                                   .Equals(unitString, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Parses an age string (e.g., "10 Months") into its numeric age and corresponding <see cref="Enums.AgeUnits"/> value.
        /// </summary>
        /// <param name="ageString">The age string containing both the numeric part and the unit (e.g., "10 Months").</param>
        /// <returns>
        /// A tuple containing the parsed age as an integer and the corresponding <see cref="Enums.AgeUnits"/> enum value.
        /// </returns>
        /// <exception cref="AssertionException">
        /// Thrown when the age string format is invalid, or if the numeric part of the string cannot be parsed as an integer.
        /// </exception>
        public static (int age, Enums.AgeUnits unit) ParseAge(string ageString)
        {
            // Split the age string into numeric and unit parts (e.g., "10" and "Місяців")
            var parts = ageString.Split(' ');
            if (parts.Length != 2)
            {
                throw new AssertionException($"Invalid age format: {ageString}");
            }

            // Parse the numeric part of the string (e.g., "10")
            if (!int.TryParse(parts[0], out var age))
            {
                throw new AssertionException($"Invalid age number: {parts[0]}");
            }

            // Parse the unit part of the string (e.g., "Місяців")
            var unit = ParseAgeUnit(parts[1]);

            // Multiply the age by the corresponding unit's value
            age *= (int)unit;

            return (age, unit);
        }
    }
}

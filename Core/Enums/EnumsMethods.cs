using Core.CustomAttributes;

namespace Core
{
    /// <summary>
    /// A static class containing helper methods for working with enums and their custom attributes.
    /// </summary>
    public static partial class Enums
    {
        /// <summary>
        /// Retrieves a random value from the specified enum type.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <returns>A random value from the enum of type <typeparamref name="T"/>.</returns>
        public static T GetRandomValue<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();  // Get all values of the enum type
            var random = new Random();  // Create a random number generator
            return values[random.Next(values.Count)];  // Return a random enum value
        }

        /// <summary>
        /// Retrieves the value of a specified attribute from an enum field.
        /// If the attribute or value is not found, returns the string representation of the enum field.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="enumValue">The enum value for which to retrieve the attribute value.</param>
        /// <param name="attributeType">The type of attribute to search for on the enum value.</param>
        /// <returns>The value of the specified attribute, or the string representation of the enum field if the attribute is not found.</returns>
        public static string GetAttributeValue<T>(T enumValue, Type attributeType) where T : Enum
        {
            // Get the FieldInfo for the enum value
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            if (fieldInfo == null)
            {
                // If fieldInfo is null, return the default string representation
                return enumValue.ToString();
            }

            // Get the attribute from the fieldInfo
            if (Attribute.GetCustomAttribute(fieldInfo, attributeType) is not Attribute attribute)
            {
                // If the attribute is null, return the default string representation
                return enumValue.ToString();
            }

            // Get the "Value" property from the attribute
            var valueProperty = attribute.GetType().GetProperty("Value");
            if (valueProperty == null)
            {
                // If the "Value" property does not exist, return the default string representation
                return enumValue.ToString();
            }

            // Get the value of the "Value" property
            var value = valueProperty.GetValue(attribute);
            return value?.ToString() ?? enumValue.ToString();
        }

        /// <summary>
        /// Retrieves an enum value of type <typeparamref name="T"/> based on its associated <see cref="StringValueAttribute"/>.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="stringValue">The string value to match against the <see cref="StringValueAttribute"/>.</param>
        /// <returns>The matching enum value, or null if no match is found.</returns>
        public static T? GetEnumByStringValue<T>(string stringValue) where T : Enum
        {
            // Iterate through all enum values of type T
            foreach (var enumValue in Enum.GetValues(typeof(T)).Cast<T>())
            {
                // Retrieve the attribute value for the current enum value
                var attributeValue = GetAttributeValue(enumValue, typeof(StringValueAttribute));
                if (attributeValue == stringValue)
                {
                    // Return the matching enum value if found
                    return enumValue;
                }
            }
            return default;
        }
    }
}

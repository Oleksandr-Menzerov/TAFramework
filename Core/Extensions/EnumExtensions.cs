using Core.CustomAttributes;

namespace Core.Extensions
{
    /// <summary>
    /// A static class that provides extension methods for working with enum values and their custom attributes.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Retrieves the string value associated with an enum field, using the <see cref="StringValueAttribute"/>.
        /// If no attribute is found, returns the default string representation of the enum value.
        /// </summary>
        /// <param name="enumValue">The enum value for which to retrieve the string value.</param>
        /// <returns>The string value from the <see cref="StringValueAttribute"/> or the default enum value's string representation if no attribute is found.</returns>
        public static string GetStringValue(this Enum enumValue)
        {
            // Get the FieldInfo for the enum value
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            if (fieldInfo is null)
                return enumValue.ToString();  // Safeguard against null FieldInfo

            // Retrieve the StringValueAttribute associated with the enum value
            var attr = (StringValueAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(StringValueAttribute));
            return attr?.Value ?? enumValue.ToString();  // Return the attribute's value or default to enum's string representation
        }

        /// <summary>
        /// Retrieves the parent type associated with an enum field, using the <see cref="ParentTypeAttribute"/>.
        /// If no attribute is found, returns the default string representation of the enum value.
        /// </summary>
        /// <param name="enumValue">The enum value for which to retrieve the parent type.</param>
        /// <returns>The parent type from the <see cref="ParentTypeAttribute"/> or the default enum value's string representation if no attribute is found.</returns>
        public static string GetParentType(this Enum enumValue)
        {
            // Get the FieldInfo for the enum value
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            if (fieldInfo is null)
                return enumValue.ToString();  // Safeguard against null FieldInfo

            // Retrieve the ParentTypeAttribute associated with the enum value
            var attr = (ParentTypeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(ParentTypeAttribute));
            return attr?.Value ?? enumValue.ToString();  // Return the attribute's value or default to enum's string representation
        }
    }
}

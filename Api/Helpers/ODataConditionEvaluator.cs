using System.Globalization;

namespace Api.Helpers
{
    /// <summary>
    /// Provides utility methods for evaluating OData-like conditions on objects of a specified type.
    /// </summary>
    public static class ODataConditionEvaluator
    {
        /// <summary>
        /// Evaluates a condition on a specified item of type <typeparamref name="T"/>. 
        /// The condition should be in the format "<field> <operator> <value>".
        /// </summary>
        /// <typeparam name="T">The type of the object on which the condition is evaluated.</typeparam>
        /// <param name="item">The object to evaluate the condition against.</param>
        /// <param name="condition">The condition string in the format "<field> <operator> <value>".</param>
        /// <returns>True if the condition is met, otherwise false.</returns>
        /// <exception cref="ArgumentException">Thrown when the condition format is invalid or when the specified property or operator is not supported.</exception>
        public static bool EvaluateCondition<T>(T item, string condition)
        {
            // Split the condition into parts
            var parts = condition.Split(' ', 3);

            if (parts.Length < 3)
            {
                throw new ArgumentException("Invalid condition format.");
            }

            var field = parts[0];
            var operatorStr = parts[1];
            var valueStr = parts[2];

            var property = typeof(T).GetProperty(field) ?? throw new ArgumentException($"Property '{field}' does not exist.");
            var propertyValue = property.GetValue(item);

            // Convert the value to the appropriate type
            var value = ConvertValue(valueStr, property.PropertyType);

            // Evaluate the condition
            return operatorStr.ToLower() switch
            {
                "eq" => Equals(propertyValue, value),
                "ne" => !Equals(propertyValue, value),
                "gt" => Comparer<object>.Default.Compare(propertyValue, value) > 0,
                "ge" => Comparer<object>.Default.Compare(propertyValue, value) >= 0,
                "lt" => Comparer<object>.Default.Compare(propertyValue, value) < 0,
                "le" => Comparer<object>.Default.Compare(propertyValue, value) <= 0,
                _ => throw new ArgumentException($"Operator '{operatorStr}' is not supported.")
            };
        }

        /// <summary>
        /// Converts a string representation of a value to the specified target type.
        /// </summary>
        /// <param name="valueStr">The string representation of the value to convert.</param>
        /// <param name="targetType">The target type to convert the value to.</param>
        /// <returns>The converted value as an object.</returns>
        /// <exception cref="ArgumentException">Thrown when the conversion for the specified type is not supported.</exception>
        private static object ConvertValue(string valueStr, Type targetType)
        {
            if (targetType == typeof(string))
            {
                return valueStr;
            }
            if (targetType == typeof(int))
            {
                return int.Parse(valueStr);
            }
            if (targetType == typeof(double))
            {
                return double.Parse(valueStr);
            }
            if (targetType == typeof(decimal))
            {
                return decimal.Parse(valueStr);
            }
            if (targetType == typeof(bool))
            {
                return bool.Parse(valueStr);
            }
            if (targetType == typeof(DateTime))
            {
                return DateTime.Parse(valueStr, CultureInfo.InvariantCulture);
            }
            // Add other type conversions as needed
            throw new ArgumentException($"Conversion for type '{targetType.Name}' is not supported.");
        }
    }
}

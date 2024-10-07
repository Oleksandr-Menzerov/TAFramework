namespace Tests.Utils
{
    /// <summary>
    /// Provides extension methods for working with SpecFlow tables.
    /// </summary>
    public static partial class TableExtensions
    {
        /// <summary>
        /// Extracts properties from the SpecFlow table where headers start with "Prop[" and end with "]".
        /// </summary>
        /// <param name="table">The SpecFlow table to extract properties from.</param>
        /// <returns>A dictionary containing the extracted property names and their corresponding values.</returns>
        public static Dictionary<string, string> ExtractProperties(this Table table)
        {
            var properties = new Dictionary<string, string>();

            // Iterate over table headers to find properties defined in the format "Prop[...]" 
            foreach (var header in table.Header)
            {
                if (header.StartsWith("Prop[") && header.EndsWith(']'))
                {
                    // Extract property names and values
                    var propertyNames = header[5..^1].Split(',');
                    var values = table.Rows[0][header].Split(',');

                    // Map property names to their respective values
                    for (int i = 0; i < propertyNames.Length && i < values.Length; i++)
                    {
                        var propertyName = propertyNames[i].Trim();
                        var value = values[i].Trim();
                        properties[propertyName] = value;
                    }
                }
            }

            return properties;
        }
    }
}

using Core.Helpers;

namespace Tests.Utils
{
    /// <summary>
    /// Provides extension methods for working with SpecFlow tables.
    /// </summary>
    public static partial class TableExtensions
    {
        /// <summary>
        /// Fills fields in the SpecFlow table with random values where headers start with "Random".
        /// </summary>
        /// <param name="table">The SpecFlow table to fill with random values.</param>
        /// <returns>The modified SpecFlow table with random values.</returns>
        public static Table FillRandomFields(this Table table)
        {
            // Iterate over the headers of the table
            foreach (var header in table.Header)
            {
                // Check if the header indicates a random value should be generated
                if (header.StartsWith("Random"))
                {
                    // Fill each row's specified header with a random value
                    foreach (var row in table.Rows)
                    {
                        row[header] = ValueHelper.GenerateRandomValue(row[header]);
                    }
                }
            }

            // Iterate over each row in the table to fill in any remaining "Random" fields
            foreach (var row in table.Rows)
            {
                foreach (var header in table.Header)
                {
                    // Check again if the row's value for the header indicates a random value
                    if (row[header].StartsWith("Random"))
                    {
                        row[header] = ValueHelper.GenerateRandomValue(row[header]);
                    }
                }
            }

            return table; // Return the modified table
        }
    }
}

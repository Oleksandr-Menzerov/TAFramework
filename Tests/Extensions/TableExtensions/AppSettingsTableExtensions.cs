using Core.Helpers;

namespace Tests.Utils
{
    /// <summary>
    /// Provides extension methods for working with SpecFlow tables.
    /// </summary>
    public static partial class TableExtensions
    {
        /// <summary>
        /// Fills in the AppSettings fields in the SpecFlow table with corresponding values from the application settings.
        /// </summary>
        /// <param name="table">The SpecFlow table to be processed.</param>
        /// <returns>The updated SpecFlow table with AppSettings values filled in.</returns>
        public static Table FillAppSettingsFields(this Table table)
        {
            // Iterate over table headers to find AppSettings keys and fill their values in the rows.
            foreach (var header in table.Header)
            {
                if (header.StartsWith("AppSettings"))
                {
                    foreach (var row in table.Rows)
                    {
                        row[header] = ValueHelper.GetAppSettingsValue(row[header]);
                    }
                }
            }

            // Iterate over each row to find and fill AppSettings values in the cell values.
            foreach (var row in table.Rows)
            {
                foreach (var header in table.Header)
                {
                    if (row[header].StartsWith("AppSettings"))
                    {
                        row[header] = ValueHelper.GetAppSettingsValue(row[header]);
                    }
                }
            }

            return table;
        }
    }
}

namespace Api.Helpers
{
    /// <summary>
    /// A helper class for building OData query parameters.
    /// </summary>
    public class ODataQueryHelper
    {
        /// <summary>
        /// A list to store the query parts as name-value pairs.
        /// </summary>
        private readonly List<(string Name, string Value)> _queryParts = [];

        /// <summary>
        /// Adds a $top query option to limit the number of results returned.
        /// </summary>
        /// <param name="value">The maximum number of records to return.</param>
        /// <returns>A list of query parts with the added $top option.</returns>
        public List<(string Name, string Value)> Top(int value)
        {
            _queryParts.Add(("$top", value.ToString()));
            return _queryParts;
        }

        /// <summary>
        /// Adds a $skip query option to skip a specified number of records.
        /// </summary>
        /// <param name="value">The number of records to skip.</param>
        /// <returns>A list of query parts with the added $skip option.</returns>
        public List<(string Name, string Value)> Skip(int value)
        {
            _queryParts.Add(("skip", value.ToString()));
            return _queryParts;
        }

        /// <summary>
        /// Adds an $orderby query option to sort the results by a specified field.
        /// </summary>
        /// <param name="field">The field by which to order the results.</param>
        /// <param name="ascending">Specifies whether the sorting should be in ascending order. Defaults to true.</param>
        /// <returns>A list of query parts with the added $orderby option.</returns>
        public List<(string Name, string Value)> OrderBy(string field, bool ascending = true)
        {
            _queryParts.Add(("orderby", $"{field} {(ascending ? "asc" : "desc")}"));
            return _queryParts;
        }

        /// <summary>
        /// Adds a $filter query option to filter the results based on a specified condition.
        /// </summary>
        /// <param name="filter">The filter condition in OData syntax.</param>
        /// <returns>A list of query parts with the added $filter option.</returns>
        public List<(string Name, string Value)> Filter(string filter)
        {
            _queryParts.Add(("$filter", Uri.EscapeDataString(filter)));
            return _queryParts;
        }
    }
}

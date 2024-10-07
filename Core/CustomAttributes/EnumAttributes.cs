namespace Core.CustomAttributes
{
    /// <summary>
    /// Attribute to assign a string value to an enum field. 
    /// This can be useful for providing additional metadata or descriptions for enum values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class StringValueAttribute(string value) : Attribute
    {
        /// <summary>
        /// Gets the string value assigned to the enum field.
        /// </summary>
        public string Value { get; } = value;
    }

    /// <summary>
    /// Attribute to associate a parent type (as a string) with an enum field. 
    /// This can be useful for grouping enum values by their parent categories.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ParentTypeAttribute(string value) : Attribute
    {
        /// <summary>
        /// Gets the parent type (as a string) assigned to the enum field.
        /// </summary>
        public string Value { get; } = value;
    }
}

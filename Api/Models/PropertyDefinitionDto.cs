namespace Api.Models
{
    public class PropertyDefinitionDto
    {
        public string Name { get; set; } = string.Empty;
        public int PropertyDefinitionType { get; set; }
        public bool IsMandatory { get; set; }
        public string Category { get; set; } = string.Empty;
        public long Id { get; set; }
        public List<PropertyValueDto>? PropertyValues { get; set; }
    }

    public class PropertyValueDto
    {
        public long Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public PropertyDefinitionDto ParentPropertyValue { get; set; } = new();
    }
}

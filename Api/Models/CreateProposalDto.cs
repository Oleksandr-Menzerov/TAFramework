namespace Api.Models
{
    public class CreateProposalDto
    {
        public bool IsActive { get; set; } = true;
        public int PetOrigin { get; set; } = 1;

        public string Title { get; set; } = string.Empty;

        public string PetName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Summary { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public int Age { get; set; } = 1;
        public int AgeUnits { get; set; } = 1;

        public List<CreateProposalPhotoDto>? Photos { get; set; }
        public List<CreateProposalPropertyDto> Properties { get; set; } = [];
    }

    public class CreateProposalPropertyDto
    {
        public PropertyDefinition PropertyDefinition { get; set; } = new PropertyDefinition();
        public PredefinedValue PredefinedValue { get; set; } = new PredefinedValue();
    }

    public class PropertyDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class PredefinedValue
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public ParentPropertyValue? ParentPropertyValue { get; set; }
    }

    public class ParentPropertyValue
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
    }

    public class CreateProposalPhotoDto
    {
        public int Id { get; set; } = 0;
        public string? Url { get; set; }
        public string? Image { get; set; }
    }
}

using static Core.Enums;

namespace Api.Models
{
    public class ProposalDto
    {
        public bool IsActive { get; set; }
        public int PetOrigin { get; set; }
        public string Title { get; set; } = string.Empty;
        public string PetName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Summary { get; set; }
        public string Location { get; set; } = string.Empty;
        public long Id { get; set; }
        public int? Age { get; set; }
        public AgeUnits? AgeUnits { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public List<ProposalPhotoDto>? Photos { get; set; }
        public List<PropertyDto>? Properties { get; set; }
        public AppUserDto AppUser { get; set; } = new();
    }

    public class ProposalPhotoDto
    {
        public long Id { get; set; }
        public string? Url { get; set; }
    }

    public class PropertyDto
    {
        public long Id { get; set; }
        public string? CustomValue { get; set; }
        public PropertyDefinitionDto PropertyDefinition { get; set; } = new();
        public PropertyValueDto PredefinedValue { get; set; } = new();
    }

    public class AppUserDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string Address { get; set; } = string.Empty;
    }

    public class ProposalsDto
    {
        public List<ProposalDto> Items { get; set; } = [];
        public int TotalCount { get; set; }
    }
}

using Core;
using Ui.Elements;

namespace Ui.Models
{
    public class PetProposal
    {
        public UiElement? Element { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public int Age { get; set; }
        public Enums.AgeUnits AgeUnits { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }

    public class DetailedPetProposal : PetProposal
    {
        public string ContactName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PetType { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string OwnerType { get; set; } = string.Empty;
        public List<string> HealthInfo { get; set; } = [];
        public List<string> Documents { get; set; } = [];
        public string AdditionalInfo { get; set; } = string.Empty;
    }
}

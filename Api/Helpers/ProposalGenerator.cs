using Api.Models;
using Bogus;
using Core.CustomAttributes;
using Core;
using Core.Extensions;

namespace Api.Helpers
{
    /// <summary>
    /// A helper class to generate random proposals for testing or seeding data.
    /// </summary>
    public static class ProposalGenerator
    {
        /// <summary>
        /// Creates a Faker instance to generate random proposals based on the <see cref="CreateProposalDto"/>.
        /// </summary>
        /// <returns>A Faker that generates <see cref="CreateProposalDto"/> objects.</returns>
        public static Faker<CreateProposalDto> CreateRandomProposal()
        {
            var proposalFaker = new Faker<CreateProposalDto>()
                .RuleFor(p => p.Title, f => f.Lorem.Sentence())    // Generates a random sentence for the Title
                .RuleFor(p => p.PetName, f => f.Name.FirstName())   // Generates a random first name for the Pet's name
                .RuleFor(p => p.Price, f => f.Finance.Amount(0, 1000)) // Generates a random price between 0 and 1000
                .RuleFor(p => p.Summary, f => f.Lorem.Paragraph())  // Generates a random paragraph for the Summary
                .RuleFor(p => p.Location, f => f.Address.City())    // Generates a random city name for the Location
                .RuleFor(p => p.Properties, f => GenerateRandomProperties()) // Generates a list of random properties
                .RuleFor(p => p.AgeUnits, f => (int)Enums.GetRandomValue<Enums.AgeUnits>()) // Randomly selects age units (day, week, etc.)
                .RuleFor(p => p.Age, (f, p) => GenerateRandomAgeInDays((Enums.AgeUnits)p.AgeUnits)); // Calculates a random age in days based on age unit

            return proposalFaker;
        }

        /// <summary>
        /// Generates a random age in days based on the provided <see cref="Enums.AgeUnits"/>.
        /// </summary>
        /// <param name="ageUnit">The unit of time (Day, Week, Month, Year) to base the calculation on.</param>
        /// <returns>The calculated age in days.</returns>
        private static int GenerateRandomAgeInDays(Enums.AgeUnits ageUnit)
        {
            return ageUnit switch
            {
                Enums.AgeUnits.Day => new Faker().Random.Int(1, 30),     // Randomly selects between 1 to 30 days
                Enums.AgeUnits.Week => new Faker().Random.Int(1, 12) * (int)Enums.AgeUnits.Week, // Randomly selects between 1 to 12 weeks and converts to days
                Enums.AgeUnits.Month => new Faker().Random.Int(1, 12) * (int)Enums.AgeUnits.Month, // Randomly selects between 1 to 12 months and converts to days
                Enums.AgeUnits.Year => new Faker().Random.Int(1, 10) * (int)Enums.AgeUnits.Year,   // Randomly selects between 1 to 10 years and converts to days
                _ => 365   // Defaults to 365 days if no valid unit is provided
            };
        }

        /// <summary>
        /// Generates a list of random properties for a proposal, such as species, breed, and gender.
        /// </summary>
        /// <returns>A list of <see cref="CreateProposalPropertyDto"/> objects.</returns>
        private static List<CreateProposalPropertyDto> GenerateRandomProperties()
        {
            var animalSubType = Enums.GetRandomValue<Enums.AnimalSubTypes>(); // Randomly selects an animal subtype
            var animalSubTypeName = Enums.GetAttributeValue(animalSubType, typeof(StringValueAttribute)); // Retrieves the name of the animal subtype
            var parentType = Enums.GetAttributeValue(animalSubType, typeof(ParentTypeAttribute)); // Retrieves the parent type of the animal
            var parentId = (int)Enums.GetEnumByStringValue<Enums.AnimalTypes>(parentType); // Retrieves the ID of the parent type
            var gender = Enums.GetRandomValue<Enums.Genders>(); // Randomly selects a gender
            var genderValue = Enums.GetAttributeValue(gender, typeof(StringValueAttribute)); // Retrieves the gender's string value

            // Definitions for the properties (specie, breed, gender)
            (string Name, int Id) specieProperty = (Enums.PropertyDefinitions.Specie.GetStringValue(), (int)Enums.PropertyDefinitions.Specie);
            (string Name, int Id) breedProperty = (Enums.PropertyDefinitions.Breed.GetStringValue(), (int)Enums.PropertyDefinitions.Breed);
            (string Name, int Id) genderProperty = (Enums.PropertyDefinitions.Gender.GetStringValue(), (int)Enums.PropertyDefinitions.Gender);

            return
            [
                // Generates a property for species
                new Faker<CreateProposalPropertyDto>()
                    .RuleFor(p => p.PropertyDefinition, f => new PropertyDefinition { Name = specieProperty.Name, Id = specieProperty.Id })
                    .RuleFor(p => p.PredefinedValue, f => new PredefinedValue { Value = parentType, Id = parentId })
                    .Generate(),

                // Generates a property for breed
                new Faker<CreateProposalPropertyDto>()
                    .RuleFor(p => p.PropertyDefinition, f => new PropertyDefinition { Name = breedProperty.Name, Id = breedProperty.Id })
                    .RuleFor(p => p.PredefinedValue, f => new PredefinedValue { Value = animalSubTypeName, Id = (int)animalSubType })
                    .Generate(),

                // Generates a property for gender
                new Faker<CreateProposalPropertyDto>()
                    .RuleFor(p => p.PropertyDefinition, f => new PropertyDefinition { Name = genderProperty.Name, Id = genderProperty.Id })
                    .RuleFor(p => p.PredefinedValue, f => new PredefinedValue { Value = genderValue, Id = (int)gender })
                    .Generate()
            ];
        }
    }
}

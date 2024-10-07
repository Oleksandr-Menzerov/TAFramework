using Api.Models;

namespace Api.Helpers
{
    /// <summary>
    /// A helper class for building proposal properties from a dictionary and a list of property definitions.
    /// </summary>
    public static class ProposalPropertiesHelper
    {
        /// <summary>
        /// Builds a list of proposal properties based on a dictionary of property names and values.
        /// For each property, it looks up the corresponding property definition and predefined value in the list of provided property definitions.
        /// </summary>
        /// <param name="propertiesDict">A dictionary where keys are property names and values are their respective values.</param>
        /// <param name="propertyDefinitions">A list of property definitions containing details about possible properties and their values.</param>
        /// <returns>A list of <see cref="CreateProposalPropertyDto"/> objects.</returns>
        public static List<CreateProposalPropertyDto> BuildProposalProperties(Dictionary<string, string> propertiesDict, List<PropertyDefinitionDto> propertyDefinitions)
        {
            var proposalProperties = new List<CreateProposalPropertyDto>();

            // Iterate through each property in the dictionary
            foreach (var property in propertiesDict)
            {
                // Find the corresponding property definition based on the property name
                var propertyDefinition = propertyDefinitions.Find(pd => pd.Name == property.Key);

                if (propertyDefinition != null)
                {
                    // Find the corresponding predefined value for the property value
                    var predefinedValue = propertyDefinition.PropertyValues?.Find(pv => pv.Value == property.Value);

                    // If a valid predefined value is found, create a proposal property
                    if (predefinedValue != null)
                    {
                        proposalProperties.Add(CreateProposalProperty(propertyDefinition, predefinedValue));
                    }
                }
            }

            return proposalProperties;
        }

        /// <summary>
        /// Creates a proposal property DTO from a property definition and a predefined value.
        /// </summary>
        /// <param name="propertyDefinition">The definition of the property being created.</param>
        /// <param name="propertyValue">The predefined value of the property.</param>
        /// <returns>A <see cref="CreateProposalPropertyDto"/> object containing the property definition and value.</returns>
        private static CreateProposalPropertyDto CreateProposalProperty(PropertyDefinitionDto propertyDefinition, PropertyValueDto propertyValue)
        {
            // Map the PropertyValueDto to PredefinedValue
            var predefinedValue = MapToPredefinedValue(propertyValue);

            return new CreateProposalPropertyDto
            {
                PropertyDefinition = new PropertyDefinition
                {
                    Id = (int)propertyDefinition.Id,  // Convert the property definition ID to integer
                    Name = propertyDefinition.Name    // Use the name from the property definition
                },
                PredefinedValue = predefinedValue    // Assign the predefined value
            };
        }

        /// <summary>
        /// Maps a <see cref="PropertyValueDto"/> object to a <see cref="PredefinedValue"/>.
        /// </summary>
        /// <param name="propertyValue">The property value DTO to be mapped.</param>
        /// <returns>A <see cref="PredefinedValue"/> object with the mapped ID and value.</returns>
        private static PredefinedValue MapToPredefinedValue(PropertyValueDto propertyValue)
        {
            // Implement the mapping logic between PropertyValueDto and PredefinedValue
            return new PredefinedValue
            {
                Id = (int)propertyValue.Id,  // Convert the property value ID to integer
                Value = propertyValue.Value  // Use the value from the property value DTO
            };
        }
    }
}

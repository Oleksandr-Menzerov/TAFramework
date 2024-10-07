using Api.Clients;
using Api.Models;
using FluentAssertions.Execution;

namespace Tests.Steps.Api
{
    [Binding]
    [Parallelizable(ParallelScope.All)]
    public class PropertyDefinitionsSteps(ScenarioContext scenarioContext) : BaseApiSteps(scenarioContext)
    {
        private readonly PropertyDefinitionsApiClient _propertyDefinitionsApiClient = new();

        [When(@"I request all property definitions")]
        public async Task WhenIRequestAllPropertyDefinitions()
        {
            var propertyDefinitions = await _propertyDefinitionsApiClient.GetPropertyDefinitions();
            _scenarioContext["PropertyDefinitions"] = propertyDefinitions;
        }

        [Then(@"I should receive a list of property definitions")]
        public void ThenIShouldReceiveAListOfPropertyDefinitions()
        {
            var propertyDefinitions = _scenarioContext.Get<List<PropertyDefinitionDto>>("PropertyDefinitions");
            propertyDefinitions.Should().NotBeEmpty();
        }

        [Then(@"each property definition should have required fields")]
        public void ThenEachPropertyDefinitionShouldHaveRequiredFields()
        {
            var propertyDefinitions = _scenarioContext.Get<List<PropertyDefinitionDto>>("PropertyDefinitions");
            foreach (var propertyDefinition in propertyDefinitions)
            {
                propertyDefinition.Name.Should().NotBeNullOrEmpty();
                propertyDefinition.Id.Should().BeGreaterThan(0);
            }
        }

        [Given(@"I have a list of property definitions")]
        public async Task GivenIHaveAListOfPropertyDefinitions()
        {
            await WhenIRequestAllPropertyDefinitions();
        }

        [When(@"I request the property definition with id (.*)")]
        public async Task WhenIRequestThePropertyDefinitionWithId(int id)
        {
            var propertyDefinition = await _propertyDefinitionsApiClient.GetPropertyDefinition(id);
            _scenarioContext["SpecificPropertyDefinition"] = propertyDefinition;
        }

        [Then(@"I should receive the correct property definition")]
        public void ThenIShouldReceiveTheCorrectPropertyDefinition()
        {
            var propertyDefinition = _scenarioContext.Get<PropertyDefinitionDto>("SpecificPropertyDefinition");
            propertyDefinition.Should().NotBeNull();
            propertyDefinition.Id.Should().Be(1);
        }

        [Then(@"each property definition should have the following structure")]
        public void ThenEachPropertyDefinitionShouldHaveTheFollowingStructure(Table table)
        {
            var propertyDefinitions = _scenarioContext.Get<List<PropertyDefinitionDto>>("PropertyDefinitions");

            using (new AssertionScope())
            {
                foreach (var propertyDefinition in propertyDefinitions)
                {
                    propertyDefinition.Should().BeAssignableTo<PropertyDefinitionDto>();

                    foreach (var row in table.Rows)
                    {
                        var property = typeof(PropertyDefinitionDto).GetProperty(row["Field"]);
                        property.Should().NotBeNull();
                        var propertyType = property?.PropertyType;
                        propertyType?.Name.ToLower().Should().Contain(row["Type"].ToLower());
                    }
                }
            }
        }

        [When(@"I filter property definitions by type (.*)")]
        public void WhenIFilterPropertyDefinitionsByType(int type)
        {
            var propertyDefinitions = _scenarioContext.Get<List<PropertyDefinitionDto>>("PropertyDefinitions");
            var filteredDefinitions = propertyDefinitions.Where(pd => pd.PropertyDefinitionType == type).ToList();
            _scenarioContext["FilteredPropertyDefinitions"] = filteredDefinitions;
        }

        [Then(@"all filtered property definitions should have type (.*)")]
        public void ThenAllFilteredPropertyDefinitionsShouldHaveType(int type)
        {
            var filteredDefinitions = _scenarioContext.Get<List<PropertyDefinitionDto>>("FilteredPropertyDefinitions");
            filteredDefinitions.Should().AllSatisfy(pd => pd.PropertyDefinitionType.Should().Be(type));
        }

        [When(@"I filter mandatory property definitions")]
        public void WhenIFilterMandatoryPropertyDefinitions()
        {
            var propertyDefinitions = _scenarioContext.Get<List<PropertyDefinitionDto>>("PropertyDefinitions");
            var mandatoryDefinitions = propertyDefinitions.Where(pd => pd.IsMandatory).ToList();
            _scenarioContext["MandatoryPropertyDefinitions"] = mandatoryDefinitions;
        }

        [Then(@"all filtered property definitions should be mandatory")]
        public void ThenAllFilteredPropertyDefinitionsShouldBeMandatory()
        {
            var mandatoryDefinitions = _scenarioContext.Get<List<PropertyDefinitionDto>>("MandatoryPropertyDefinitions");
            mandatoryDefinitions.Should().AllSatisfy(pd => pd.IsMandatory.Should().BeTrue());
        }
    }
}

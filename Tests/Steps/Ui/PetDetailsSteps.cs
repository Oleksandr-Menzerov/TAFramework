using Api.Models;
using FluentAssertions.Execution;
using Ui.Models;
using Ui.Pages;

namespace Tests.Steps.Ui
{
    [Binding]
    [Parallelizable(ParallelScope.All)]
    public class PetDetailsSteps(ScenarioContext scenarioContext) : BaseUiSteps(scenarioContext)
    {
        private readonly PetDetailsPage _petDetailsPage = new();

        [When(@"I see the pet details")]
        [Then(@"I see the pet details")]
        public void ThenISeePetDetails()
        {
            _petDetailsPage.WaitForLoaderToDisappear();
            _petDetailsPage.GetPetDetailsContainer().WaitForVisible();

            var details = _petDetailsPage.GetPetDetails();
            _scenarioContext["Details"] = details;
        }

        [When(@"I open the pet details page")]
        public void WhenIOpenThePetDetailsPage()
        {
            var proposal = _scenarioContext.Get<ProposalDto>("CreatedProposal");
            var proposalId = proposal.Id;
            proposalId.Should().BeGreaterThan(0);
            _basePage.NavigateToUrl($"our-pets/{proposalId}");
        }

        [Then(@"the created proposal details should match the pet details on the UI")]
        public void ThenTheCreatedProposalDetailsShouldMatchThePetDetailsOnTheUI()
        {
            var createdProposal = _scenarioContext.Get<ProposalDto>("CreatedProposal");

            var petDetails = _scenarioContext.Get<DetailedPetProposal>("Details");

            using (new AssertionScope())
            {
                petDetails.Title.Should().Be(createdProposal.Title);
                petDetails.PetType.Should().Be(GetPropertyValue(createdProposal, "Вид тварини"));
                petDetails.Breed.Should().Be(GetPropertyValue(createdProposal, "Різновид"));
                petDetails.Sex.Should().Be(GetPropertyValue(createdProposal, "Стать"));
                petDetails.Price.Should().Be(createdProposal.Price);
                petDetails.Location.Should().Be(createdProposal.Location);
                petDetails.Age.Should().Be(createdProposal.Age);
                petDetails.AgeUnits.Should().Be(createdProposal.AgeUnits);
            }
        }

        private static string GetPropertyValue(ProposalDto proposal, string propertyName)
        {
            var property = proposal.Properties.Find(p => p.PropertyDefinition.Name == propertyName);
            return property?.PredefinedValue.Value ?? string.Empty;
        }
    }
}

using Ui.Models;
using Ui.Pages;

namespace Tests.Steps.Ui
{
    [Binding]
    [Parallelizable(ParallelScope.All)]
    public class PetProposalsSteps(ScenarioContext scenarioContext) : BaseUiSteps(scenarioContext)
    {
        private readonly PetProposalsPage _petProposalsPage = new();

        [When(@"I see the proposals")]
        [Then(@"I see the proposals")]
        public void ThenISeeProposals()
        {
            var proposals = _petProposalsPage.WaitAndGetPetProposals();
            Assert.That(!proposals.Count.Equals(0), "No pet proposals found");
            _scenarioContext["Proposals"] = proposals;
        }

        [When(@"I click on the (.*) pet card")]
        public void WhenIClickOnThePetCard(int index)
        {
            var proposals = _scenarioContext.Get<List<PetProposal>>("Proposals");

            if (index < 1 || index > proposals.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }

            var proposal = proposals[index - 1];
            proposal.Element?.Click();
        }

        [Then(@"proposals should be sorted by '(.*)' (descending|ascending)")]
        public void ThenProposalsShouldBeSortedBy(string sortingField, string direction)
        {
            var proposals = _petProposalsPage.WaitAndGetPetProposals();

            // Get the property dynamically using reflection
            var propertyInfo = typeof(PetProposal).GetProperty(sortingField) ?? throw new ArgumentException($"Invalid sorting field: {sortingField}");

            // Define the sorting expression dynamically using reflection
            object? sortExpression(PetProposal proposal) => propertyInfo.GetValue(proposal);

            // Sort using LINQ based on direction
            var sortedProposals = direction == "ascending"
                ? [.. proposals.OrderBy(sortExpression)]
                : proposals.OrderByDescending(sortExpression).ToList();

            // Compare original list with sorted list using FluentAssertions
            proposals.Should().Equal(sortedProposals);
        }

        [When(@"I sort proposals '(.*)'")]
        public void WhenISortProposals(string sortingField)
        {
            var dropdown = _petProposalsPage.GetDropdown("sort");
            dropdown.ExpandAndSelect(sortingField);
        }
    }
}

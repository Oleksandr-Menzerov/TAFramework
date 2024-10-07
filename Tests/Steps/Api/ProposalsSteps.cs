using Api.Clients;
using Api.Helpers;
using Api.Models;
using TechTalk.SpecFlow.Assist;
using Tests.Extensions;
using Tests.Utils;

namespace Tests.Steps.Api
{
    [Binding]
    [Parallelizable(ParallelScope.All)]
    public class ProposalsSteps(ScenarioContext scenarioContext) : BaseApiSteps(scenarioContext)
    {
        private readonly ProposalsApiClient _proposalsApiClient = new();
        private readonly PropertyDefinitionsApiClient _propertyDefinitionsApiClient = new();

        [Given(@"I have retrieved all existing proposals")]
        public async Task GivenIHaveRetrievedAllExistingProposals()
        {
            var response = await _proposalsApiClient.GetProposals();
            _scenarioContext["AllProposals"] = response?.Items;
        }

        [When(@"I request proposals with top (\d+)")]
        public async Task WhenIRequestProposalsWithTop(int top)
        {
            var queryBuilder = new ODataQueryHelper().Top(top);
            var response = await _proposalsApiClient.GetProposals(queryBuilder);
            _scenarioContext["FilteredProposals"] = response?.Items;
        }

        [Then(@"I should see (\d+) proposals in the result")]
        public void ThenIShouldSeeProposals(int expectedCount)
        {
            var filteredProposals = _scenarioContext.Get<List<ProposalDto>>("FilteredProposals");
            filteredProposals.Should().NotBeNullOrEmpty();
            filteredProposals.Count.Should().Be(expectedCount);
        }

        [When(@"I request proposals with skip (\d+)")]
        public async Task WhenIRequestProposalsWithSkip(int skip)
        {
            var queryBuilder = new ODataQueryHelper().Skip(skip);
            var response = await _proposalsApiClient.GetProposals(queryBuilder);
            _scenarioContext["FilteredProposals"] = response?.Items;
        }

        [Then(@"the first proposal should match the (\d+)(st|nd|rd|th) proposal from all proposals")]
        [Then(@"the first proposal should match the (\d+) proposal from all proposals")]
        public void ThenTheFirstProposalShouldMatchTheProposalFromAllProposals(int index)
        {
            var allProposals = _scenarioContext.Get<List<ProposalDto>>("AllProposals");
            var filteredProposals = _scenarioContext.Get<List<ProposalDto>>("FilteredProposals");
            filteredProposals[0].Id.Should().Be(allProposals[index - 1].Id);
        }

        [When(@"I request proposals ordered by (.*) (ascending|descending)")]
        public async Task WhenIRequestProposalsOrderedBy(string field, string direction)
        {
            var queryBuilder = new ODataQueryHelper().OrderBy(field, direction.Equals("ascending", StringComparison.OrdinalIgnoreCase));
            var response = await _proposalsApiClient.GetProposals(queryBuilder);
            _scenarioContext["FilteredProposals"] = response?.Items;
        }

        [Then(@"the proposals should be sorted by (.*) in (ascending|descending) order")]
        public void ThenTheProposalsShouldBeSortedByInOrder(string field, string direction)
        {
            var filteredProposals = _scenarioContext.Get<List<ProposalDto>>("FilteredProposals");
            filteredProposals.Should().NotBeNullOrEmpty();
            filteredProposals.Should().BeOrderedBy(field, direction);
        }


        [When(@"I filter proposals with ""(.*)""")]
        public async Task WhenIFilterProposalsWith(string filter)
        {
            var queryBuilder = new ODataQueryHelper().Filter(filter);
            var response = await _proposalsApiClient.GetProposals(queryBuilder);
            _scenarioContext["FilteredProposals"] = response?.Items;
        }

        [Then(@"all returned proposals should satisfy the condition ""(.*)""")]
        public void ThenAllReturnedProposalsShouldSatisfyTheCondition(string condition)
        {
            var filteredProposals = _scenarioContext.Get<List<ProposalDto>>("FilteredProposals");
            filteredProposals.Should().NotBeNullOrEmpty();
            filteredProposals.Should().AllSatisfy(p => ODataConditionEvaluator.EvaluateCondition(p, condition));
        }

        [Given(@"I have the following proposal details")]
        public void GivenIHaveTheFollowingProposalDetails(Table table)
        {
            var proposalDetails = table.CreateInstance<CreateProposalDto>();
            _scenarioContext["ProposalDetails"] = proposalDetails;
        }

        [Then(@"the proposal should be created successfully")]
        public void ThenTheProposalShouldBeCreatedSuccessfully()
        {
            var createdProposal = _scenarioContext.Get<ProposalDto>("CreatedProposal");
            createdProposal.Id.Should().BeGreaterThan(0);
        }

        [Then(@"the proposal details should match the input")]
        public void ThenTheProposalDetailsShouldMatchTheInput()
        {
            var inputDetails = _scenarioContext.Get<CreateProposalDto>("ProposalDetails");
            var createdProposal = _scenarioContext.Get<ProposalDto>("CreatedProposal");

            createdProposal.Title.Should().Be(inputDetails.Title);
            createdProposal.PetName.Should().Be(inputDetails.PetName);
            createdProposal.Price.Should().Be(inputDetails.Price);
            createdProposal.Location.Should().Be(inputDetails.Location);
        }

        [When(@"I filter proposals with price greater than (.*)")]
        public async Task WhenIFilterProposalsWithPriceGreaterThan(double price)
        {
            await WhenIFilterProposalsWith($"Price gt {price}");
        }

        [When(@"I have created a proposal with the following details")]
        [Given(@"I have created a proposal with the following details")]
        public async Task GivenIHaveCreatedAProposalWithTheFollowingDetails(Table table)
        {
            var propertyDefinitions = await _propertyDefinitionsApiClient.GetPropertyDefinitions();
            var propertiesDict = table.ExtractProperties();
            var proposalProperties = ProposalPropertiesHelper.BuildProposalProperties(propertiesDict, propertyDefinitions);

            var proposalDetails = table.CreateInstance<CreateProposalDto>();
            proposalDetails.Properties = proposalProperties;

            _scenarioContext["ProposalDetails"] = proposalDetails;
            await CreateProposal(proposalDetails);
        }

        public async Task CreateProposal(CreateProposalDto proposalDetails)
        {
            var createdProposal = await _proposalsApiClient.CreateProposal(proposalDetails);
            _testDataCleaner.AddCleanupAction(async () =>
            {
                await _proposalsApiClient.DeleteProposal(createdProposal.Id);
            }, $"prop{createdProposal.Id}");
            _scenarioContext["CreatedProposal"] = createdProposal;
        }

        [Given(@"I have created a proposal with random values")]
        public async Task GivenIHaveCreatedAProposalWithRandomValues(Table table)
        {
            table = table.FillRandomFields();
            
            await GivenIHaveCreatedAProposalWithTheFollowingDetails(table);
        }

        [Given(@"I have created a proposal with random values via API")]
        public async Task GivenIHaveCreatedAProposalWithRandomValuesViaApi()
        {
            var proposal = ProposalGenerator.CreateRandomProposal().Generate();
            await CreateProposal(proposal);
        }

        [When(@"I update the proposal with the following details")]
        public async Task WhenIUpdateTheProposalWithTheFollowingDetails(Table table)
        {
            var propertyDefinitions = await _propertyDefinitionsApiClient.GetPropertyDefinitions();
            var propertiesDict = table.ExtractProperties();
            var proposalProperties = ProposalPropertiesHelper.BuildProposalProperties(propertiesDict, propertyDefinitions);

            var updateDetails = table.CreateInstance<CreateProposalDto>();
            updateDetails.Properties = proposalProperties;

            _scenarioContext["UpdateDetails"] = updateDetails;
            var createdProposal = _scenarioContext.Get<ProposalDto>("CreatedProposal");
            await _proposalsApiClient.UpdateProposal(createdProposal.Id, updateDetails);
            var updatedProposal = await _proposalsApiClient.GetProposal(createdProposal.Id);
            _scenarioContext["UpdatedProposal"] = updatedProposal;
        }

        [Then(@"the proposal should be updated successfully")]
        public void ThenTheProposalShouldBeUpdatedSuccessfully()
        {
            var updatedProposal = _scenarioContext.Get<ProposalDto>("UpdatedProposal");
            updatedProposal.Should().NotBeNull();
        }

        [Then(@"the proposal details should match the updated input")]
        public void ThenTheProposalDetailsShouldMatchTheUpdatedInput()
        {
            var updateDetails = _scenarioContext.Get<CreateProposalDto>("UpdateDetails");
            var updatedProposal = _scenarioContext.Get<ProposalDto>("UpdatedProposal");

            updatedProposal.Title.Should().Be(updateDetails.Title);
            updatedProposal.PetName.Should().Be(updateDetails.PetName);
            updatedProposal.Price.Should().Be(updateDetails.Price);
            updatedProposal.Location.Should().Be(updateDetails.Location);
        }

        [When(@"I delete the proposal")]
        public async Task WhenIDeleteTheProposal()
        {
            var createdProposal = _scenarioContext.Get<ProposalDto>("CreatedProposal");
            await _proposalsApiClient.DeleteProposal(createdProposal.Id);
            _testDataCleaner.RemoveCleanupAction($"prop{createdProposal.Id}");
        }

        [Then(@"the proposal should be deleted successfully")]
        public async Task ThenTheProposalShouldBeDeletedSuccessfully()
        {
            var createdProposal = _scenarioContext.Get<ProposalDto>("CreatedProposal");
            await _proposalsApiClient.Invoking(client => client.GetProposal(createdProposal.Id))
                .Should().ThrowAsync<Exception>()
                .WithMessage("*Error retrieving proposal*");
        }
    }
}

using Core.Configuration;
using Ui.Pages;

namespace Tests.Steps.Ui
{
    [Binding]
    [Parallelizable(ParallelScope.All)]
    public class NavigationSteps(ScenarioContext scenarioContext) : BaseUiSteps(scenarioContext)
    {
        [Given(@"I am on the (.*) page")]
        public void GivenTheUserIsOnThePage(string pageName)
        {
            _basePage.NavigateToUrl(GetUrlByPageName(pageName));
        }

        private static string GetUrlByPageName(string pageName)
        {
            return pageName.ToLowerInvariant() switch
            {
                "main" => ConfigurationManager.AppSettings.UiBaseUrl,
                "pets" => ConfigurationManager.AppSettings.UiBaseUrl + "our-pets",
                _ => pageName
            };
        }
    }
}

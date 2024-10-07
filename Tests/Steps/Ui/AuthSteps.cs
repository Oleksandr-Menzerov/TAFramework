using Core.Configuration;
using Ui.Pages;

namespace Tests.Steps.Ui
{
    [Binding]
    [Parallelizable(ParallelScope.All)]
    public class AuthSteps(ScenarioContext scenarioContext) : BaseUiSteps(scenarioContext)
    {
        private readonly AuthPage _authPage = new();

        [Given(@"I am logged in")]
        public void GivenTheUserIsLoggedIn()
        {
            new NavigationSteps(_scenarioContext).GivenTheUserIsOnThePage("main");

            _authPage.GetLink("Акаунт").Click();
            _authPage.GetInput("Електронна пошта").EnterText(ConfigurationManager.AppSettings.Login);
            _authPage.GetInput("Пароль").EnterText(ConfigurationManager.AppSettings.Password);
            _authPage.GetButton("Увійти").Click();
            _authPage.WaitForLoaderToDisappear();
        }
    }
}

using Core.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Ui.Elements;
using UI.Driver;

namespace Ui.Pages
{
    public class BasePage
    {
        protected IWebDriver Driver = UI.Driver.Driver.GetCurrentDriver();
        private WebDriverWait Wait => new(Driver, TimeSpan.FromSeconds(5));

        public string GetCurrentUrl() => Driver.Url;
        public void Refresh() => Driver.Navigate().Refresh();
        public void NavigateToUrl(string? url = null) => Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings.UiBaseUrl + url);
        public void SwitchToLastTab() => Driver.SwitchTo().Window(Driver.WindowHandles[^1]);
        public void SwitchToFirstTab() => Driver.SwitchTo().Window(Driver.WindowHandles[1]);

        public void SwitchToNextTab()
        {
            var currentHandleIndex = Driver.WindowHandles.IndexOf(Driver.CurrentWindowHandle);
            var nextHandleIndex = (currentHandleIndex + 1) % Driver.WindowHandles.Count;
            Driver.SwitchTo().Window(Driver.WindowHandles[nextHandleIndex]);
        }

        public void SwitchToPreviousTab()
        {
            var currentHandleIndex = Driver.WindowHandles.IndexOf(Driver.CurrentWindowHandle);
            var previousHandleIndex = (currentHandleIndex - 1 + Driver.WindowHandles.Count) % Driver.WindowHandles.Count;
            Driver.SwitchTo().Window(Driver.WindowHandles[previousHandleIndex]);
        }

        public void SwitchToTab(string title)
        {
            foreach (var handle in Driver.WindowHandles)
            {
                Driver.SwitchTo().Window(handle);
                if (Driver.Title.Contains(title))
                {
                    return;
                }
            }
            throw new NoSuchWindowException($"No tab with title {title} found");
        }

        public void SwitchToTab(int index)
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[index]);
        }

        public void WaitForLoaderToDisappear()
        {
            By spinnerLocator = By.XPath("//pet-world-loader");
            try
            {
                Wait.Until(driver =>
                {
                    var spinner = driver.FindElement(spinnerLocator);
                    return spinner.Size.Width == 0 && spinner.Size.Height == 0;
                });
            }
            catch (NoSuchElementException)
            {
                // Loader can not be found
            }
        }

        public object ExecuteScript(string script) => Driver.Scripts().ExecuteScript(script);

        public Button GetButton(string buttonName) => new(buttonName);
        public Input GetInput(string inputName) => new(inputName);
        public Checkbox GetCheckbox(string checkboxName) => new(checkboxName);
        public Link GetLink(string linkName) => new(linkName);
        public Label GetLabel(string labelName) => new(labelName);
        public Dropdown GetDropdown(string dropdownClass) => new(dropdownClass);
        public NamedDropdown GetNamedDropdown(string dropdownName) => new(dropdownName);
        public RadioButton GetRadioButton(string radioButtonName) => new(radioButtonName);
        public Link GetChips(string chipsName) => new(chipsName);
        public Header GetHeader(string headerText) => new(headerText);
        public Modal GetModal() => new();

        public Link ProfileLink() => new(By.XPath(".//a[@class='link-profile']"));
    }
}

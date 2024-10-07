using OpenQA.Selenium;

namespace Ui.Elements
{
    public class Dropdown : UiElement
    {
        public Dropdown(string dropdownClass) : base(By.XPath($"//pet-world-select[contains(@class, '{dropdownClass}')]"), "Dropdown") { }
        public Dropdown(By locator) : base(locator, "Dropdown") { }

        private static By OptionLocator(string optionText) => By.XPath($".//div[contains(@class,'ng-option') and contains(., '{optionText}')]");
        private static By SelectedOptionLocator() => By.XPath(".//div[contains(@class,'ng-option ng-option-marked') and contains(., '')]");
        private static By OptionsLocator() => By.XPath(".//div[contains(@class,'ng-option') and contains(., '')]");
        private static By ClearLocator() => By.XPath(".//span[@title='Clear all']");
        private static By ArrowLocator() => By.XPath(".//span[@class='ng-arrow-wrapper']");
        private static By ExpandedLocator() => By.XPath(".//div[@aria-expanded]");


        public DropdownInput GetInput()
        {
            return new(GetElement().FindElement(By.TagName("input")));
        }

        public void Clear()
        {
            GetElement().FindElement(ClearLocator()).Click();
        }

        public bool IsExpanded()
        {
            var element = GetElement().FindElement(ExpandedLocator());
            return element.GetAttribute("aria-expanded") == "true";
        }

        public void Expand()
        {
            if (!IsExpanded())
            {
                GetElement().FindElement(ArrowLocator()).Click();
            }
        }

        public void Collapse()
        {
            if (IsExpanded())
            {
                GetElement().FindElement(ArrowLocator()).Click();
            }
        }

        public void SelectOption(string optionText)
        {
            Wait.Until(driver => GetElement().FindElement(OptionLocator(optionText))).Click();
        }

        public void ExpandAndSelect(string text)
        {
            Expand();
            SelectOption(text);
            Collapse();
        }

        public List<string> GetOptionList()
        {
            Click();
            var options = Driver.FindElements(OptionsLocator());

            return options.Select(option => option.Text).ToList();
        }

        public string GetSelectedOption()
        {
            var selectedOption = Driver.FindElement(SelectedOptionLocator());
            return selectedOption.Text;
        }
    }
}

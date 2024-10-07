using OpenQA.Selenium;

namespace Ui.Elements
{
    public class NamedDropdown : Dropdown
    {
        public NamedDropdown(string dropdownName) : base(By.XPath($"//pet-world-select[.//span[contains(., '{dropdownName}')]]")) { }
        public NamedDropdown(By locator) : base(locator) { }

        public void SearchAndSelect(string text)
        {
            var input = GetInput();
            input.Click();
            input.EnterText(text);
            SelectOption(text);
        }
    }

    public class DropdownInput(IWebElement element) : UiElement(element, "NamedDropdown Input")
    {
        public void EnterText(string text)
        {
            var inputElement = GetElement();
            inputElement.Clear();
            inputElement.SendKeys(text);
        }
    }
}

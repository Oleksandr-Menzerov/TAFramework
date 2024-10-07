using OpenQA.Selenium;

namespace Ui.Elements
{
    public class Button : UiElement
    {
        public Button(string buttonName) : base(By.XPath($"//button[contains(text(), '{buttonName}')]"), "Button") { }

        public Button(By locator) : base(locator, "Button") { }

        public Button(IWebElement element) : base(element, "Button") { }

    }
}

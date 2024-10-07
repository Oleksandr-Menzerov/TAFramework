using OpenQA.Selenium;

namespace Ui.Elements
{
    public class Label : UiElement
    {
        public Label(By locator) : base(locator, "Label") { }
        public Label(string labelText) : base(By.XPath(
            $".//p[contains(.,'{labelText}')]" +
            $"|.//label[contains(.,'{labelText}')]" +
            $"|.//text[contains(.,'{labelText}')]" +
            $"|.//h1[contains(.,'{labelText}')]" +
            $"|.//h2[contains(.,'{labelText}')]" +
            $"|.//h3[contains(.,'{labelText}')]"
            ), "Label") { }
        public Label(IWebElement element) : base(element, "Label") { }

    }
}

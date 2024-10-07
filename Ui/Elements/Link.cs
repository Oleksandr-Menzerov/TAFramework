using OpenQA.Selenium;

namespace Ui.Elements
{
    public class Link : UiElement
    {
        public Link(string linkName) : base(By.XPath($"//a[contains(text(), '{linkName}')]"), "Link") { }
        public Link(By locator) : base(locator, "Link") { }
    }
}

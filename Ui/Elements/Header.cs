using OpenQA.Selenium;

namespace Ui.Elements
{
    public class Header : UiElement
    {
        public Header(By locator) : base(locator, "Header") { }

        public Header(string headerText) : base(By.XPath($".//h1[contains(.,'{headerText}')]|.//h2[contains(.,'{headerText}')]|.//h3[contains(.,'{headerText}')]"), "Header") { }

        public Header(IWebElement element) : base(element, "Header") { }


        public string GetLevel()
        {
            var element = GetElement();
            return element.TagName;
        }
    }
}

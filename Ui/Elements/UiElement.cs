using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using Ui.Extensions;

namespace Ui.Elements
{
    public class UiElement
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;
        protected By? Locator;
        protected IWebElement? Element;
        protected string ElementType;

        public UiElement(By locator, string elementType)
        {
            Driver = UI.Driver.Driver.GetCurrentDriver();
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            Locator = locator;
            ElementType = elementType;
        }

        public UiElement(IWebElement element, string elementType)
        {
            Driver = UI.Driver.Driver.GetCurrentDriver();
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            Element = element;
            ElementType = elementType;
        }


        public IWebElement? TryGetElement()
        {
            if (Element != null) return Element;

            try
            {
                return Wait.Until(driver =>
                {
                    var element = driver.FindElement(Locator);
                    return element;
                });
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public IWebElement GetElement()
        {
            return TryGetElement() ?? throw new NoSuchElementException($"{ElementType} with {Locator} can not be found.");
        }

        public bool IsDisplayed()
        {
            var element = TryGetElement();
            if (element == null) return false;
            else return element.Displayed;
        }

        public bool IsNotDisplayed()
        {
            try
            {
                Wait.Until(driver =>
                {
                    try
                    {
                        var element = driver.FindElement(Locator);
                        return !element.Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        return true;
                    }
                });

                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public bool IsDisabled()
        {
            return Wait.Until(driver =>
            {
                var element = TryGetElement();
                return element != null && element.GetAttribute("disabled") != null;
            });
        }

        public bool IsEnabled()
        {
            return Wait.Until(driver =>
            {
                var element = TryGetElement();
                return element != null && element.GetAttribute("disabled") == null;
            });
        }

        public virtual void Click()
        {
            GetElement().Click();
        }

        public virtual void ClickOn()
        {
            GetElement().ClickOn();
        }

        public string GetText()
        {
            return GetElement().Text;
        }

        public void WaitForVisible()
        {
            Wait.Until(driver => GetElement().Displayed);
        }

        public void WaitForNotVisible()
        {
            Wait.Until(driver => !GetElement().Displayed);
        }

        public void MoveAndClick(int offsetX = 0, int offsetY = 0)
        {
            GetElement().MoveAndClick(offsetX, offsetY);
        }
    }
}

using OpenQA.Selenium;

namespace UI.Driver.DriverFactories
{
    public interface IDriverFactory
    {
        IWebDriver CreateDriver();
    }
}

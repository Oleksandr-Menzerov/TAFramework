using OpenQA.Selenium;

namespace UI.Driver
{
    /// <summary>
    /// A static class that provides extension methods for the <see cref="IWebDriver"/> interface.
    /// </summary>
    public static class DriverExtension
    {
        /// <summary>
        /// Gets an instance of <see cref="IJavaScriptExecutor"/> from the current <see cref="IWebDriver"/> instance.
        /// </summary>
        /// <param name="driver">The current <see cref="IWebDriver"/> instance.</param>
        /// <returns>An instance of <see cref="IJavaScriptExecutor"/> that allows execution of JavaScript commands.</returns>
        public static IJavaScriptExecutor Scripts(this IWebDriver driver)
        {
            return (IJavaScriptExecutor)driver;
        }
    }
}

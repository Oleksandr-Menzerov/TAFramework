using OpenQA.Selenium;
using UI.Driver.DriverFactories;

namespace UI.Driver
{
    /// <summary>
    /// A static class responsible for managing the WebDriver instance.
    /// </summary>
    public static class Driver
    {
        /// <summary>
        /// The factory used to create instances of WebDriver.
        /// </summary>
        private static readonly IDriverFactory _driverFactory = DriverFactorySelector.GetDriverFactory();

        /// <summary>
        /// A thread-local instance of the WebDriver, allowing for multiple threads to have their own WebDriver instance.
        /// </summary>
        private static readonly ThreadLocal<IWebDriver?> _current = new();

        /// <summary>
        /// Gets the current WebDriver instance for the thread.
        /// </summary>
        /// <returns>The current <see cref="IWebDriver"/> instance.</returns>
        /// <exception cref="WebDriverException">Thrown when the WebDriver instance is not initialized.</exception>
        public static IWebDriver GetCurrentDriver()
        {
            if (_current.Value == null)
            {
                throw new WebDriverException("WebDriver instance is not initialized.");
            }
            return _current.Value;
        }

        /// <summary>
        /// Creates a new WebDriver instance and manages it.
        /// </summary>
        /// <exception cref="WebDriverException">Thrown when there is an issue creating the WebDriver instance.</exception>
        public static void CreateDriver()
        {
            try
            {
                _current.Value = CreateWebDriver();
                ManageDriver();
            }
            catch (Exception ex)
            {
                throw new WebDriverException(ex.Message);
            }
        }

        /// <summary>
        /// Creates a WebDriver instance using the selected driver factory.
        /// </summary>
        /// <returns>An instance of <see cref="IWebDriver"/>.</returns>
        /// <exception cref="NotSupportedException">Thrown when the driver type is unsupported.</exception>
        private static IWebDriver CreateWebDriver()
        {
            return _driverFactory switch
            {
                ChromeDriverFactory chromeDriverFactory => chromeDriverFactory.CreateDriver(),
                // Add browsers if needed. Do not forget to add the _driverFactory for it.
                _ => throw new NotSupportedException("Unsupported driver type"),
            };
        }

        /// <summary>
        /// Manages the WebDriver instance, deleting all cookies.
        /// </summary>
        private static void ManageDriver()
        {
            _current.Value?.Manage().Cookies.DeleteAllCookies();
        }

        /// <summary>
        /// Quits the current WebDriver instance and clears the thread-local value.
        /// </summary>
        public static void Quit()
        {
            if (_current.Value != null)
            {
                _current.Value.Quit();
                _current.Value = null;
            }
        }
    }
}

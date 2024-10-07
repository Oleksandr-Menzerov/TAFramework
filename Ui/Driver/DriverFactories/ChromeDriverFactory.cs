using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Core.Configuration;

namespace UI.Driver.DriverFactories
{
    /// <summary>
    /// A factory class for creating instances of the Chrome WebDriver.
    /// Implements the <see cref="IDriverFactory"/> interface.
    /// </summary>
    public class ChromeDriverFactory : IDriverFactory
    {
        /// <summary>
        /// Creates a new instance of the Chrome WebDriver with specified options.
        /// </summary>
        /// <returns>An instance of <see cref="IWebDriver"/> configured for Chrome.</returns>
        public IWebDriver CreateDriver()
        {
            var chromeOptions = new ChromeOptions();

            // Check if headless mode is enabled and set the appropriate options
            if (ConfigurationManager.SeleniumSettings.Headless)
            {
                chromeOptions.AddArgument("headless");
                chromeOptions.AddArgument($"window-size={ConfigurationManager.SeleniumSettings.Resolution}");
            }

            // Set standard options for the Chrome browser
            chromeOptions.AddArgument("start-maximized");
            chromeOptions.AddArgument("disable-extensions");
            chromeOptions.AddArgument("disable-popup-blocking");
            chromeOptions.AddArgument("disable-infobars");
            chromeOptions.AddUserProfilePreference("download.default_directory", AppDomain.CurrentDomain.BaseDirectory);

            // Use user profile if specified
            if (ConfigurationManager.SeleniumSettings.UseUserProfile)
            {
                string userDataDir = ConfigurationManager.SeleniumSettings.UserProfilePath;

                // Create an absolute path from the relative path
                string absoluteUserDataDir = Path.GetFullPath(userDataDir);

                // Create the directory if it does not exist
                if (!Directory.Exists(absoluteUserDataDir))
                {
                    Directory.CreateDirectory(absoluteUserDataDir);
                }

                // Add user data directory argument
                chromeOptions.AddArgument($"user-data-dir={absoluteUserDataDir}");
            }

            // Set up the WebDriver manager for Chrome
            new DriverManager().SetUpDriver(new ChromeConfig());

            return new ChromeDriver(chromeOptions);
        }
    }
}

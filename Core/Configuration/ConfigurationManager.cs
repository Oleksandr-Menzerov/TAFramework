using Microsoft.Extensions.Configuration;

namespace Core.Configuration
{
    /// <summary>
    /// A static class responsible for managing and accessing application configuration settings.
    /// </summary>
    public static class ConfigurationManager
    {
        /// <summary>
        /// A readonly instance of <see cref="IConfiguration"/> used to load the application's configuration from the appsettings.json file.
        /// </summary>
        private static readonly IConfiguration _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())  // Sets the base path to the current directory of the application
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)  // Loads the appsettings.json file with auto-reload on changes
            .Build();  // Builds the configuration

        /// <summary>
        /// Provides access to the application's settings by binding the "AppSettings" section from the configuration file.
        /// </summary>
        public static AppSettings AppSettings => _configuration.GetSection("AppSettings").Get<AppSettings>()!;  // Retrieves the "AppSettings" section and maps it to the AppSettings object

        /// <summary>
        /// Provides access to the Selenium settings by binding the "SeleniumSettings" section from the configuration file.
        /// </summary>
        public static SeleniumSettings SeleniumSettings => _configuration.GetSection("SeleniumSettings").Get<SeleniumSettings>()!;  // Retrieves the "SeleniumSettings" section and maps it to the SeleniumSettings object
    }
}

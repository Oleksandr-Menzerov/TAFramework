namespace Core.Configuration
{
    /// <summary>
    /// Class representing the Selenium configuration settings.
    /// </summary>
    public class SeleniumSettings
    {
        public bool Headless { get; set; } = false; // Indicates whether to run in headless mode
        public string Resolution { get; set; } = string.Empty; // Desired resolution for the browser
        public bool UseUserProfile { get; set; } = false; // Indicates whether to use a user profile
        public string UserProfilePath { get; set; } = string.Empty; // Path to the user profile
    }
}

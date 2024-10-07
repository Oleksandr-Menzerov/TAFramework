namespace Core.Configuration
{
    /// <summary>
    /// Class representing the Application configuration settings.
    /// </summary>
    public class AppSettings
    {
        public string ApiBaseUrl { get; set; } = string.Empty;
        public string UiBaseUrl { get; set; } = string.Empty;
        public int DefaultTimeout { get; set; }
        public string AuthToken { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

using OpenQA.Selenium;
using UI.Driver;

namespace Tests.Utils
{
    public static class ScreenshotUtils
    {
        private static readonly string _screenshotsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "Screenshots");

        public static (string Path, string Base64) AddScreenshot(ScenarioContext context, bool getBase64 = false)
        {
            var driver = Driver.GetCurrentDriver();
            var takesScreenshot = (ITakesScreenshot)driver;
            string errorMessage = string.Empty;
            for (int i = 0; i < 3; i++) // Retry up to 3 times
            {
                try
                {
                    var screenshot = takesScreenshot.GetScreenshot();
                    var fileName = context.ScenarioInfo.Title.Replace(' ', '_')
                        + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + ".png";
                    Directory.CreateDirectory(_screenshotsDir);
                    var relativePath = Path.Combine(_screenshotsDir, fileName);
                    var absolutePath = Path.GetFullPath(relativePath);
                    screenshot.SaveAsFile(absolutePath);
                    var url = GetScreenshotsUrlFromEnvironment();
                    string path = !string.IsNullOrEmpty(url) ? $"{url}/{fileName}" : absolutePath;
                    string base64 = string.Empty;

                    if (getBase64)
                    {
                        byte[] screenshotBytes = screenshot.AsByteArray;
                        base64 = Convert.ToBase64String(screenshotBytes);
                    }

                    return (path,  base64);

                }
                catch (WebDriverException ex) when (i < 2) // Retry if it's a WebDriverException
                {
                    errorMessage = ex.Message;
                    Thread.Sleep(350); // Wait a bit before retrying
                }
            }

            throw new WebDriverException($"Failed to capture screenshot after multiple attempts. {errorMessage}");
        }

        private static string GetScreenshotsUrlFromEnvironment()
        {
            return Environment.GetEnvironmentVariable("SCREENSHOTS_BASE_URL") ?? string.Empty;
        }
    }
}

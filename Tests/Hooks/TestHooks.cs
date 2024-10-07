using System.Drawing;
using TechTalk.SpecFlow.Infrastructure;
using Tests.Extensions;
using Tests.Utils;
using UI.Driver;

namespace Tests.Hooks
{
    /// <summary>
    /// Contains hooks that run before and after scenarios in SpecFlow tests.
    /// </summary>
    [Binding]
    public class TestHooks(ISpecFlowOutputHelper specFlowOutputHelper, ScenarioContext scenarioContext, FeatureContext featureContext)
    {
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper = specFlowOutputHelper;
        private readonly TestDataCleaner _testDataCleaner = new(scenarioContext);

        /// <summary>
        /// Initializes the retry count before the scenario starts.
        /// </summary>
        [BeforeScenario(Order = 0)]
        public void InitializeRetryCount()
        {
            RetryCounter.IncreaseRetryCount(featureContext, scenarioContext);
        }

        /// <summary>
        /// Initializes the WebDriver before UI scenarios.
        /// </summary>
        [BeforeScenario(Order = 1)]
        public void InitializeDriver()
        {
            if (scenarioContext.ScenarioInfo.Tags.Contains("UI"))
            {
                Driver.CreateDriver();

                // Only for presentation on the second screen
                //var driver = Driver.GetCurrentDriver();
                //driver.Manage().Window.Position = new Point(1920, 0);
                //driver.Manage().Window.Maximize();
            }
        }

        /// <summary>
        /// Takes a screenshot and quits the driver after the scenario finishes.
        /// </summary>
        [AfterScenario(Order = 0)]
        public void MakeScreenshotAndQuitDriver()
        {
            if (scenarioContext.ScenarioInfo.Tags.Contains("UI"))
            {
                if (scenarioContext.TestError != null)
                {
                    try
                    {
                        var screenshot = ScreenshotUtils.AddScreenshot(scenarioContext);
                        _specFlowOutputHelper.WriteLine("Screenshot added");
                        _specFlowOutputHelper.AddImg(screenshot.Path, "Screenshot");
                        _specFlowOutputHelper.AddAttachment(screenshot.Path);
                        //_specFlowOutputHelper.EmbedBase64Image(screenshot.Base64, "Screenshot");
                    }
                    catch (Exception ex)
                    {
                        _specFlowOutputHelper.WriteLine($"Failed to capture screenshot: {ex.Message}");
                    }
                }
                Driver.Quit();
            }
        }

        /// <summary>
        /// Cleans up test data after the scenario finishes.
        /// </summary>
        [AfterScenario(Order = 1)]
        public async Task CleanUpTestData()
        {
            var errors = await _testDataCleaner.CleanUpAsync();
            foreach (var error in errors)
            {
                _specFlowOutputHelper.WriteLine(error);
            }
        }

        /// <summary>
        /// Reports the retry count after the scenario finishes.
        /// </summary>
        [AfterScenario(Order = 2)]
        public void ReportRetryCount()
        {
            var retryCountReport = RetryCounter.GetRetryCountReport(featureContext, scenarioContext);

            if (retryCountReport != string.Empty)
            {
                _specFlowOutputHelper.WriteLine(retryCountReport);
            }
        }
    }
}

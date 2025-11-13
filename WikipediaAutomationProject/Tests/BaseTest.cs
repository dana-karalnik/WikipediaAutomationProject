using AventStack.ExtentReports;
using Microsoft.Playwright;
using WikipediaAutomationProject.Helpers.Extent;

namespace WikipediaAutomationProject.Tests
{
    public class BaseTest
    {
        protected IPage Page;
        protected ExtentReports Extent;
        protected ExtentTest Test;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Extent = ReportManager.GetReporter();
        }

        [SetUp]
        public async Task SetupAsync()
        {
            Page = await PlaywrightHelper.CreateNewPageAsync();
            Test = Extent.CreateTest(TestContext.CurrentContext.Test.Name);
            Test.Info($"Test '{TestContext.CurrentContext.Test.Name}' started.");
        }

        [TearDown]
        public async Task TearDownAsync()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;
            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                var screenshotPath = await CaptureScreenshotAsync();
                Test.Fail($"Test failed: {message}")
                    .AddScreenCaptureFromPath(screenshotPath);
            }
            else
            {
                Test.Pass("Test passed successfully.");
            }

            Extent.Flush();
            if (Page != null)
                await Page.CloseAsync();
        }

        /// <summary>
        /// Takes a screenshot and saves it to the Reports/Screenshots folder
        /// </summary>
        /// <returns></returns>
        private async Task<string> CaptureScreenshotAsync()
        {
            var screenshotsDir = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "Screenshots");
            Directory.CreateDirectory(screenshotsDir);

            var fileName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            var filePath = Path.Combine(screenshotsDir, fileName);

            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = filePath, FullPage = true });
            return filePath;
        }
    }
}

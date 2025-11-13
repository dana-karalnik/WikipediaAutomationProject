using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

[SetUpFixture]
public class PlaywrightHelper
{
    public static IPlaywright? PlaywrightInstance { get; private set; }
    public static IBrowser? Browser { get; private set; }

    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        PlaywrightInstance = await Playwright.CreateAsync();
        Browser = await PlaywrightInstance.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });

        TestContext.Progress.WriteLine("Playwright initialized successfully");
    }

    [OneTimeTearDown]
    public async Task GlobalTeardown()
    {
        if (Browser?.IsConnected == true)
            await Browser.CloseAsync();

        PlaywrightInstance?.Dispose();
        TestContext.Progress.WriteLine("Playwright disposed");
    }

    public static async Task<IPage> CreateNewPageAsync()
    {
        if (Browser == null)
            throw new InvalidOperationException("Browser not initialized. Make sure PlaywrightHelper setup has run");
        var context = await Browser.NewContextAsync();
        return await context.NewPageAsync();
    }
}

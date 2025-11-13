using Microsoft.Playwright;
using WikipediaAutomationProject.Components;

public class BaseAppearanceComponent : BaseComponent
{
    private readonly ILocator radioOptions;

    public BaseAppearanceComponent(IPage page, string locator) : base(page, locator)
    {
        radioOptions = page.Locator($"{locator} .cdx-radio");
    }

    /// <summary>
    /// Click on specific option
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    protected async Task SelectOptionByValue(string value)
    {
        await radioOptions.Locator($"input[value='{value}']").ClickAsync();
    }

}

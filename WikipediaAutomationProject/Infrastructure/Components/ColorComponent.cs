using Microsoft.Playwright;
using WikipediaAutomationProject.Helpers;
using WikipediaAutomationProject.Infrastructure.Enums;

public class ColorComponent : BaseAppearanceComponent
{

    public ColorComponent(IPage page)
        : base(page, "#skin-client-prefs-skin-theme")
    {
    }
    /// <summary>
    /// Click on specific color
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public async Task SelectColor(ColorOptions color)
    {
        var value = color.GetDescription();
        await SelectOptionByValue(value);
    }

}

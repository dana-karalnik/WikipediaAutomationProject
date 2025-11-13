using Microsoft.Playwright;
using WikipediaAutomationProject.Components;
using WikipediaAutomationProject.Helpers;
using WikipediaAutomationProject.Infrastructure.Constants;
using WikipediaAutomationProject.Infrastructure.Enums;
using WikipediaAutomationProject.Pages.Base;
using System.Threading.Tasks;

namespace WikipediaAutomationProject.Infrastructure.Pages
{
    public class PlaywrightPage : BasePage
    {
        public DebuggingFeaturesComponent DebuggingFeaturesSection { get; }
        public MicrosoftDevToolsComponent MicrosoftDevToolsComponent { get; }
        public ColorComponent ColorComponent { get; }
        public PlaywrightPage(IPage page) : base(page, UrlConstants.WikipediaBase + WikiPages.Playwright)
        {
            DebuggingFeaturesSection = new DebuggingFeaturesComponent(page);
            ColorComponent = new ColorComponent(page);
            MicrosoftDevToolsComponent = new MicrosoftDevToolsComponent(page);

        }
        /// <summary>
        /// Check if page html with specific color
        /// </summary>
        /// <param name="expectedColor"></param>
        /// <returns>true if page with expected color else false</returns>
        public async Task<bool> IsPageWithSpecificColor(ColorOptions expectedColor)
        {
            var bodyClass = await page.Locator("html").GetAttributeAsync("class");
            var expected = expectedColor.GetDescription();
            return bodyClass != null && bodyClass.Contains(expected);
        }
    }
}

using Microsoft.Playwright;

namespace WikipediaAutomationProject.Pages.Base
{
    public abstract class BasePage
    {
        protected readonly IPage page;
        protected readonly string url;

        protected BasePage(IPage page, string url)
        {
            this.page = page;
            this.url = url;
        }

        /// <summary>
        /// Navigate to url
        /// </summary>
        /// <returns></returns>
        public virtual async Task NavigateAsync()
        {
            await page.GotoAsync(url);
        }
    }
}

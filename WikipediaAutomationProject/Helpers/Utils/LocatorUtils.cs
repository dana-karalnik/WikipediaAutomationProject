using Microsoft.Playwright;

namespace WikipediaAutomationProject.Helpers.Utils
{
    internal class LocatorUtils
    {
        /// <summary>
        /// Check if element is a link
        /// </summary>
        /// <param name="element"></param>
        /// <returns>true if element is a link else false</returns>
        public static async Task<bool> IsLinkAsync(ILocator element)
        {
            var tagName = (await element.EvaluateAsync<string>("el => el.tagName")).ToLower();
            if (tagName == "a")
            {
                var href = await element.GetAttributeAsync("href");
                return !string.IsNullOrEmpty(href);
            }
            return false;
        }
    }
}


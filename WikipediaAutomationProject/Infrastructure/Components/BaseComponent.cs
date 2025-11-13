using Microsoft.Playwright;

namespace WikipediaAutomationProject.Components
{
    public class BaseComponent
    {
        protected IPage page;
        protected string componentSelector;
        protected ILocator componentLocator;

        public BaseComponent(IPage page, string selector)
        {
            this.page = page;
            this.componentSelector = selector;
            this.componentLocator = page.Locator(selector);
        }

        /// <summary>
        /// Find locator in component by selector
        /// </summary>
        /// <param name="selector"></param>
        /// <returns>locator</returns>
        protected ILocator Find(string selector)
        {
            return componentLocator.Locator(selector);
        }
    }
}

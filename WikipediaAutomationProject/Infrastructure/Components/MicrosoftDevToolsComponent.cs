using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace WikipediaAutomationProject.Components
{
    public class MicrosoftDevToolsComponent : BaseComponent
    {
        public MicrosoftDevToolsComponent(IPage page) : base(page, "xpath=//div[contains(@aria-labelledby,'Microsoft') and contains(@aria-labelledby,'development') and contains(@aria-labelledby,'tools')]") { }
       
        /// <summary>
        /// Finds all technology elements
        /// </summary>
        /// <returns>technology elements</returns>
        public ILocator GetTechnologyElements()
        {
            return Find("td li [title]");
        }


    }
}

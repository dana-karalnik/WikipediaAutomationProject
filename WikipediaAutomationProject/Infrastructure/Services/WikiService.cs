using Newtonsoft.Json.Linq;
using WikipediaAutomationProject.Infrastructure.Constants;
using WikipediaAutomationProject.Infrastructure.Services;

namespace WikipediaAutomationProject.Helpers.Api
{
    public class WikiService : BaseHttpClient
    {
        private const string ParseSectionsQuery = "?action=parse&page={0}&prop=sections&format=json";
        private const string ParseByIndexQuery = "?action=parse&page={0}&prop=text&section={1}&format=json";

        public WikiService() : base(UrlConstants.WikipediaApiBase)
        {
        }

        /// <summary>
        /// Retrieves cleaned text content of a specific section by its name
        /// </summary>
        /// <param name="pageTitle">The Wikipedia page title</param>
        /// <param name="sectionName">The section name</param>
        /// <returns>Plain text content of the requested section</returns>
        /// <exception cref="InvalidOperationException">Thrown if the section is not found or cannot be parsed</exception>
        public async Task<string> GetSectionTextByNameAsync(string pageTitle, string sectionName)
        {
            string sectionIndex = await GetSectionIndexAsync(pageTitle, sectionName);
            if (sectionIndex == null)
                throw new InvalidOperationException($"Section '{sectionName}' not found on page '{pageTitle}'");

            string htmlContent = await GetSectionHtmlAsync(pageTitle, sectionIndex);

            return string.IsNullOrWhiteSpace(htmlContent) ?
                throw new InvalidOperationException($"Failed to retrieve HTML for '{sectionName}'")
                : WikiContentParser.ExtractCleanText(htmlContent);
        }

        /// <summary>
        /// Finds the section index by its name
        /// </summary>
        /// <param name="pageTitle"></param>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        private async Task<string?> GetSectionIndexAsync(string pageTitle, string sectionName)
        {
            string endpoint = string.Format(ParseSectionsQuery, pageTitle);
            var response = await GetAsync(endpoint);
            var json = JObject.Parse(response);
            var sections = json["parse"]?["sections"];
            if (sections == null)
                return null;
            var section = sections?
                .FirstOrDefault(s => string.Equals(s?["line"]?
                .ToString(), sectionName, StringComparison.OrdinalIgnoreCase));
            return section?["index"]?.ToString();
        }

        /// <summary>
        /// Retrieves the raw HTML content of a section by index
        /// </summary>
        /// <param name="pageTitle"></param>
        /// <param name="sectionIndex"></param>
        /// <returns></returns>
        private async Task<string> GetSectionHtmlAsync(string pageTitle, string sectionIndex)
        {
            string endpoint = string.Format(ParseByIndexQuery, pageTitle, sectionIndex);
            var response = await GetAsync(endpoint);
            var json = JObject.Parse(response);
            return json["parse"]?["text"]?["*"]?.ToString() ?? string.Empty;
        }
    }
}
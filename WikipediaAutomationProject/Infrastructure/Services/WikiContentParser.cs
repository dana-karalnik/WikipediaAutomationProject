using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikipediaAutomationProject.Helpers.Utils;

namespace WikipediaAutomationProject.Infrastructure.Services
{
    internal class WikiContentParser
    {     
        /// <summary>
        /// Extracts relevant text content (paragraphs and lists) from Wikipedia HTML,
        /// after cleaning up footnotes and reference lists.
        /// </summary>
        public static string ExtractCleanText(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return string.Empty;
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            HtmlUtils.RemoveNodes(doc, "//sup | //ol[@class='references']");
            var nodes = doc.DocumentNode.SelectNodes("//p | //ul | //ol");
            return HtmlUtils.BuildCleanText(nodes);
        }
    }
}

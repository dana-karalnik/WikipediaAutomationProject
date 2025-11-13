using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WikipediaAutomationProject.Helpers.Utils
{
    internal class HtmlUtils
    {
        /// <summary>
        /// Removes specific nodes from HTML using provided XPath
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="xPath"></param>
        public static void RemoveNodes(HtmlDocument doc, string xPath)
        {
            foreach (var node in doc.DocumentNode.SelectNodes(xPath) ?? Enumerable.Empty<HtmlNode>())
                node.Remove();
        }

        /// <summary>
        /// Extracts text from selected nodes
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns>normalized string</returns>
        public static string BuildCleanText(IEnumerable<HtmlNode> nodes)
        {
            if (nodes == null || !nodes.Any())
                return string.Empty;
            var textParts = nodes
                .Select(node => node.InnerText.Trim())
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .ToList();
            var text = string.Join(" ", textParts);
            text = System.Net.WebUtility.HtmlDecode(text);
            text = Regex.Replace(text, "\\s+", " ").Trim();
            return text;
        }
    }
}

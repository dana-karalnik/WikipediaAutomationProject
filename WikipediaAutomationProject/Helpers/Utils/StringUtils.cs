using System.Text.RegularExpressions;

namespace WikipediaAutomationProject.Helpers.Utils
{
    public static class StringUtils
    {

        /// <summary>
        /// Get unique words from text
        /// </summary>
        /// <param name="text"></param>
        /// <returns>list of unique words</returns>
        public static List<string> GetUniqueWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>();
            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return words
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        /// <summary>
        /// Normalize text by converting to lowercase and removing all non-letter characters and extra spaces
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string with only letters</returns>
        public static string NormalizeText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            string lower = text.ToLowerInvariant();
            string onlyLetters = Regex.Replace(lower, @"[^a-z\s]", " ");
            string singleSpaced = Regex.Replace(onlyLetters, @"\s+", " ").Trim();
            return singleSpaced;
        }

      
    }
}

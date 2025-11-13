using Microsoft.Playwright;
using NUnit.Framework;
using WikipediaAutomationProject.Components;
using WikipediaAutomationProject.Helpers.Api;
using WikipediaAutomationProject.Helpers.Utils;
using WikipediaAutomationProject.Infrastructure.Constants;
using WikipediaAutomationProject.Infrastructure.Enums;
using WikipediaAutomationProject.Infrastructure.Pages;

namespace WikipediaAutomationProject.Tests
{
    [TestFixture]
    public class PlaywriteTests : BaseTest
    {
        private PlaywrightPage playwrightPage;

        [SetUp]
        public async Task SetupTestAsync()
        {
            playwrightPage = new PlaywrightPage(Page);
            await playwrightPage.NavigateAsync();
        }

        [Test]
        public async Task ExtractDebuggingFeatures()
        {
            Test.Info("Extracting Debugging Features section text via UI");
            var uiSectionText = await playwrightPage.DebuggingFeaturesSection.GetFullTextAsync();
            Assert.That(!string.IsNullOrEmpty(uiSectionText), "Debugging Features section text was not extracted via UI");

            var text = StringUtils.NormalizeText(uiSectionText);
            var uniqueWordsUi = StringUtils.GetUniqueWords(text);
            Test.Info($"Extracted {uniqueWordsUi.Count} unique words from Debugging Features section via UI");

            Test.Info("Extracting Debugging Features section text via API");
            var wiki = new WikiService();
            string apiSectionText = await wiki.GetSectionTextByNameAsync(WikiPages.Playwright, WikiSections.DebuggingFeatures);
            Assert.That(!string.IsNullOrEmpty(apiSectionText), "Debugging Features section text was not extracted via API");

            Test.Info("Normalize text and validate unique words count are equal");
            var textApi = StringUtils.NormalizeText(apiSectionText);
            var uniqueWordsApi = StringUtils.GetUniqueWords(textApi);
            Test.Info($"Extracted {uniqueWordsApi.Count} unique words from Debugging Features section via API");

            Assert.That(uniqueWordsUi.Count, Is.EqualTo(uniqueWordsApi.Count),
                $"Mismatch in unique words count: UI={uniqueWordsUi.Count}, API={uniqueWordsApi.Count}");
        }

        [Test]
        public async Task ValidateMicrosoftDevToolsLinks()
        {
            Test.Info("Validate all technology names are a text link");
            var techElements = playwrightPage.MicrosoftDevToolsComponent.GetTechnologyElements();
            int count = await techElements.CountAsync();
            var failedTechnologies = new List<string>();

            for (int i = 0; i < count; i++)
            {
                var item = techElements.Nth(i);
                string name = (await item.TextContentAsync())?.Trim() ?? "[Unknown]";
                bool isLink = await LocatorUtils.IsLinkAsync(item);
                if (!isLink)
                    failedTechnologies.Add(name);
            }

            if (failedTechnologies.Count > 0)
            {
                Assert.Fail($"Technologies not a link: \n{string.Join("\n", failedTechnologies)}");
            }
        }

        [Test]
        public async Task ValidateSetColorBetaToDark()
        {
            Test.Info("Click on Dark in Color(beta)");
            await playwrightPage.ColorComponent.SelectColor(ColorOptions.Dark);

            Test.Info("Validate color changed");
            bool changed = await playwrightPage.IsPageWithSpecificColor(ColorOptions.Dark);
            Assert.That(changed, Is.True, "Failed to set or validate Dark color theme in Color (beta) control.");
        }
    }
}

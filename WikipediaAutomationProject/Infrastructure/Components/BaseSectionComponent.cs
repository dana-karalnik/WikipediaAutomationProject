using Microsoft.Playwright;
using WikipediaAutomationProject.Components;

public class BaseSectionComponent : BaseComponent
{
    protected String sectionId;

    public BaseSectionComponent(IPage page, string sectionId)
        : base(page, $"//*[@id='{sectionId}']")
    {
        this.sectionId = sectionId;
    }

    /// <summary>
    /// Extracts all text from section
    /// </summary>
    /// <returns>combined section text</returns>
    public async Task<string> GetFullTextAsync()
    {
        var paragraphLocator = page.Locator($"//*[@id='{sectionId}']/parent::div/following-sibling::*[(self::p or self::ul or self::ol) and preceding-sibling::div[1]/h3[@id='{sectionId}']]");
        var paragraphText = await paragraphLocator.AllInnerTextsAsync();
        return string.Join(" ", paragraphText).Trim();
    }

}
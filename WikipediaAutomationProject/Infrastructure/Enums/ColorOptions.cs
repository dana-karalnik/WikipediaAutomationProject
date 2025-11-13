using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace WikipediaAutomationProject.Infrastructure.Enums
{
    public enum ColorOptions
    {
        [Description("day")]  
        Light,

        [Description("night")]  
        Dark,

        [Description("os")] 
        Automatic
    }
}

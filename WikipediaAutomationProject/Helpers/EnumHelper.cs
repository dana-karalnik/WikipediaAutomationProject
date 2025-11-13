using System.Reflection;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;


namespace WikipediaAutomationProject.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// Retrieves the description of specific enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns>description of enum</returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo? field = value.GetType().GetField(value.ToString());
            DescriptionAttribute? attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }
    }
}

using Our.Umbraco.ThemeEngine.Core.Configuration;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Our.Umbraco.ThemeEngine.Core.Extensions
{
    public static class PublishedContentExtensions
    {
        private static IThemeEngineConfiguration _configuration
        {
            get
            {
                return ThemeEngineConfigurationSection.GetConfiguration();
            }
        } 

        public static string GetTheme(this IPublishedContent content)
        {
            var hasDefaultValue = string.IsNullOrWhiteSpace(_configuration.DefaultTheme) == false;

            return hasDefaultValue ? content.GetPropertyValue(_configuration.ThemePropertyAlias, _configuration.FindThemePropertyRecursively, _configuration.DefaultTheme)
                                   : content.GetPropertyValue<string>(_configuration.ThemePropertyAlias, _configuration.FindThemePropertyRecursively);
        }
    }
}

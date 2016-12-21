using System.Configuration;

namespace Our.Umbraco.ThemeEngine.Core.Configuration
{
    public class ThemeEngineConfigurationSection : ConfigurationSection, IThemeEngineConfiguration
    {
        private const string ThemeEngineConfigurationSectionName = "Our.Umbraco.ThemeEngine";

        private const string DefaultThemeConfigurationPropertyName = "defaultTheme";
        private const string FindThemePropertyRecursivelyPropertyName = "findThemePropertyRecursively";
        private const string ThemePropertyAliasConfigurationPropertyName = "themePropertyAlias";

        [ConfigurationProperty(name: DefaultThemeConfigurationPropertyName, IsRequired = false)]
        public string DefaultTheme
        {
            get
            {
                return base[DefaultThemeConfigurationPropertyName] as string;
            }

            set
            {
                base[DefaultThemeConfigurationPropertyName] = value;
            }
        }

        [ConfigurationProperty(name: FindThemePropertyRecursivelyPropertyName, IsRequired = true, DefaultValue = true)]
        public bool FindThemePropertyRecursively
        {
            get
            {
                return (bool)base[FindThemePropertyRecursivelyPropertyName];
            }

            set
            {
                base[FindThemePropertyRecursivelyPropertyName] = value;
            }
        }

        [ConfigurationProperty(name: ThemePropertyAliasConfigurationPropertyName, IsRequired = true)]
        public string ThemePropertyAlias
        {
            get
            {
                return base[ThemePropertyAliasConfigurationPropertyName] as string;
            }

            set
            {
                base[ThemePropertyAliasConfigurationPropertyName] = value;
            }
        }

        public static IThemeEngineConfiguration GetConfiguration()
        {
            return ConfigurationManager.GetSection(ThemeEngineConfigurationSectionName) as ThemeEngineConfigurationSection;
        }
    }
}

namespace Our.Umbraco.ThemeEngine.Core.Configuration
{
    public interface IThemeEngineConfiguration
    {
        string DefaultTheme { get; set; }
        bool FindThemePropertyRecursively { get; set; }
        string ThemePropertyAlias { get; set; }
    }
}

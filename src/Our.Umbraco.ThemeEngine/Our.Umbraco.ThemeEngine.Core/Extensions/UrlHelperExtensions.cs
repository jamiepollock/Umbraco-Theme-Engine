using System.Web;
using System.Web.Mvc;

using Umbraco.Core;

namespace Our.Umbraco.ThemeEngine.Core.Extensions
{
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Returns the url of a themed asset
        /// ex: @Url.ThemedAsset(Model, "images/favicon.ico")
        /// NOTE: requires '@using ClientDependency.Core.Mvc' in View
        /// </summary>
        /// <param name="Url">UrlHelper</param>
        /// <param name="SiteThemeName"></param>
        /// <param name="RelativeAssetPath">Path to file inside [theme]/Assets/ folder</param>
        /// <returns></returns>
        public static string ThemedAsset(this UrlHelper url, string SiteThemeName, string RelativeAssetPath)
        {
            var themeRoot = ThemeEngineHelper.GetFinalThemePath(SiteThemeName, PathType.ThemeRoot);
            return VirtualPathUtility.ToAbsolute(themeRoot).EnsureEndsWith('/') + "Assets/" + RelativeAssetPath;
        }
    }
}

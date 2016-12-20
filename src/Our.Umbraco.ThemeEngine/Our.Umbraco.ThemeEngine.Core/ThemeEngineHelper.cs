using System;
using System.IO;

using Umbraco.Core;
using Umbraco.Core.IO;

namespace Our.Umbraco.ThemeEngine.Core
{
    public static class ThemeEngineHelper
    {
        /// <summary>
        /// Returns the final path to the requested type, based on the theme and file existence.
        /// </summary>
        /// <param name="SiteThemeName">Theme Name</param>
        /// <param name="PathType">Type of Path to return</param>
        /// <param name="ViewName">Name of the View (without extension) (optional)</param>
        /// <param name="AlternateStandardPath">If the non-themed path is not standard, provide the full path here (optional)</param>
        /// <returns></returns>
        public static string GetFinalThemePath(string SiteThemeName, PathType PathType, string ViewName = "", string AlternateStandardPath = "")
        {
            if (SiteThemeName.IsNullOrWhiteSpace())
            {
                throw new InvalidOperationException("No theme has been set for this website root, republish the root with a selected theme.");
            }

            var finalPath = "";
            var standardPath = "";
            var themePath = "";

            var baseThemePath = string.Format("~/App_Plugins/Theming/Themes/{0}", SiteThemeName);

            switch (PathType)
            {
                case PathType.ThemeRoot:
                    themePath = string.Format("{0}/", baseThemePath);
                    standardPath = themePath;
                    break;

                case PathType.View:
                    standardPath = AlternateStandardPath != "" ? AlternateStandardPath : string.Format("~/Views/{0}.cshtml", ViewName);
                    themePath = string.Format("{0}/Views/{1}.cshtml", baseThemePath, ViewName);
                    break;

                case PathType.PartialView:
                    standardPath = AlternateStandardPath != "" ? AlternateStandardPath : string.Format("~/Views/Partials/{0}.cshtml", ViewName);
                    themePath = string.Format("{0}/Views/Partials/{1}.cshtml", baseThemePath, ViewName);
                    break;

                case PathType.GridEditor:
                    standardPath = AlternateStandardPath != "" ? AlternateStandardPath : string.Format("~/Views/Partials/Grid/Editors/{0}.cshtml", ViewName);
                    themePath = string.Format("{0}/Views/Partials/Grid/Editors/{1}.cshtml", baseThemePath, ViewName);
                    break;

                default:
                    break;
            }

            if (File.Exists(IOHelper.MapPath(themePath)))
            {
                finalPath = themePath;
            }
            else
            {
                finalPath = standardPath;
            }

            return finalPath;
        }

        /// <summary>
        /// Shortcut for 'GetFinalThemePath()' with PathType.ThemeRoot
        /// </summary>
        /// <param name="SiteThemeName"></param>
        /// <returns></returns>
        public static string GetThemePath(string SiteThemeName)
        {
            var path = GetFinalThemePath(SiteThemeName, PathType.ThemeRoot);
            return path;
        }

        /// <summary>
        /// Shortcut for 'GetFinalThemePath()' with PathType.View
        /// </summary>
        /// <param name="SiteThemeName"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public static string GetThemeViewPath(string SiteThemeName, string ViewName)
        {
            var path = GetFinalThemePath(SiteThemeName, PathType.View, ViewName);
            return path;
        }

        /// <summary>
        /// Shortcut for 'GetFinalThemePath()' with PathType.PartialView
        /// </summary>
        /// <param name="SiteThemeName"></param>
        /// <param name="ViewName"></param>
        /// <returns></returns>
        public static string GetThemePartialViewPath(string SiteThemeName, string ViewName)
        {

            var path = GetFinalThemePath(SiteThemeName, PathType.PartialView, ViewName);
            return path;
        }

        public static string GetCssOverridePath(string CssOverrideFileName)
        {
            if (CssOverrideFileName.IsNullOrWhiteSpace())
            {
                return "";
            }
            else
            {
                var path = "/App_Plugins/Theming/CssOverrides/{0}";
                return string.Format(path, CssOverrideFileName);
            }

        }
    }
}
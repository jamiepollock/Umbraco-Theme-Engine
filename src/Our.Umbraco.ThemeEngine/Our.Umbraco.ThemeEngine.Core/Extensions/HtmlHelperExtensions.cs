using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using ClientDependency.Core.Mvc;

using Umbraco.Core;
using Umbraco.Core.Logging;

namespace Our.Umbraco.ThemeEngine.Core.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static HtmlHelper RequiresThemedCss(this HtmlHelper html, string SiteThemeName, string FilePath)
        {
            var themeRoot = ThemeEngineHelper.GetFinalThemePath(SiteThemeName, PathType.ThemeRoot);
            return html.RequiresCss(themeRoot + "Assets/css" + FilePath.EnsureStartsWith('/'));
        }

        public static HtmlHelper RequiresThemedJs(this HtmlHelper html, string SiteThemeName, string FilePath)
        {
            var themeRoot = ThemeEngineHelper.GetFinalThemePath(SiteThemeName, PathType.ThemeRoot);
            return html.RequiresJs(themeRoot + "Assets/js" + FilePath.EnsureStartsWith('/'));
        }

        public static HtmlHelper RequiresThemedCssFolder(this HtmlHelper html, string SiteThemeName)
        {
            var themeRoot = ThemeEngineHelper.GetFinalThemePath(SiteThemeName, PathType.ThemeRoot);
            return html.RequiresFolder(themeRoot + "Assets/css",
                100, "*.css", (absPath, pri) => html.RequiresCss(absPath, pri));

        }

        //TODO: This is only here as a hack until CDF 1.8.0 is released and shipped that fixes a bug
        private static HtmlHelper RequiresFolder(this HtmlHelper html, string folderPath, int priority, string fileSearch, Action<string, int> requiresAction)
        {
            var httpContext = html.ViewContext.HttpContext;
            var systemRootPath = httpContext.Server.MapPath("~/");
            var folderMappedPath = httpContext.Server.MapPath(folderPath);

            if (folderMappedPath.StartsWith(systemRootPath))
            {
                var files = Directory.GetFiles(folderMappedPath, fileSearch, SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    var absoluteFilePath = "~/" + file.Substring(systemRootPath.Length).Replace("\\", "/");
                    requiresAction(absoluteFilePath, priority);
                    html.RequiresJs(absoluteFilePath, priority);
                }
            }

            return html;
        }

        public static HtmlHelper RequiresThemedJsFolder(this HtmlHelper html, string SiteThemeName)
        {
            var themeRoot = ThemeEngineHelper.GetFinalThemePath(SiteThemeName, PathType.ThemeRoot);
            return html.RequiresJsFolder(themeRoot + "Assets/js");
        }

        /// <summary>
        /// Renders a partial view in the current theme
        /// </summary>
        /// <param name="html"></param>
        /// <param name="SiteThemeName"></param>
        /// <param name="PartialName"></param>
        /// <param name="ViewModel"></param>
        /// <param name="ViewData"></param>
        /// <returns></returns>
        public static IHtmlString ThemedPartial(this HtmlHelper html, string SiteThemeName, string PartialName, object ViewModel, ViewDataDictionary ViewData = null)
        {
            try
            {
                var path = ThemeEngineHelper.GetFinalThemePath(SiteThemeName, PathType.PartialView, PartialName);
                return html.Partial(path, ViewModel, ViewData);
            }
            catch (Exception ex)
            {
                var msg = string.Format("Error rendering partial view '{0}'", PartialName);
                LogHelper.Error<IHtmlString>(msg, ex);
                return new HtmlString(string.Format("<span class=\"error\">{0}</span>", msg));
            }
        }

        /// <summary>
        /// Renders a partial view in the current theme
        /// </summary>
        /// <param name="html"></param>
        /// <param name="SiteThemeName"></param>
        /// <param name="PartialName"></param>
        /// <param name="ViewData"></param>
        /// <returns></returns>
        public static IHtmlString ThemedPartial(this HtmlHelper html, string SiteThemeName, string PartialName, ViewDataDictionary ViewData = null)
        {
            if (ViewData == null)
            {
                ViewData = html.ViewData;
            }
            try
            {
                var path = ThemeEngineHelper.GetFinalThemePath(SiteThemeName, PathType.PartialView, PartialName);
                return html.Partial(path, ViewData);
            }
            catch (Exception ex)
            {
                var msg = string.Format("Error rendering partial view '{0}'", PartialName);
                LogHelper.Error<IHtmlString>(msg, ex);
                return new HtmlString(string.Format("<span class=\"error\">{0}</span>", msg));
            }

        }

    }
}

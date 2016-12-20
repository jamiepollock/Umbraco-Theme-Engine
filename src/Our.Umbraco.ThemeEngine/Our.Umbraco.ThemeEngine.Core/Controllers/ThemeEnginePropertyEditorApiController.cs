using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;

using Umbraco.Core.IO;
using Umbraco.Web.WebApi;

namespace Our.Umbraco.ThemeEngine.Core.Controllers
{
    public class ThemeEnginePropertyEditorApiController : UmbracoApiController
    {
        //  /Umbraco/Api/ThemeEnginePropertyEditorApi/GetThemes
        [HttpGet]
        public IEnumerable<string> GetThemes()
        {
            var dir = IOHelper.MapPath("~/App_Plugins/ThemeEngine/Themes");
            var allDirs = Directory.GetDirectories(dir).Select(x => new DirectoryInfo(x).Name);
            return allDirs;
        }
    }
}

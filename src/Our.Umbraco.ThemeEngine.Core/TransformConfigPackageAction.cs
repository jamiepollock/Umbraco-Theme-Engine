using Microsoft.Web.XmlTransform;
using System;
using System.Web;
using umbraco.cms.businesslogic.packager.standardPackageActions;
using umbraco.interfaces;
using Umbraco.Core.Logging;

namespace Our.Umbraco.ThemeEngine.Core
{
    public class TransformConfigPackageAction : IPackageAction
    {

        public string Alias()
        {
            return "UTE.TransformConfig";
        }

        public bool Execute(string packageName, System.Xml.XmlNode xmlData)
        {

            try
            {
                //The config file we want to modify
                var file = xmlData.Attributes.GetNamedItem("file").Value;
                var sourceDocFileName = VirtualPathUtility.ToAbsolute(file);

                //The xdt file used for tranformation 
                var xdtfile = xmlData.Attributes.GetNamedItem("xdtfile").Value;
                var xdtFileName = VirtualPathUtility.ToAbsolute(xdtfile);

                // The translation at-hand
                using (var xmlDoc = new XmlTransformableDocument())
                {
                    xmlDoc.PreserveWhitespace = true;
                    xmlDoc.Load(HttpContext.Current.Server.MapPath(sourceDocFileName));

                    using (var xmlTrans = new XmlTransformation(HttpContext.Current.Server.MapPath(xdtFileName)))
                    {
                        if (xmlTrans.Apply(xmlDoc))
                        {
                            // If we made it here, sourceDoc now has transDoc's changes
                            // applied. So, we're going to save the final result off to
                            // destDoc.
                            xmlDoc.Save(HttpContext.Current.Server.MapPath(sourceDocFileName));

                            LogHelper.Info<TransformConfigPackageAction>(() => string.Format("Updated file {0} with transform: {1}", sourceDocFileName, xdtFileName));
                        }
                        else
                        {
                            throw new Exception(string.Format("Unable to apply XmlTransformation to Updated file {0} with transform: {1}", sourceDocFileName, xdtFileName));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<TransformConfigPackageAction>("Our.Umbraco.ThemeEngine Package Install Error", ex);
            }

            return true;
        }

        public System.Xml.XmlNode SampleXml()
        {
            var str = "<Action runat=\"install\" undo=\"false\" alias=\"UTE.TransformConfig\" file=\"~/web.config\" xdtfile=\"~/app_plugins/demo/web.config.xdt></Action>";
            return helper.parseStringToXmlNode(str);
        }

        public bool Undo(string packageName, System.Xml.XmlNode xmlData)
        {
            return false;
        }
    }
}

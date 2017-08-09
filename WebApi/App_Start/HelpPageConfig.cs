using SH3H.WAP.WebApi.Areas.HelpPage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SH3H.WAP.WebApi
{
    public class HelpPageConfig
    {
        public static void Register(HttpConfiguration config)
        {
            List<string> files = new List<string>(); 
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            foreach (var file in Directory.GetFiles(folder, "SH3H.*.xml"))
            {
                string fileName = Path.GetFileName(file);
                files.Add(HttpContext.Current.Server.MapPath("~/bin/" + fileName));
            }
            config.SetDocumentationProvider(new MultiXmlDocumentationProvider(files.ToArray()));
        }
    }
}
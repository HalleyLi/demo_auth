using SH3H.WAP.WebApi.Filters;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SH3H.WAP.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            GlobalConfiguration.Configuration.Filters.Add(new WapExceptionFilterAttribute());            
            filters.Add(new HandleErrorAttribute());
        }
    }
}

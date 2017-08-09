using SH3H.SDK.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SH3H.WAP.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            HelpPageConfig.Register(GlobalConfiguration.Configuration);

            MapperConfig.Register(GlobalConfiguration.Configuration);
        }

        protected void Application_End()
        {
        }

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError();
            LogManager.Get().Throw(ex);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogManager.Get().Throw((Exception)e.ExceptionObject);
        }
    }
}

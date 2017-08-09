using Microsoft.Practices.Unity;
using SH3H.SDK.Infrastructure;
using SH3H.SDK.Share;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Unity.WebApi;

namespace SH3H.WAP.WebApi
{
    public static class UnityConfig
    {
        /// <summary>
        /// 定义WebApi Unity配置文件路径
        /// </summary>
        private const string CONFIG_FILE_UNITY_WEBAPI = "Configs/entlib/entlib.unity.webapi.config";

        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = UnityHelper.GetContainer(CONFIG_FILE_UNITY_WEBAPI, null);
            config.DependencyResolver = new UnityDependencyResolver(container);
            config.Services.Replace(typeof(IAssembliesResolver), new UnityAssemblyResolver(container));

        }
    }
}
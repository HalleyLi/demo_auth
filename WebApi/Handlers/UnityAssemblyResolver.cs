using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Dispatcher;

namespace SH3H.WAP.WebApi
{
    /// <summary>
    /// 定义基于Unity容器的WebApi程序集解析器
    /// </summary>
    public class UnityAssemblyResolver : IAssembliesResolver
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="container">Unity容器对象实例</param>
        public UnityAssemblyResolver(IUnityContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// 定义Unity容器对象实例
        /// </summary>
        private IUnityContainer container;

        /// <summary>
        /// 获取当前Unity容器中的所有程序集
        /// </summary>
        /// <returns>
        /// 返回程序集列表
        /// </returns>
        public ICollection<Assembly> GetAssemblies()
        {
            List<Assembly> assemblies = new List<Assembly>();
            assemblies.Add(this.GetType().Assembly);

            if (container != null)
            {
                foreach(var reg in container.Registrations)
                {
                    Assembly mappedAssembly = reg.MappedToType.Assembly;
                    if (!assemblies.Contains(mappedAssembly))
                        assemblies.Add(mappedAssembly);

                    Assembly registAssembly = reg.RegisteredType.Assembly;
                    if (!assemblies.Contains(registAssembly))
                        assemblies.Add(registAssembly);
                }
            }
            return assemblies;
        }
    }
}
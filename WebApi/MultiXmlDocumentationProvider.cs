using SH3H.WAP.WebApi.Areas.HelpPage;
using SH3H.WAP.WebApi.Areas.HelpPage.ModelDescriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Description;

namespace SH3H.WAP.WebApi
{
    /// <summary>
    /// 定义多XML文件提供器对象，<see cref="HelpPageConfig"/>中通过当前类加载API注释
    /// </summary>
    public class MultiXmlDocumentationProvider : 
        IDocumentationProvider, IModelDocumentationProvider
    {

        private readonly XmlDocumentationProvider[] Providers;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paths">XML文件路径数组</param>
        public MultiXmlDocumentationProvider(params string[] paths)
        {
            this.Providers = paths.Select(p => new XmlDocumentationProvider(p)).ToArray();
        }

        /// <inheritdoc/>
        public string GetDocumentation(HttpParameterDescriptor parameterDescriptor)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(parameterDescriptor));
        }

        /// <inheritdoc/>
        public string GetDocumentation(HttpActionDescriptor actionDescriptor)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(actionDescriptor));
        }

        /// <inheritdoc/>
        public string GetDocumentation(HttpControllerDescriptor controllerDescriptor)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(controllerDescriptor));
        }

        /// <inheritdoc/>
        public string GetResponseDocumentation(HttpActionDescriptor actionDescriptor)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(actionDescriptor));
        }

        /// <inheritdoc/>
        public string GetDocumentation(MemberInfo member)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(member));
        }

        /// <inheritdoc/>
        public string GetDocumentation(Type type)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(type));
        }

        /// <summary>
        /// 获取第一个符合条件的<see cref="XmlDocumentationProvider"/>类型对象
        /// </summary>
        /// <param name="expr">用户查询<see cref="XmlDocumentationProvider"/>对象的回调函数</param>
        /// <returns>返回对应的XML字符串内容</returns>
        private string GetFirstMatch(Func<XmlDocumentationProvider, string> expr)
        {
            return this.Providers
                .Select(expr)
                .FirstOrDefault(p => !String.IsNullOrWhiteSpace(p));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SH3H.SDK.WebApi.Core
{
    /// <summary>
    /// 定义资源特性，用于标识每个控制器所对应的资源
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ResourceAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">模块名称</param>
        public ResourceAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 获取或设置模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置资源URL路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 获取或设置参数列表
        /// </summary>
        public string Params { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SH3H.SDK.WebApi.Core
{
    /// <summary>
    /// 定义Action参数特性，用于生成Resource脚本中的Action
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ActionParamAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ActionParamAttribute()
        {
            this.IsCache = false;
            this.IsArray = false;
        }

        /// <summary>
        /// 获取或设置Action参数字符串
        /// </summary>
        public string Params { get; set; }

        /// <summary>
        /// 获取或设置一个值用于表示返回值是否是数组
        /// </summary>
        public bool IsArray { get; set; }

        /// <summary>
        /// 获取或设置Action的headers属性字符串
        /// </summary>
        public string Headers { get; set; }

        /// <summary>
        /// 获取或设置一个值用于表示是否将请求结果进行缓存
        /// </summary>
        public bool IsCache { get; set; }
    }
}
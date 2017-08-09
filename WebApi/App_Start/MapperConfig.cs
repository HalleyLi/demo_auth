using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace SH3H.WAP.WebApi
{
    /// <summary>
    /// 初始化AutoMapper
    /// </summary>
    public class MapperConfig
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

            MapperRegister.Initialize(folder);
        }
    }
}
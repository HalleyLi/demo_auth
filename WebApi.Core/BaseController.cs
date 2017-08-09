using Newtonsoft.Json;
using SH3H.SDK.Service.Core;
using SH3H.SharpFrame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace SH3H.SDK.WebApi.Controllers
{
    /// <summary>
    /// 定义API控制器抽象类，所有控制器应该集成当前控制器
    /// </summary>    
    public abstract class BaseController : ApiController
    {
        /// <summary>
        /// 根据服务类型获取服务对象实例
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns>返回服务对象实例</returns>
        protected TService GetService<TService>()
        {
            return ServiceFactory.GetService<TService>();
        }

        /// <summary>
        /// 从消息头中获取Token
        /// </summary>
        /// <returns>返回Token</returns>
        internal protected string GetToken()
        {
            return GetHeaderValueByKey("Token");
        }

        /// <summary>
        /// 从消息头中获取应用名称
        /// </summary>
        /// <returns>返回应用名称</returns>
        internal protected string GetAppIdentity()
        {
            return GetHeaderValueByKey("App");
        }

        /// <summary>
        /// 从消息头中获取用户编号
        /// </summary>
        /// <returns>返回用户编号</returns>
        internal protected int GetUserId()
        {
            string value = GetHeaderValueByKey("UserId");
            if (Guard.IsNotNullOrEmpty(value))
                return Convert.ToInt32(value);
            return 0;
        }

        /// <summary>
        /// 根据关键字从消息头中获取对应的值
        /// </summary>
        /// <param name="key">消息头中项名称</param>
        /// <returns>返回消息头中查询的值或者返回NULL</returns>
        protected string GetHeaderValueByKey(string key)
        {
            var headers = ActionContext.Request.Headers.Where(kv => string.Compare(kv.Key, key, true) == 0);
            if (headers.Count() > 0)
            {
                var header = headers.SingleOrDefault();
                return header.Value.FirstOrDefault();
            }
            return null;
        }
    }

    /// <summary>
    /// 定义API控制器抽象类，所有控制器应该集成当前控制器
    /// </summary>
    /// <typeparam name="TService">服务类型</typeparam>
    public abstract class BaseController<TService>
        : BaseController where TService : class
    {
        /// <summary>
        /// 获取当前服务实例
        /// </summary>
        public TService Service
        {
            get { return GetService<TService>(); }
        }
    }
    /// <summary>
    /// 定义API控制器抽象类，扩展服务数量
    /// </summary>
    /// <typeparam name="TService1">服务类型1</typeparam>
    /// <typeparam name="TService2">服务类型2</typeparam>
    public abstract class BaseController<TService1, TService2>
        : BaseController
        where TService1 : class
        where TService2 : class 
    {
        /// <summary>
        /// 获取当前服务1实例
        /// </summary>
        public TService1 Service1
        {
            get { return GetService<TService1>(); }
        }

        /// <summary>
        /// 获取当前服务2实例
        /// </summary>
        public TService2 Service2
        {
            get { return GetService<TService2>(); }
        }
    }

    /// <summary>
    /// 定义API控制器抽象类，扩展服务数量
    /// </summary>
    /// <typeparam name="TService1">服务类型1</typeparam>
    /// <typeparam name="TService2">服务类型2</typeparam>
    /// <typeparam name="TService3">服务类型3</typeparam>
    public abstract class BaseController<TService1, TService2, TService3>
        : BaseController
        where TService1 : class
        where TService2 : class
        where TService3 : class
    {
        /// <summary>
        /// 获取当前服务1实例
        /// </summary>
        public TService1 Service1
        {
            get { return GetService<TService1>(); }
        }

        /// <summary>
        /// 获取当前服务2实例
        /// </summary>
        public TService2 Service2
        {
            get { return GetService<TService2>(); }
        }

        /// <summary>
        /// 获取当前服务2实例
        /// </summary>
        public TService3 Service3
        {
            get { return GetService<TService3>(); }
        }
    }
}

using SH3H.SDK.Infrastructure.Logging;
using SH3H.SDK.Share;
using SH3H.SDK.WebApi.Core.Models;
using SH3H.SDK.Definition.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.Filters;

namespace SH3H.WAP.WebApi.Filters
{
    /// <summary>
    /// 定义平台异常过滤器
    /// </summary>
    public class WapExceptionFilterAttribute
        : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            // 发生异常后需要日志记录
            if (context.Exception != null)
            {
                // 将异常记录日志
                string uri = context.Request.RequestUri.ToString();
                LogManager.Get().ErrorFormat("请求异常, Uri={0}", uri);
                LogManager.Get().Throw(context.Exception);                
            }

            // 如果为平台异常WapException，返回WapResponse对象
            if (context.Exception is WapException)
            {
                var ex = (WapException)context.Exception;

                // 返回平台响应对象
                HttpStatusCode statusCode = (HttpStatusCode)HttpContext.Current.Response.StatusCode;
                var resp = new WapNull(ex.Code, ex.Message, statusCode);
                //resp.Data = context.Exception;
                var message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new ObjectContent<WapNull>(resp, new JsonMediaTypeFormatter());
                context.Response = message;
            }
        }
    }
}
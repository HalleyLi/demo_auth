using Microsoft.Ajax.Utilities;
using SH3H.SDK.Share;
using SH3H.SDK.WebApi.Controllers;
using SH3H.SDK.WebApi.Core;
using SH3H.SDK.WebApi.Core.Models;
using SH3H.SharpFrame;
using SH3H.WAP.WebApi.Templates;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using WapConsts = SH3H.WAP.Share.Consts;

namespace SH3H.WAP.WebApi.Controllers
{
    /// <summary>
    /// 定义杂项控制器，其中包含了不属于具体功能的方法
    /// </summary>
    [Resource("wapMiscRes")]
    [RoutePrefix(WapConsts.URL_PREFIX_WAP + "/misc")]    
    public class MiscellaneousController : BaseController
    {
        /// <summary>
        /// 获取或设置资源脚本字符串
        /// </summary>
        private static string resourceScript = null;

        /// <summary>
        /// 获取或设置压缩后的资源脚本字符串
        /// </summary>
        private static string minifiedResourceScript = null;

        /// <summary>
        /// 获取或设置Resource文件的Etag
        /// </summary>
        private static string etag = null;

        /// <summary>
        /// 获取服务资源脚本
        /// </summary>
        /// <param name="compress">脚本是否需要进行压缩</param>
        /// <returns>返回服务资源脚本</returns>
        [HttpGet]                
        [Route("scripts/resource/compress={compress}")]
        public IHttpActionResult GetResourceScript([FromUri]bool compress = true)
        {
            if (resourceScript == null)
            {
                ResourceScriptTemplate template = new ResourceScriptTemplate();
                template.Session = new Dictionary<string, object>();
                template.Session.Add("apis", Configuration.Services.GetApiExplorer().ApiDescriptions);
                template.Session.Add("req", this.RequestContext);
                string script = template.TransformText();               

                // 生成Etag
                etag = Utils.GetMd5(script);

                // Pretty script
                Beautifier b = new Beautifier();
                resourceScript = b.Beautify(script);

                // Minified script
                var minifier = new Minifier();
                minifiedResourceScript = minifier.MinifyJavaScript(script);
            }

            string content = resourceScript;
            if (compress) content = minifiedResourceScript;
            
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new StringContent(content, Encoding.UTF8, "application/javascript");
            message.Headers.ETag = new EntityTagHeaderValue("\"" + etag + "\"");
            return ResponseMessage(message);
        }

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns>返回服务器时间</returns>
        [HttpGet]
        [Route("getServerTime")]
        [ActionName("getServerTime")]
        public WapDateTime GetServerTime()
        {
            return new WapDateTime(DateTime.Now);
        }
    }    
}

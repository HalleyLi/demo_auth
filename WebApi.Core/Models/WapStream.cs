using SH3H.SDK.Share;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http;

namespace SH3H.SDK.WebApi.Core.Models
{
    /// <summary>
    /// 定义WebApi数据流类型的响应对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model")]
    public class WapStream
        : WapResponse<Stream>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="stream">返回数据流</param>
        public WapStream(Stream stream) : base(stream) { }

        /// <summary>
        /// 创建HTTP响应消息对象
        /// </summary>
        /// <returns>
        /// 返回<see cref="HttpResponseMessage" />类型对象
        /// </returns>
        protected override HttpResponseMessage CreateResponseMessage()
        {
            var response = new HttpResponseMessage(StatusCode);
            response.Content = new StreamContent(base.Data);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");            
            return response;
        }
    }
}
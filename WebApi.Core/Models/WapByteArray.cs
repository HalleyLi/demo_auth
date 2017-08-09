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
    /// 定义WebApi二级制数组响应类型
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model")]
    public class WapByteArray
        : WapResponse<byte[]>
    {
        /// <summary>
        /// 内部构造函数
        /// </summary>
        protected WapByteArray() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bytes">二进制数组</param>
        public WapByteArray(byte[] bytes) : base(bytes) { }

        /// <summary>
        /// 创建HTTP响应消息对象
        /// </summary>
        /// <returns>
        /// 返回<see cref="HttpResponseMessage" />类型对象
        /// </returns>
        protected override HttpResponseMessage CreateResponseMessage()
        {
            var response = new HttpResponseMessage(StatusCode);
            response.Content = new ByteArrayContent(base.Data);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return response;
        }
    }
}
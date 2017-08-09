using SH3H.SDK.Definition;
using SH3H.SDK.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace SH3H.SDK.WebApi.Core.Models
{
    /// <summary>
    /// 定义WebApi响应对象
    /// </summary>
    /// <typeparam name="TReturn">返回值类型</typeparam>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model")]
    public class WapResponse<TReturn> : IHttpActionResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WapResponse()
        {
            this.StatusCode = HttpStatusCode.OK;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">返回值</param>
        public WapResponse(TReturn value)
        {
            this.Data = value;
            this.StatusCode = HttpStatusCode.OK;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">返回值</param>
        /// <param name="code">返回代码</param>
        public WapResponse(TReturn value, int code = StateCode.CODE_SUCCESS)
            : this(value)
        {            
            this.Code = code;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">返回代码</param>
        /// <param name="message">返回消息</param>
        public WapResponse(int code, string message)
            : this(code, message, HttpStatusCode.OK)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">返回代码</param>
        /// <param name="message">返回消息</param>
        /// <param name="statusCode">状态码</param>
        public WapResponse(int code, string message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            this.Code = code;
            this.Message = message;
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// 获取或设置返回码
        /// </summary>
        [DataMember(Name="code")]
        public int Code { get; set; }

        /// <summary>
        /// 获取或设置HTTP状态码
        /// </summary>        
        [DataMember(Name="statusCode")]
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// 获取或设置返回消息字符串
        /// </summary>
        [DataMember(Name="message")]
        public string Message { get; set; }

        /// <summary>
        /// 获取或设置WebApi返回值
        /// </summary>
        [DataMember(Name="data")]
        public TReturn Data { get; set; }

        #region IHttpActionResult

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return CreateResponseMessage();
        }
        
        /// <summary>
        /// 创建HTTP响应消息对象
        /// </summary>
        /// <returns>返回<see cref="HttpResponseMessage"/>类型对象</returns>
        protected virtual HttpResponseMessage CreateResponseMessage()
        {
            var response = new HttpResponseMessage(StatusCode);
            response.Content = new ObjectContent(this.GetType(), this, new JsonMediaTypeFormatter());
            return response;
        }

        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            string val = Data == null ? "" : Data.ToString();
            string str = string.Format(
                "Code={0}\r\nMessage={1}\r\nValue={2}",
                Code, Message, val);
            return str;
        }
    }
}
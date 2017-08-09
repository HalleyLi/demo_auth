using SH3H.SDK.Share;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http;

namespace SH3H.SDK.WebApi.Core.Models
{
    /// <summary>
    /// 定义WebApi数组类型响应对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model")]
    public class WapArray
        : WapResponse<Array>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="array">返回对象数组</param>
        public WapArray(Array array) : base(array) { }
    }
}
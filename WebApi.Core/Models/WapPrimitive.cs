using SH3H.SDK.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.SDK.WebApi.Core.Models
{
    /// <summary>
    /// 定义NULL返回值
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model")]
    public class WapNull : WapResponse<object>
    {
        /// <summary>
        /// 构造函数
        /// </summary>        
        public WapNull() : base(null) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">返回代码</param>
        /// <param name="message">返回消息</param>
        /// <param name="statusCode">Http请求状态码</param>
        public WapNull(int code, string message, HttpStatusCode statusCode)
            : base(code, message, statusCode) { }
    }

    /// <summary>
    /// 定义Boolean返回值类型
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model")]
    public class WapBoolean : WapResponse<Boolean>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">返回值</param>
        public WapBoolean(Boolean value)
            : base(value)
        {
        }
    }

    /// <summary>
    /// 定义Int32返回值类型
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model")]
    public class WapInt16 : WapResponse<Int16>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">返回值</param>
        public WapInt16(Int16 value)
            : base(value)
        {
        }
    }

    /// <summary>
    /// 定义Int32返回值类型
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model")]
    public class WapInt32 : WapResponse<Int32>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">返回值</param>
        public WapInt32(Int32 value)
            : base(value)
        {
        }
    }

    /// <summary>
    /// 定义Int64返回值类型
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model")]
    public class WapInt64 : WapResponse<Int64>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">返回值</param>
        public WapInt64(Int64 value)
            : base(value)
        {
        }
    }

    /// <summary>
    /// 定义Single返回值类型
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model")]
    public class WapSingle : WapResponse<Single>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">返回值</param>
        public WapSingle(Single value)
            : base(value)
        {
        }
    }

    /// <summary>
    /// 定义Double返回值类型
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model")]
    public class WapDouble : WapResponse<Double>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">返回值</param>
        public WapDouble(Double value)
            : base(value)
        {
        }
    }

    /// <summary>
    /// 定义String返回值类型
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model")]
    public class WapString : WapResponse<String>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">返回值</param>
        public WapString(String value)
            : base(value)
        {
        }
    }

    /// <summary>
    /// 定义DateTime返回值类型
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model")]
    public class WapDateTime : WapResponse<DateTime>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">返回值</param>
        public WapDateTime(DateTime value)
            : base(value)
        {
        }
    }

    /// <summary>
    /// 定义Guid返回值类型
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model")]
    public class WapGuid : WapResponse<Guid>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">返回值</param>
        public WapGuid(Guid value)
            : base(value)
        {
        }
    }
}

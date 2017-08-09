using SH3H.SDK.Share;
using System;
using System.Runtime.Serialization;

namespace SH3H.WAP.Auth.Model
{
    /// <summary>
    /// 定义权限认证系统审计对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/Auth")]
    public class WapAuthAudit
    {
        /// <summary>
        /// 关键词1
        /// </summary>
        [DataMember]
        public string Keyword1 { get; set; }

        /// <summary>
        /// 关键词2
        /// </summary>
        [DataMember]
        public string Keyword2 { get; set; }

        /// <summary>
        /// 关键词3
        /// </summary>
        [DataMember]
        public string Keyword3 { get; set; }

        /// <summary>
        /// 跟踪标识
        /// </summary>
        [DataMember]
        public string TrackingGuid { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        [DataMember]
        public string UserAccount { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [DataMember]
        public DateTime OperateDate { get; set; }

        /// <summary>
        /// 操作函数
        /// </summary>
        [DataMember]
        public string OperateFunc { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        [DataMember]
        public string OperateContent { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        [DataMember]
        public string ClientIp { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        [DataMember]
        public int? AckResult { get; set; }

        /// <summary>
        /// 记录ID
        /// </summary>
        [DataMember]
        public double? AuditId { get; set; }
    }
}
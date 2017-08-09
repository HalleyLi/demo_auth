using SH3H.SDK.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Model
{
    /// <summary>
    /// 标签对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/config")]
    public class WapTag
    {

        /// <summary>
        /// 标签id
        /// </summary>
        [DataMember]
        public int TagId { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        [DataMember]
        public string TagName { get; set; }

        /// <summary>
        /// 标签组code
        /// </summary>
        [DataMember]
        public string TagGroupCode { get; set; }

        /// <summary>
        /// 应用标识
        /// </summary>
        [DataMember]
        public string AppIdentity { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 引用次数
        /// </summary>
        [DataMember]
        public int ReferenceCount { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        [DataMember]
        public string Color { get; set; }

        /// <summary>
        /// 标签描述
        /// </summary>
        [DataMember]
        public string Comment { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [DataMember]
        public DateTime? UpdateTime { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapTag)) return false;
            return ((WapTag)obj).TagId == this.TagId;
        }

    }
}

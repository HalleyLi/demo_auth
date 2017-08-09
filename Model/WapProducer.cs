using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using SH3H.SDK.Share;
using SH3H.SDK.Definition.Entities;

namespace SH3H.WAP.Model
{
    /// <summary>
    /// 水表厂商对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/config")]
    public class WapProducer
    {

        /// <summary>
        /// 获取或设置制造商名称
        /// </summary>
        [DataMember(Name = "producerId")]
        public int ProducerId { get; set; }

        /// <summary>
        /// 获取或设置制造商名称
        /// </summary>
        [DataMember(Name = "producerName")]
        public string ProducerName { get; set; }

        /// <summary>
        /// 获取或设置生产商类型
        /// </summary>
        [DataMember(Name = "type")]
        public int DeviceType { get; set; }

        /// <summary>
        /// 获取或设置生产商地址
        /// </summary>
        [DataMember(Name = "address")]
        public string ProducerAddress { get; set; }

        /// <summary>
        /// 获取或设置联系人
        /// </summary>
        [DataMember(Name = "contact")]
        public string ProducerContact { get; set; }

        /// <summary>
        /// 获取或设置联系电话
        /// </summary>
        [DataMember(Name = "tel")]
        public string ProducerTelephone { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        [DataMember(Name = "remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置租户ID
        /// </summary>
        [DataMember(Name = "tenantId")]
        public int TenantId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapProducer)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapProducer>(this, obj as WapProducer);
        }
    }
}

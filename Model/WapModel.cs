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
    /// 水表型号对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/config")]
    public class WapModel
    {
        /// <summary>
        /// 获取或设置型号名称
        /// </summary>
        [DataMember(Name = "modelId")]
        public int ModelId { get; set; }

        /// <summary>
        /// 获取或设置型号名称
        /// </summary>
        [DataMember(Name = "modelName")]
        public string ModelName { get; set; }

        /// <summary>
        /// 获取或设置型号类型
        /// </summary>
        [DataMember(Name = "type")]
        public int DeviceType { get; set; }

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
            if (!(obj is WapModel)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapModel>(this, obj as WapModel);
        }
    }
}

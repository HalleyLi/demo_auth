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
    /// 口径对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/config")]
    public class WapCaliber 
    {

        /// <summary>
        /// 获取或设置编号
        /// </summary>
        [DataMember(Name = "caliberId")]
        public int CaliberId { get; set; }

        /// <summary>
        /// 获取或设置口径名称
        /// </summary>
        [DataMember(Name = "caliberName")]
        public string CaliberName { get; set; }

        /// <summary>
        /// 获取或设置口径值
        /// </summary>
        [DataMember(Name = "caliberValue")]
        public string CaliberValue { get; set; }

        /// <summary>
        /// 获取或设置租户ID
        /// </summary>
        [DataMember(Name = "tenantId")]
        public int TenantId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapCaliber)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapCaliber>(this, obj as WapCaliber);
        }
    }
}

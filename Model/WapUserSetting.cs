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
    /// 定义用户配置对象实体
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/config")]
    public class WapUserSetting
    {
        /// <summary>
        /// 获取或设置用户配置编号
        /// </summary>
        [DataMember]
        public int UserSettingId { get; set; }

        /// <summary>
        /// 获取或设置用户编号
        /// </summary>
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 获取或设置用配置编码
        /// </summary>
        [DataMember]
        public string UserSettingCode { get; set; }

        /// <summary>
        /// 获取或设置用户配置值
        /// </summary>
        [DataMember]
        public string UserSettingValue { get; set; }

        /// <summary>
        /// 获取或设置应用标识
        /// </summary>
        [DataMember]
        public string AppIdentity { get; set; }

        /// <summary>
        /// 获取或设置用户配置类型
        /// </summary>
        [DataMember]
        public string UserSettingType { get; set; }

        /// <summary>
        /// 获取或设置用户配置内容
        /// </summary>
        [DataMember]
        public string UserSettingText { get; set; }

        /// <summary>
        /// 获取或设置创建人编号
        /// </summary>
        [DataMember]
        public int CreatorId { get; set; }

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        [DataMember]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 获取或设置修改人编号
        /// </summary>
        [DataMember]
        public int ModifierId { get; set; }

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        [DataMember]
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 获取或设置备注内容
        /// </summary>
        [DataMember]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置参数编号
        /// </summary>
        [DataMember]
        public int SchemeId { get; set; }

        /// <summary>
        /// 获取或设置是否为组别
        /// </summary>
        [DataMember]
        public bool IsGroup { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapUserSetting)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapUserSetting>(this, obj as WapUserSetting);
        }
    }
}

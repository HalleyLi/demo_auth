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
    /// 系统配置对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/config")]
    public class WapConfiguration
    {
        /// <summary>
        /// 获取配置编号
        /// </summary>
        [DataMember(Name = "configId")]
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置配置名称
        /// </summary>
        [DataMember(Name = "configName")]
        public string ConfigName { get; set; }


        /// <summary>
        /// 获取或设置配置编码
        /// </summary>
        [DataMember(Name = "configCode")]
        public string ConfigCode { get; set; }

        /// <summary>
        /// 获取或设置配置项类型
        /// </summary>
        [DataMember(Name = "configType")]
        public int ConfigType { get; set; }

        /// <summary>
        /// 获取或设置配置值
        /// </summary>
        [DataMember(Name = "configValue")]
        public string ConfigValue { get; set; }

        /// <summary>
        /// 获取或设置配置默认值
        /// </summary>
        [DataMember(Name = "default")]
        public string ConfigDefault { get; set; }

        /// <summary>
        /// 获取或设置配置应用
        /// </summary>
        [DataMember(Name = "app")]
        public string ConfigApp { get; set; }

        /// <summary>
        /// 获取或设置配置分组
        /// </summary>
        [DataMember(Name = "group")]
        public string ConfigGroup { get; set; }

        /// <summary>
        /// 获取或设置配置状态
        /// </summary>
        [DataMember(Name = "active")]
        public int ConfigState { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        [DataMember(Name = "remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置参数编号
        /// </summary>
        [DataMember(Name = "schemeId")]
        public int SchemeId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapConfiguration)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapConfiguration>(this, obj as WapConfiguration);
        }
    }
}

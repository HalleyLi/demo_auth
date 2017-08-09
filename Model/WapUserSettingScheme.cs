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
    /// 定义用户配置参数对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/config")]
    public class WapUserSettingScheme
    {
        /// <summary>
        /// 获取或设置参数编号
        /// </summary>
        [DataMember]
        public int SchemeId { get; set; }

        /// <summary>
        /// 获取或设置数据类型
        /// </summary>
        [DataMember]
        public string DataType { get; set; }

        /// <summary>
        /// 获取或设置默认值
        /// </summary>
        [DataMember]
        public string DefaultValue { get; set; }

        /// <summary>
        /// 获取或设置最小值
        /// </summary>
        [DataMember]
        public string MinValue { get; set; }

        /// <summary>
        /// 获取设置最大值
        /// </summary>
        [DataMember]
        public string MaxValue { get; set; }

        /// <summary>
        /// 获取或设置精度值
        /// </summary>
        [DataMember]
        public string Precision { get; set; }

        /// <summary>
        /// 获取或设置数据长度
        /// </summary>
        [DataMember]
        public int DataLength { get; set; }

        /// <summary>
        /// 获取或设置选择框词语
        /// </summary>
        [DataMember]
        public string WordCode { get; set; }

        /// <summary>
        /// 获取或设置控件类型
        /// </summary>
        [DataMember]
        public string ControlType { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapUserSettingScheme)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapUserSettingScheme>(this, obj as WapUserSettingScheme);
        }
    }
}

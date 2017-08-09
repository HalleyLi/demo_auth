using SH3H.SDK.Share;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Model
{
    /// <summary>
    /// 定义用户配置参数Dto对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/config")]
    public class WapUserSettingSchemeDto
    {
        /// <summary>
        /// 获取或设置参数编号
        /// </summary>
        [DataMember(Name = "schemeId")]
        public int SchemeId { get; set; }

        /// <summary>
        /// 获取或设置数据类型
        /// </summary>
        [DataMember(Name = "dataType")]
        public string DataType { get; set; }

        /// <summary>
        /// 获取或设置默认值
        /// </summary>
        [DataMember(Name = "defaultValue")]
        public string DefaultValue { get; set; }

        /// <summary>
        /// 获取或设置最小值
        /// </summary>
        [DataMember(Name = "minValue")]
        public string MinValue { get; set; }

        /// <summary>
        /// 获取设置最大值
        /// </summary>
        [DataMember(Name = "maxValue")]
        public string MaxValue { get; set; }

        /// <summary>
        /// 获取或设置精度值
        /// </summary>
        [DataMember(Name = "precision")]
        public string Precision { get; set; }

        /// <summary>
        /// 获取或设置数据长度
        /// </summary>
        [DataMember(Name = "dataLength")]
        public int DataLength { get; set; }

        /// <summary>
        /// 获取或设置选择框词语
        /// </summary>
        [DataMember(Name = "wordCode")]
        public string WordCode { get; set; }

        /// <summary>
        /// 获取或设置控件类型
        /// </summary>
        [DataMember(Name = "controlType")]
        public string ControlType { get; set; }

        /// <summary>
        /// 验证模型
        /// </summary>
        /// <returns>True: 验证通过; False:验证失败</returns>
        public ValidateResult Validate()
        {
            ValidateResult result = new ValidateResult();

            //数据类型不能为空
            if (string.IsNullOrEmpty(this.DataType))
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "数据类型不能为空 ");
            }
            return result;
        }

        /// <summary>
        /// DTO转化为数据库模型
        /// </summary>
        /// <returns></returns>
        public WapUserSettingScheme ToModel()
        {
            WapUserSettingScheme userSettingScheme = new WapUserSettingScheme();
            userSettingScheme.SchemeId = this.SchemeId;
            userSettingScheme.DataType = this.DataType;
            userSettingScheme.DefaultValue = this.DefaultValue;
            userSettingScheme.MinValue = this.MinValue;
            userSettingScheme.MaxValue = this.MaxValue;
            userSettingScheme.Precision = this.Precision;
            userSettingScheme.DataLength = this.DataLength;
            userSettingScheme.ControlType = this.ControlType;
            userSettingScheme.WordCode = this.WordCode;
            return userSettingScheme;
        }

        /// <summary>
        /// 数据库模型转换为DTO
        /// </summary>
        /// <param name="userSettingScheme"></param>
        /// <returns></returns>
        public static WapUserSettingSchemeDto FromModel(WapUserSettingScheme userSettingScheme)
        {
            WapUserSettingSchemeDto userSettingSchemeDto = new WapUserSettingSchemeDto();
            userSettingSchemeDto.SchemeId = userSettingScheme.SchemeId;
            userSettingSchemeDto.DataType = userSettingScheme.DataType;
            userSettingSchemeDto.DefaultValue = userSettingScheme.DefaultValue;
            userSettingSchemeDto.MinValue = userSettingScheme.MinValue;
            userSettingSchemeDto.MaxValue = userSettingScheme.MaxValue;
            userSettingSchemeDto.Precision = userSettingScheme.Precision;
            userSettingSchemeDto.DataLength = userSettingScheme.DataLength;
            userSettingSchemeDto.ControlType = userSettingScheme.ControlType;
            userSettingSchemeDto.WordCode = userSettingScheme.WordCode;
            return userSettingSchemeDto;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapUserSettingSchemeDto)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapUserSettingSchemeDto>(this, obj as WapUserSettingSchemeDto);
        }
    }
}

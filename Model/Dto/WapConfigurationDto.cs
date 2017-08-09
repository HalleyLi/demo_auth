using SH3H.WAP.Share;
using System;
using System.Runtime.Serialization;

namespace SH3H.WAP.Model.Dto
{
    /// <summary>
    /// 系统配置对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/config")]
    public class WapConfigurationDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        [DataMember(Name = "configId")]
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember(Name = "configName")]
        public string ConfigName { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [DataMember(Name = "configCode")]
        public string ConfigCode { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DataMember(Name = "configType")]
        public int ConfigType { get; set; }

        /// <summary>
        /// 配置值
        /// </summary>
        [DataMember(Name = "configValue")]
        public string ConfigValue { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [DataMember(Name = "default")]
        public string ConfigDefault { get; set; }

        /// <summary>
        /// 所属应用
        /// </summary>
        [DataMember(Name = "app")]
        public string ConfigApp { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        [DataMember(Name = "group")]
        public string ConfigGroup { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        [DataMember(Name = "active")]
        public bool ConfigState { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember(Name = "remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置参数编号
        /// </summary>
        [DataMember(Name = "schemeId")]
        public int SchemeId { get; set; }


        #region 模转
        /// <summary>
        /// 返回DTO对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static WapConfigurationDto FromModel(WapConfiguration model)
        {
            WapConfigurationDto result = new WapConfigurationDto()
            {
                ConfigApp = model.ConfigApp,
                ConfigCode = model.ConfigCode,
                ConfigDefault = model.ConfigDefault,
                ConfigGroup = model.ConfigGroup,
                ConfigName = model.ConfigName,
                ConfigState = model.ConfigState == 1,
                ConfigType = model.ConfigType,
                ConfigValue = model.ConfigValue,
                Id = model.Id,
                Remark = model.Remark,
                SchemeId = model.SchemeId
            };
            return result;
        }


        /// <summary>
        /// 以当前对象状态生成一个Model对象
        /// </summary>
        /// <returns></returns>
        public WapConfiguration ToModel()
        {
            WapConfiguration result = new WapConfiguration()
            {
                ConfigApp = this.ConfigApp,
                ConfigCode = this.ConfigCode,
                ConfigDefault = this.ConfigDefault,
                ConfigGroup = this.ConfigGroup,
                ConfigName = this.ConfigName,
                ConfigState = this.ConfigState ? 1 : 0,
                ConfigType = this.ConfigType,
                ConfigValue = this.ConfigValue,
                Id = this.Id,
                Remark = this.Remark,
                SchemeId = this.SchemeId
            };

            return result;
        }
        #endregion

        /// <summary>
        /// 验证模型
        /// </summary>
        /// <returns>True: 验证通过; False:验证失败</returns>
        public ValidateResult Validate()
        {
            ValidateResult result = new ValidateResult();

            //名字不能为空 长度不能超过50
            if (string.IsNullOrEmpty(this.ConfigName) || this.ConfigName.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "名字不能为空 长度不能超过50");
            }

            //Code不能为空 长度不能超过50
            if (string.IsNullOrEmpty(this.ConfigCode) || this.ConfigCode.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "Code不能为空 长度不能超过50");
            }

            //配置值长度不能超过100
            if (!string.IsNullOrEmpty(this.ConfigValue) && this.ConfigValue.Length > 100)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "配置值长度不能超过100");
            }

            //配置默认值不能超过100个字符
            if (!string.IsNullOrEmpty(this.ConfigDefault) && this.ConfigDefault.Length > 100)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "配置默认值不能超过100个字符");
            }

            //所属应用标识不能为空 长度不能超过20
            if (string.IsNullOrEmpty(this.ConfigApp) || this.ConfigApp.Length > 20)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "所属应用标识不能为空 长度不能超过20");
            }

            //备注不能超过500个字符
            if (!string.IsNullOrEmpty(this.Remark) && this.Remark.Length > 500)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "配置默认值不能超过500个字符");
            }

            return result;
        }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapConfigurationDto)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapConfigurationDto>(this, obj as WapConfigurationDto);
        }
    }
}

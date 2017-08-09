using SH3H.SDK.Share;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Model.Dto
{
    /// <summary>
    /// 定义用户配置对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/config")]
    public class WapUserSettingDto
    {
        /// <summary>
        /// 获取或设置用户配置编号
        /// </summary>
        [DataMember(Name = "settingId")]
        public int UserSettingId { get; set; }

        /// <summary>
        /// 获取或设置用户编号
        /// </summary>
        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        /// <summary>
        /// 获取或设置用配置编码
        /// </summary>
        [DataMember(Name = "settingCode")]
        public string UserSettingCode { get; set; }

        /// <summary>
        /// 获取或设置用户配置值
        /// </summary>
        [DataMember(Name = "settingValue")]
        public string UserSettingValue { get; set; }

        /// <summary>
        /// 获取或设置应用标识
        /// </summary>
        [DataMember(Name = "appIdentity")]
        public string AppIdentity { get; set; }

        /// <summary>
        /// 获取或设置用户配置类型
        /// </summary>
        [DataMember(Name="settingType")]
        public string UserSettingType { get; set; }

        /// <summary>
        /// 获取或设置用户配置内容
        /// </summary>
        [DataMember(Name = "settingText")]
        public string UserSettingText { get; set; }

        /// <summary>
        /// 获取或设置创建人编号
        /// </summary>
        [DataMember(Name = "creatorId")]
        public int CreatorId { get; set; }

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        [DataMember(Name = "createTime")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 获取或设置修改人编号
        /// </summary>
        [DataMember(Name = "modifierId")]
        public int ModifierId { get; set; }

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        [DataMember(Name = "modifyTime")]
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 获取或设置备注内容
        /// </summary>
        [DataMember(Name = "remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置参数编号
        /// </summary>
        [DataMember(Name = "schemeId")]
        public int SchemeId { get; set; }

        /// <summary>
        /// 获取或设置是否为组别
        /// </summary>
        [DataMember(Name = "isGroup")]
        public bool IsGroup { get; set; }

        /// <summary>
        /// 验证模型
        /// </summary>
        /// <returns>True: 验证通过; False:验证失败</returns>
        public ValidateResult Validate()
        {
            ValidateResult result = new ValidateResult();

            //用户配置编码不能为空 长度不能超过50
            if (string.IsNullOrEmpty(this.UserSettingCode) || this.UserSettingCode.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "用户配置编码不能为空 长度不能超过50");
            }

            //用户配置值不能为空 长度不能超过2000
            if (string.IsNullOrEmpty(this.UserSettingValue) || this.UserSettingValue.Length > 2000)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "用户配置值不能为空 长度不能超过2000");
            }

            //应用标识不能为空 长度不能超过50
            if (string.IsNullOrEmpty(this.AppIdentity) || this.AppIdentity.Length > 100)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "应用标识不能为空 长度不能超过100");
            }

            //用户配置类型不能为空 长度不能超过50
            if (string.IsNullOrEmpty(this.UserSettingType) || this.UserSettingType.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "用户配置类型不能为空 长度不能超过50");
            }

            //用户配置内容不能为空
            if (string.IsNullOrEmpty(this.UserSettingText))
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "用户配置内容不能为空 ");
            }

            return result;
        }

        /// <summary>
        /// DTO转化为数据库模型
        /// </summary>
        /// <returns></returns>
        public WapUserSetting ToModel()
        {
            WapUserSetting userSetting = new WapUserSetting();
            userSetting.AppIdentity = this.AppIdentity;
            userSetting.UserId = this.UserId;
            userSetting.UserSettingCode = this.UserSettingCode;
            userSetting.UserSettingId = this.UserSettingId;
            userSetting.UserSettingValue = this.UserSettingValue;
            userSetting.CreatorId = this.CreatorId;
            userSetting.CreateTime = this.CreateTime;
            userSetting.ModifierId = this.ModifierId;
            userSetting.ModifyTime = this.ModifyTime;
            userSetting.UserSettingType = this.UserSettingType;
            userSetting.UserSettingText = this.UserSettingText;
            userSetting.Remark = this.Remark;
            userSetting.SchemeId = this.SchemeId;
            userSetting.IsGroup = this.IsGroup;
            return userSetting;
        }

        /// <summary>
        /// 数据库模型转换为DTO
        /// </summary>
        /// <param name="userSetting"></param>
        /// <returns></returns>
        public static WapUserSettingDto FromModel(WapUserSetting userSetting)
        {
            WapUserSettingDto userSettingDto = new WapUserSettingDto();
            userSettingDto.UserId = userSetting.UserId;
            userSettingDto.UserSettingCode = userSetting.UserSettingCode;
            userSettingDto.UserSettingId = userSetting.UserSettingId;
            userSettingDto.UserSettingValue = userSetting.UserSettingValue;
            userSettingDto.AppIdentity = userSetting.AppIdentity;
            userSettingDto.CreatorId = userSetting.CreatorId;
            userSettingDto.CreateTime = userSetting.CreateTime;
            userSettingDto.ModifierId = userSetting.ModifierId;
            userSettingDto.ModifyTime = userSetting.ModifyTime;
            userSettingDto.UserSettingType = userSetting.UserSettingType;
            userSettingDto.UserSettingText = userSetting.UserSettingText;
            userSettingDto.Remark = userSetting.Remark;
            userSettingDto.SchemeId = userSetting.SchemeId;
            userSettingDto.IsGroup = userSetting.IsGroup;
            return userSettingDto;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapUserSettingDto)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapUserSettingDto>(this, obj as WapUserSettingDto);
        }
    }
}

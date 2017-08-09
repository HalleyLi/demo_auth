using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Model.Dto
{
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/config")]
    public class ModifyUserSettingGroupDto
    {
        /// <summary>
        /// 获取或设置用户配置编号
        /// </summary>
        [DataMember(Name = "settingId")]
        public int UserSettingId { get; set; }

        /// <summary>
        /// 获取或设置用户配置类型
        /// </summary>
        [DataMember(Name = "settingType")]
        public string UserSettingType { get; set; }

        /// <summary>
        /// 获取或设置用户配置内容
        /// </summary>
        [DataMember(Name = "settingText")]
        public string UserSettingText { get; set; }

        /// <summary>
        /// 获取或设置修改人编号
        /// </summary>
        [DataMember(Name = "modifierId")]
        public int ModifierId { get; set; }

        /// <summary>
        /// 获取或设置备注内容
        /// </summary>
        [DataMember(Name = "remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 验证模型
        /// </summary>
        /// <returns>True: 验证通过; False:验证失败</returns>
        public ValidateResult Validate()
        {
            ValidateResult result = new ValidateResult();
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
            userSetting.ModifierId = this.ModifierId;
            userSetting.UserSettingType = this.UserSettingType;
            userSetting.UserSettingText = this.UserSettingText;
            userSetting.Remark = this.Remark;
            userSetting.UserSettingId = this.UserSettingId;
            return userSetting;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapUserSettingDto)) return false;
            return SH3H.WAP.Share.Utils.EqualsObj<WapUserSettingDto>(this, obj as WapUserSettingDto);
        }
    }
}

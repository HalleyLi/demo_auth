using SH3H.SDK.Share;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Model.Dto
{    
    /// <summary>
    /// 用户密码修改传入对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/Auth")]
    public class ChangePasswordInputDto
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        [DataMember(Name = "oldPassword")]
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [DataMember(Name = "newPassword")]
        public string NewPassword { get; set; }

        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        public ValidateResult Validate()
        {
            ValidateResult result = new ValidateResult();

            if (string.IsNullOrEmpty(this.OldPassword))
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "旧密码不允许为空");
            }
            if (this.OldPassword.Length > 200)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "旧密码长度不允许超过200");
            }

            if (string.IsNullOrEmpty(this.NewPassword))
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "新密码不允许为空");
            }
            if (this.NewPassword.Length > 200)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "新密码长度不允许超过200");
            }

            return result;
        }
    }
}

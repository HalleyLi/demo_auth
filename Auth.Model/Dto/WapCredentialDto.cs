/* ============================================================
* Filename:		WapCredential
* Created:		6/18/2014 9:51:34 AM
* MachineName:  YUANJIE-01
* Author:		yuanjie@shanghai3h.com
* Description: 
* Modify:
* ============================================================*/

using SH3H.SDK.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Model.Dto
{
    /// <summary>
    /// 定义用户登陆证书对象，描述登陆用户的相关认证信息
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/Auth")]
    public class WapCredentialDto
    {
        /// <summary>
        /// 获取或设置用户主键
        /// </summary>
        [DataMember(Name = "userKey")]
        public string UserKey { get; set; }

        /// <summary>
        /// 获取或设置用户编号
        /// </summary>
        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        /// <summary>
        /// 获取或设置账户
        /// </summary>
        [DataMember(Name = "account")]
        public string Account { get; set; }

        /// <summary>
        /// 获取或设置登陆用户姓名
        /// </summary>
        [DataMember(Name = "userName")]
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置登陆用户工号
        /// </summary>
        [DataMember(Name = "jobNumber")]
        public string JobNumber { get; set; }

        /// <summary>
        /// 获取或设置用户登陆唯一识别号
        /// </summary>
        [DataMember(Name = "token")]
        public string Token { get; set; }

        /// <summary>
        /// 获取或设置用户登陆时间
        /// </summary>
        [DataMember(Name = "loginTime")]
        public DateTime LoginTime { get; set; }

        #region Override

        public override int GetHashCode()
        {
            return UserId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var cred = obj as WapCredentialDto;
            if (cred == null) return false;
            return cred.Token == this.Token;
        }

        public override string ToString()
        {
            return Token;
        }

        #endregion
    }
}

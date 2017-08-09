/* ============================================================
* Filename:		WapLogin
* Created:		7/15/2014 1:04:47 PM
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
    /// 定义用户登陆信息
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/Auth")]
    public class WapLogonDto
    {
        /// <summary>
        /// 获取或设置用户登陆密码
        /// </summary>
        [DataMember(Name = "password")]
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置登陆的应用程序标识
        /// </summary>
        [DataMember(Name = "appIdentity")]
        public string AppIdentity { get; set; }

        /// <summary>
        /// 获取或设置登录的账户
        /// </summary>
        [DataMember(Name = "account")]
        public string Account { get; set; }

        /// <summary>
        /// 获取或设置ClientKey
        /// </summary>
        [DataMember(Name = "clientKey")]
        public string ClientKey { get; set; }
    }
}

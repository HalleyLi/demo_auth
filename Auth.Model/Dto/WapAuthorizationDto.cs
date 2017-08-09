/* ============================================================
* Filename:		WapAuthFunction
* Created:		8/20/2014 11:26:55 AM
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
    /// 定义授权对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/Auth")]
    public class WapAuthorizationDto
    {
        /// <summary>
        /// 获取或设置功能点名称
        /// </summary>
        [DataMember(Name = "authName")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置功能点编码
        /// </summary>
        [DataMember(Name = "authCode")]
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置当前功能是否已经授权
        /// </summary>
        [DataMember(Name = "authorized")]
        public bool Authorised { get; set; }

        #region Override

        public override string ToString()
        {
            return string.Format("{0}({1})", Name, Code);
        }

        #endregion
    }
}

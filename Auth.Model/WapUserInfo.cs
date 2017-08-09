using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Model
{
    /// <summary>
    /// 定义用户组织信息实体
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/Auth")]
    public class WapUserInfo
    {
        /// <summary>
        /// 获取或设置用户编号
        /// </summary>        
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 获取或设置用户姓名
        /// </summary>        
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置站点名称
        /// </summary>
        [DataMember]
        public string OrganizationName { get; set; }

        /// <summary>
        /// 获取或设置站点编码
        /// </summary>
        [DataMember]
        public string OrganizationCode { get; set; }

        /// <summary>
        /// 获取或设置文件MD5哈希值
        /// </summary>
        [DataMember]
        public string FileHash { get; set; }

        /// <summary>
        /// 获取或设置直接父站点名称
        /// </summary>
        [DataMember]
        public string ParentOrganizationName { get; set; }

    }
}

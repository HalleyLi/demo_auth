using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class WapUserInfoDto
    {
        /// <summary>
        /// 获取或设置用户编号
        /// </summary>        
        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        /// <summary>
        /// 获取或设置用户姓名
        /// </summary>        
        [DataMember(Name = "userName")]
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置站点名称
        /// </summary>
        [DataMember(Name = "organizationName")]
        public string OrganizationName { get; set; }

        /// <summary>
        /// 获取或设置站点编码
        /// </summary>
        [DataMember(Name = "organizationCode")]
        public string OrganizationCode { get; set; }

        /// <summary>
        /// 获取或设置头像地址
        /// </summary>
        [DataMember(Name = "imageUrl")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 获取或设置详细站点信息
        /// </summary>
        [DataMember(Name = "organizationDetailName")]
        public string OrganizationDetailName { get; set; }
       
        /// <summary>
        /// 数据库模型转换为DTO
        /// </summary>
        /// <param name="userInfo">用户组织对象</param>
        /// <returns>用户组织输出对象</returns>
        public static WapUserInfoDto FromModel(WapUserInfo userInfo)
        {
            WapUserInfoDto userInfoDto = new WapUserInfoDto();
            userInfoDto.UserId = userInfo.UserId;
            userInfoDto.UserName = userInfo.UserName;
            userInfoDto.OrganizationName = userInfo.OrganizationName;
            userInfoDto.OrganizationCode = userInfo.OrganizationCode;
            if (userInfo.FileHash != null) 
            {
                userInfoDto.ImageUrl = ConfigurationManager.AppSettings["FileRequestRootUrl"] + "/image/" + userInfo.FileHash;
            }
            userInfoDto.OrganizationDetailName = String.Format(userInfo.ParentOrganizationName + " " + userInfo.OrganizationName).Replace("\r", "").Replace("\n", "");
            return userInfoDto;
        }
    }
}

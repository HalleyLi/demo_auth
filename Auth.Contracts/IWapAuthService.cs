using SH3H.SDK.Share;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Contracts
{
    /// <summary>
    /// 定义权限服务接口，提供系统的身份认证、权限管理的相关功能
    /// </summary>
    public interface IWapAuthService
    {
        /// <summary>
        /// 用户登出
        /// </summary>
        /// <param name="token">用户登录时返回的标识号</param>
        void Logoff(string token);
     
        /// <summary>
        /// 保持当前用户登陆状态
        /// </summary>
        /// <param name="token">用户登录时返回的标识号</param>
        /// <param name="appIdentity">应用标识</param>
        void Ping(string token, string appIdentity);

        /// <summary>
        /// 获取指定权限认证标识在指定应用的授权列表
        /// </summary>
        /// <param name="token">访问凭证</param>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>功能点权限列表</returns>
        IEnumerable<WapAuthorizationDto> GetAuthorizations(string token, string appIdentity);

        /// <summary>
        /// 获取指定用户的授权列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>返回当前用户的授权列表</returns>
        IEnumerable<WapAuthorizationDto> GetAuthorizations(int userId, string appIdentity);

        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="password">密码</param>
        /// <param name="appIdentity">应用标识</param
        /// <param name="clientKey"></param>
        /// <returns></returns>
        WapCredentialDto Logon(string account, string password, string appIdentity, string clientKey);

        /// <summary>
        /// 检测密码强度
        /// 强度定义：0 字符串不符合长度要求；1 弱强度；2 中强度；3 高强度
        /// </summary>
        /// <param name="password">密码字符串</param>
        /// <returns></returns>
        int CheckPasswordStrength(string password);

    }
}

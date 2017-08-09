using Newtonsoft.Json;
using SH3H.SDK.Definition.Exceptions;
using SH3H.SDK.Share;
using SH3H.SDK.WebApi.Controllers;
using SH3H.SDK.WebApi.Core;
using SH3H.SDK.WebApi.Core.Models;
using SH3H.SharpFrame;
using SH3H.WAP.Auth.Contracts;
using SH3H.WAP.Auth.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SH3H.WAP.Auth.Controllers
{
    /// <summary>
    /// 定义WAP权限认证服务控制器
    /// </summary>
    [Resource("wapAuthRes")]
    [RoutePrefix("wap/v2/auth")]
    public class WapAuthController
        : BaseController<IWapAuthService>
    {

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="logon">用户登录信息对象</param>
        /// <returns>返回登陆后的用户标识对象</returns>
        [HttpPost]
        [Route("logon")]
        [ActionName("logon")]
        public WapResponse<WapCredentialDto> Logon([FromBody]WapLogonDto logon)
        {
            var credential = Service.Logon(logon.Account, logon.Password, logon.AppIdentity, logon.ClientKey);
            return new WapResponse<WapCredentialDto>(credential);
        }

        /// <summary>
        /// 用户注销
        /// </summary>
        /// <returns>返回成功或失败</returns>
        [HttpPost]
        [Route("logoff")]
        [ActionName("logoff")]
        public WapBoolean Logoff()
        {
            var token = CheckToken();
            Service.Logoff(token);
            return new WapBoolean(true);
        }

        /// <summary>
        /// 应用ping连接
        /// </summary>        
        /// <param name="appIdentity">应用标识</param>
        /// <returns>返回成功或失败</returns>
        [HttpGet]
        [Route("ping")]
        [ActionName("ping")]
        public WapBoolean Ping(string appIdentity)
        {
            var token = CheckToken();
            Service.Ping(token, appIdentity);
            return new WapBoolean(true);
        }   

        /// <summary>
        /// 获取当前用户在指定应用的授权列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>返回当前用户的授权列表</returns>
        [HttpGet]
        [Route("authorizations")]
        [ActionName("getAuthorizations")]
        public WapCollection<WapAuthorizationDto> GetAuthorizations(string appIdentity)
        {
            var token = CheckToken();
            var authorizations = Service.GetAuthorizations(token, appIdentity);
            return new WapCollection<WapAuthorizationDto>(authorizations);
        }

        /// <summary>
        /// 检测密码强度
        /// 强度定义：0 字符串不符合长度或格式要求；1 弱强度；2 中强度；3 高强度；9 字符串符合要求
        /// </summary>
        /// <param name="password">密码字符串</param>
        /// <returns></returns>
        [HttpPost]
        [Route("password/strength")]
        [ActionName("checkPasswordStrength")]
        public WapInt32 CheckPasswordStrength([FromBody]string password)
        {
            var rank = Service.CheckPasswordStrength(password);
            return new WapInt32(rank);
        }

        private string CheckToken()
        {
            var token = base.GetToken();
            if (Guard.IsNullOrEmpty(token))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ACCESS_TOKEN_INVALID, "非法的访问"); 
            }

            return token;
        }
    }
}
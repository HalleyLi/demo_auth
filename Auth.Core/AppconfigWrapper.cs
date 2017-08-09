using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Core
{
    public class AppconfigWrapper
    {
        /// <summary>
        /// loop ticket online interval,
        /// </summary>
        public static int DetectTicketOnlineInterval
        {
            get { return AppconfigWrapperCreator.detectTicketOnlineInterval; }
        }

        /// <summary>
        /// 域帐号，默认具有的角色
        /// </summary>
        public static string DomainAccountDefaultRoles
        {
            get { return AppconfigWrapperCreator.domainAccountDefaultRoles; }
        }

        /// <summary>
        /// 是否允许域帐号登录
        /// </summary>
        public static bool AllowDomainAccountLogin
        {
            get { return AppconfigWrapperCreator.allowDomainAccountLogin; }
        }

        public static string BitAnswerSN
        {
            get { return AppconfigWrapperCreator._bitAnswerSN; }
        }

        /// <summary>
        /// 域名称
        /// </summary>
        public static string DomainName
        {
            get { return AppconfigWrapperCreator.domainName; }
        }

        /// <summary>
        /// 域服务器地址
        /// </summary>        
        public static string DomainPath
        {
            get { return AppconfigWrapperCreator.domainPath; }
        }

        /// <summary>
        /// 用户密码匹配模式
        /// </summary>
        public static string PasswordPattern
        {
            get { return AppconfigWrapperCreator.passwordPattern; }
        }

        internal class AppconfigWrapperCreator
        {
            internal static readonly int detectTicketOnlineInterval = 30;//秒
            internal static string domainAccountDefaultRoles;
            internal static bool allowDomainAccountLogin = false;
            internal static string domainName = "";
            internal static string _bitAnswerSN = "";
            internal static string domainPath = "";
            internal static string passwordPattern = "";

            static AppconfigWrapperCreator()
            {
                var appSettings = ConfigurationManager.AppSettings;
                var authSettings = (NameValueCollection)ConfigurationManager.GetSection("authSettings");
                string[] keys = appSettings.AllKeys;
                string[] authKeys = authSettings.AllKeys;

                string key = "detect-ticketonline-interval";
                if (keys.Contains(key))
                {
                    int.TryParse(appSettings[key], out detectTicketOnlineInterval);
                }
                else if(authKeys.Contains(key))
                {
                    int.TryParse(authSettings[key], out detectTicketOnlineInterval);
                }

                key = "domainaccout-default-roles";
                if (keys.Contains(key))
                {
                    domainAccountDefaultRoles = appSettings[key];
                }
                else if (authKeys.Contains(key))
                {
                    domainAccountDefaultRoles = authSettings[key];
                }

                key = "allow-domain-account-login";
                if (keys.Contains(key))
                {
                    bool.TryParse(appSettings[key], out allowDomainAccountLogin);
                }
                else if (authKeys.Contains(key))
                {
                    bool.TryParse(authSettings[key], out allowDomainAccountLogin);
                }

                key = "domain-name";
                if (keys.Contains(key))
                {
                    domainName = appSettings[key];
                }
                else if (authKeys.Contains(key))
                {
                    domainName = authSettings[key]; 
                }

                key = "bitanswer-sn";
                if (keys.Contains(key))
                {
                    _bitAnswerSN = appSettings[key];
                }
                else if (authKeys.Contains(key))
                {
                    _bitAnswerSN = authSettings[key];
                }

                key = "domain-path";
                if (keys.Contains(key))
                {
                    domainPath = appSettings[key];
                }
                else if (authKeys.Contains(key))
                {
                    domainPath = authSettings[key];
                }

                key = "password-pattern";
                if (keys.Contains(key))
                {
                    passwordPattern = appSettings[key];
                }
                else if (authKeys.Contains(key))
                {
                    passwordPattern = authSettings[key];
                }


            }
        }
    }
}

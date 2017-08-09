using SH3H.SDK.Infrastructure.Logging;
using SH3H.SharpFrame;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;
using SH3H.WAP.Auth.Model.Dto;
using SH3H.WAP.Auth.Contracts;
using SH3H.WAP.Auth.Model;
using System.Xml.Linq;
using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Auth.DataAccess.Repo;
using System.Threading;
using SH3H.WAP.Share;
using SH3H.WAP.Auth.Core;
using SH3H.SDK.Service.Core;
using System.Text.RegularExpressions;


namespace SH3H.WAP.Auth.Service
{
    /// <summary>
    /// 定义权限服务实现
    /// </summary>
    public class WapAuthServiceImpl : BaseService, IWapAuthService
    {
        /// <summary>
        /// 用户登出
        /// </summary>
        /// <param name="token">权限认证标识</param>
        public void Logoff(string token)
        {
            try
            {
                WapUser user = TicketContainer.Instance.GetUserByTicket(token);
                if (user != null)
                {
                    this.AddAudit(user.UserKey, "Logoff",
                        string.Format("{0}登出", user.Account));
                }
                TicketContainer.Instance.RemoveTicket(token);
            }
            catch(Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "未知异常");
            }
        }

        /// <summary>
        /// 客户端与服务端间心跳
        /// </summary>
        /// <param name="token">权限认证标识</param>
        /// <param name="appIdentity">应用程序标识</param>
        /// <returns></returns>
        public void Ping(string token, string appIdentity)
        {
            try
            {
                TicketContainer.Instance.PingTicket(token, appIdentity);
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "未知异常");
            }
        }

        /// <summary>
        /// 获取指定权限认证标识在指定应用的授权列表
        /// </summary>
        /// <param name="token">权限认证标识</param>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>功能点权限列表</returns>
        public IEnumerable<WapAuthorizationDto> GetAuthorizations(string token, string appIdentity)
        {
            //参数验证
            if (Guard.IsNullOrEmpty(token))
            {
                throw new WapException(StateCode.CODE_ARGUMENT_NULL, "权限认证标识不能为空");
            }
            if (Guard.IsNullOrEmpty(appIdentity))
            {
                throw new WapException(StateCode.CODE_ARGUMENT_NULL, "应用标识不能为空");
            }

            try
            {
                var user = this.GetUserByToken(token);
                return GetAuthorizations(user.Id, appIdentity);
            }
            catch(WapException wapEx)
            {
                throw;
            }
            catch(Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "未知异常");
            }
        }

        /// <summary>
        /// 获取功能点权限列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>功能点权限列表</returns>
        public IEnumerable<WapAuthorizationDto> GetAuthorizations(int userId, string appIdentity)
        {
            //参数验证
            if (Guard.IsNull(userId))
            {
                throw new WapException(StateCode.CODE_ARGUMENT_NULL, "用户编号不能为空");
            }
            if (Guard.IsNullOrEmpty(appIdentity))
            {
                throw new WapException(StateCode.CODE_ARGUMENT_NULL, "应用标识不能为空");
            }

            try
            {
                var funcRepo = new WapFunctionAllRepository();
                var userRepo = new WapUserRepository();
                var roleRepo = new WapRoleRepository();
                var roleKeys = new List<string>();
                var user = userRepo.GetUserByUserId(userId);
                var roles = roleRepo.GetRolesByUserId(userId);
                foreach (var role in roles)
                {
                    roleKeys.Add(role.RoleKey);
                }
                var appFuncs = funcRepo.GetFunctionAllsByAppIdentity(appIdentity);
                var userFuncs = funcRepo.GetFunctionAllsByRoles(roleKeys);

                var funcAlls = new List<WapFunctionAllDto>();
                foreach (var appFunc in appFuncs)
                {
                    var funcAll = userFuncs.Where(u => u.Key == appFunc.Key).FirstOrDefault();
                    if (funcAll != null)
                    {
                        funcAlls.Add(funcAll);
                    }
                }

                var authoritions = new List<WapAuthorizationDto>();
                foreach (var auth in funcAlls)
                {
                    var authorition = new WapAuthorizationDto();
                    authorition.Code = auth.Code;
                    authorition.Name = auth.Name;
                    authorition.Authorised = (bool)auth.Active;
                    authoritions.Add(authorition);
                }
                return authoritions;
            }
            catch(WapException wapEx)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "未知异常");
            }
        }


        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <param name="appIdentity">应用标识</param>
        /// <param name="clientKey">clientKey</param>
        /// <returns>用户登陆证书对象</returns>
        public WapCredentialDto Logon(string account, string password, string appIdentity, string clientKey)
        {
            //参数验证
            if (Guard.IsNullOrEmpty(account))
            {
                throw new WapException(StateCode.CODE_ARGUMENT_NULL, "用户账号不能为空");
            }
            if (Guard.IsNullOrEmpty(password))
            {
                throw new WapException(StateCode.CODE_ARGUMENT_NULL, "用户密码不能为空");
            }
            if (Guard.IsNullOrEmpty(appIdentity))
            {
                throw new WapException(StateCode.CODE_ARGUMENT_NULL, "应用标识不能为空");
            }

#if !DEBUG
            if (LogOnMessage.HasRun != true)
            {
                LogOnBitAnswer();
            }
#endif
            if (LogOnMessage.HasRun == true && LogOnMessage.HasSuccess == false)
            {
                LogManager.Get().Error(LogOnMessage.ErrorMessage);
                throw new WapException(StateCode.CODE_BITANSWER_FAILED, "系统认证失败");
            }

            WapUser user = new WapUser();
            int errorCode = 0;

            try
            {
                //平台用户验证登录
                bool blnAccountCheck = ValidateAccount(account, password, out user, out errorCode);
                if (!blnAccountCheck)
                {
                    // 如果允许进行域认证
                    if (AppconfigWrapper.AllowDomainAccountLogin)
                    {
                        string domainName = null;
                        bool blnDomainAccountCheck = ValidateDomainAccount(account, password, out domainName);
                        if (blnDomainAccountCheck)
                        {
                            // 为域用户创建新账户
                            bool bln = TryGenerateDomainAccount(account, password, domainName, out user);
                            if (!bln)
                            {
                                throw new WapException(StateCode.CODE_USERNAME_PASSWORD_ERROR, "创建域用户失败");
                            }
                        }
                        else
                        {
                            throw new WapException(StateCode.CODE_USERNAME_PASSWORD_ERROR, "验证域账户失败");
                        }
                    }
                    else
                    {
                        throw new WapException(errorCode, "验证账户失败");
                    }
                }

                //应用程序validate
                WapApp app = null;
                var blnCheck = ValidateUserAccessApp(user.UserKey, appIdentity, out app, out errorCode);
                if (!blnCheck && errorCode != 0)
                {
                    this.AddAudit(user.UserKey, "Logon",
                        string.Format("{0}登录失败，用户无访问应用程序权限。", user.Account));
                    throw new WapException(errorCode, "用户无访问应用程序权限");
                }

                //当前帐号对应的用户是否已经登录 app 
                var appExInfo = ApplicationExWrapper.AppExList.FirstOrDefault(p => string.CompareOrdinal(p.AppIdentity, appIdentity) == 0);
                if (appExInfo != null && !appExInfo.AllowLoginElseWhere)
                {
                    var appIdentitys = TicketContainer.Instance.GetLogOnApps(account);
                    if (appIdentitys.FirstOrDefault(p => string.CompareOrdinal(p, appIdentity) == 0) != null)
                    {
                        this.AddAudit(user.UserKey, "Logon",
                            string.Format("{0}登录失败，用户已在异地登录。", user.Account));
                        throw new WapException(StateCode.CODE_ACCESS_USER_EXIST, "用户已在异地登录");
                    }
                }

                //当前在线用户数量
                int onlineUserNumber = TicketContainer.Instance.GetAppOnlineUsers(appIdentity).Count;
                if (appExInfo != null && appExInfo.MaxOnlineUserNumber > 0)
                {
                    if (onlineUserNumber + 1 > appExInfo.MaxOnlineUserNumber)
                    {
                        this.AddAudit(user.UserKey, "Logon",
                            string.Format("{0}登录失败，当前登录用户数已达最大值。", user.Account));
                        throw new WapException(StateCode.CODE_ACCESS_USER_MAX, "当前登录用户数已达最大值");
                    }
                }

                //生成token
                Random rand = new Random();
                int start = rand.Next(0, 9);
                int length = rand.Next(10, 20);
                string ticket = string.Format("{0}{1}{2}{3}{4}{5}", Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid().ToString().Substring(start, length), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
                ticket = ticket.Replace("-", string.Empty);
                TicketContainer.Instance.AddTicket(ticket, app, clientKey, user);

                var credential = new WapCredentialDto();
                credential.Token = ticket;
                credential.UserKey = user.UserKey;
                credential.JobNumber = user.JobNumber;
                credential.UserName = user.Name;
                credential.UserId = user.Id;
                credential.Account = user.Account;
                credential.LoginTime = DateTime.Now;
                return credential;
            }
            catch(WapException wapEx)
            {
                throw;
            }
            catch(Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "未知异常");
            }
        }

        /// <summary>
        /// 检测密码强度
        /// 强度定义：0 字符串不符合长度或格式要求；1 弱强度；2 中强度；3 高强度；9 字符串符合要求
        /// </summary>
        /// <param name="password">密码字符串</param>
        /// <returns></returns>
        public int CheckPasswordStrength(string password)
        {
            var rank = 0;

            try
            {
                var pattern = AppconfigWrapper.PasswordPattern;
                string[] seprator = new string[] { " | " };
                string[] rankPatterns = pattern.Split(seprator, StringSplitOptions.RemoveEmptyEntries);
                if (rankPatterns.Length == 1)
                {
                    if (Regex.IsMatch(password, rankPatterns[0])) rank = 9;
                }
                else
                {
                    if (Regex.IsMatch(password, rankPatterns[0])) rank = 1;
                    if (Regex.IsMatch(password, rankPatterns[1])) rank = 2;
                    if (Regex.IsMatch(password, rankPatterns[2])) rank = 3;
                }
            }
            catch(Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "未知异常");
            }

            return rank;
        }
        
         /// <summary>
        /// 根据token获取用户对象
        /// </summary>
        /// <param name="token">用户认证标识</param>
        /// <returns>用户对象</returns>
        private WapUser GetUserByToken(string token)
        {
            var user = TicketContainer.Instance.GetUserByTicket(token);
            return user;
        }

        /// <summary>
        /// bitAnswer验证
        /// </summary>
        private void LogOnBitAnswer()
        {
            LogOnMessage.HasRun = true;
            try
            {
                string bitAnswerSN = AppconfigWrapper.BitAnswerSN;

                if (string.IsNullOrEmpty(bitAnswerSN))
                {
                    bitAnswerSN = Guid.NewGuid().ToString();
                    LogOnMessage.BitAnswerSn = bitAnswerSN;
                }
                LogOnMessage._bitAnswer.Login(null, bitAnswerSN, LoginMode.Local);
                LogOnMessage.HasSuccess = true;
            }
            catch (BitAnswerException ex)
            {
                LogOnMessage.ErrorCode = ex.ErrorCode;
                LogOnMessage.HasSuccess = false;
                LogOnMessage.ErrorMessage = "系统授权失败：" + ex.Message;
            }
        }

        /// <summary>
        /// 验证账户
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="password">密码</param>
        /// <param name="user">用户</param>
        /// <param name="errorCode">错误代码</param>
        /// <returns></returns>
        private bool ValidateAccount(string account, string password, out WapUser user, out int errorCode)
        {
            errorCode = 0;
            WapUserRepository userRepo = new WapUserRepository();
            user = userRepo.GetUserByAccount(account).FirstOrDefault();
            if (user == null)
            {
                errorCode = StateCode.CODE_ACCOUNT_INEXIST;
                return false;
            }

            //帐号区分大小写
            if (string.CompareOrdinal(account, user.Account) != 0)
            {
                errorCode = StateCode.CODE_ACCOUNT_INEXIST;
                return false;
            }

            //帐号是否在用
            if (user.Active == null || !user.Active.Value)
            {
                errorCode = StateCode.CODE_ACCOUNT_UNUSE;
                return false;
            }

            // 验证用户密码
            string encryptPassword = SH3H.WAP.Share.Utils.GetMD5(password);
            if (string.Compare(encryptPassword, user.Password, true) != 0)
            {
                errorCode = StateCode.CODE_PASSWORD_ERROR;
                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证域账户
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="pwd">密码</param>
        /// <param name="name">用户名称</param>
        /// <returns></returns>
        private bool ValidateDomainAccount(string account, string pwd, out string name)
        {
            name = null;
            try
            {
                LdapAuthentication ldap = new LdapAuthentication(AppconfigWrapper.DomainPath);
                var authenticated = ldap.IsAuthenticated(AppconfigWrapper.DomainName, account, pwd);
                if (authenticated) name = ldap.Cn;
                return authenticated;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 获取域账户
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="password">密码</param>
        /// <param name="name">用户名称</param>
        /// <param name="recUser">新生成的用户</param>
        /// <returns></returns>
        private bool TryGenerateDomainAccount(string account, string password, string name, out WapUser recUser)
        {
            bool blnGenerateSuccess = true;
            recUser = null;
            var encrtyPassword = Utils.GetMD5(password);

            //生成域用户实体
            WapAuthSeqRepository seqRepo = new WapAuthSeqRepository();
            var seq = seqRepo.CreateSequence("user");
            WapUser user = new WapUser();
            user.UserKey = seq.IdentityKey;
            user.SortSn = seq.Sn;
            user.Id = seq.Sn;
            user.Name = name;
            user.Account = account;
            user.Password = encrtyPassword;
            user.Comment = "域帐户生成";
            user.DomainAccount = account;
            user.JobNumber = "";
            user.Active = true;
            user.Code = GetPyCode(name);
            try
            {
                WapUserRepository appRepo = new WapUserRepository();
                recUser = appRepo.AddUser(user);

                // 获取指定角色的RoleKey
                WapRoleRepository roleRepo = new WapRoleRepository();
                var role = roleRepo.GetRoleByRoleName(AppconfigWrapper.DomainAccountDefaultRoles);
                if (role != null)
                {
                    var array = new RoleUserRelation[1];
                    array[0] = new RoleUserRelation();
                    array[0].RelationKey = Guid.NewGuid().ToString();
                    array[0].RoleKey = role.RoleKey;
                    array[0].UserKey = user.UserKey;
                    WapUserRepository userRepo = new WapUserRepository();
                    userRepo.ModifyRoleUserRelation(null, array);
                }
            }
            catch
            {
                blnGenerateSuccess = false;
            }

            return blnGenerateSuccess;
        }

        /// <summary>
        /// 验证用户是否有访问某个应用程序的权限
        /// </summary>
        /// <param name="userKey">用户KEY</param>
        /// <param name="appIdentity">应用标识</param>
        /// <param name="errorCode">错误代码</param>
        /// <returns></returns>
        private bool ValidateUserAccessApp(string userKey, string appIdentity, out WapApp app, out int errorCode)
        {
            errorCode = 0;
            WapAppRepository repo = new WapAppRepository();
            var allApps = repo.GetAllApps();
            app = allApps.FirstOrDefault(p => string.CompareOrdinal(p.AppIdentity, appIdentity) == 0);
            if (app == null)
            {
                errorCode = StateCode.CODE_APP_INEXIST; ;
                return false;
            }

            if (!app.Active.HasValue || !app.Active.Value)
            {
                errorCode = StateCode.CODE_APP_UNUSE;
                return false;
            }

            var userappArr = repo.GetAppByUserKey(userKey);
            var userapp = userappArr.FirstOrDefault(p => string.CompareOrdinal(p.AppIdentity, appIdentity) == 0);
            if (userapp == null)
            {
                errorCode = StateCode.CODE_APP_ACCESS_INVALID;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取拼音码
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="maxPylength">返回拼音简码的最大长度</param>
        /// <returns></returns>
        private string GetPyCode(string text, int maxPylength = 6)
        {
            if (string.IsNullOrEmpty(text)) return "";

            string firstPycode = "";
            try
            {
                firstPycode = SpellCodeHelper.GetFirstPYCode(text);
                if (firstPycode.Length > maxPylength)
                {
                    firstPycode = firstPycode.Substring(0, maxPylength);
                }
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
            return firstPycode.ToUpper();
        }

        private void AddAudit(string key, string actionName, string content)
        {
            var auditRepo = new WapAuthAuditRepository();
            //记录数据审计日志
            ThreadPool.QueueUserWorkItem(p =>
            {
                var audit = new WapAuthAudit();
                audit.TrackingGuid = key;
                audit.OperateFunc = actionName;
                audit.OperateContent = content;
                auditRepo.AddAudit(audit);
            });
        }

    }
}

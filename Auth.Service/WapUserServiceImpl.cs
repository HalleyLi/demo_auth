using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SH3H.SDK.Definition.Exceptions;
using SH3H.SDK.Service.Core;
using SH3H.WAP.Auth.Contracts;
using SH3H.WAP.Auth.Core;
using SH3H.WAP.Auth.DataAccess.Repo;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Service
{
    /// <summary>
    /// 用户服务实现
    /// </summary>
    public class WapUserServiceImpl : BaseService, IWapUserService
    {
        private const string InitialPassWord = "0000";

        private IWapUserRepository _userRepository = null;
        private IAuthSeqRepository _authSeqRepository = null;
        private IWapAuthAuditRepository _authAuditRepository = null;      

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userRepository">IWapUserRepository接口</param>
        /// <param name="authSeqRespository">IAuthSeqRepository接口</param>
        /// <param name="authAuditRepository">IWapAuthAuditRepository接口</param>
        public WapUserServiceImpl(IWapUserRepository userRepository, IAuthSeqRepository authSeqRespository, IWapAuthAuditRepository authAuditRepository)
        {
            this._userRepository = userRepository;
            this._authSeqRepository = authSeqRespository;
            this._authAuditRepository = authAuditRepository;           
        }


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns>用户对象</returns>
        public WapUserDto AddUser(WapUserInDto user)
        {
            if (user == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户对象不允许为空");
            }

            var validateResult = user.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            
            try
            {
                WapUser model = WapUserInDto.ToModel(user);
                if (model == null)
                {
                    return null;
                }

                model.UserKey = "";
                SaveCheck(model);

                //初始化数据                
                var seq = this._authSeqRepository.CreateSequence("user");
                model.UserKey = seq.IdentityKey;
                model.Code = string.IsNullOrEmpty(model.Code) ? this.GetPyCode(model.Name) : model.Code;
                model.Active = model.Active;
                model.SortSn = seq.Sn;
                model.Id = seq.Sn;
                model.Password = string.IsNullOrEmpty(user.Password) ? SH3H.WAP.Share.Utils.GetMD5(InitialPassWord) : SH3H.WAP.Share.Utils.GetMD5(user.Password);
                model.DomainAccount = string.IsNullOrEmpty(model.DomainAccount) ? string.Empty : model.DomainAccount;
                
                var ret = this._userRepository.AddUser(model);
                if (ret != null)
                {
                    //记录数据审计日志
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        var audit = new WapAuthAudit();
                        audit.TrackingGuid = ret.UserKey;
                        audit.OperateFunc = "AddUser";
                        audit.OperateContent = user.ToString();                        
                        this._authAuditRepository.AddAudit(audit);
                    });
                }
                return WapUserDto.FromModel(ret);
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userKey">KEY</param>
        /// <param name="user">用户对象</param>
        /// <returns>是否修改成功</returns>
        public WapUserDto ModifyUser(string userKey, WapUserInDto user)
        {
            if (string.IsNullOrEmpty(userKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "userKey不允许为空");
            }

            if (!Utils.IsGuid(userKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, "userKey不是Guid");
            }

            if (user == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户更新对象不允许为空");
            }

            var validateResult = user.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }

            try
            {
                WapUser model = WapUserInDto.ToModel(user);
                model.Code = string.IsNullOrEmpty(model.Code) ? this.GetPyCode(model.Name) : model.Code;
                model.UserKey = userKey;

                var ret = this._userRepository.ModifyUser(userKey, model);
                if (ret != null)
                {
                    //记录数据审计日志
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        var audit = new WapAuthAudit();
                        audit.TrackingGuid = model.UserKey;
                        audit.OperateFunc = "ModifyUser";
                        audit.OperateContent = "修改用户信息";
                        this._authAuditRepository.AddAudit(audit);
                    });
                }
                return WapUserDto.FromModel(ret);
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }

        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns>用户信息列表</returns>
        public IEnumerable<WapUserDto> GetAllUsers()
        {
            try
            {
                IEnumerable<WapUserDto> dtos = WapUserDto.FromModel(this._userRepository.GetAllUsers());

                return dtos;
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 根据用户标识获取用户信息
        /// </summary>
        /// <param name="userKey">用户标识</param>
        /// <returns>返回用户对象，如果用户不存在则返回NULL</returns>
        public WapUserDto GetUserByUserKey(string userKey)
        {
            if (string.IsNullOrEmpty(userKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "userKey不允许为空");
            }

            if (!Utils.IsGuid(userKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, "userKey不是Guid");
            }

            try
            {
                WapUserDto dto = WapUserDto.FromModel(this._userRepository.GetUserByUserKey(userKey));

                return dto;
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 根据用户编号获取用户信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回用户对象，如果用户不存在则返回NULL</returns>
        public WapUserDto GetUserByUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_USER_UNKNOWN, "未知的用户");
            }

            try
            {
                WapUserDto dto = WapUserDto.FromModel(this._userRepository.GetUserByUserId(userId));

                return dto;
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 根据token值获取用户信息
        /// </summary>
        /// <param name="token">权限token</param>
        /// <returns>返回用户信息对象.</returns>
        public WapUserDto GetUserByToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "token不允许为空");
            }

            try
            {
                WapUser model = TicketContainer.Instance.GetUserByTicket(token);
                WapUserDto dto = WapUserDto.FromModel(model);

                return dto;
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 根据角色KEY获取用户对象
        /// </summary>
        /// <param name="roleKey">角色KEY</param>
        /// <returns>用户对象列表</returns>
        public IEnumerable<WapUserDto> GetUsersByRoleKey(string roleKey)
        {
            if (string.IsNullOrEmpty(roleKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "roleKey不允许为空");
            }

            if (!Utils.IsGuid(roleKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, "roleKey不是Guid");
            }

            try
            {
                IEnumerable<WapUserDto> dtos = WapUserDto.FromModel(this._userRepository.GetUsersByRoleKey(roleKey));

                return dtos;
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userKey">用户KEY</param>
        /// <param name="reset">是否重置，重置则将密码设为"0000",否则修改密码</param>
        /// <param name="dto">新旧密码信息</param>
        /// <returns>修改成功返回true.否则返回false.</returns>       
        public bool ChangePassword(string userKey, bool reset, ChangePasswordInputDto dto)
        {
            if (string.IsNullOrEmpty(userKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "userKey不允许为空");
            }

            if (!Utils.IsGuid(userKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, "userKey不是Guid");
            }

            if (dto == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "密码对象不允许为空");
            }

            var validateResult = dto.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }

            try
            {               
                dto.OldPassword = SH3H.WAP.Share.Utils.GetMD5(dto.OldPassword); ;
                dto.NewPassword = SH3H.WAP.Share.Utils.GetMD5(dto.NewPassword); ;
                var ret = this._userRepository.UpdatePassword(userKey, reset, dto);
                if (ret)
                {
                    //记录数据审计日志
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        var audit = new WapAuthAudit();
                        audit.TrackingGuid = userKey;
                        audit.OperateFunc = "changePassword";
                        audit.OperateContent = "修改密码";
                        this._authAuditRepository.AddAudit(audit);
                    });
                }
                return ret;
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 根据userKey修改用户状态
        /// </summary>
        /// <param name="userKey">用户Key</param>
        /// <param name="state">用户状态信息</param>
        /// <returns>更新成功返回true.否则返回false.</returns>
        public bool UpdateUserStateByUserKey(string userKey, WapUserState state)
        {
            if (string.IsNullOrEmpty(userKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "userKey不允许为空");
            }

            if (!Utils.IsGuid(userKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, "userKey不是Guid");
            }

            if (state == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户状态对象不允许为空");
            }

            try
            {
                return this._userRepository.UpdateUserStateByUserKey(userKey, state);
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 根据组织KEY获取指定的用户
        /// </summary>
        /// <param name="organizationKey">组织KEY</param>
        /// <returns>返回用户列表.</returns>
        public IEnumerable<WapUserDto> GetUserByOrganizationKey(string organizationKey)
        {
            if (string.IsNullOrEmpty(organizationKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "organizationKey允许为空");
            }

            if (!Utils.IsGuid(organizationKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, "userKey不是Guid");
            }

            try
            {
                IEnumerable<WapUserDto> dtos = WapUserDto.FromModel(this._userRepository.SelectUserByOrganizationKey(organizationKey));

                return dtos;
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 获取拼音码
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="maxPylength">返回拼音简码的最大长度</param>
        /// <returns></returns>
        protected string GetPyCode(string text, int maxPylength = 6)
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
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
            return firstPycode.ToUpper();
        }

        /// <summary>
        ///用户检验
        /// </summary>
        /// <param name="user">用户对象</param>
        private void SaveCheck(WapUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("The parameter user can not be null.");
            }

            if (string.IsNullOrEmpty(user.Name))
            {
                throw new WapException(StateCode.CODE_USERNAME_ERROR, "用户名错误");
            }

            if (string.IsNullOrEmpty(user.Account))
            {
                throw new WapException(StateCode.CODE_ACCOUNT_INEXIST, "账号不存在");
            }

            bool blnExist = false;
            blnExist = this._userRepository.IsNameExist(user.UserKey, user.Name);
            if (blnExist)
            {
                throw new WapException(StateCode.CODE_USER_UNKNOWN, "未知的用户");
            }

            if (!string.IsNullOrEmpty(user.Account))
            {
                blnExist = this._userRepository.IsAccountExist(user.UserKey, user.Account);
                if (blnExist)
                {
                    throw new WapException(StateCode.CODE_ACCOUNT_INEXIST, "账号不存在");
                }
            }

            if (!string.IsNullOrEmpty(user.JobNumber))
            {
                blnExist = this._userRepository.IsWorknoExist(user.UserKey, user.JobNumber);
                if (blnExist)
                {
                    throw new WapException(StateCode.CODE_WORKNO_INEXIST, "工号不存在");
                }
            }
        }




        ///// <summary>
        ///// 重置用户密码
        ///// </summary>
        ///// <param name="userKey">用户KEY</param>
        ///// <returns>是否重置成功</returns>
        //public bool ResetPassword(string userKey)
        //{
        //    WapUserRepository repo = new WapUserRepository();
        //    var user = repo.GetUserByUserKey(userKey).FirstOrDefault();
        //    if (user == null)
        //        throw new WapException(305);

        //    //该密码应该是 0000经过前段加密过后的值
        //    string initPwd = InitialPassWord;
        //    user.UserKey = userKey;
        //    user.Password = MD5Encrypt.GetMD5(initPwd);
        //    var ret = repo.ModifyUser(user);
        //    if (ret != null)
        //    {
        //        //记录数据审计日志
        //        ThreadPool.QueueUserWorkItem(p =>
        //        {
        //            var audit = new WapAuthAudit();
        //            audit.TrackingGuid = userKey;
        //            audit.OperateFunc = "ResetPassword";
        //            audit.OperateContent = "重置密码";
        //            var auditRepo = new WapAuthAuditRepository();
        //            auditRepo.AddAudit(audit);
        //        });
        //    }
        //    return ret;
        //}

        /// <summary>
        /// 根据账号获取用户信息
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns>用户对象</returns>
        public WapUser GetUserByAccount(string account)
        {
            return this._userRepository.GetUserByAccount(account).FirstOrDefault();
        }

        #region 私有方法

        private string CheckUrl(string url1)
        {
            if (string.IsNullOrEmpty(url1))
            {
                return "";
            }
            if (url1.EndsWith("/"))
            {
                return url1;
            }
            else
            {
                return url1 + "/";
            }
        }

        #endregion
    }
}

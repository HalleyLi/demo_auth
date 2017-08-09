using SH3H.SDK.Definition.Exceptions;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.SDK.Service.Core;
using SH3H.WAP.Auth.Contracts;
using SH3H.WAP.Auth.DataAccess.Repo;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Service
{
    /// <summary>
    /// WAP权限认证应用服务实现
    /// </summary>
    public class WapAppServiceImpl : BaseService, IWapAppService
    {
        private IWapAppRepository _appResponsitory = null;
        private IAuthSeqRepository _authSeqRepository = null;
        private IWapAuthAuditRepository _authAuditRepository = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="appResponsitory">IWapAppRepository接口<</param>
        /// <param name="authSeqRespository">IAuthSeqRepository接口<</param>
        /// <param name="authAuditRepository">IWapAuthAuditRepository接口<</param>
        public WapAppServiceImpl(IWapAppRepository appResponsitory,IAuthSeqRepository authSeqRespository,IWapAuthAuditRepository authAuditRepository)
        {
            _appResponsitory = appResponsitory;
            _authSeqRepository = authSeqRespository;
            _authAuditRepository = authAuditRepository;
        }

        /// <summary>
        /// 添加应用
        /// </summary>
        /// <param name="app">应用对象</param>
        /// <returns>添加成功返回添加的对象,失败返回Null</returns>
        public WapAppDto AddApp(WapAppAddDto app)
        {
            if (app == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "应用对象不允许为空");
            }
            
            var validateResult = app.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }

            WapApp model = WapAppAddDto.ToModel(app);
            if (model == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "应用对象不允许为空");
            }

            if (this._appResponsitory.IsExitAppIdentity(model.AppIdentity))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_APP_EXIST, "应用标识已存在");
            }

            try
            {
                var seq = this._authSeqRepository.CreateSequence("app");
                model.AppKey = seq.IdentityKey;
                model.SortSn = seq.Sn;
                model.PyCode = string.IsNullOrEmpty(model.PyCode) ? this.GetPyCode(model.AppName) : model.PyCode;
                model.Active = true;                

                var ret = _appResponsitory.AddApp(model);
                if (ret != null)
                {
                    //记录数据审计日志
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        var audit = new WapAuthAudit();
                        audit.TrackingGuid = ret.AppKey;
                        audit.OperateFunc = "AddApp";
                        audit.OperateContent = model.ToString();
                        this._authAuditRepository.AddAudit(audit);
                    });
                }

                return WapAppDto.FromModel(ret);
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
        /// 修改应用信息
        /// </summary>
        /// <param name="appKey">应用Key</param>
        /// <param name="app">应用对象</param>
        /// <returns>成功或失败</returns>
        public WapAppDto ModifyApp(string appKey, WapAppUpdateDto app)
        {
            if (string.IsNullOrEmpty(appKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "appKey不允许为空");
            }

            if (!Utils.IsGuid(appKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, "appKey不是Guid");
            }

            if (app == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "应用更新对象不允许为空");
            }
            
            var validateResult = app.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }

            if (!Utils.IsGuid(appKey))
            {
                throw new WapException(StateCode.CODE_ARGUMENT_TYPE_ERROR, "appKey类型错误！");
            }

            try
            {
                WapApp model = WapAppUpdateDto.ToModel(app);
                model.PyCode = string.IsNullOrEmpty(model.PyCode) ? this.GetPyCode(model.AppName) : model.PyCode;
                model.AppKey = appKey;
                var ret = this._appResponsitory.ModifyApp(appKey, model);
                if (ret != null)
                {
                    //记录数据审计日志
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        var audit = new WapAuthAudit();
                        audit.TrackingGuid = appKey;
                        audit.OperateFunc = "ModifyApp";
                        audit.OperateContent = app.ToString();
                        this._authAuditRepository.AddAudit(audit);
                    });
                }
                return WapAppDto.FromModel(ret);
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
        /// 修改应用状态
        /// </summary>
        /// <param name="appKey">KEY</param>
        /// <param name="dto">应用状态对象信息</param>
        /// <returns>修改成功,返回true.否则返回false.</returns>
        public bool ModifyState(string appKey, WapAppStateDto dto)
        {
            if (string.IsNullOrEmpty(appKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "appKey不允许为空");
            }

            if (!Utils.IsGuid(appKey))
            {
                throw new WapException(StateCode.CODE_ARGUMENT_TYPE_ERROR, "appKey类型错误！");
            }

            try
            {
                WapAppState model = WapAppStateDto.ToModel(dto);
                var ret = this._appResponsitory.ModifyState(appKey, model);
                if (ret)
                {
                    //记录数据审计日志
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        var audit = new WapAuthAudit();
                        audit.TrackingGuid = appKey;
                        audit.OperateFunc = "DeleteApp";
                        audit.OperateContent = appKey;
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
        /// 获取所有的应用程序列表
        /// </summary>
        /// <returns>返回应用程序列表</returns>
        public IEnumerable<WapAppDto> GetAllApps()
        {
            try
            {
                IEnumerable<WapAppDto> dtos = WapAppDto.FromModel(this._appResponsitory.GetAllApps());

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
        /// 通过搜索关键字获取应用列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<WapAppDto> SelectListByKeyword(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "keyword不允许为空");
            }

            try
            {
                IEnumerable<WapAppDto> dtos = WapAppDto.FromModel(this._appResponsitory.SelectListByKeyword(keyword));

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
        /// 通过appKey获取应用
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public WapAppDto SelectAppByKey(string appKey)
        {
            if (string.IsNullOrEmpty(appKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "appKey不允许为空");
            }

            if (!Utils.IsGuid(appKey))
            {
                throw new WapException(StateCode.CODE_ARGUMENT_TYPE_ERROR, "appKey类型错误！");
            }

            try
            {
                WapAppDto dto = WapAppDto.FromModel(this._appResponsitory.SelectAppByKey(appKey));

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
        /// 通过appIdentity获取应用
        /// </summary>
        /// <param name="appIdentity"></param>
        /// <returns></returns>
        public WapAppDto SelectAppByIdentity(string appIdentity)
        {
            if (string.IsNullOrEmpty(appIdentity))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "appIdentity不允许为空");
            }

            try
            {
                WapAppDto dto = WapAppDto.FromModel(this._appResponsitory.SelectAppByIdentity(appIdentity));

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
        /// 根据用户KEY获取APP
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns></returns>
        public IEnumerable<WapAppDto> GetAppByUserKey(string userKey)
        {
            if (string.IsNullOrEmpty(userKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "userKey不允许为空");
            }

            if (!Utils.IsGuid(userKey))
            {
                throw new WapException(StateCode.CODE_ARGUMENT_TYPE_ERROR, "userKey类型错误！");
            }

            try
            {
                IEnumerable<WapAppDto> dtos = WapAppDto.FromModel(this._appResponsitory.GetAppByUserKey(userKey));

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
        /// 根据应用的appKey获取指定应用的角色导出脚本
        /// </summary>
        /// <param name="appKey">应用KEY</param>
        /// <returns>返回字符串</returns>
        public string GetRoleString(string appKey)
        {
            if (string.IsNullOrEmpty(appKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "appKey不允许为空");
            }

            if (!Utils.IsGuid(appKey))
            {
                throw new WapException(StateCode.CODE_ARGUMENT_TYPE_ERROR, "appKey类型错误！");
            }

            try
            {
                return this._appResponsitory.GetRoleString(appKey);
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
        /// 根据应用的appKey获取导出字符串
        /// </summary>
        /// <param name="appKey">应用KEY</param>
        /// <param name="isJson">是否JSON字符串</param>
        /// <returns>返回字符串</returns>
        public string GetAppString(string appKey, bool isJson)
        {
            if (string.IsNullOrEmpty(appKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "appKey不允许为空");
            }

            if (!Utils.IsGuid(appKey))
            {
                throw new WapException(StateCode.CODE_ARGUMENT_TYPE_ERROR, "appKey类型错误！");
            }

            try
            {
                return this._appResponsitory.GetAppString(appKey, isJson);
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
        /// 删除应用
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <returns>是否删除成功</returns>
        public bool DeleteApp(string appKey)
        {
            if (string.IsNullOrEmpty(appKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "appKey不允许为空");
            }

            try
            {
                var ret = this._appResponsitory.DeleteApp(appKey);
                if (ret)
                {
                    //记录数据审计日志
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        var audit = new WapAuthAudit();
                        audit.TrackingGuid = appKey;
                        audit.OperateFunc = "DeleteApp";
                        audit.OperateContent = appKey;
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
        /// 禁用应用
        /// </summary>
        /// <param name="appKey">应用标识</param>
        /// <returns>是否禁用成功</returns>
        public bool DeactiveApp(string appKey)
        {
            if (string.IsNullOrEmpty(appKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "appKey不允许为空");
            }

            try
            {
                var ret = this._appResponsitory.DeactiveApp(appKey);
                if (ret)
                {
                    //记录数据审计日志
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        var audit = new WapAuthAudit();
                        audit.TrackingGuid = appKey;
                        audit.OperateFunc = "DeactiveApp";
                        audit.OperateContent = appKey;
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
        /// 启用应用
        /// </summary>
        /// <param name="appKey">应用标识</param>
        /// <returns>是否启用成功</returns>
        public bool ActiveApp(string appKey)
        {
            if (string.IsNullOrEmpty(appKey))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "appKey不允许为空");
            }

            try
            {
                var ret = this._appResponsitory.ActiveApp(appKey);
                if (ret)
                {
                    //记录数据审计日志
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        var audit = new WapAuthAudit();
                        audit.TrackingGuid = appKey;
                        audit.OperateFunc = "ActiveApp";
                        audit.OperateContent = appKey;
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
        /// 获取当前用户授权的应用程序列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回应用程序列表</returns>
        public IEnumerable<WapApp> GetAuthorizedApps(int userId)
        {
            if (userId <= 0 )
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_USER_UNKNOWN, "未知的用户");
            }
            try
            {
                return this._appResponsitory.GetAuthorizedApps(userId);
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
    }
}

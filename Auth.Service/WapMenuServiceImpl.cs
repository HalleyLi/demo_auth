using SH3H.SDK.Definition;
using SH3H.SDK.Definition.Exceptions;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.SDK.Service.Core;
using SH3H.WAP.Auth.Contracts;
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
    /// WAP权限 菜单服务实现
    /// </summary>
    public class WapMenuServiceImpl : BaseService, IWapMenuService
    {
        // Repository类型的字段声明
        private IWapMenuRepository _wapMenuRepository = null;
        // Repository类型的字段声明
        private IAuthSeqRepository _authSeqRepository = null;
        // Repository类型的字段声明
        private IWapAuthAuditRepository _wapAuthAudit = null;

        /// <summary>
        /// 依赖注入初始化服务
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="seq"></param>
        /// <param name="audit"></param>
        public WapMenuServiceImpl(IWapMenuRepository menu, IAuthSeqRepository seq, IWapAuthAuditRepository audit)
        {
            _wapMenuRepository = menu;
            _authSeqRepository = seq;
            _wapAuthAudit = audit;
        }

        /// <summary>
        /// 获取所有的菜单项列表
        /// </summary>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        public IEnumerable<WapMenuDto> SelectAllAppMenus()
        {
            try
            {
                var result = _wapMenuRepository.SelectAllAppMenus();
                return result.Select(p => { return WapMenuDto.FromModel(p); }).ToList();
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取所有的菜单项列表服务执行异常");
            }
        }

        /// <summary>
        /// 获取指定应用的菜单项列表
        /// </summary>
        /// <param name="appKey">APP Key</param>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        public IEnumerable<WapMenuDto> SelectAppMenus(string appKey)
        {
            try
            {
                CheckNullStr(appKey, "应用参数");
                CheckGuidStr(appKey, "应用参数");

                var result = _wapMenuRepository.SelectAppMenus(appKey);
                return result.Select(p => { return WapMenuDto.FromModel(p); }).ToList();
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取指定应用的菜单项列表执行异常");
            }
        }

        /// <summary>
        /// 通过appIdentity获取指定应用的菜单项列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        public IEnumerable<WapMenuDto> SelectAppMenusByAppId(string appIdentity)
        {
            try
            {
                CheckNullStr(appIdentity, "应用标识");

                var result = _wapMenuRepository.SelectAppMenusByAppId(appIdentity);
                return result.Select(p => { return WapMenuDto.FromModel(p); }).ToList();
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "通过应用标识获取指定应用的菜单项列表执行异常");
            }
        }

        /// <summary>
        /// 通过menukey获取菜单
        /// </summary>
        /// <param name="menukey"></param>
        /// <returns></returns>
        public WapMenuDto GetMenu(string menukey)
        {
            try
            {
                CheckNullStr(menukey, "菜单主键");

                var result = _wapMenuRepository.GetMenu(menukey);

                if (result == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "菜单不存在");
                }

                return WapMenuDto.FromModel(result);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, " 通过菜单获取菜单执行异常");
            }
        }

        /// <summary>
        /// 通过菜单对象条件查询菜单对象列表 
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public IEnumerable<WapMenuDto> GetMenu(WapMenuDto menu)
        {
            try
            {
                CheckNull<WapMenuDto>(menu, "筛选对象");

                var result = _wapMenuRepository.GetMenu(menu.ToModel());
                return result.Select(p => { return WapMenuDto.FromModel(p); }).ToList();
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, " 通过菜单对象条件查询菜单对象列表 执行异常");
            }
        }

        /// <summary>
        /// 通过查询条件获取菜单列表
        /// </summary>
        /// <param name="userkey">用户Key</param>
        /// <param name="appkey">应用Key</param>
        /// <returns></returns>
        public IEnumerable<WapMenuDto> GetUserMenu(string userkey, string appkey)
        {
            try
            {
                CheckNullStr(userkey, "用户主键");
                CheckNullStr(appkey, "应用主键");
                CheckGuidStr(userkey, "用户主键");
                CheckGuidStr(appkey, "应用主键");

                var result = _wapMenuRepository.GetUserMenu(userkey, appkey);
                return result.Select(p => { return WapMenuDto.FromModel(p); }).ToList();
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "通过查询条件获取菜单列表执行异常");
            }
        }

        /// <summary>
        /// 通过功能点Key列表获取菜单列表
        /// </summary>
        /// <param name="funckey"></param>
        /// <returns></returns>
        public IEnumerable<WapMenuDto> GetFuncMenu(string funckey)
        {
            try
            {
                CheckNullStr(funckey, "功能点");
                CheckGuidStr(funckey, "功能点");

                var result = _wapMenuRepository.GetFuncMenu(funckey);
                return result.Select(p => { return WapMenuDto.FromModel(p); }).ToList();
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "通过功能点Key列表获取菜单列表执行异常");
            }
        }

        /// <summary>
        /// 查询指定应用中对应URL的菜单项
        /// </summary>
        /// <param name="appname">应用名称</param>
        /// <param name="url">URL地址</param>
        /// <returns>返回菜单项列表</returns>
        public IEnumerable<WapMenuDto> GetMenuByUrl(string appname, string url)
        {
            try
            {
                CheckNullStr(appname, "功能点");
                CheckNullStr(url, "功能点");

                var result = _wapMenuRepository.GetMenuByUrl(appname, url);
                return result.Select(p => { return WapMenuDto.FromModel(p); }).ToList();
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "查询指定应用中对应URL的菜单项执行异常");
            }
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu">菜单对象</param>
        /// <returns>是否添加成功</returns>
        public WapMenuDto AddMenu(WapMenuDto menu)
        {
            try
            {
                if (menu == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "菜单对象不允许为空");
                }
                if( string.IsNullOrEmpty(menu.FuncKey))
                {
                    menu.FuncKey = "00000000-0000-0000-0000-000000000000";
                }
                var validateResult = menu.Validate();
                if (!validateResult.IsValid)
                {
                    throw validateResult.BuildException();
                }
                var model = menu.ToModel();
                if (model == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "菜单对象不允许为空");
                }
                var seq = _authSeqRepository.CreateSequence("menu");

                if (seq == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_GET_SEQ_ERROR, "获取菜单序号错误");
                }

                menu.MenuKey = seq.IdentityKey;

                var ret = _wapMenuRepository.AddMenu(menu.ToModel());
                if (ret)
                {
                    //记录数据审计日志
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        var audit = new WapAuthAudit();
                        audit.TrackingGuid = menu.MenuKey;
                        audit.OperateFunc = "AddMenu";
                        audit.OperateContent = menu.ToString();
                        _wapAuthAudit.AddAudit(audit);
                    });
                }
                return menu;
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menuKey">菜单Key</param>
        /// <param name="menu">菜单对象</param>
        /// <returns>是否修改成功</returns>
        public WapMenuDto UpdateMenu(string menuKey, WapMenuDto menu)
        {
            try
            {
                CheckNullStr(menuKey, "菜单主键");
                CheckGuidStr(menuKey, "菜单主键");
                if (menu == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "菜单对象不允许为空");
                }
                if (string.IsNullOrEmpty(menu.FuncKey))
                {
                    menu.FuncKey = "00000000-0000-0000-0000-000000000000";
                }
                var validateResult = menu.Validate();
                if (!validateResult.IsValid)
                {
                    throw validateResult.BuildException();
                }
                var model = menu.ToModel();
                if (model == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "菜单对象不允许为空");
                }

                var result = _wapMenuRepository.GetMenu(menuKey);

                if (result == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "菜单不存在");
                }

                var ret = _wapMenuRepository.UpdateMenu(menuKey, menu.ToModel());
                if (ret)
                {
                    //记录数据审计日志
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        var audit = new WapAuthAudit();
                        audit.TrackingGuid = menuKey;
                        audit.OperateFunc = "UpdateMenu";
                        audit.OperateContent = menu.ToString();
                        _wapAuthAudit.AddAudit(audit);
                    });
                }
                return menu;
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "修改菜单执行异常");
            }
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuKey">菜单Key</param>
        /// <returns>是否修改成功</returns>
        public bool DeleteMenu(string menuKey)
        {
            try
            {
                CheckNullStr(menuKey, "菜单主键");
                CheckGuidStr(menuKey, "菜单主键");

                var result = _wapMenuRepository.GetMenu(menuKey);

                if (result == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "菜单不存在");
                }

                var ret = _wapMenuRepository.DeleteMenu(menuKey);
                if (ret)
                {
                    //记录数据审计日志
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        var audit = new WapAuthAudit();
                        audit.TrackingGuid = menuKey;
                        audit.OperateFunc = "DeleteMenu";
                        audit.OperateContent = menuKey;
                        _wapAuthAudit.AddAudit(audit);
                    });
                }
                return ret;
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "删除菜单执行异常");
            }
        }

        /// <summary>
        /// 禁用菜单
        /// </summary>
        /// <param name="menuKey">菜单标识</param>
        /// <returns>是否禁用成功</returns>
        public bool DeactiveMenu(string menuKey)
        {
            try
            {
                CheckNullStr(menuKey, "菜单主键");
                CheckGuidStr(menuKey, "菜单主键");

                var result = _wapMenuRepository.GetMenu(menuKey);

                if (result == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "菜单不存在");
                }

                var ret = _wapMenuRepository.DeactiveMenu(menuKey);
                if (ret)
                {
                    //记录数据审计日志
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        var audit = new WapAuthAudit();
                        audit.TrackingGuid = menuKey;
                        audit.OperateFunc = "DeactiveMenu";
                        audit.OperateContent = menuKey;
                        _wapAuthAudit.AddAudit(audit);
                    });
                }
                return ret;
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "禁用菜单执行异常");
            }
        }

        /// <summary>
        /// 修改功能点状态
        /// </summary>
        /// <param name="menuKey"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public bool UpdateMenuState(string menuKey, WapStateDto active)
        {
            try
            {
                CheckNullStr(menuKey, "菜单主键");
                CheckGuidStr(menuKey, "菜单主键");
                CheckNull<WapStateDto>(active, "状态");

                var result = _wapMenuRepository.GetMenu(menuKey);

                if (result == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "菜单不存在");
                }

                //记录数据审计日志
                ThreadPool.QueueUserWorkItem(p =>
                {
                    var audit = new WapAuthAudit();
                    audit.TrackingGuid = menuKey;
                    audit.OperateFunc = "UpdateMenuState";
                    audit.OperateContent = menuKey;
                    _wapAuthAudit.AddAudit(audit);
                });

                try
                {
                    return _wapMenuRepository.UpdateMenuState(menuKey, active.Active);
                }
                catch (Exception ex)
                {
                    LogManager.Get().Throw(ex);
                    return false;
                }
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "修改功能点状态执行异常");
            }
        }

        /// <summary>
        /// 启用菜单
        /// </summary>
        /// <param name="menuKey">菜单标识</param>
        /// <returns>是否启用成功</returns>
        public bool ActiveMenu(string menuKey)
        {
            try
            {
                var result = _wapMenuRepository.GetMenu(menuKey);

                if (result == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "菜单不存在");
                }

                CheckNullStr(menuKey, "菜单主键");
                CheckGuidStr(menuKey, "菜单主键");

                var ret = _wapMenuRepository.ActiveMenu(menuKey);
                if (ret)
                {
                    //记录数据审计日志
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        var audit = new WapAuthAudit();
                        audit.TrackingGuid = menuKey;
                        audit.OperateFunc = "ActiveMenu";
                        audit.OperateContent = menuKey;
                        _wapAuthAudit.AddAudit(audit);
                    });
                }
                return ret;
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "启用菜单执行异常");
            }
        }

        /// <summary>
        /// 根据菜单标识更新父菜单标识
        /// </summary>
        /// <param name="menuKey">菜单标识</param>
        /// <param name="parentMenuKey">父菜单标识</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateMenuParent(string menuKey, string parentMenuKey)
        {
            try
            {
                CheckNullStr(menuKey, "菜单主键");
                CheckGuidStr(menuKey, "菜单主键");

                CheckNullStr(parentMenuKey, "上级菜单主键");
                CheckGuidStr(parentMenuKey, "上级菜单主键");

                var result = _wapMenuRepository.GetMenu(menuKey);

                if (result == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "菜单不存在");
                }

                //var parentResult = _wapMenuRepository.GetMenu(parentMenuKey);
                //if (parentResult == null)
                //{
                //    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "上级菜单不存在");
                //}

                var ret = _wapMenuRepository.UpdateMenuParent(menuKey, parentMenuKey);
                if (ret)
                {
                    //记录数据审计日志
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        var audit = new WapAuthAudit();
                        audit.TrackingGuid = menuKey;
                        audit.OperateFunc = "UpdateMenuParent";
                        audit.OperateContent = parentMenuKey;
                        _wapAuthAudit.AddAudit(audit);
                    });
                }
                return ret;
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "根据菜单标识更新父菜单标识执行异常");
            }

        }

        #region 参数检查
        /// <summary>
        /// 检查入参是否是GUID类型字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="fieldName">参数中文名</param>
        /// <returns></returns>
        public static void CheckGuidStr(string str, string fieldName)
        {
            if (!Utils.IsGuid(str))
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_INVALID_ARGUMENTS, "参数 " + fieldName + " 不是合法的GUID类型字符串");
            }
        }

        /// <summary>
        /// 检查入参是否是空字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="fieldName">参数中文名</param>
        /// <returns></returns>
        public static void CheckNullStr(string str, string fieldName)
        {

            if (string.IsNullOrEmpty(str))
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_INVALID_ARGUMENTS, "参数 " + fieldName + " 不能为空");

            }
        }

        /// <summary>
        /// 检查入参是否是空
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static void CheckNull<T>(T obj, string fieldName)
        {
            if (obj == null)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_INVALID_ARGUMENTS, "参数 " + fieldName + " 不能为空");

            }
        }

        /// <summary>
        /// 统一更新排序值
        /// </summary>
        /// <param name="sortIndexes">编号索引号字典</param>
        public bool ModifySortIndexByKey(Dictionary<string, int> sortIndexes)
        {
            return _wapMenuRepository.ModifySortIndexByKey(sortIndexes);
        }

        #endregion

    }
}

using Newtonsoft.Json;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.SDK.Service.Core;
using SH3H.WAP.Auth.Contracts;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using SH3H.WAP.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SH3H.WAP.Auth.Model.Dto;
using SH3H.WAP.Share;
using SH3H.SDK.Definition.Exceptions;

namespace SH3H.WAP.Auth.Service
{
    /// <summary>
    /// WAP权限 功能点 功能组及其相关服务实现
    /// </summary>
    public class WapFunctionServiceImpl : BaseService, IWapFunctionService
    {
        // Repository类型的字段声明
        private IWapFunctionRepository _wapFunctionRepository = null;
        // Repository类型的字段声明
        private IWapFunctionGroupRepository _wapFunctionGroupRepository = null;
        // Repository类型的字段声明
        private IWapAppRepository _wapAppRepository = null;
        // Repository类型的字段声明
        private IAuthSeqRepository _authSeqRepository = null;
        // Repository类型的字段声明
        private IWapAuthAuditRepository _wapAuthAudit = null;
        // Repository类型的字段声明
        private IWapRoleRepository _wapRoleRepository = null;
        // Repository类型的字段声明
        private IWapRoleFunctionRepository _wapRoleFunctionRepository = null;
        // Repository类型的字段声明
        private IWapFunctionAllRepository _wapFunctionAllRepository = null;
        // Repository类型的字段声明
        private IWapFuncRelativeRepository _wapFuncRelativeRepository = null;
        // Repository类型的字段声明
        private IWapFuncGroupRelativeRepository _wapFuncGroupRelativeRepository = null;

        /// <summary>
        /// 依赖注入方式初始化服务
        /// </summary>
        /// <param name="func"></param>
        /// <param name="funcGroup"></param>
        /// <param name="app"></param>
        /// <param name="seq"></param>
        /// <param name="audit"></param>
        /// <param name="role"></param>
        /// <param name="rolefunc"></param>
        /// <param name="funcall"></param>
        /// <param name="funcrelate"></param>
        /// <param name="funcgrouprelate"></param>
        public WapFunctionServiceImpl(IWapFunctionRepository func,
                                    IWapFunctionGroupRepository funcGroup,
                                    IWapAppRepository app,
                                    IAuthSeqRepository seq,
                                    IWapAuthAuditRepository audit,
                                    IWapRoleRepository role,
                                    IWapRoleFunctionRepository rolefunc,
                                    IWapFunctionAllRepository funcall,
                                    IWapFuncRelativeRepository funcrelate,
                                    IWapFuncGroupRelativeRepository funcgrouprelate
            )
        {
            _wapFunctionRepository = func;
            _wapFunctionGroupRepository = funcGroup;
            _wapAppRepository = app;
            _authSeqRepository = seq;
            _wapAuthAudit = audit;
            _wapRoleRepository = role;
            _wapRoleFunctionRepository = rolefunc;
            _wapFunctionAllRepository = funcall;
            _wapFuncRelativeRepository = funcrelate;
            _wapFuncGroupRelativeRepository = funcgrouprelate;
        }

        private static string SeqKey_Function = "function";
        private static string SeqKey_FunctionGroup = "functiongroup";
        private static string SeqKey_RoleFunctionRelation = "rolefunctionRelation";

        #region Function

        ///<summary> 
        ///获取功能点 
        ///</summary> 
        /// <param name="functionkey"></param> 
        ///<return></return> 
        public WapFunctionDto GetFunction(string functionkey)
        {
            try
            {
                CheckNullStr(functionkey, "功能点主键");
                CheckGuidStr(functionkey, "功能点主键");
                var result = _wapFunctionRepository.GetFunction(functionkey);

                if (result == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "功能点不存在");
                }

                return WapFunctionDto.FromModel(result);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取功能点执行异常");
            }
        }

        /// <summary>
        ///  获取功能点集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WapFunctionDto> GetAllFunctions()
        {
            try
            {
                var result = _wapFunctionRepository.GetAllFunction();
                return result.Select(p => { return WapFunctionDto.FromModel(p); }).ToList();
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取功能点集合执行异常");
            }
        }

        /// <summary>
        ///新增功能点
        /// </summary>
        /// <param name="function">功能点</param>
        /// <returns></returns> 
        public WapFunctionDto AddFunction(WapFunctionDto function)
        {
            try
            {
                CheckNull<WapFunctionDto>(function, "查询功能点对象");

                var validateResult = function.Validate();
                if (!validateResult.IsValid)
                {
                    throw validateResult.BuildException();
                }

                var seq = _authSeqRepository.CreateSequence(SeqKey_Function);
                if (seq == null)
                {
                    throw new WapException(StateCode.CODE_GET_SEQ_ERROR, "获取功能点序号错误");
                }
                function.Key = seq.IdentityKey;

                //生成拼音码
                function.Pycode = string.IsNullOrEmpty(function.Pycode) ? SpellCodeHelper.GetFirstPYCode(function.Pycode).ToUpper() : function.Pycode;
                if (function.Pycode.Length > 10)
                {
                    function.Pycode.Substring(0, 10);
                }

                WapFunctionGroup group = _wapFunctionGroupRepository.GetFunctionGroup(function.FuncGroupKey);
                if (group != null)
                {
                    group.FuncAppKey = group.FuncAppKey;
                }

                AddAudit(function.Key, "AddFunction", function);

                if (!_wapFunctionRepository.AddFunction(function.ToModel()))
                {
                    throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "新增功能点失败");
                }
                return function;
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "新增功能点执行异常");
            }
        }

        /// <summary>
        /// 删除功能点
        /// </summary>
        /// <param name="functionkey">功能点</param>
        /// <returns></returns> 
        public bool DeleteFunction(string functionkey)
        {
            try
            {
                CheckNullStr(functionkey, "功能点主键");
                CheckGuidStr(functionkey, "功能点主键");
                var resultInput = this._wapFunctionRepository.GetFunction(functionkey);

                if (resultInput == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "功能点不存在");
                }

                AddAudit(functionkey, "DeleteFunction", functionkey);

                return _wapFunctionRepository.DeleteFunction(functionkey);

            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "删除功能点执行异常");
            }
        }

        /// <summary>
        /// 更新功能点
        /// </summary>
        /// <param name="function">Function</param>
        /// <returns></returns> 
        public WapFunctionDto UpdateFunction(WapFunctionDto function)
        {
            try
            {
                CheckNull<WapFunctionDto>(function, "更新功能点对象");
                var resultInput = this._wapFunctionRepository.GetFunction(function.Key);

                if (resultInput == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "功能点不存在");
                }

                AddAudit(function.Key, "UpdateFunction", function);

                var validateResult = function.Validate();
                if (!validateResult.IsValid)
                {
                    throw validateResult.BuildException();
                }

                if (!_wapFunctionRepository.UpdateFunction(function.ToModel()))
                {
                    throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "更新功能点失败");
                }
                return function;
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "更新功能点执行异常");
            }
        }

        /// <summary>
        /// 向某个功能点组添加一个功能点
        /// </summary>
        /// <param name="functionGroupRelative"></param>
        /// <returns></returns>
        public WapFuncGroupRelativeDto Add(WapFuncGroupRelativeDto functionGroupRelative)
        {
            try
            {
                CheckNull<WapFuncGroupRelativeDto>(functionGroupRelative, "功能点组对象");

                var validateResult = functionGroupRelative.Validate();
                if (!validateResult.IsValid)
                {
                    throw validateResult.BuildException();
                }

                //生成唯一编号
                var authSeq = _authSeqRepository.CreateSequence(SeqKey_Function);
                functionGroupRelative.Key = authSeq.IdentityKey;
                functionGroupRelative.Sortsn = authSeq.Sn;
                functionGroupRelative.Active = true;
                functionGroupRelative.IsRelative = true;
                //添加功能点

                var result = _wapFunctionRepository.AddFunction(functionGroupRelative);
                if (result)
                {
                    AddAudit(Guid.NewGuid(), "Add", functionGroupRelative.ToString());
                }
                return functionGroupRelative;
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "向某个功能点组添加一个功能点执行异常");
            }
        }

        /// <summary>
        /// 获取所有功能点
        /// </summary>
        /// <returns>返回所有功能点列表</returns>
        public IEnumerable<WapFunctionAllDto> GetAllFunction()
        {
            try
            {
                return _wapFunctionAllRepository.GetFunctionAlls();
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取所有功能点执行异常");
            }
        }

        /// <summary>
        /// 获取所有功能点
        /// </summary>
        /// <param name="appidentity"></param> 
        /// <returns>返回所有功能点列表</returns>
        public IEnumerable<WapFunctionAllDto> GetAllFunctionByAppIdentity(string appidentity)
        {
            try
            {
                CheckNullStr(appidentity, "应用标识");
                var resultApp = this._wapAppRepository.SelectAppByIdentity(appidentity);

                if (resultApp == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "应用不存在");
                }

                return _wapFunctionAllRepository.GetFunctionAllsByAppIdentity(appidentity);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取所有功能点执行异常");
            }
        }

        /// <summary>
        /// 获取所有功能点
        /// </summary>
        /// <param name="appkey"></param> 
        /// <returns>返回所有功能点列表</returns>
        public IEnumerable<WapFunctionAllDto> GetAllFunctionByAppKey(string appkey)
        {
            try
            {
                CheckNullStr(appkey, "应用主键");
                CheckGuidStr(appkey, "应用主键");
                var resultApp = this._wapAppRepository.SelectAppByKey(appkey);

                if (resultApp == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "应用不存在");
                }

                return _wapFunctionAllRepository.GetFunctionAllsByAppKey(appkey);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取所有功能点执行异常");
            }
        }

        /// <summary>
        /// 凭角色获取功能点集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WapFunctionAllDto> GetAllFunctionByRoles(IEnumerable<string> rolekeys)
        {
            try
            {
                return _wapFunctionAllRepository.GetFunctionAllsByRoles(rolekeys);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "凭角色获取功能点集合执行异常");
            }
        }

        ///<summary> 
        ///获取功能点 
        ///</summary> 
        /// <param name="code"></param> 
        ///<return></return> 
        public WapFunctionDto GetFunctionByCode(string code)
        {
            try
            {
                CheckNullStr(code, "功能点代码");

                var result = _wapFunctionRepository.GetFunctionByCode(code);
                if (result == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "功能点不存在");
                }

                return WapFunctionDto.FromModel(result);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取功能点 执行异常");
            }
        }

        /// <summary>
        /// 激活功能点
        /// </summary>
        /// <param name="functionkey"></param>
        /// <returns></returns>
        public bool ActiveFunction(string functionkey)
        {
            try
            {
                CheckNullStr(functionkey, "功能点主键");
                CheckGuidStr(functionkey, "功能点主键");

                var resultInput = this._wapFunctionRepository.GetFunction(functionkey);

                if (resultInput == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "功能点不存在");
                }

                AddAudit(functionkey, "ActiveFunction", functionkey);

                return _wapFunctionRepository.UpdateFunctionActive(functionkey, true);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "激活功能点执行异常");
            }
        }

        /// <summary>
        /// 禁用功能点
        /// </summary>
        /// <param name="functionkey"></param>
        /// <returns></returns>
        public bool DeactiveFunction(string functionkey)
        {
            try
            {
                CheckNullStr(functionkey, "功能点主键");
                CheckGuidStr(functionkey, "功能点主键");

                var resultInput = this._wapFunctionRepository.GetFunction(functionkey);

                if (resultInput == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "功能点不存在");
                }

                AddAudit(functionkey, "DeactiveFunction", functionkey);

                return _wapFunctionRepository.UpdateFunctionActive(functionkey, false);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "禁用功能点执行异常");
            }
        }

        /// <summary>
        /// 修改功能点状态
        /// </summary>
        /// <param name="funckey"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public bool UpdateFunctionState(string funckey, WapStateDto active)
        {
            try
            {
                CheckNullStr(funckey, "功能点");
                CheckGuidStr(funckey, "功能点");
                CheckNull(active, "状态");

                var resultInput = this._wapFunctionRepository.GetFunction(funckey);

                if (resultInput == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "功能点不存在");
                }

                AddAudit(funckey, "UpdateFunctionState", funckey);

                return _wapFunctionRepository.UpdateFunctionActive(funckey, active.Active);
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
        /// 通过appKey和funcCode获取功能点
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <param name="funcCode">功能点Code</param>
        /// <returns>存在返回true\不存在返回false</returns>
        public bool IsHaveFuncByFuncCodeAndAppKey(string appKey, string funcCode)
        {
            try
            {
                if (string.IsNullOrEmpty(appKey))
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "appKey不允许为空");
                }
                if (string.IsNullOrEmpty(funcCode))
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "funcCode不允许为空");
                }
                return _wapFunctionRepository.IsHaveFuncByFuncCodeAndAppKey(appKey,funcCode);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "执行异常");
            }
        }

        /// <summary>
        /// 功能点模糊查询
        /// </summary>
        /// <param name="searchText">搜索关键字</param>
        /// <returns>功能点列表</returns>
        public IEnumerable<WapFunctionDto> FuzzySearch(string searchText)
        {
            var result = _wapFunctionRepository.FuzzySearch(searchText);
            return result.Select(r => WapFunctionDto.FromModel(r)).ToList();
        }

        #endregion

        #region  RoleFunction

        /// <summary>
        /// 获取角色和功能点的关联
        /// </summary>
        /// <param name="appIdentity"></param>
        /// <returns></returns>
        public IEnumerable<WapRoleFunctionDto> GetAllRoleFunctionRelationByAppIdentity(string appIdentity)
        {
            try
            {
                CheckNullStr(appIdentity, "应用主键");
                var resultApp = this._wapAppRepository.SelectAppByIdentity(appIdentity);

                if (resultApp == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "应用不存在");
                }

                var result = _wapRoleFunctionRepository.GetAllRoleFunctionByAppIdentity(appIdentity);

                return result.Select(p => { return WapRoleFunctionDto.FromModel(p); }).ToList();
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取角色和功能点的关联执行异常");
            }
        }

        /// <summary>
        /// 获取角色和功能点的关联
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public IEnumerable<WapRoleFunctionDto> GetAllRoleFunctionRelationByAppKey(string appKey)
        {
            try
            {
                CheckNullStr(appKey, "应用标识");
                CheckGuidStr(appKey, "应用标识");
                var resultApp = this._wapAppRepository.SelectAppByKey(appKey);

                if (resultApp == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "应用不存在");
                }

                var result = _wapRoleFunctionRepository.GetAllRoleFunctionByAppKey(appKey);
                return result.Select(p => { return WapRoleFunctionDto.FromModel(p); }).ToList();
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取角色和功能点的关联执行异常");
            }
        }

        /// <summary>
        ///获取功能点信息
        /// </summary>
        /// <param name="relationKey"></param>
        /// <returns></returns>
        public WapRoleFunctionDto GetRoleFunctionRelationByKey(string relationKey)
        {
            try
            {
                CheckNullStr(relationKey, "角色功能点关联主键");
                CheckGuidStr(relationKey, "角色功能点关联主键");

                var result = _wapRoleFunctionRepository.GetRoleFunctionByKey(relationKey);
                if (result == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "角色功能点关联不存在");
                }

                return WapRoleFunctionDto.FromModel(result);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取功能点信息执行异常");
            }
        }

        /// <summary>
        /// 根据角色和应用获取功能点信息
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="appIdentity"></param>
        /// <returns></returns>
        public IEnumerable<WapFuncRelativeDto> GetRoleFunctionRelationByRoleKeyAndAppIdentity(string roleKey, string appIdentity)
        {
            try
            {
                CheckNullStr(roleKey, "角色主键");
                CheckNullStr(appIdentity, "应用标识");
                CheckGuidStr(roleKey, "角色主键");
                var resultRole = this._wapRoleRepository.GetRoleByRoleKey(roleKey);

                if (resultRole == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "角色不存在");
                }
                var resultApp = this._wapAppRepository.SelectAppByIdentity(appIdentity);

                if (resultApp == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "应用不存在");
                }

                return _wapFuncRelativeRepository.GetRoleFunctionByRoleKeyAndAppIdentity(roleKey, appIdentity);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "根据角色和应用获取功能点信息执行异常");
            }
        }

        /// <summary>
        /// 根据角色和应用获取功能点信息
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public IEnumerable<WapFuncRelativeDto> GetRoleFunctionRelationByRoleKeyAndAppKey(string roleKey, string appKey)
        {
            try
            {
                CheckNullStr(roleKey, "角色主键");
                CheckNullStr(appKey, "应用主键");
                CheckGuidStr(roleKey, "角色主键");
                CheckGuidStr(appKey, "应用主键");
                var resultRole = this._wapRoleRepository.GetRoleByRoleKey(roleKey);

                if (resultRole == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "角色不存在");
                }
                var resultApp = this._wapAppRepository.SelectAppByKey(appKey);

                if (resultApp == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "应用不存在");
                }

                return _wapFuncRelativeRepository.GetRoleFunctionByRoleKeyAndAppKey(roleKey, appKey);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "根据角色和应用获取功能点信息执行异常");
            }
        }

        /// <summary>
        /// 根据角色获取功能点组信息
        /// </summary>
        /// <param name="roleKey">角色标识</param>
        /// <returns>功能点组列表</returns>
        public IEnumerable<WapFuncRelativeDto> GetRoleFunction(string roleKey)
        {
            try
            {
                CheckNullStr(roleKey, "角色主键");
                CheckGuidStr(roleKey, "角色主键");
                return _wapFuncRelativeRepository.GetRoleFunction(roleKey);


            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "根据角色获取功能点组信息执行异常");
            }
        }

        /// <summary>
        /// 更新角色功能点关联关系
        /// </summary>
        /// <param name="relationModel">需要关联和需要解绑的功能点角色关系</param>
        /// <returns>是否更新成功</returns>
        public WapUpdateRoleFuncRelationDto UpdateRoleFunctionRelation(WapUpdateRoleFuncRelationDto relationModel)
        {
            try
            {
                CheckNull<WapUpdateRoleFuncRelationDto>(relationModel, "更新功能点关联对象");

                if ((relationModel.AddArr == null || relationModel.AddArr.Count() == 0) &&
                    (relationModel.DelArr == null || relationModel.DelArr.Count() == 0))
                {
                    throw new WapException(StateCode.CODE_ARGUMENT_NULL, "新增和删除不能都为空");
                }

                AddAudit(Guid.NewGuid(), "UpdateRoleFunctionRelation", relationModel);

                var validateResult = relationModel.Validate();
                if (!validateResult.IsValid)
                {
                    throw validateResult.BuildException();
                }

                if (relationModel != null && relationModel.AddArr != null && relationModel.AddArr.Count() > 0)
                {
                    foreach (var item in relationModel.AddArr)
                    {
                        var seq = _authSeqRepository.CreateSequence(SeqKey_RoleFunctionRelation);
                        if (seq == null)
                        {

                            throw new WapException(StateCode.CODE_GET_SEQ_ERROR, "获取功能点和角色关联序号错误");
                        }
                        item.RelationKey = seq.IdentityKey;
                    }
                }

                if (!_wapRoleFunctionRepository.UpdateRoleFunctionRelation(relationModel.AddArr, relationModel.DelArr))
                {

                    throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "新增功能点和角色关联失败");
                }

                return relationModel;
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "更新角色功能点关联关系执行异常");
            }
        }

        /// <summary>
        /// 更新角色和功能点的关系
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="relationModel"></param>
        /// <returns></returns>
        public bool UpdateRoleFunctionRelation(string roleKey, WapUpdateFuncsRelationDto relationModel)
        {
            try
            {

                CheckNullStr(roleKey, "角色主键");
                CheckGuidStr(roleKey, "角色主键");
                CheckNull<WapUpdateFuncsRelationDto>(relationModel, "更新功能点关联对象");

                if ((relationModel.AddFuncs == null || relationModel.AddFuncs.Count() == 0) &&
                    (relationModel.DelFuncs == null || relationModel.DelFuncs.Count() == 0))
                {
                    throw new WapException(StateCode.CODE_ARGUMENT_NULL, "新增和删除不能都为空");
                }


                var resultInput = this._wapRoleRepository.GetRoleByRoleKey(roleKey);

                if (resultInput == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "角色不存在");
                }

                if (relationModel.AddFuncs == null)
                {
                    relationModel.AddFuncs = new List<string>();
                }
                if (relationModel.DelFuncs == null)
                {
                    relationModel.DelFuncs = new List<string>();
                }

                List<WapRoleFunctionDto> add = new List<WapRoleFunctionDto>();
                List<WapRoleFunctionDto> del = new List<WapRoleFunctionDto>();

                WapUpdateRoleFuncRelationDto urfrd = new WapUpdateRoleFuncRelationDto()
                {
                    AddArr = relationModel.AddFuncs.Select(p => new WapRoleFunctionDto() { RoleKey = roleKey, FuncKey = p }).ToList(),
                    DelArr = relationModel.DelFuncs.Select(p => new WapRoleFunctionDto() { RoleKey = roleKey, FuncKey = p }).ToList(),
                };
                var result = UpdateRoleFunctionRelation(urfrd);
                return result != null;
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "更新角色和功能点的关系执行异常");
            }
        }

        #endregion

        #region FunctionGroup Relation

        /// <summary>
        /// 凭功能组名获取下属功能点
        /// </summary>
        /// <param name="groupkey"></param>
        /// <returns></returns>
        public IEnumerable<WapFuncGroupRelativeDto> GetFunctionGroupRelation(string groupkey)
        {
            try
            {
                CheckNullStr(groupkey, "功能组主键");
                CheckGuidStr(groupkey, "功能组主键");
                var resultInput = this._wapFunctionGroupRepository.GetFunctionGroup(groupkey);

                if (resultInput == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "功能组不存在");
                }

                return _wapFuncGroupRelativeRepository.GetFunctionGroup(groupkey);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "凭功能组名获取下属功能点执行异常");
            }
        }

        /// <summary>
        /// 更新功能组和功能点关联
        /// </summary>
        /// <param name="relationModel"></param>
        /// <returns></returns>
        public WapUpdateFuncGroupRelationDto UpdateFunctionGroupRelation(WapUpdateFuncGroupRelationDto relationModel)
        {
            try
            {
                CheckNull<WapUpdateFuncGroupRelationDto>(relationModel, "更新功能点关联对象");

                if ((relationModel.AddArr == null || relationModel.AddArr.Count() == 0) &&
                    (relationModel.DelArr == null || relationModel.DelArr.Count() == 0))
                {
                    throw new WapException(StateCode.CODE_ARGUMENT_NULL, "新增和删除不能都为空");
                }

                AddAudit(Guid.NewGuid(), "UpdateFunctionGroupRelation", relationModel);

                var validateResult = relationModel.Validate();
                if (!validateResult.IsValid)
                {
                    throw validateResult.BuildException();
                }

                if (!_wapFuncGroupRelativeRepository.UpdateFunctionGroupRelation(relationModel.AddArr, relationModel.DelArr))
                {
                    throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "更新功能组和功能点关联失败");
                }
                return relationModel;

            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "更新功能组和功能点关联执行异常");
            }
        }

        #endregion

        #region FunctionGroup

        /// <summary>
        ///新增功能点组
        /// </summary>
        /// <param name="functiongroup">功能点</param>
        /// <returns></returns> 
        public WapFunctionGroupDto AddFunctionGroup(WapFunctionGroupDto functiongroup)
        {

            try
            {
                CheckNull<WapFunctionGroupDto>(functiongroup, "新增功能组对象");

                var validateResult = functiongroup.Validate();
                if (!validateResult.IsValid)
                {
                    throw validateResult.BuildException();
                }

                WapFunctionGroup model = functiongroup.ToModel();

                var seq = _authSeqRepository.CreateSequence(SeqKey_FunctionGroup);
                if (seq == null)
                {
                    throw new WapException(StateCode.CODE_GET_SEQ_ERROR, "获取功能组序号错误");
                }
                model.FuncGroupKey = new Guid(seq.IdentityKey);
                //功能点组中当用户不传入PARENT_FUNC_GROUP_KEY时，默认设置为'00000000-0000-0000-0000-000000000000'
                if (model.ParentFuncGroupKey == null)
                {
                    model.ParentFuncGroupKey = Guid.Empty;
                }
                //生成拼音码
                model.FuncGroupPycode = string.IsNullOrEmpty(model.FuncGroupPycode) ? SpellCodeHelper.GetFirstPYCode(model.FuncGroupName).ToUpper() : model.FuncGroupPycode;
                if (model.FuncGroupPycode.Length > 10)
                {
                    model.FuncGroupPycode.Substring(0, 10);
                }
                AddAudit(model.FuncGroupKey, "AddFunctionGroup", functiongroup);

                if (!_wapFunctionGroupRepository.AddFunctionGroup(model))
                {
                    throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "新增功能组失败");
                }

                functiongroup.FuncGroupKey = model.FuncGroupKey.ToString();
                return functiongroup;
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "新增功能点组执行异常");
            }
        }

        /// <summary>
        /// 删除功能点组
        /// </summary>
        /// <param name="functiongroupkey">功能点组</param>
        /// <returns></returns> 
        public bool DeleteFunctionGroup(string functiongroupkey)
        {
            try
            {
                CheckNullStr(functiongroupkey, "功能组主键");
                CheckGuidStr(functiongroupkey, "功能组主键");

                var resultInput = this._wapFunctionGroupRepository.GetFunctionGroup(functiongroupkey);

                if (resultInput == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "功能组不存在");
                }

                AddAudit(functiongroupkey, "DeleteFunctionGroup", functiongroupkey);

                return _wapFunctionGroupRepository.DeleteFunctionGroup(functiongroupkey);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "删除功能点组执行异常");
            }
        }

        ///<summary> 
        ///获取功能点组 
        ///</summary> 
        /// <param name="functiongroupkey"></param> 
        ///<return></return> 
        public WapFunctionGroupDto GetFunctionGroup(string functiongroupkey)
        {
            try
            {
                CheckNullStr(functiongroupkey, "功能组主键");
                CheckGuidStr(functiongroupkey, "功能组主键");

                var resultInput = this._wapFunctionGroupRepository.GetFunctionGroup(functiongroupkey);

                if (resultInput == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "功能组不存在");
                }

                WapFunctionGroup model = _wapFunctionGroupRepository.GetFunctionGroup(functiongroupkey);
                WapFunctionGroupDto result = WapFunctionGroupDto.FromModel(model);

                WapApp app = _wapAppRepository.SelectAppByKey(model.FuncAppKey);
                if (app != null)
                {
                    result.FuncAppName = app.AppName;
                }

                return result;
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取功能点组执行异常");
            }
        }

        /// <summary>
        /// 获取所有功能点组(原)
        /// </summary>
        /// <returns>功能点组列表</returns>
        public IEnumerable<WapFunctionGroupDto> GetAllFunctionGroups()
        {

            try
            {
                IEnumerable<WapFunctionGroup> grouplist = _wapFunctionGroupRepository.GetAllFunctionGroup();
                IEnumerable<WapApp> applist = _wapAppRepository.GetAllApps();

                if (applist == null)
                {
                    applist = new List<WapApp>();
                }
                return grouplist.Select(p =>
                {
                    var result = WapFunctionGroupDto.FromModel(p);
                    //App获取全小写 FunctionGroup获取全大写
                    var app = applist.Where(wp => string.Compare(wp.AppKey, result.FuncAppKey, true) == 0).FirstOrDefault();
                    if (app != null)
                    {
                        result.FuncAppName = app.AppName;
                    }
                    return result;
                }).ToList();
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取所有功能点组执行异常");
            }
        }

        /// <summary>
        /// 获取所有功能点组
        /// </summary>
        /// <param name="appidentity"></param> 
        /// <returns>功能点组列表</returns>
        public IEnumerable<WapFunctionGroupDto> GetAllFunctionGroupsByAppIdentity(string appidentity)
        {
            try
            {
                CheckNullStr(appidentity, "应用标识");

                var resultInput = this._wapAppRepository.SelectAppByIdentity(appidentity);
                if (resultInput == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "应用不存在");
                }

                IEnumerable<WapFunctionGroup> grouplist = _wapFunctionGroupRepository.GetAllFunctionGroupByAppIdentity(appidentity);
                IEnumerable<WapApp> applist = _wapAppRepository.GetAllApps();

                if (applist == null)
                {
                    applist = new List<WapApp>();
                }
                return grouplist.Select(p =>
                {
                    var result = WapFunctionGroupDto.FromModel(p);
                    //App获取全小写 FunctionGroup获取全大写
                    var app = applist.Where(wp => string.Compare(wp.AppKey, result.FuncAppKey, true) == 0).FirstOrDefault();
                    if (app != null)
                    {
                        result.FuncAppName = app.AppName;
                    }
                    return result;
                }).ToList();
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, " 获取所有功能点组执行异常");
            }
        }

        /// <summary>
        /// 获取所有功能点组
        /// </summary>
        /// <param name="appkey"></param> 
        /// <returns>功能点组列表</returns>
        public IEnumerable<WapFunctionGroupDto> GetAllFunctionGroupsByAppKey(string appkey)
        {
            try
            {
                CheckNullStr(appkey, "应用主键");
                CheckGuidStr(appkey, "应用主键");

                var resultInput = this._wapAppRepository.SelectAppByKey(appkey);
                if (resultInput == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "应用不存在");
                }

                IEnumerable<WapFunctionGroup> grouplist = _wapFunctionGroupRepository.GetAllFunctionGroupByAppKey(appkey);
                IEnumerable<WapApp> applist = _wapAppRepository.GetAllApps();

                if (applist == null)
                {
                    applist = new List<WapApp>();
                }
                return grouplist.Select(p =>
                {
                    var result = WapFunctionGroupDto.FromModel(p);
                    //App获取全小写 FunctionGroup获取全大写
                    var app = applist.Where(wp => string.Compare(wp.AppKey, result.FuncAppKey, true) == 0).FirstOrDefault();
                    if (app != null)
                    {
                        result.FuncAppName = app.AppName;
                    }
                    return result;
                }).ToList();
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取所有功能点组执行异常");
            }
        }

        /// <summary>
        /// 更新功能点组
        /// </summary>
        /// <param name="functiongroup">FunctionGroup</param>
        /// <returns></returns> 
        public WapFunctionGroupDto UpdateFunctionGroup(WapFunctionGroupDto functiongroup)
        {
            try
            {
                CheckNull<WapFunctionGroupDto>(functiongroup, "更新功能组对象");

                var resultInput = this._wapFunctionGroupRepository.GetFunctionGroup(functiongroup.FuncGroupKey);

                if (resultInput == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "功能组不存在");
                }

                AddAudit(functiongroup.FuncGroupKey, "UpdateFunctionGroup", functiongroup);

                var validateResult = functiongroup.Validate();
                if (!validateResult.IsValid)
                {
                    throw validateResult.BuildException();
                }

                WapFunctionGroup result = functiongroup.ToModel();
                if (!_wapFunctionGroupRepository.UpdateFunctionGroup(result))
                {
                    throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "更新功能组对象失败");
                }
                return functiongroup;
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "更新功能点组执行异常");
            }
        }


        /// <summary>
        /// 获取所有功能点组
        /// </summary>
        /// <returns>功能点组列表</returns>
        public IEnumerable<WapFunctionGroupDto> GetAllFunctionGroup()
        {
            try
            {
                return GetAllFunctionGroups().Where(p => p.FuncGroupActive == true).ToList();
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取所有功能点组执行异常");
            }
        }

        /// <summary>
        /// 激活功能点
        /// </summary>
        /// <param name="functiongroupkey"></param>
        /// <returns></returns>
        public bool ActiveFunctionGroup(string functiongroupkey)
        {
            try
            {
                CheckNullStr(functiongroupkey, "功能组主键");
                CheckGuidStr(functiongroupkey, "功能组主键");

                var result = this._wapFunctionGroupRepository.GetFunctionGroup(functiongroupkey);

                if (result == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "功能组不存在");
                }

                AddAudit(functiongroupkey, "ActiveFunctionGroup", functiongroupkey);

                return _wapFunctionGroupRepository.UpdateFunctionGroupActive(functiongroupkey, true);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "激活功能点执行异常");
            }
        }

        /// <summary>
        /// 禁用功能点
        /// </summary>
        /// <param name="functiongroupkey"></param>
        /// <returns></returns>
        public bool DeactiveFunctionGroup(string functiongroupkey)
        {
            try
            {
                CheckNullStr(functiongroupkey, "功能组主键");
                CheckGuidStr(functiongroupkey, "功能组主键");

                var result = this._wapFunctionGroupRepository.GetFunctionGroup(functiongroupkey);

                if (result == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "功能组不存在");
                }

                AddAudit(functiongroupkey, "DeactiveFunctionGroup", functiongroupkey);

                return _wapFunctionGroupRepository.UpdateFunctionGroupActive(functiongroupkey, false);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "禁用功能点执行异常");
            }
        }


        /// <summary>
        /// 更新功能组状态
        /// </summary>
        /// <param name="funcGroupKey"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public bool UpdateFuncGroupState(string funcGroupKey, WapStateDto active)
        {
            try
            {
                CheckNullStr(funcGroupKey, "功能组主键");
                CheckGuidStr(funcGroupKey, "功能组主键");
                CheckNull<WapStateDto>(active, "状态");

                var result = this._wapFunctionGroupRepository.GetFunctionGroup(funcGroupKey);

                if (result == null)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_MODEL_NOT_EXIST, "功能组不存在");
                }

                AddAudit(funcGroupKey, "UpdateFuncGroupState", funcGroupKey);

                return _wapFunctionGroupRepository.UpdateFunctionGroupActive(funcGroupKey, active.Active);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "更新功能组状态执行异常");
            }
        }

        /// <summary>
        /// 通过appKey和groupName获取功能点组(该功能点组是否存在)
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <param name="groupName">功能点组名称</param>
        /// <returns>存在返回true\不存在返回false</returns>
        public bool IsHaveFuncGroupByNameAndAppKey(string appKey, string groupName)
        {
            try
            {
                if (string.IsNullOrEmpty(appKey))
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "appKey不允许为空");
                }
                if (string.IsNullOrEmpty(groupName))
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "groupName不允许为空");
                }
                return _wapFunctionGroupRepository.IsHaveFuncGroupByNameAndAppKey(appKey, groupName);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "执行异常");
            }
        }

        #endregion

        #region 审计
        private void AddAudit(string key, string actionName, string content)
        {
            //记录数据审计日志
            ThreadPool.QueueUserWorkItem(p =>
            {
                try
                {
                    var audit = new WapAuthAudit();
                    audit.TrackingGuid = key;
                    audit.OperateFunc = actionName;
                    audit.OperateContent = content;
                    _wapAuthAudit.AddAudit(audit);
                }
                catch (Exception)
                {
                    //记录审计数据崩溃
                    //目前暂无记录方式
                }
            });
        }

        private void AddAudit(string key, string actionName, object content)
        {
            string strContent = JsonConvert.SerializeObject(content);
            if (strContent.Length > 4000)
            {
                strContent = strContent.Substring(0, 4000);
            }

            AddAudit(key, actionName, strContent);
        }

        private void AddAudit(Guid key, string actionName, object content)
        {
            string strContent = JsonConvert.SerializeObject(content);
            if (strContent.Length > 4000)
            {
                strContent = strContent.Substring(0, 4000);
            }
            AddAudit(key.ToString(), actionName, strContent);
        }

        private void AddAudit(Guid key, string actionName, string content)
        {
            string strContent = JsonConvert.SerializeObject(content);
            if (strContent.Length > 4000)
            {
                strContent = strContent.Substring(0, 4000);
            }
            AddAudit(key.ToString(), actionName, strContent);
        }


        #endregion

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

        #endregion

    }
}

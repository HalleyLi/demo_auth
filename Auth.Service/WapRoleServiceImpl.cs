using SH3H.SDK.Definition.Exceptions;
using SH3H.SDK.Service.Core;
using SH3H.WAP.Auth.Contracts;
using SH3H.WAP.Auth.DataAccess.Repo;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using SH3H.WAP.Share;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Service
{
    /// <summary>
    /// 角色信息服务
    /// </summary>
    public class WapRoleServiceImpl : BaseService, IWapRoleService
    {
        // Repository类型的字段声明
        private IWapRoleRepository _roleRepository = null;
        // Repository类型的字段声明
        private IAuthSeqRepository _authSeqRepository = null;
        // Repository类型的字段声明
        private IWapAuthAuditRepository _wapAuthAudit = null;

        /// <summary>
        /// 组织结构服务实例构造函数
        /// </summary>
        /// <param name="repository">组织对象仓库接口</param>
        /// <param name="seq">权限认证仓库接口</param>
        /// <param name="audit">数据审计认证仓库接口</param>
        public WapRoleServiceImpl(IWapRoleRepository repository, IAuthSeqRepository seq, IWapAuthAuditRepository audit)
        {
            _roleRepository = repository;
            _authSeqRepository = seq;
            _wapAuthAudit = audit;
        }

        private string sequenceKey = "role";

        #region 2.0

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role">角色对象</param>
        /// <returns>是否添加成功</returns>
        public WapRoleDto CreateRole(WapRoleDto role)
        {
            //验证数据
            if (role == null)
                throw new WapException(StateCode.CODE_ARGUMENT_NULL, "角色模型为空！");
            var validateResult = role.Validate();
            if (!validateResult.IsValid)
                throw validateResult.BuildException();
            //生成主键key
            var authSeq = _authSeqRepository.CreateSequence(sequenceKey);
            role.RoleKey = authSeq.IdentityKey;
            role.RoleSortsn = authSeq.Sn;
            //生成拼音码
            role.RolePycode = string.IsNullOrEmpty(role.RolePycode) ? SpellCodeHelper.GetFirstPYCode(role.RoleName).ToUpper(): role.RolePycode;
            if (role.RolePycode.Length > 10) 
            {
                role.RolePycode.Substring(0,10);
            }
            //角色中当用户不传入PARENT_ROLE_KEY时，默认设置为'00000000-0000-0000-0000-000000000000'
            if (role.ParentRoleKey == null)
            {
                role.ParentRoleKey = Guid.Empty.ToString();
            }
            try
            {
                //增加角色
                var result = _roleRepository.CreateRole(role.ToModel());
                if (result != null)
                {
                    //增加审计
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        Model.WapAuthAudit audit = new Model.WapAuthAudit();
                        audit.TrackingGuid = role.RoleKey;
                        audit.OperateFunc = "AddRole";
                        audit.OperateContent = role.ToString();
                        _wapAuthAudit.AddAudit(audit);
                    });
                }
                return WapRoleDto.FromModel(result);
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
        /// 修改角色
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public WapRoleDto UpdateRole(string roleKey, WapRoleDto role)
        {
            //验证参数
            if (!Utils.IsGuid(roleKey))
                throw new WapException(StateCode.CODE_ARGUMENT_TYPE_ERROR, "RoleKey类型错误！");
            if (role == null)
                throw new WapException(StateCode.CODE_ARGUMENT_NULL, "角色模型为空！");
            if (roleKey != role.RoleKey)
                throw new WapException(StateCode.CODE_ARGUMENT_NOT_EQUAL, "角色模型中的RoleKey与第一个参数roleKey不一致！");
            var validateResult = role.Validate();
            if (!validateResult.IsValid)
                throw validateResult.BuildException();
            try
            {
                //修改角色
                var result = _roleRepository.UpdateRole(roleKey, role.ToModel());
                if (result != null)
                {
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        //增加审计
                        Model.WapAuthAudit audit = new Model.WapAuthAudit();
                        audit.TrackingGuid = roleKey;
                        audit.OperateFunc = "ModifyRole";
                        audit.OperateContent = role.ToString();
                        _wapAuthAudit.AddAudit(audit);
                    });
                }
                return WapRoleDto.FromModel(result);
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
        /// 修改角色状态
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="activeState"></param>
        /// <returns></returns>
        public bool UpdateRoleState(string roleKey, WapStateDto activeState)
        {
            //验证参数
            if (!Utils.IsGuid(roleKey))
                throw new WapException(StateCode.CODE_ARGUMENT_TYPE_ERROR, "RoleKey类型错误！");
            try
            {
                //激活角色
                var result = _roleRepository.UpdateRoleState(roleKey, activeState.Active);
                if (result)
                {
                    ThreadPool.QueueUserWorkItem(p =>
                    {
                        //增加审计
                        Model.WapAuthAudit audit = new Model.WapAuthAudit();
                        audit.TrackingGuid = roleKey;
                        audit.OperateFunc = "UpdateRoleState";
                        audit.OperateContent = roleKey;
                        _wapAuthAudit.AddAudit(audit);
                    });
                }
                return result;
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
        /// 获取所有角色信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WapRoleDto> GetAllRoles()
        {
            try
            {
                var result = _roleRepository.GetAllRoles();
                return result.Select(r => WapRoleDto.FromModel(r));
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
        /// 根据角色KEY查询角色
        /// </summary>
        /// <param name="roleKey">角色KEY</param>
        /// <returns>角色对象</returns>
        public WapRoleDto GetRoleByRoleKey(string roleKey)
        {
            //验证参数
            if (!Utils.IsGuid(roleKey))
                throw new WapException(StateCode.CODE_ARGUMENT_TYPE_ERROR, "RoleKey类型错误！");
            try
            {
                var result = _roleRepository.GetRoleByRoleKey(roleKey);
                return WapRoleDto.FromModel(result);
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
        /// 根据用户KEY查询该用户所关联的所有角色
        /// </summary>
        /// <param name="userKey">用户KEY</param>
        /// <returns>返回角色对象列表</returns>
        public IEnumerable<WapRoleDto> GetRolesByUserKey(string userKey)
        {
            //验证参数
            if (!Utils.IsGuid(userKey))
                throw new WapException(StateCode.CODE_ARGUMENT_TYPE_ERROR, "UserKey类型错误！");
            try
            {
                var result = _roleRepository.GetRolesByUserKey(userKey);
                return result.Select(r => WapRoleDto.FromModel(r)).ToList();
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
        /// 根据角色拼音码查询该角色
        /// </summary>
        /// <param name="pyCode">角色拼音码</param>
        /// <returns>角色对象</returns>
        public WapRoleDto GetRoleByPyCode(string pyCode)
        {
            //验证参数
            if (pyCode == null || string.IsNullOrWhiteSpace(pyCode))
                throw new WapException(StateCode.CODE_ARGUMENT_NULL, "PYCode为空！");
            try
            {
                var result = _roleRepository.GetRoleByPyCode(pyCode);
                return WapRoleDto.FromModel(result);
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
        /// 获取指定角色名的角色
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <returns>角色对象</returns>
        public WapRoleDto GetRoleByRoleName(string roleName)
        {
            //验证参数
            if (roleName == null || string.IsNullOrWhiteSpace(roleName))
                throw new WapException(StateCode.CODE_ARGUMENT_NULL, "RoleName为空！");
            try
            {
                var result = _roleRepository.GetRoleByRoleName(roleName);
                return WapRoleDto.FromModel(result);
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
        /// 根据角色标识更新父角色标识
        /// </summary>
        /// <param name="roleKey">角色标识</param>
        /// <param name="parentRoleKey">父角色标识</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateParentRoleKeyByRoleKey(string roleKey, string parentRoleKey)
        {
            //验证参数
            if (!Utils.IsGuid(roleKey))
                throw new WapException(StateCode.CODE_ARGUMENT_TYPE_ERROR, "RoleKey类型错误！");
            if (!Utils.IsGuid(parentRoleKey))
                throw new WapException(StateCode.CODE_ARGUMENT_TYPE_ERROR, "ParentRoleKey类型错误！");
            try
            {
                return _roleRepository.UpdateParentRoleKeyByRoleKey(roleKey, parentRoleKey);
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
        /// 更新用户对应角色关系
        /// </summary>
        /// <param name="userKey">用户Key</param>
        /// <param name="updateInmodel">角色关系输入对象</param>
        /// <returns>是否更新成功</returns>
        public bool CreateOrUpdateUserRoleRelation(string userKey, Model.Dto.WapUpdateRoleRelationDto updateInmodel)
        {
            //验证参数
            if (!Utils.IsGuid(userKey))
                throw new WapException(StateCode.CODE_ARGUMENT_TYPE_ERROR, "UserKey类型错误！");
            if (updateInmodel == null)
                throw new WapException(StateCode.CODE_ARGUMENT_NULL, "关系模型为空！");
            try
            {
                //生成主键
                //var authSeq = _authSeqRepository.CreateSequence(sequenceKey);

                List<WapRoleUserRelationDto> delRoleUser = new List<WapRoleUserRelationDto>();
                List<WapRoleUserRelationDto> addRoleUser = new List<WapRoleUserRelationDto>();

                WapRoleUserRelationDto relation = null;

                var delArr = updateInmodel.DelRoles;
                for (int i = 0; i < delArr.Count(); i++)
                {
                    string rolekey = delArr.ToList()[i];
                    if (!Utils.IsGuid(rolekey))
                        throw new WapException(StateCode.CODE_ARGUMENT_TYPE_ERROR, "DelRoles中第" + (i + 1) + "个RoleKey类型错误！");
                    relation = new WapRoleUserRelationDto();
                    relation.RoleKey = rolekey;
                    relation.UserKey = userKey;
                    //relation.RelationKey = authSeq.IdentityKey;
                    delRoleUser.Add(relation);
                }
                var addArr = updateInmodel.AddRoles;
                for (int j = 0; j < addArr.Count(); j++)
                {
                    string rolekey = addArr.ToList()[j];
                    if (!Utils.IsGuid(rolekey))
                        throw new WapException(StateCode.CODE_ARGUMENT_TYPE_ERROR, "AddRoles中第" + (j + 1) + "个RoleKey类型错误！");
                    relation = new WapRoleUserRelationDto();
                    relation.RoleKey = rolekey;
                    relation.UserKey = userKey;
                    relation.RelationKey = Guid.NewGuid().ToString();
                    addRoleUser.Add(relation);
                }

                return _roleRepository.CreateOrUpdateUserRoleRelation(userKey, delRoleUser, addRoleUser);
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
        /// 角色模糊查询
        /// </summary>
        /// <param name="searchText">模糊查询关键字</param>
        /// <returns>角色列表信息</returns>
        public IEnumerable<WapRoleDto> FuzzySearch(string searchText)
        {
            var result = _roleRepository.FuzzySearch(searchText);
            return result.Select(r => WapRoleDto.FromModel(r)).ToList();
        }

        #endregion


        #region 1.6

        /// <summary>
        /// 激活角色
        /// </summary>
        /// <param name="roleKey"></param>
        /// <returns></returns>
        public bool ActiveRole(string roleKey)
        {
            //激活角色
            WapRoleRepository repo = new WapRoleRepository();
            var result = repo.ActiveRole(roleKey);
            if (result)
            {
                ThreadPool.QueueUserWorkItem(p =>
                {
                    //增加审计
                    Model.WapAuthAudit audit = new Model.WapAuthAudit();
                    audit.TrackingGuid = roleKey;
                    audit.OperateFunc = "ActiveRole";
                    audit.OperateContent = roleKey;
                    var auditRepo = new WapAuthAuditRepository();
                    auditRepo.AddAudit(audit);
                });
            }
            return result;
        }

        /// <summary>
        /// 禁用角色
        /// </summary>
        /// <param name="roleKey"></param>
        /// <returns></returns>
        public bool DeActiveRole(string roleKey)
        {
            //禁用角色
            WapRoleRepository repo = new WapRoleRepository();
            var result = repo.DeActiveRole(roleKey);
            if (result)
            {
                ThreadPool.QueueUserWorkItem(p =>
                {
                    //增加审计
                    Model.WapAuthAudit audit = new Model.WapAuthAudit();
                    audit.TrackingGuid = roleKey;
                    audit.OperateFunc = "DeActiveRole";
                    audit.OperateContent = roleKey;
                    var auditRepo = new WapAuthAuditRepository();
                    auditRepo.AddAudit(audit);
                });
            }
            return result;
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool UpdateRole(Model.WapRole role)
        {
            WapRoleRepository repo = new WapRoleRepository();
            return repo.UpdateRole(role);
        }

        /// <summary>
        /// 统一更新角色排序值
        /// </summary>
        /// <param name="sortIndexes">角色编号索引号字典</param>
        public bool ModifySortIndexByKey(Dictionary<string, int> sortIndexes)
        {
            WapRoleRepository repo = new WapRoleRepository();
            return repo.ModifySortIndexByKey(sortIndexes);
        }

        /// <summary>
        /// 根据用户编号查询该用户所关联的所有角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回角色对象列表</returns>
        public IEnumerable<Model.WapRole> GetRolesByUserId(int userId)
        {
            WapRoleRepository repo = new WapRoleRepository();
            return repo.GetRolesByUserId(userId);
        }

        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Model.Dto.WapRoleRelativeDto> getAllRoleRelations()
        {
            WapRoleRepository repo = new WapRoleRepository();
            return repo.getAllRoleRelations();
        }

        /// <summary>
        /// 获取角色树
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>角色树</returns>
        public IEnumerable<Model.Dto.WapRoleOutputDto> GetRolesOutputByUserId(int userId)
        {
            WapRoleRepository repo = new WapRoleRepository();
            return repo.GetRolesOutputByUserId(userId);
        }

        /// <summary>
        /// 激活角色状态
        /// </summary>
        /// <param name="roleKeys">角色标识符列表</param>
        /// <returns>角色状态是否激活成功</returns>
        public bool ActiveRole(IEnumerable<string> roleKeys)
        {
            WapRoleRepository repo = new WapRoleRepository();
            return repo.ActiveRole(roleKeys);
        }

        /// <summary>
        /// 禁用角色状态
        /// </summary>
        /// <param name="roleKeys">角色标识符列表</param>
        /// <returns>角色状态是否禁用成功</returns>
        public bool DeactiveRole(IEnumerable<string> roleKeys)
        {
            WapRoleRepository repo = new WapRoleRepository();
            return repo.DeactiveRole(roleKeys);
        }

        #endregion
    }
}

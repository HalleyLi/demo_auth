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
    /// 定义角色权限服务接口，提供系统角色的权限管理的相关功能
    /// </summary>
    public interface IWapRoleService
    {
        #region 2.0

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role">角色对象</param>
        /// <returns>是否添加成功</returns>
        WapRoleDto CreateRole(WapRoleDto role);

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleKey">角色Key</param>
        /// <param name="role">角色实体</param>
        /// <returns>角色实体</returns>
        WapRoleDto UpdateRole(string roleKey, WapRoleDto role);

        /// <summary>
        /// 修改角色状态
        /// </summary>
        /// <param name="roleKey">角色Key</param>
        /// <param name="activeState">角色状态实体</param>
        /// <returns>是否修改成功</returns>
        bool UpdateRoleState(string roleKey, WapStateDto activeState);

        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns>角色信息对象</returns>
        IEnumerable<WapRoleDto> GetAllRoles();

        /// <summary>
        /// 根据角色KEY查询角色
        /// </summary>
        /// <param name="roleKey">角色KEY</param>
        /// <returns>角色对象</returns>
        WapRoleDto GetRoleByRoleKey(string roleKey);

        /// <summary>
        /// 根据用户KEY查询该用户所关联的所有角色
        /// </summary>
        /// <param name="userKey">用户KEY</param>
        /// <returns>返回角色对象列表</returns>
        IEnumerable<WapRoleDto> GetRolesByUserKey(string userKey);

        /// <summary>
        /// 根据角色拼音码查询角色
        /// </summary>
        /// <param name="pyCode">角色拼音码</param>
        /// <returns>角色对象</returns>
        WapRoleDto GetRoleByPyCode(string pyCode);

        /// <summary>
        /// 获取指定角色名的角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        WapRoleDto GetRoleByRoleName(string roleName);

        /// <summary>
        /// 根据角色标识更新父角色标识
        /// </summary>
        /// <param name="roleKey">角色标识</param>
        /// <param name="parentRoleKey">父角色标识</param>
        /// <returns>是否更新成功</returns>
        bool UpdateParentRoleKeyByRoleKey(string roleKey, string parentRoleKey);

        /// <summary>
        /// 更新用户对应角色关系
        /// </summary>
        /// <param name="userKey">用户key</param>
        /// <param name="updateInmodel">角色关系输入对象</param>
        /// <returns>是否成功</returns>
        bool CreateOrUpdateUserRoleRelation(string userKey, WapUpdateRoleRelationDto updateInmodel);

        /// <summary>
        /// 角色模糊查询
        /// </summary>
        /// <param name="searchText">模糊查询关键字</param>
        /// <returns>角色列表信息</returns>
        IEnumerable<WapRoleDto> FuzzySearch(string searchText);

        #endregion


        #region 1.6

        /// <summary>
        /// 激活角色
        /// </summary>
        /// <param name="roleKey"></param>
        /// <returns></returns>
        bool ActiveRole(string roleKey);

        /// <summary>
        /// 禁用角色
        /// </summary>
        /// <param name="roleKey"></param>
        /// <returns></returns>
        bool DeActiveRole(string roleKey);

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="role">角色对象</param>
        /// <returns>是否更新成功</returns>
        bool UpdateRole(WapRole role);

        /// <summary>
        /// 统一更新角色排序值
        /// </summary>
        /// <param name="sortIndexes">角色编号索引号字典</param>
        bool ModifySortIndexByKey(Dictionary<string, int> sortIndexes);

        /// <summary>
        /// 根据用户编号查询该用户所关联的所有角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回角色对象列表</returns>
        IEnumerable<WapRole> GetRolesByUserId(int userId);

        /// <summary>
        /// 获取角色树
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>角色树</returns>
        IEnumerable<WapRoleOutputDto> GetRolesOutputByUserId(int userId);

        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns>是否查询成功</returns>
        IEnumerable<WapRoleRelativeDto> getAllRoleRelations();

        /// <summary>
        /// 激活角色状态
        /// </summary>
        /// <param name="roleKeys">角色标识符列表</param>
        /// <returns>角色状态是否激活成功</returns>
        bool ActiveRole(IEnumerable<string> roleKeys);

        /// <summary>
        /// 禁用角色状态
        /// </summary>
        /// <param name="roleKeys">角色标识符列表</param>
        /// <returns>角色状态是否禁用成功</returns>
        bool DeactiveRole(IEnumerable<string> roleKeys);

        #endregion
    }
}

using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess.Repo.Contact
{
    /// <summary>
    /// 仓库接口
    /// </summary>
    public interface IWapRoleRepository
    {
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        WapRole CreateRole(WapRole role);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        WapRole UpdateRole(string roleKey, WapRole role);

        /// <summary>
        /// 修改角色状态
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="activeState"></param>
        /// <returns></returns>
        bool UpdateRoleState(string roleKey, bool activeState);

        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<Model.WapRole> GetAllRoles();

        /// <summary>
        /// 根据角色KEY查询角色
        /// </summary>
        /// <param name="roleKey"></param>
        /// <returns></returns>
        WapRole GetRoleByRoleKey(string roleKey);

        /// <summary>
        /// 根据用户KEY查询该用户所关联的所有角色
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns></returns>
        IEnumerable<WapRole> GetRolesByUserKey(string userKey);

        /// <summary>
        /// 根据角色拼音码查询该角色
        /// </summary>
        /// <param name="pyCode"></param>
        /// <returns></returns>
        WapRole GetRoleByPyCode(string pyCode);

        /// <summary>
        /// 获取指定角色名的角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        WapRole GetRoleByRoleName(string roleName);

        /// <summary>
        /// 根据角色标识更新父角色标识
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="parentRoleKey"></param>
        bool UpdateParentRoleKeyByRoleKey(string roleKey, string parentRoleKey);

        /// <summary>
        /// 更新用户对应角色关系
        /// </summary>
        /// <param name="userKey"></param>
        /// <param name="deletedArr"></param>
        /// <param name="addArr"></param>
        /// <returns></returns>
        bool CreateOrUpdateUserRoleRelation(string userKey, IEnumerable<WapRoleUserRelationDto> deletedArr, IEnumerable<WapRoleUserRelationDto> addArr);

        /// <summary>
        /// 角色模糊查询
        /// </summary>
        /// <param name="searchText">模糊查询关键字</param>
        /// <returns>角色列表信息</returns>
        IEnumerable<WapRole> FuzzySearch(string searchText);
    }
}

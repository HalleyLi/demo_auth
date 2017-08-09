using SH3H.SDK.DataAccess.Repo;
using SH3H.SDK.Infrastructure.Caching;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess.Repo
{
    /// <summary>
    /// 定义角色对象仓库
    /// </summary>
    public class WapRoleRepository :
       Repository<IWapRoleStorage>, IWapRoleRepository
    {
        /// <summary>
        /// 缓存区域
        /// </summary>
        private string roleCacheConst = Consts.URL_PREFIX_WAP_ROLE;

        #region 2.0

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role">角色对象</param>
        /// <returns>是否添加成功</returns>
        public WapRole CreateRole(WapRole role)
        {
            var result = Storage.CreateRole(role);
            if (result != null)
            {
                var cache = CacheManager.Get();
                cache.Put<WapRole>(role.RoleKey.ToString(), role, roleCacheConst);
            }
            return result;
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public WapRole UpdateRole(string roleKey, WapRole role)
        {
            var result = Storage.UpdateRole(roleKey, role);
            if (result != null)
            {
                var cache = CacheManager.Get();
                cache.Put<WapRole>(role.RoleKey.ToString(), role, roleCacheConst);
            }
            return result;
        }

        /// <summary>
        /// 修改角色状态
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="activeState"></param>
        /// <returns></returns>
        public bool UpdateRoleState(string roleKey, bool activeState)
        {
            if (Storage.UpdateRoleState(roleKey, activeState))
            {
                var cache = CacheManager.Get();
                cache.Remove(roleKey, roleCacheConst);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns>角色信息对象</returns>
        public IEnumerable<Model.WapRole> GetAllRoles()
        {
            return Storage.GetAllRoles();
        }

        /// <summary>
        /// 根据角色KEY查询角色
        /// </summary>
        /// <param name="roleKey">角色KEY</param>
        /// <returns>角色对象</returns>
        public WapRole GetRoleByRoleKey(string roleKey)
        {
            var role = Storage.GetRoleByRoleKey(roleKey);
            return role;
        }

        /// <summary>
        /// 根据用户KEY查询该用户所关联的所有角色
        /// </summary>
        /// <param name="userKey">用户KEY</param>
        /// <returns>返回角色对象列表</returns>
        public IEnumerable<WapRole> GetRolesByUserKey(string userKey)
        {
            return Storage.GetRolesByUserKey(userKey);
        }

        /// <summary>
        /// 根据角色拼音码查询该角色
        /// </summary>
        /// <param name="pyCode">角色拼音码</param>
        /// <returns>角色对象</returns>
        public WapRole GetRoleByPyCode(string pyCode)
        {
            var result = Storage.GetRoleByPyCode(pyCode);
            return result;
        }

        /// <summary>
        /// 获取指定角色名的角色
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <returns>角色对象</returns>
        public WapRole GetRoleByRoleName(string roleName)
        {
            var result = Storage.GetRoleByRoleName(roleName);
            return result;
        }

        /// <summary>
        /// 根据角色标识更新父角色标识
        /// </summary>
        /// <param name="roleKey">角色标识</param>
        /// <param name="parentRoleKey">父角色标识</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateParentRoleKeyByRoleKey(string roleKey, string parentRoleKey)
        {
            return Storage.UpdateParentRoleKeyByRoleKey(roleKey, parentRoleKey);
        }

        /// <summary>
        /// 更新用户对应角色关系
        /// </summary>
        /// <param name="userKey">用户Key</param>
        /// <param name="deletedArr">待删除关系</param>
        /// <param name="addArr">待添加关系</param>
        /// <returns>是否更新成功</returns>
        public bool CreateOrUpdateUserRoleRelation(string userKey, IEnumerable<WapRoleUserRelationDto> deletedArr, IEnumerable<WapRoleUserRelationDto> addArr)
        {
            return Storage.CreateOrUpdateUserRoleRelation(userKey, deletedArr, addArr);
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
            return Storage.ActiveRole(roleKey);
        }

        /// <summary>
        /// 禁用角色
        /// </summary>
        /// <param name="roleKey"></param>
        /// <returns></returns>
        public bool DeActiveRole(string roleKey)
        {
            return Storage.DeActiveRole(roleKey);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool UpdateRole(WapRole role)
        {
            return Storage.UpdateRole(role);
        }

        /// <summary>
        /// 统一更新角色排序值
        /// </summary>
        /// <param name="sortIndexes">角色编号索引号字典</param>
        public bool ModifySortIndexByKey(Dictionary<string, int> sortIndexes)
        {
            return Storage.UpdateSortIndexByKey(sortIndexes);
        }

        /// <summary>
        /// 激活角色状态
        /// </summary>
        /// <param name="roleKeys">角色标识符列表</param>
        /// <returns></returns>
        public bool ActiveRole(IEnumerable<string> roleKeys)
        {
            return Storage.ActiveRole(roleKeys);
        }

        /// <summary>
        /// 禁用角色状态
        /// </summary>
        /// <param name="roleKeys">角色标识符列表</param>
        /// <returns></returns>
        public bool DeactiveRole(IEnumerable<string> roleKeys)
        {
            return Storage.DeactiveRole(roleKeys);
        }

        /// <summary>
        /// 根据用户编号查询该用户所关联的所有角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回角色对象列表</returns>
        public IEnumerable<WapRole> GetRolesByUserId(int userId)
        {
            return Storage.GetRolesByUserId(userId);
        }

        /// <summary>
        /// 获取角色树
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>角色树</returns>
        public IEnumerable<Model.Dto.WapRoleOutputDto> GetRolesOutputByUserId(int userId)
        {
            return Storage.GetRolesOutputByUserId(userId);
        }

        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns>是否查询成功</returns>
        public IEnumerable<Model.Dto.WapRoleRelativeDto> getAllRoleRelations()
        {
            return Storage.getAllRoleRelations();
        }
        /// <summary>
        /// 角色模糊查询
        /// </summary>
        /// <param name="searchText">关键字</param>
        /// <returns>角色列表</returns>
        public IEnumerable<WapRole> FuzzySearch(string searchText)
        {
            return Storage.FuzzySearch(searchText);
        }

        #endregion
    }
}

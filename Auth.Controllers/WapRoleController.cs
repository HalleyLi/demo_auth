using SH3H.SDK.WebApi.Controllers;
using SH3H.SDK.WebApi.Core;
using SH3H.SDK.WebApi.Core.Models;
using SH3H.WAP.Auth.Contracts;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SH3H.WAP.Auth.Controllers
{
    /// <summary>
    /// 定义WAP权限认证角色服务控制器
    /// </summary>
    [Resource("wapRoleRes")]
    [RoutePrefix("wap/v2")]
    public class WapRoleController :
        BaseController<IWapRoleService>
    {
        #region 2.0

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role">角色对象</param>
        /// <returns>是否添加成功</returns>
        [HttpPost]
        [Route("roles")]
        [ActionName("createRole")]
        public WapResponse<WapRoleDto> CreateRole([FromBody]WapRoleDto role)
        {
            var result = Service.CreateRole(role);
            return new WapResponse<WapRoleDto>(result);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleKey">角色key</param>
        /// <param name="role">角色实体</param>
        /// <returns>是否修改成功</returns>
        [HttpPut]
        [Route("roles/{roleKey}")]
        [ActionName("updateRole")]
        public WapResponse<WapRoleDto> UpdateRole(string roleKey, [FromBody]WapRoleDto role)
        {
            var result = Service.UpdateRole(roleKey, role);
            return new WapResponse<WapRoleDto>(result);
        }

        /// <summary>
        /// 修改角色状态
        /// </summary>
        /// <param name="roleKey">角色key</param>
        /// <param name="activeState">期望的状态</param>
        /// <returns>修改后的状态</returns>
        [HttpPatch]
        [Route("roles/{roleKey}/state")]
        [ActionName("updateRoleState")]
        public WapBoolean UpdateRoleState(string roleKey, [FromBody]WapStateDto activeState)
        {
            var result = Service.UpdateRoleState(roleKey, activeState);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns>角色信息对象</returns>
        [HttpGet]
        [Route("roles")]
        [ActionName("getAllRoles")]
        public WapCollection<WapRoleDto> GetAllRoles()
        {
            var result = Service.GetAllRoles();
            return new WapCollection<WapRoleDto>(result);
        }

        /// <summary>
        /// 获取指定角色
        /// </summary>
        /// <param name="roleKey">角色key</param>
        /// <returns>角色实体</returns>
        [HttpGet]
        [Route("roles/{roleKey}")]
        [ActionName("getRoleByKey")]
        public WapResponse<WapRoleDto> GetRoleByRoleKey(string roleKey)
        {
            var result = Service.GetRoleByRoleKey(roleKey);
            return new WapResponse<WapRoleDto>(result);
        }

        /// <summary>
        /// 获取指定用户的所有角色
        /// </summary>
        /// <param name="userKey">用户KEY</param>
        /// <returns>返回角色对象列表</returns>
        [HttpGet]
        [Route("users/{userKey}/roles")]
        [ActionName("getRolesByUser")]
        public WapCollection<WapRoleDto> GetRolesByUserKey(string userKey)
        {
            var result = Service.GetRolesByUserKey(userKey);
            return new WapCollection<WapRoleDto>(result);
        }

        /// <summary>
        /// 获取指定拼音码的角色
        /// </summary>
        /// <param name="pyCode">角色拼音码</param>
        /// <returns>角色对象</returns>
        [HttpGet]
        [Route("roles")]
        [ActionName("getRoleByPyCode")]
        public WapResponse<WapRoleDto> GetRoleByPyCode(string pyCode)
        {
            return new WapResponse<WapRoleDto>(Service.GetRoleByPyCode(pyCode));
        }

        /// <summary>
        /// 获取指定角色名的角色
        /// </summary>
        /// <param name="roleName"> 角色名</param>
        /// <returns>角色对象</returns>
        [HttpGet]
        [Route("roles")]
        [ActionName("getRoleByName")]
        public WapResponse<WapRoleDto> GetRoleByRoleName(string roleName)
        {
            return new WapResponse<WapRoleDto>(Service.GetRoleByRoleName(roleName));
        }

        /// <summary>
        /// 根据角色标识更新父角色标识
        /// </summary>
        /// <param name="roleKey">角色标识</param>
        /// <param name="parentRoleKey">父角色标识</param>
        /// <returns>是否更新成功</returns>
        [HttpPut]
        [Route("roles/{parentRoleKey}/child/{roleKey}")]
        [ActionName("updateRoleRelation")]
        public WapBoolean UpdateParentRoleKeyByRoleKey(string roleKey, string parentRoleKey)
        {
            var result = Service.UpdateParentRoleKeyByRoleKey(roleKey, parentRoleKey);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 更新用户对应角色关系
        /// </summary>
        /// <param name="userKey">用户key</param>
        /// <param name="updateInmodel">角色关系输入对象</param>
        /// <returns>是否更新成功</returns>
        [HttpPost]
        [Route("users/{userKey}/roles/relation")]
        [ActionName("createUserRoleRelation")]
        public WapBoolean CreateOrUpdateUserRoleRelation(string userKey, [FromBody]WapUpdateRoleRelationDto updateInmodel)
        {
            var result = Service.CreateOrUpdateUserRoleRelation(userKey, updateInmodel);
            return new WapBoolean(result);
        }


        /// <summary>
        /// 统一更新角色排序值
        /// </summary>
        /// <param name="sortIndexes">角色编号索引号字典</param>
        /// <returns>是否更新成功</returns>
        [HttpPost]
        [Route("roles/sort")]
        [ActionName("sort")]
        public WapBoolean ModifySortIndexByKey([FromBody]Dictionary<string, int> sortIndexes)
        {
            var result = Service.ModifySortIndexByKey(sortIndexes);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 角色模糊查询
        /// </summary>
        /// <param name="searchText">模糊查询关键字</param>
        /// <returns>角色列表信息</returns>
        [HttpGet]
        [Route("roles/search")]
        [ActionName("fuzzyQuery")]
        public WapResponse<IEnumerable<WapRoleDto>> FuzzyQuery(string searchText)
        {
            var result= Service.FuzzySearch(searchText);
            return new WapResponse<IEnumerable<WapRoleDto>>(result);
        }

        #endregion


        #region 1.6

        /// <summary>
        /// 激活角色
        /// </summary>
        /// <param name="roleKey">角色key</param>
        /// <returns>是否激活成功</returns>
        [HttpPut]
        [Route("{roleKey}/active")]
        [ActionName("activeRole")]
        private WapBoolean ActiveRole(string roleKey)
        {
            var result = Service.ActiveRole(roleKey);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 禁用角色
        /// </summary>
        /// <param name="roleKey">角色key</param>
        /// <returns>是否禁用成功</returns>
        [HttpPut]
        [Route("{roleKey}/deactive")]
        [ActionName("deactiveRole")]
        private WapBoolean DeActiveRole(string roleKey)
        {
            var result = Service.DeActiveRole(roleKey);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="role">角色对象</param>
        /// <returns>是否更新成功</returns>
        [HttpPut]
        [Route("modifyRole")]
        [ActionName("modifyRole")]
        private WapBoolean ModifyRole([FromBody]WapRole role)
        {
            var result = Service.UpdateRole(role);
            return new WapBoolean(result);
        }
        /// <summary>
        /// 根据用户编号查询该用户所关联的所有角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回角色对象列表</returns>
        [HttpGet]
        [Route("userId/{userId}")]
        [ActionName("getRolesByUserId")]
        private IEnumerable<WapRole> GetRolesByUserId(int userId)
        {
            return Service.GetRolesByUserId(userId);
        }

        /// <summary>
        /// 根据用户编号查询该用户所关联的所有角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回角色树对象列表</returns>
        [HttpGet]
        [Route("output/userId/{userId}")]
        [ActionName("getWapRolesByUserIdOutput")]
        private IEnumerable<WapRoleOutputDto> GetWapRolesOutputByUserId(int userId)
        {
            return Service.GetRolesOutputByUserId(userId);
        }

        ///// <summary>
        ///// 获取所有角色信息
        ///// </summary>
        ///// <returns>是否查询成功</returns>
        //[HttpGet]
        //[Route("getAllRoleUsers")]
        //[ActionName("getAllRoleUsers")]
        //public IEnumerable<WapRoleRelative> getAllRoleRelations()
        //{
        //    return Service.getAllRoleRelations();
        //}


        /// <summary>
        /// 激活角色状态
        /// </summary>
        /// <param name="roleKeys">角色标识符列表</param>
        /// <returns>角色状态是否激活成功</returns>
        [HttpPost]
        [Route("activeRole")]
        [ActionName("activeRole")]
        private WapBoolean ActiveRole(IEnumerable<string> roleKeys)
        {
            var result = Service.ActiveRole(roleKeys);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 禁用角色状态
        /// </summary>
        /// <param name="roleKeys">角色标识符列表</param>
        /// <returns>角色状态是否禁用成功</returns>
        [HttpPost]
        [Route("deActiveRole")]
        [ActionName("deActiveRole")]
        private WapBoolean DeactiveRole(IEnumerable<string> roleKeys)
        {
            var result = Service.DeactiveRole(roleKeys);
            return new WapBoolean(result);
        }

        #endregion
    }
}

using SH3H.SDK.Definition.Exceptions;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.SDK.Share;
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
    /// 定义WAP权限认证用户服务控制器
    /// </summary>
    [Resource("wapUserRes")]
    [RoutePrefix("wap/v2")]
    public class WapUserController
         : BaseController<IWapUserService,IWapUserInfoService>
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns>返回是否添加成功</returns>
        [HttpPost]
        [Route("users")]
        [ActionName("createUser")]
        public WapResponse<WapUserDto> AddUser(WapUserInDto user)
        {
            var data = Service1.AddUser(user);
            return new WapResponse<WapUserDto>(data);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userKey">用户Key</param>
        /// <param name="user">用户对象</param>
        /// <returns>返回是否更新成功</returns>
        [HttpPut]
        [Route("users/{userKey}")]
        [ActionName("updateUser")]
        public WapResponse<WapUserDto> UpdateUser([FromUri]string userKey,[FromBody]WapUserInDto user)
        {
            var result = Service1.ModifyUser(userKey, user);
            return new WapResponse<WapUserDto>(result);
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns>返回用户列表</returns>
        [HttpGet]
        [Route("users")]
        [ActionName("getAllUsers")]
        public WapCollection<WapUserDto> GetAllUsers()
        {
            var data = Service1.GetAllUsers();
            return new WapCollection<WapUserDto>(data);
        }

        /// <summary>
        /// 获取指定用户
        /// </summary>
        /// <param name="userKey">用户标识</param>
        /// <returns>返回用户对象，如果用户不存在则返回NULL</returns>
        [HttpGet]
        [Route("users/{userKey}")]
        [ActionName("getUserByKey")]
        public WapResponse<WapUserDto> GetUserByUserKey(string userKey)
        {
            var data = Service1.GetUserByUserKey(userKey);
            return new WapResponse<WapUserDto>(data);
        }

        /// <summary>
        /// 根据编号获取用户
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns>返回用户对象，如果用户不存在则返回NULL</returns>
        [HttpGet]
        [Route("users")]
        [ActionName("getUserById")]
        public WapResponse<WapUserDto> GetUserByUserId([FromUri]int userId)
        {
            var data = Service1.GetUserByUserId(userId);
            return new WapResponse<WapUserDto>(data);
        }

         /// <summary>
        /// 根据token值获取用户信息
        /// </summary>
        /// <param name="token">权限token</param>
        /// <returns>返回用户信息对象.</returns>
        [HttpGet]
        [Route("users")]
        [ActionName("getUserByToken")]
        public WapResponse<WapUserDto> GetUserByToken(string token)
        {
            var data = Service1.GetUserByToken(token);
            return new WapResponse<WapUserDto>(data);
        }

        /// <summary>
        /// 根据角色KEY获取关联用户
        /// </summary>
        /// <param name="roleKey">角色KEY</param>
        /// <returns>用户集合</returns>
        [HttpGet]
        [Route("roles/{roleKey}/users")]
        [ActionName("getUsersByRole")]
        public WapCollection<WapUserDto> GetUsersByRoleKey(string roleKey)
        {
            var data = Service1.GetUsersByRoleKey(roleKey);
            return new WapCollection<WapUserDto>(data);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userKey">用户标识</param>
        /// <param name="reset">是否重置</param>
        /// <param name="dto">新旧密码对象信息</param>
        [HttpPatch]
        [Route("users/{userKey}/password")]
        [ActionName("changePassword")]
        public WapBoolean ChangePassword(string userKey, bool reset, [FromBody]ChangePasswordInputDto dto)
        {
            var data = Service1.ChangePassword(userKey, reset, dto);
            return new WapBoolean(data);
        }

        /// <summary>
        /// 根据userKey修改用户状态
        /// </summary>
        /// <param name="userKey">用户Key</param>
        /// <param name="state">用户状态信息</param>
        /// <returns>更新成功返回true.否则返回false.</returns>
        [HttpPatch]
        [Route("users/{userKey}/state")]
        [ActionName("updateUserState")]
        public WapBoolean UpdateUserStateByUserKey([FromUri]string userKey, [FromBody]WapUserState state)
        {
            var data = Service1.UpdateUserStateByUserKey(userKey, state);
            return new WapBoolean(data);
        }

        /// <summary>
        /// 根据组织KEY获取指定的用户
        /// </summary>
        /// <param name="organizationKey">组织KEY</param>
        /// <returns>返回用户列表.</returns>
        [HttpGet]
        [Route("organizations/{organizationKey}/users")]
        [ActionName("getUsersByOrganization")]
        public WapCollection<WapUserDto> GetUserByOrganizationKey(string organizationKey)
        {
            var data = Service1.GetUserByOrganizationKey(organizationKey);
            return new WapCollection<WapUserDto>(data);
        }

        /// <summary>
        /// 根据用户获取用户组织信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>用户组织信息</returns>
        [HttpGet]
        [Route("user_information/{userId}")]
        [ActionName("getUserInfoByUserId")]
        public WapResponse<WapUserInfoDto> GetUserInfoByUserId(int userId)
        {
            var result = Service2.GetUserInfoByUserId(userId);
            return new WapResponse<WapUserInfoDto>(result);
        }

        ///// <summary>
        ///// 根据userKey重置密码
        ///// </summary>
        ///// <param name="userKey">用户唯一标识</param>
        ///// <returns>是否重置成功</returns>
        //[HttpPut]
        //[Route("{userKey}/resetPassword")]
        //[ActionName("resetPassword")]
        //public WapBoolean ResetPassword(string userKey)
        //{
        //    var result = Service.ResetPassword(userKey);
        //    return new WapBoolean(result);
        //}
    }
}

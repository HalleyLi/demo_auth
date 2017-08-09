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
    /// 用户服务接口
    /// </summary>
    public interface IWapUserService
    {
        /// <summary>
        /// 用户添加
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        WapUserDto AddUser(WapUserInDto user);

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userKey">KEY</param>
        /// <param name="user">用户对象</param>
        /// <returns>返回是否更新成功</returns>
        WapUserDto ModifyUser(string userKey,WapUserInDto user);

        /// <summary>
        /// 返回所有用户列表
        /// </summary>
        IEnumerable<WapUserDto> GetAllUsers();

        /// <summary>
        /// 根据用户标识获取用户信息
        /// </summary>
        /// <param name="userKey">用户标识</param>
        /// <returns>返回用户对象，如果用户不存在则返回NULL</returns>
        WapUserDto GetUserByUserKey(string userKey);

        /// <summary>
        /// 根据用户编号获取用户信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回用户对象，如果用户不存在则返回NULL</returns>
        WapUserDto GetUserByUserId(int userId);

        /// <summary>
        /// 根据token值获取用户信息
        /// </summary>
        /// <param name="token">权限token</param>
        /// <returns>返回用户信息对象.</returns>
        WapUserDto GetUserByToken(string token);

        /// <summary>
        /// 根据角色KEY获取关联用户
        /// </summary>
        /// <param name="roleKey">角色KEY</param>
        /// <returns>用户集合</returns>
        IEnumerable<WapUserDto> GetUsersByRoleKey(string roleKey);

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userKey">用户KEY</param>
        /// <param name="reset">是否重置，重置则将密码设为"0000",否则修改密码</param>
        /// <param name="dto">新旧密码信息</param>
        /// <returns>修改成功返回true.否则返回false.</returns>
        bool ChangePassword(string userKey, bool reset, ChangePasswordInputDto dto);

        /// <summary>
        /// 根据userKey修改用户状态
        /// </summary>
        /// <param name="userKey">用户Key</param>
        /// <param name="state">用户状态信息</param>
        /// <returns>更新成功返回true.否则返回false.</returns>
        bool UpdateUserStateByUserKey(string userKey, WapUserState state);

        /// <summary>
        /// 根据组织KEY获取指定的用户
        /// </summary>
        /// <param name="organizationKey">组织KEY</param>
        /// <returns>返回用户列表.</returns>
        IEnumerable<WapUserDto> GetUserByOrganizationKey(string organizationKey);

        ///// <summary>
        ///// 根据userKey重置密码
        ///// </summary>
        ///// <param name="userKey">用户唯一标识</param>
        ///// <returns>是否重置成功</returns>
        //bool ResetPassword(string userKey);

        /// <summary>
        /// 通过账户获取用户
        /// </summary>
        /// <param name="account">账户</param>
        /// <returns>用户</returns>
        WapUser GetUserByAccount(string account);
    }
}

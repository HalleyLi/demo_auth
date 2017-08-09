using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess.Repo.Contact
{
    public interface IWapUserRepository
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns>用户对象</returns>
        WapUser AddUser(WapUser user);       


        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userKey">KEY</param>
        /// <param name="user">用户对象</param>
        /// <returns>返回是否更新成功</returns>
        WapUser ModifyUser(string userKey,WapUser user);

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns>用户对象列表</returns>
        IEnumerable<WapUser> GetAllUsers();

        /// <summary>
        /// 根据账户获取用户信息
        /// </summary>
        /// <param name="userKey">用户KEY</param>
        /// <returns>用户信息</returns>
        WapUser GetUserByUserKey(string userKey);

        /// <summary>
        /// 根据用户编号获取用户信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>用户对象</returns>
        WapUser GetUserByUserId(int userId);

        /// <summary>
        /// 根据角色KEY获取用户信息
        /// </summary>
        /// <param name="roleKey">角色KEY</param>
        /// <returns>用户对象列表</returns>
        IEnumerable<WapUser> GetUsersByRoleKey(string roleKey);

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userKey">用户KEY</param>
        /// <param name="reset">是否重置，重置则将密码设为"0000",否则修改密码</param>
        /// <param name="dto">新旧密码信息</param>
        /// <returns>修改成功返回true.否则返回false.</returns>
        bool UpdatePassword(string userKey, bool reset, ChangePasswordInputDto dto);

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
        IEnumerable<WapUser> SelectUserByOrganizationKey(string organizationKey);

        /// <summary>
        /// 更新用户角色信息
        /// </summary>
        /// <param name="deleteArr">删除的用户角色关系</param>
        /// <param name="addArr">新增的用户角色关系</param>
        /// <returns>是否更新成功</returns>
        bool ModifyRoleUserRelation(IEnumerable<RoleUserRelation> deleteArr, IEnumerable<RoleUserRelation> addArr);

        /// <summary>
        /// 根据账户获取用户信息
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns>用户信息</returns>
        IEnumerable<WapUser> GetUserByAccount(string account);

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="userkey">用户KEY</param>
        /// <param name="userName">用户名称</param>
        /// <returns>false为不存在,true为存在</returns>
        bool IsNameExist(string userkey, string userName);

        /// <summary>
        /// 检查帐户名是否存在
        /// </summary>
        /// <param name="userkey">用户KEY</param>
        /// <param name="account">账号</param>
        /// <returns>false为不存在,true为存在</returns>
        bool IsAccountExist(string userkey, string account);

        /// <summary>
        /// 检查工号是否存在
        /// </summary>
        /// <param name="userkey">用户KEY</param>
        /// <param name="workno">工号</param>
        /// <returns>false为不存在,true为存在</returns>
        bool IsWorknoExist(string userkey, string workno);        
    }
}

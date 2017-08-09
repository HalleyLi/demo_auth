using SH3H.SDK.DataAccess.Repo;
using SH3H.SDK.Infrastructure.Caching;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess.Repo
{
    /// <summary>
    /// 定义用户数据仓库
    /// </summary>
    public class WapUserRepository:
        Repository<IWapUserStorage>, IWapUserRepository
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns>用户对象</returns>
        public WapUser AddUser(WapUser user)
        {
            return Storage.Insert(user);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userKey">KEY</param>
        /// <param name="user">用户对象</param>
        /// <returns>返回是否更新成功</returns>
        public WapUser ModifyUser(string userKey,WapUser user)
        {
            return Storage.UpdateUser(userKey, user);

            //var cach = CacheManager.Get();

            //var result=Storage.UpdateUser(userKey,user);
            //if (result != null)
            //{
            //    cach.Put<WapUser>(userKey, result, SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER);
            //}
            //return result;
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns>用户对象列表</returns>
        public IEnumerable<WapUser> GetAllUsers()
        {
            return Storage.GetAllUsers();
            //var cach = CacheManager.Get();

            //if (cach.Exist("GetAllUsers",SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER))
            //{
            //    return cach.Get<IEnumerable<WapUser>>("GetAllUsers", SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER);
            //}
            //else
            //{
            //    var item = Storage.GetAllUsers();
            //    if (item　!= null)
            //    {
            //        cach.Put<IEnumerable<WapUser>>("GetAllUsers", item,SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER);
            //    }
            //    return item;
            //}
        }

        /// <summary>
        /// 根据账户获取用户信息
        /// </summary>
        /// <param name="userKey">用户KEY</param>
        /// <returns>用户信息</returns>
        public WapUser GetUserByUserKey(string userKey)
        {
            return Storage.GetUserByUserKey(userKey);

            //var cach = CacheManager.Get();

            //if (cach.Exist(userKey,SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER))
            //{
            //    return cach.Get<WapUser>(userKey, SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER);
            //}
            //else
            //{
            //    var item = Storage.GetUserByUserKey(userKey);
            //    if (item != null)
            //    {
            //        cach.Put<WapUser>(userKey, item, SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER);
            //    }
            //    return item;
            //}
        }

        /// <summary>
        /// 根据用户编号获取用户信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>用户对象</returns>
        public WapUser GetUserByUserId(int userId)
        {
            return Storage.GetUserByUserId(userId);

            //var cach = CacheManager.Get();

            //if (cach.Exist(userId.ToString(), SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER))
            //{
            //    return cach.Get<WapUser>(userId.ToString(), SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER);
            //}
            //else
            //{
            //    var item = Storage.GetUserByUserId(userId);
            //    if (item != null)
            //    {
            //        cach.Put<WapUser>(userId.ToString(), item, SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER);
            //    }
            //    return item;
            //}
        }

        /// <summary>
        /// 根据角色KEY获取用户信息
        /// </summary>
        /// <param name="roleKey">角色KEY</param>
        /// <returns>用户对象列表</returns>
        public IEnumerable<WapUser> GetUsersByRoleKey(string roleKey)
        {
            return Storage.SelectUsersByRoleKey(roleKey);
            //var cach = CacheManager.Get();

            //if (cach.Exist(roleKey, SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER))
            //{
            //    return cach.Get<IEnumerable<WapUser>>(roleKey, SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER);
            //}
            //else
            //{
            //    var item = Storage.SelectUsersByRoleKey(roleKey);
            //    if (item != null)
            //    {
            //        cach.Put<IEnumerable<WapUser>>(roleKey, item, SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER);
            //    }
            //    return item;
            //}
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userKey">用户KEY</param>
        /// <param name="reset">是否重置，重置则将密码设为"0000",否则修改密码</param>
        /// <param name="dto">新旧密码信息</param>
        /// <returns>修改成功返回true.否则返回false.</returns>
        public bool UpdatePassword(string userKey, bool reset, ChangePasswordInputDto dto)
        {
            var result = Storage.UpdatePassword(userKey, reset, dto);
            //if (result)
            //{
            //    var cach = CacheManager.Get();
            //    if (cach.Exist(userKey,SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER))
            //    {
            //        var dtoOld = cach.Get<WapUser>(userKey, SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER);
            //        dtoOld.Password = dto.NewPassword;
            //        cach.Put<WapUser>(userKey,dtoOld, SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER);
            //    }
            //}
            return result;
        }

        /// <summary>
        /// 根据userKey修改用户状态
        /// </summary>
        /// <param name="userKey">用户Key</param>
        /// <param name="state">用户状态信息</param>
        /// <returns>更新成功返回true.否则返回false.</returns>
        public bool UpdateUserStateByUserKey(string userKey, WapUserState state)
        {
            var result = Storage.UpdateUserStateByUserKey(userKey, state);
            //if (result)
            //{
            //    var cach = CacheManager.Get();
            //    if (cach.Exist(userKey, SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER))
            //    {
            //        var dtoOld = cach.Get<WapApp>(userKey, SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER);
            //        dtoOld.Active = state.Active;
            //        cach.Put<WapApp>(userKey, dtoOld, SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER);
            //    }
            //}
            return result;
        }

        /// <summary>
        /// 根据组织KEY获取指定的用户
        /// </summary>
        /// <param name="organizationKey">组织KEY</param>
        /// <returns>返回用户列表.</returns>
        public IEnumerable<WapUser> SelectUserByOrganizationKey(string organizationKey)
        {
            return Storage.SelectUserByOrganizationKey(organizationKey);
            //var cach = CacheManager.Get();

            //if (cach.Exist(organizationKey, SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER))
            //{
            //    return cach.Get<IEnumerable<WapUser>>(organizationKey, SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER);
            //}
            //else
            //{
            //    var item = Storage.SelectUserByOrganizationKey(organizationKey);
            //    if (item != null)
            //    {
            //        cach.Put<IEnumerable<WapUser>>(organizationKey, item, SH3H.WAP.Share.Consts.URL_PREFIX_WAP_USER);
            //    }
            //    return item;
            //}
        }

        /// <summary>
        /// 更新用户角色信息
        /// </summary>
        /// <param name="deleteArr">删除的用户角色关系</param>
        /// <param name="addArr">新增的用户角色关系</param>
        /// <returns>是否更新成功</returns>
        public bool ModifyRoleUserRelation(IEnumerable<RoleUserRelation> deleteArr, IEnumerable<RoleUserRelation> addArr)
        {
            return Storage.UpdateRoleUserRelation(deleteArr,addArr);
        }

        /// <summary>
        /// 根据账户获取用户信息
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns>用户信息</returns>
        public IEnumerable<WapUser> GetUserByAccount(string account)
        {
            return Storage.SelectUserByAccount(account);
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="userkey">用户KEY</param>
        /// <param name="userName">用户名称</param>
        /// <returns>false为不存在,true为存在</returns>
        public bool IsNameExist(string userkey, string userName)
        {
            return Storage.IsNameExist(userkey, userName);
        }

        /// <summary>
        /// 检查帐户名是否存在
        /// </summary>
        /// <param name="userkey">用户KEY</param>
        /// <param name="account">账号</param>
        /// <returns>false为不存在,true为存在</returns>
        public bool IsAccountExist(string userkey, string account)
        {
            return Storage.IsAccountExist(userkey,account);
        }

        /// <summary>
        /// 检查工号是否存在
        /// </summary>
        /// <param name="userkey">用户KEY</param>
        /// <param name="workno">工号</param>
        /// <returns>false为不存在,true为存在</returns>
        public bool IsWorknoExist(string userkey, string workno)
        {
            return Storage.IsWorknoExist(userkey,workno);
        }
    }
}

using SH3H.SDK.DataAccess.Repo;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Model;
using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.DataAccess.Repo
{
    /// <summary>
    /// 定义用户配置仓库
    /// </summary>
    public class WapUserSettingRepository : Repository<IWapUserSettingStorage>, IWapUserSettingRepository
    {
        /// <summary>
        /// 添加用户配置
        /// </summary>
        /// <returns>新增后的用户配置对象</returns>
        public WapUserSetting Add(WapUserSetting userSetting)
        {
            return Storage.Insert(userSetting);
        }

        /// <summary>
        /// 添加用户配置组别
        /// </summary>
        /// <returns>新增后的用户配置组别对象</returns>
        public WapUserSetting AddGroup(WapUserSetting userSetting)
        {
            return Storage.InsertGroup(userSetting);
        }

        /// <summary>
        /// 删除用户配置
        /// </summary>
        /// <param name="userSettingId">用户配置编号</param>
        /// <returns>是否删除成功</returns>
        public bool Remove(int userSettingId)
        {
            return Storage.Delete(userSettingId);
        }

        /// <summary>
        /// 根据用户配置编号修改用户配置
        /// </summary>
        /// <param name="userSetting">用户配置对象</param>
        /// <returns>修改后的用户配置对象</returns>
        public WapUserSetting Modify(WapUserSetting userSetting)
        {
            return Storage.Update(userSetting);
        }

        /// <summary>
        /// 根据用户配置编号修改用户配置组别
        /// </summary>
        /// <param name="userSetting">用户配置对象</param>
        /// <returns>修改后的用户配置对象组别</returns>
        public WapUserSetting ModifyGroup(WapUserSetting userSetting)
        {
            return Storage.UpdateGroup(userSetting);
        }

        /// <summary>
        /// 根据用户编号和配置编码修改用户配置
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="code">用户配置编码</param>
        /// <returns>更新后的用户配置对象</returns>
        public WapUserSetting ModifyByUserIdAndCode(int userId, string code, WapUserSetting userSetting)
        {
            return Storage.UpdateByUserIdAndCode(userId, code, userSetting);
        }

        /// <summary>
        ///获取所有用户配置
        /// </summary>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSetting> GetAll()
        {
            return Storage.SelectAll();
        }

        /// <summary>
        /// 根据配置编号获取用户配置参数
        /// </summary>
        /// <param name="userSettingId">用户配置对象编号</param>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSetting> GetByUserSettingId(int userSettingId)
        {
            return Storage.SelectByUserSettingId(userSettingId);
        }

        /// <summary>
        /// 根据配置编号获取用户配置参数
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSetting> GetByUserId(int userId)
        {
            return Storage.SelectByUserId(userId);
        }

        /// <summary>
        /// 根据用户编号和配置编码查询用户配置
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="code">配置编码</param>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSetting> GetByUserIdAndCode(int userId, string code)
        {
            return Storage.SelectByUserIdAndCode(userId, code);
        }

        /// <summary>
        /// 根据用户编号和应用标识查询用户配置
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSetting> GetByUserIdAndAppIdentity(int userId, string appIdentity)
        {
            return Storage.SelectByUserIdAndAppIdentity(userId, appIdentity);
        }

        /// <summary>
        /// 根据应用标识查询用户配置
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSetting> GetByAppIdentity(string appIdentity)
        {
            return Storage.SelectByAppIdentity(appIdentity);
        }
    }
}

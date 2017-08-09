using SH3H.WAP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.DataAccess
{
    /// <summary>
    /// 定义用户存储配置接口
    /// </summary>
    public interface IWapUserSettingStorage
    {
        /// <summary>
        /// 添加用户配置
        /// </summary>
        /// <returns>新增后的用户配置对象</returns>
        WapUserSetting Insert(WapUserSetting userSetting);


        /// <summary>
        /// 添加用户配置组别
        /// </summary>
        /// <returns>新增后的用户配置组别对象</returns>
        WapUserSetting InsertGroup(WapUserSetting userSetting);

        /// <summary>
        /// 删除用户配置
        /// </summary>
        /// <param name="userSettingId">用户配置编号</param>
        /// <returns>是否删除成功</returns>
        bool Delete(int userSettingId);

        /// <summary>
        /// 根据用户配置编号修改用户配置
        /// </summary>
        /// <param name="userSetting">用户配置对象</param>
        /// <returns>修改后的用户配置对象</returns>
        WapUserSetting Update(WapUserSetting userSetting);

        /// <summary>
        /// 根据用户配置编号修改用户配置组别
        /// </summary>
        /// <param name="userSetting">用户配置对象</param>
        /// <returns>修改后的用户配置对象组别</returns>
        WapUserSetting UpdateGroup(WapUserSetting userSetting);

        /// <summary>
        /// 根据用户编号和配置编码修改用户配置
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="code">用户配置编码</param>
        /// <returns>更新后的用户配置对象</returns>
        WapUserSetting UpdateByUserIdAndCode(int userId, string code, WapUserSetting userSetting);

        /// <summary>
        ///获取所有用户配置
        /// </summary>
        /// <returns>用户配置对象列表</returns>
        IEnumerable<WapUserSetting> SelectAll();

        /// <summary>
        /// 根据配置编号获取用户配置参数
        /// </summary>
        /// <param name="userSettingId">用户配置对象编号</param>
        /// <returns>用户配置对象列表</returns>
        IEnumerable<WapUserSetting> SelectByUserSettingId(int userSettingId);

        /// <summary>
        /// 根据配置编号获取用户配置参数
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>用户配置对象列表</returns>
        IEnumerable<WapUserSetting> SelectByUserId(int userId);

        /// <summary>
        /// 根据用户编号和配置编码查询用户配置
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="code">配置编码</param>
        /// <returns>用户配置对象列表</returns>
        IEnumerable<WapUserSetting> SelectByUserIdAndCode(int userId, string code);

        /// <summary>
        /// 根据用户编号和应用标识查询用户配置
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>用户配置对象列表</returns>
        IEnumerable<WapUserSetting> SelectByUserIdAndAppIdentity(int userId, string appIdentity);

        /// <summary>
        /// 根据应用标识查询用户配置
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>用户配置对象列表</returns>
        IEnumerable<WapUserSetting> SelectByAppIdentity(string appIdentity);

    }
}

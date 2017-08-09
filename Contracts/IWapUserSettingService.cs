using SH3H.WAP.Model;
using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Contracts
{
    /// <summary>
    /// 定义用户配置操作服务接口
    /// </summary>
    public interface IWapUserSettingService
    {
        /// <summary>
        /// 添加用户配置
        /// </summary>
        /// <returns>新增后的用户配置对象</returns>
        WapUserSettingDto Add(WapUserSettingDto userSetting);

        /// <summary>
        /// 添加用户配置组别
        /// </summary>
        /// <param name="addGroup">新增用户配置组别对象</param>
        /// <returns>新增后的用户配置组别对象</returns>
        WapUserSettingDto AddGroup(AddUserSettingGroupDto addGroup);

        /// <summary>
        /// 删除用户配置
        /// </summary>
        /// <param name="userSettingId">用户配置编号</param>
        /// <returns>是否删除成功</returns>
        bool Remove(int userSettingId);

        /// <summary>
        /// 根据用户配置编号修改用户配置
        /// </summary>
        /// <param name="userSetting">用户配置对象</param>
        /// <returns>修改后的用户配置对象</returns>
        WapUserSettingDto Modify(WapUserSettingDto userSetting);

        /// <summary>
        /// 根据用户配置编号修改用户配置组别
        /// </summary>
        /// <param name="modifyGroup">用户配置对象</param>
        /// <returns>修改后的用户配置对象组别</returns>
        WapUserSettingDto ModifyGroup(ModifyUserSettingGroupDto modifyGroup);

        /// <summary>
        /// 根据用户编号和配置编码修改用户配置
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="code">用户配置编码</param>
        /// <param name="userSetting">用户配置</param>
        /// <returns>更新后的用户配置对象</returns>
        WapUserSettingDto ModifyByUserIdAndCode(int userId, string code, WapUserSettingDto userSetting);

        /// <summary>
        ///获取所有用户配置
        /// </summary>
        /// <returns>用户配置对象列表</returns>
        IEnumerable<WapUserSettingDto> GetAll();

        /// <summary>
        /// 根据配置编号获取用户配置参数
        /// </summary>
        /// <param name="userSettingId">用户配置对象编号</param>
        /// <returns>用户配置对象列表</returns>
        IEnumerable<WapUserSettingDto> GetByUserSettingId(int userSettingId);

        /// <summary>
        /// 根据配置编号获取用户配置参数
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>用户配置对象列表</returns>
        IEnumerable<WapUserSettingDto> GetByUserId(int userId);

        /// <summary>
        /// 根据用户编号和配置编码查询用户配置
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="code">配置编码</param>
        /// <returns>用户配置对象列表</returns>
        IEnumerable<WapUserSettingDto> GetByUserIdAndCode(int userId, string code);

        /// <summary>
        /// 根据用户编号和应用标识查询用户配置
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>用户配置对象列表</returns>
        IEnumerable<WapUserSettingDto> GetByUserIdAndAppIdentity(int userId, string appIdentity);

        /// <summary>
        /// 根据应用标识查询用户配置
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>用户配置对象列表</returns>
        IEnumerable<WapUserSettingDto> GetByAppIdentity(string appIdentity);
    }
}

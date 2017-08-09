using SH3H.WAP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.DataAccess
{
    /// <summary>
    /// 定义用户配置参数数据库接口
    /// </summary>
    public interface IWapUserSettingSchemeStorage
    {
        /// <summary>
        /// 添加用户配置参数
        /// </summary>
        /// <param name="userSettingScheme">用户配置参数对象</param>
        /// <returns>新增后的用户配置参数对象</returns>
        WapUserSettingScheme Insert(WapUserSettingScheme userSettingScheme);

        /// <summary>
        /// 删除用户配置参数
        /// </summary>
        /// <param name="schemeId">用户配置参数编号</param>
        /// <returns>是否删除成功，true成功，false失败</returns>
        bool Delete(int schemeId);

        /// <summary>
        /// 根据参数编号修改用户配置参数
        /// </summary>
        /// <param name="userSettingScheme">用户配置参数对象</param>
        /// <returns>更新成功，返回更新后的对象;更新失败，返回空</returns>
        WapUserSettingScheme Update(WapUserSettingScheme userSettingScheme);

        /// <summary>
        /// 根据参数编号获取用户配置参数
        /// </summary>
        /// <param name="schemeId">参数编号</param>
        /// <returns>用户配置参数对象<returns>
        WapUserSettingScheme Select(int schemeId);
    }
}

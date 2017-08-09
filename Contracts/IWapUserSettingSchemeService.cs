using SH3H.WAP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Contracts
{
    /// <summary>
    /// 定义用户配置参数服务接口
    /// </summary>
    public interface IWapUserSettingSchemeService
    {
        /// <summary>
        /// 添加用户配置参数
        /// </summary>
        /// <param name="userSettingSchemeDto">用户配置参数对象</param>
        /// <returns>新增后的用户配置参数对象</returns>
        WapUserSettingSchemeDto Add(WapUserSettingSchemeDto userSettingSchemeDto);

        /// <summary>
        /// 删除用户配置参数
        /// </summary>
        /// <param name="schemeId">用户配置参数编号</param>
        /// <returns>是否删除成功，true成功，false失败</returns>
        bool Remove(int schemeId);

        /// <summary>
        /// 根据参数编号修改用户配置参数
        /// </summary>
        /// <param name="userSettingSchemeDto">用户配置参数对象</param>
        /// <returns>更新成功，返回更新后的对象;更新失败，返回空</returns>
        WapUserSettingSchemeDto Modify(WapUserSettingSchemeDto userSettingSchemeDto);

        /// <summary>
        /// 根据参数编号获取用户配置参数
        /// </summary>
        /// <param name="schemeId">参数编号</param>
        /// <returns>用户配置参数对象<returns>
        WapUserSettingSchemeDto Get(int schemeId);
    }
}

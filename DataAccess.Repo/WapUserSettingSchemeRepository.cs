using SH3H.SDK.DataAccess.Repo;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.DataAccess.Repo
{
    /// <summary>
    /// 定义用户配置参数仓库
    /// </summary>
    public class WapUserSettingSchemeRepository :
        Repository<IWapUserSettingSchemeStorage>,
        IWapUserSettingSchemeRepository
    {
        /// <summary>
        /// 添加用户配置参数
        /// </summary>
        /// <param name="userSettingScheme">用户配置参数对象</param>
        /// <returns>新增后的用户配置参数对象</returns>
        public WapUserSettingScheme Add(WapUserSettingScheme userSettingScheme)
        {
            return Storage.Insert(userSettingScheme);    
        }

        /// <summary>
        /// 删除用户配置参数
        /// </summary>
        /// <param name="schemeId">用户配置参数编号</param>
        /// <returns>是否删除成功，true成功，false失败</returns>
        public bool Remove(int schemeId)
        {
            return Storage.Delete(schemeId);
        }

        /// <summary>
        /// 根据参数编号修改用户配置参数
        /// </summary>
        /// <param name="userSettingScheme">用户配置参数对象</param>
        /// <returns>更新成功，返回更新后的对象;更新失败，返回空</returns>
        public WapUserSettingScheme Modify(WapUserSettingScheme userSettingScheme)
        {
            return Storage.Update(userSettingScheme);
        }

        /// <summary>
        /// 根据参数编号获取用户配置参数
        /// </summary>
        /// <param name="schemeId">参数编号</param>
        /// <returns>用户配置参数对象<returns>
        public WapUserSettingScheme Get(int schemeId)
        {
            return Storage.Select(schemeId);
        }
    }
}

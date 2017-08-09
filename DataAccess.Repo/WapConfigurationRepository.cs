using SH3H.SDK.DataAccess.Repo;
using SH3H.WAP.DataAccess;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.DataAccess.Repo
{
    public class WapConfigurationRepository
        : Repository<IWapConfigurationStorage>
        , IWapConfigurationRepository
    {
        /// <summary>
        /// 添加系统配置
        /// </summary>
        /// <param name="configuration">系统配置对象</param>
        /// <returns>系统配置对象</returns>
        public WapConfiguration Insert(WapConfiguration configuration)
        {
            return Storage.Insert(configuration);
        }

        /// <summary>
        /// 删除系统配置
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>是否删除成功</returns>
        public bool Delete(int id)
        {
            return Storage.Delete(id);
        }

        /// <summary>
        /// 修改系统配置
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="configuration">系统配置对象</param>
        /// <returns>返回时否修改成功</returns>
        public bool Update(int id, WapConfiguration configuration)
        {

            return Storage.Update(id, configuration);
        }

        /// <summary>
        /// 通过编号查询系统配置
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>系统配置对象</returns>
        public WapConfiguration SelectById(int id)
        {
            return Storage.SelectById(id);
        }

        /// <summary>
        /// 通过配置编码和分组获取系统配置列表
        /// </summary>
        /// <param name="appCode">app编码</param>
        /// <param name="group">配置分组</param>
        /// <returns>系统配置对象</returns>
        public IEnumerable<WapConfiguration> SelectByAppAndGroup(string appCode, string group)
        {
            return Storage.SelectByAppAndGroup(appCode, group);
        }

        /// <summary>
        /// 获取指定应用下的配置
        /// </summary>
        /// <param name="appCode">配置编码</param>
        /// <returns>系统配置对象</returns>
        public IEnumerable<WapConfiguration> GetConfigsByAppCode(string appCode)
        {
            return Storage.GetConfigsByAppCode(appCode);
        }

        /// <summary>
        /// 通过配置编码获取启用的分组
        /// </summary>
        /// <param name="appCode">应用编码</param>
        /// <returns>系统配置对象</returns>
        public IEnumerable<WapConfiguration> SelectGroupsByApp(string appCode)
        {
            return Storage.SelectGroupsByApp(appCode);
        }

        /// <summary>
        /// 根据配置code更新配置组code
        /// </summary>
        /// <param name="configCode">配置标识</param>
        /// <param name="group">配置组标识</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateConfigParent(string configCode, string group)
        {
            return Storage.UpdateConfigParent(configCode, group);
        }

        /// <summary>
        /// 修改配置状态
        /// </summary>
        /// <param name="id">配置ID</param>
        /// <param name="active">激活状态</param>
        /// <returns>是否修改成功</returns>
        public bool UpdateConfigState(int id, bool active)
        {
            return Storage.UpdateConfigState(id, active);
        }

        /// <summary>
        /// 获取所有系统配置
        /// </summary>
        /// <returns>系统配置对象列表</returns>
        public IEnumerable<WapConfiguration> SelectAll()
        {
            return Storage.SelectAll();
        }

    }
}

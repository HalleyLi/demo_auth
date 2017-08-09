using SH3H.SDK.DataAccess.Repo;
using SH3H.SDK.Infrastructure.Caching;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Share;
using System.Collections.Generic;

namespace SH3H.WAP.Auth.DataAccess.Repo
{
    /// <summary>
    /// 定义功能组对象仓库
    /// </summary>
    public class WapFunctionGroupRepository :
         Repository<IWapFunctionGroupStorage>
        , IWapFunctionGroupRepository
    {
        #region  基础

        ///<summary> 
        ///获取功能点组 
        ///</summary> 
        /// <param name="functiongroupkey"></param> 
        ///<return></return> 
        public WapFunctionGroup GetFunctionGroup(string functiongroupkey)
        {
            var item = Storage.GetFunctionGroup(functiongroupkey);
            return item;
        }

        /// <summary>
        ///  获取功能点组集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WapFunctionGroup> GetAllFunctionGroupByAppIdentity(string appidentity)
        {
            return Storage.GetAllFunctionGroupByAppIdentity(appidentity);
        }


        /// <summary>
        ///  获取功能点组集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WapFunctionGroup> GetAllFunctionGroupByAppKey(string appkey)
        {
            return Storage.GetAllFunctionGroupByAppKey(appkey);
        }


        /// <summary>
        ///  获取功能点组集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WapFunctionGroup> GetAllFunctionGroup()
        {
            return Storage.GetAllFunctionGroup();
        }

        /// <summary>
        ///新增功能点组
        /// </summary>
        /// <param name="functiongroup">功能点</param>
        /// <returns></returns> 
        public bool AddFunctionGroup(WapFunctionGroup functiongroup)
        {
            return Storage.AddFunctionGroup(functiongroup);
        }

        /// <summary>
        /// 删除功能点组
        /// </summary>
        /// <param name="functiongroup">功能点组</param>
        /// <returns></returns> 
        public bool DeleteFunctionGroup(string functiongroupkey)
        {
            return Storage.DeleteFunctionGroup(functiongroupkey);
        }

        /// <summary>
        /// 更新功能点组
        /// </summary>
        /// <param name="">FunctionGroup</param>
        /// <returns></returns> 
        public bool UpdateFunctionGroup(WapFunctionGroup functiongroup)
        {
            return Storage.UpdateFunctionGroup(functiongroup);
        }

        /// <summary>
        /// 更新启用状态
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        public bool UpdateFunctionGroupActive(string key, bool active)
        {
            return Storage.UpdateFunctionGroupActive(key, active);
        }

        /// <summary>
        /// 通过appKey和groupName获取功能点组(该功能点组是否存在)
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <param name="groupName">功能点组名称</param>
        /// <returns>存在返回true\不存在返回false</returns>
        public bool IsHaveFuncGroupByNameAndAppKey(string appKey, string groupName)
        {
            return Storage.IsHaveFuncGroupByNameAndAppKey(appKey, groupName);
        }
        #endregion


    }
}

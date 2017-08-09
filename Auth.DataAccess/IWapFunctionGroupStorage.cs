using SH3H.SDK.DataAccess.Core;
using SH3H.WAP.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess
{
    /// <summary>
    /// 定义功能组数据库存储对象
    /// </summary>
    public interface IWapFunctionGroupStorage : IStorage<WapFunctionGroup>
    {

        #region  基础

        ///<summary> 
        ///获取功能点组 
        ///</summary> 
        /// <param name="functiongroupkey"></param> 
        ///<return></return> 
        WapFunctionGroup GetFunctionGroup(string functiongroupkey);

        /// <summary>
        ///  获取功能点组集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<WapFunctionGroup> GetAllFunctionGroup();

        /// <summary>
        ///  获取功能点组集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<WapFunctionGroup> GetAllFunctionGroupByAppIdentity(string appidentity);

        /// <summary>
        ///  获取功能点组集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<WapFunctionGroup> GetAllFunctionGroupByAppKey(string appkey);

        /// <summary>
        ///新增功能点组
        /// </summary>
        /// <param name="functiongroup">功能点</param>
        /// <returns></returns> 
        bool AddFunctionGroup(WapFunctionGroup functiongroup);

        /// <summary>
        /// 删除功能点组
        /// </summary>
        /// <param name="functiongroup">功能点组</param>
        /// <returns></returns> 
        bool DeleteFunctionGroup(string functiongroupkey);

        /// <summary>
        /// 更新功能点组
        /// </summary>
        /// <param name="functiongroup">FunctionGroup</param>
        /// <returns></returns> 
        bool UpdateFunctionGroup(WapFunctionGroup functiongroup);

        /// <summary>
        /// 更新启用状态
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        bool UpdateFunctionGroupActive(string key, bool active);

        /// <summary>
        /// 通过appKey和groupName获取功能点组(该功能点组是否存在)
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <param name="groupName">功能点组名称</param>
        /// <returns>存在返回true\不存在返回false</returns>
        bool IsHaveFuncGroupByNameAndAppKey(string appKey, string groupName);

        #endregion

    }
}

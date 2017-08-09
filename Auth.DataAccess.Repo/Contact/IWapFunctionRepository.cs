using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using System.Collections.Generic;

namespace SH3H.WAP.Auth.DataAccess.Repo.Contact
{
    /// <summary>
    ///  定义功点及其相关组数据仓储
    /// </summary>
    public interface IWapFunctionRepository
    {
        /// <summary>
        /// 新增功能点组中功能点
        /// </summary>
        /// <param name="relative">功能点设置</param>
        /// <returns>是否成功</returns>
        bool AddFunction(WapFuncGroupRelativeDto relative);

        ///<summary> 
        ///获取功能点 
        ///</summary> 
        /// <param name="functionkey">功能点主键</param> 
        ///<return>功能点</return> 
        WapFunction GetFunction(string funckey);

        /// <summary>
        ///  获取功能点集合
        /// </summary>
        /// <returns>功能点集合</returns>
        IEnumerable<WapFunction> GetAllFunction();

        /// <summary>
        ///新增功能点
        /// </summary>
        /// <param name="function">功能点</param>
        /// <returns>是否成功</returns> 
        bool AddFunction(WapFunction function);

        /// <summary>
        /// 删除功能点
        /// </summary>
        /// <param name="funckey">功能点主键</param>
        /// <returns>是否成功</returns> 
        bool DeleteFunction(string funckey);


        /// <summary>
        /// 更新功能点
        /// </summary>
        /// <param name="function">功能点</param>
        /// <returns>是否成功</returns> 
        bool UpdateFunction(WapFunction function);

        /// <summary>
        /// 更新启用状态
        /// </summary>
        /// <param name="funckey">功能点主键</param>
        /// <param name="active">激活状态</param>
        /// <returns>是否激活</returns>
        bool UpdateFunctionActive(string funckey, bool active);

        ///<summary> 
        ///获取功能点 
        ///</summary> 
        /// <param name="code">功能点Code</param> 
        ///<return>功能点 </return> 
        WapFunction GetFunctionByCode(string code);

        /// <summary>
        /// 功能点模糊查询
        /// </summary>
        /// <param name="searchText">搜索关键字</param>
        /// <returns>功能点列表</returns>
        IEnumerable<WapFunction> FuzzySearch(string searchText);

        /// <summary>
        /// 通过appKey和funcCode获取功能点
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <param name="funcCode">功能点Code</param>
        /// <returns>存在返回true\不存在返回false</returns>
        bool IsHaveFuncByFuncCodeAndAppKey(string appKey, string funcCode);


    }
}

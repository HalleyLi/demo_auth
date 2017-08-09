using SH3H.SDK.DataAccess.Repo;
using SH3H.SDK.Infrastructure.Caching;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess.Repo
{
    /// <summary>
    /// 定义功能点及其相关对象仓库
    /// </summary>
    public class WapFunctionRepository :
         Repository<IWapFunctionStorage>
        , IWapFunctionRepository
    {
        /// <summary>
        /// 向某个功能点组添加一个功能点
        /// </summary>
        /// <param name="relative"></param>
        /// <returns></returns>
        public bool AddFunction(WapFuncGroupRelativeDto relative)
        {
            return Storage.AddFunction(relative);
        }


        ///<summary> 
        ///获取功能点 
        ///</summary> 
        /// <param name="functionkey"></param> 
        ///<return></return> 
        public WapFunction GetFunction(string functionkey)
        {
            var item = Storage.GetFunction(functionkey);
            return item;
        }

        /// <summary>
        ///  获取功能点集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WapFunction> GetAllFunction()
        {
            return Storage.GetAllFunction();
        }

        /// <summary>
        ///新增功能点
        /// </summary>
        /// <param name="function">功能点</param>
        /// <returns></returns> 
        public bool AddFunction(WapFunction function)
        {
            return Storage.AddFunction(function);
        }

        /// <summary>
        /// 删除功能点
        /// </summary>
        /// <param name="functionkey">功能点</param>
        /// <returns></returns> 
        public bool DeleteFunction(string functionkey)
        {
            return Storage.DeleteFunction(functionkey);
        }

        /// <summary>
        /// 更新功能点
        /// </summary>
        /// <param name="function">Function</param>
        /// <returns></returns> 
        public bool UpdateFunction(WapFunction function)
        {
            return Storage.UpdateFunction(function);
        }

        /// <summary>
        /// 更新启用状态
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        public bool UpdateFunctionActive(string key, bool active)
        {
            return Storage.UpdateFunctionActive(key, active);
        }

        ///<summary> 
        ///获取功能点 
        ///</summary> 
        /// <param name="code"></param> 
        ///<return></return> 
        public WapFunction GetFunctionByCode(string code)
        {
            var result = Storage.GetFunctionByCode(code);
            return result;
        }

        /// <summary>
        /// 功能点模糊查询
        /// </summary>
        /// <param name="searchText">搜索关键字</param>
        /// <returns>功能点列表</returns>
        public IEnumerable<WapFunction> FuzzySearch(string searchText)
        {
            return Storage.FuzzySearch(searchText);
        }

        /// <summary>
        /// 通过appKey和funcCode获取功能点
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <param name="funcCode">功能点Code</param>
        /// <returns>存在返回true\不存在返回false</returns>
        public bool IsHaveFuncByFuncCodeAndAppKey(string appKey, string funcCode)
        {
            return Storage.IsHaveFuncByFuncCodeAndAppKey(appKey, funcCode);
        }


    }
}

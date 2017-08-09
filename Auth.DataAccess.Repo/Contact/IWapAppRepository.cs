using SH3H.SDK.DataAccess.Repo;
using SH3H.WAP.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess.Repo.Contact
{
    public interface IWapAppRepository 
    {
        /// <summary>
        /// 添加应用
        /// </summary>
        /// <param name="app">应用对象</param>
        /// <returns>添加成功返回添加的对象,失败返回Null</returns>
        WapApp AddApp(WapApp app);  
        /// <summary>
        /// 修改应用信息
        /// </summary>
        /// <param name="appKey">应用Key</param>
        /// <param name="app">应用对象</param>
        /// <returns>成功或失败</returns>
        WapApp ModifyApp(string appKey, WapApp app);

        /// <summary>
        /// 修改应用状态
        /// </summary>
        /// <param name="appKey">KEY</param>
        /// <param name="model">应用状态对象信息</param>
        /// <returns>修改成功,返回true.否则返回false.</returns>
        bool ModifyState(string appKey, WapAppState model);

        /// <summary>
        /// 获取所有的应用程序列表
        /// </summary>
        /// <returns>返回应用程序列表</returns>
        IEnumerable<WapApp> GetAllApps();

        /// <summary>
        /// 通过搜索关键字获取应用列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IEnumerable<WapApp> SelectListByKeyword(string keyword);

        /// <summary>
        /// 通过appKey获取应用
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        WapApp SelectAppByKey(string appKey);

        /// <summary>
        /// 通过appIdentity获取应用
        /// </summary>
        /// <param name="appIdentity"></param>
        /// <returns></returns>
        WapApp SelectAppByIdentity(string appIdentity);

        /// <summary>
        /// 根据应用的appKey获取指定应用的角色导出脚本
        /// </summary>
        /// <param name="appKey">应用KEY</param>
        /// <returns>返回字符串</returns>
        string GetRoleString(string appKey);

        /// <summary>
        /// 根据应用的appKey获取导出字符串
        /// </summary>
        /// <param name="appKey">应用KEY</param>
        /// <param name="isJson">是否JSON字符串</param>
        /// <returns>返回字符串</returns>
        string GetAppString(string appKey, bool isJson);             

        /// <summary>
        /// 删除应用
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <returns>是否删除成功</returns>
        bool DeleteApp(string appKey);
        
        /// <summary>
        /// 禁用应用
        /// </summary>
        /// <param name="appKey">应用标识</param>
        /// <returns>是否禁用成功</returns>
        bool DeactiveApp(string appKey);        

        /// <summary>
        /// 启用应用
        /// </summary>
        /// <param name="appKey">应用标识</param>
        /// <returns>是否启用成功</returns>
        bool ActiveApp(string appKey);         
       
        /// <summary>
        /// 获取当前用户授权的应用程序列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回应用程序列表</returns>
        IEnumerable<WapApp> GetAuthorizedApps(int userId);               

        /// <summary>
        /// 根据用户KEY获取APP
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns></returns>
        IEnumerable<WapApp> GetAppByUserKey(string userKey);     
  
        /// <summary>
        /// 判断应用标识是否存在
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>如果存在,返回true,否则返回false.</returns>
        bool IsExitAppIdentity(string appIdentity);
    }
}

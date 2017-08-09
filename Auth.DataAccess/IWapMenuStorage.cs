using SH3H.SDK.DataAccess.Core;
using SH3H.WAP.Auth.Model;
using System.Collections.Generic;

namespace SH3H.WAP.Auth.DataAccess
{
    /// <summary>
    /// 定义WSDP菜单数据访问器
    /// </summary>
    public interface IWapMenuStorage
           : IStorage<WapMenu>
    {

        /// <summary>
        /// 获取所有的菜单项列表
        /// </summary>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        IEnumerable<WapMenu> SelectAllAppMenus();

        /// <summary>
        /// 获取指定应用的菜单项列表
        /// </summary>
        /// <param name="appKey">APP Key</param>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        IEnumerable<WapMenu> SelectAppMenus(string appKey);

        /// <summary>
        /// 通过appIdentity获取指定应用的菜单项列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        IEnumerable<WapMenu> SelectAppMenusByAppId(string appIdentity);

        /// <summary>
        /// 通过menukey获取菜单
        /// </summary>
        /// <param name="menuekey"></param>
        /// <returns></returns>
        WapMenu GetMenu(string menukey);

        /// <summary>
        /// 通过appKey和menuName获取菜单
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <param name="menuName">菜单名称</param>
        /// <returns>菜单对象</returns>
        WapMenu GetMenuByNameAndAppKey(string appKey, string menuName);

        /// <summary>
        /// 通过菜单对象条件查询菜单对象列表 
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        IEnumerable<WapMenu> GetMenu(WapMenu menu);

        /// <summary>
        /// 通过menukey数组获取菜单列表
        /// </summary>
        /// <param name="menukeys"></param>
        /// <returns></returns>
        IEnumerable<WapMenu> GetMenuList(string[] menukeys);

        /// <summary>
        /// 通过查询条件获取菜单列表
        /// </summary>
        /// <param name="userkey">用户Key</param>
        /// <param name="appkey">应用Key</param>
        /// <returns></returns>
        IEnumerable<WapMenu> GetUserMenu(string userkey, string appkey);

        /// <summary>
        /// 通过查询条件获取菜单列表
        /// </summary>
        /// <param name="useraccount">用户账号</param>
        /// <param name="appidentity">应用标识</param>
        /// <returns></returns>
        IEnumerable<WapMenu> GetMenusByUA(string useraccount, string appidentity);

        /// <summary>
        /// 通过功能点Key列表获取菜单列表
        /// </summary>
        /// <param name="funckey"></param>
        /// <returns></returns>
        IEnumerable<WapMenu> GetFuncMenu(string funckey);

        /// <summary>
        /// 查询指定应用中对应URL的菜单项
        /// </summary>
        /// <param name="appname">应用名称</param>
        /// <param name="url">URL地址</param>
        /// <returns>返回菜单项列表</returns>
        IEnumerable<WapMenu> GetMenuByUrl(string appname, string url);

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu">菜单对象</param>
        /// <returns>是否添加成功</returns>
        bool AddMenu(WapMenu menu);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menuKey">菜单Key</param>
        /// <param name="menu">菜单对象</param>
        /// <returns>是否修改成功</returns>
        bool UpdateMenu(string menuKey, WapMenu menu);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuKey">菜单Key</param>
        /// <returns>是否修改成功</returns>
        bool DeleteMenu(string menuKey);

        /// <summary>
        /// 禁用菜单
        /// </summary>
        /// <param name="menuKey">菜单标识</param>
        /// <returns>是否禁用成功</returns>
        bool DeactiveMenu(string menuKey);

        /// <summary>
        /// 启用菜单
        /// </summary>
        /// <param name="menuKey">菜单标识</param>
        /// <returns>是否启用成功</returns>
        bool ActiveMenu(string menuKey);

        /// <summary>
        /// 修改菜单状态菜单
        /// </summary>
        /// <param name="menuKey"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        bool UpdateMenuState(string menuKey, bool active);

        /// <summary>
        /// 根据菜单标识更新父菜单标识
        /// </summary>
        /// <param name="menuKey">菜单标识</param>
        /// <param name="parentMenuKey">父菜单标识</param>
        /// <returns>是否更新成功</returns>
        bool UpdateMenuParent(string menuKey, string parentMenuKey);

        /// <summary>
        /// 统一更新排序值
        /// </summary>
        /// <param name="sortIndexes">编号索引号字典</param>
        bool UpdateSortIndexByKey(Dictionary<string, int> sortIndexes);
    }
}

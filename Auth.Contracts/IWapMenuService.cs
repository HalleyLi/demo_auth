using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Contracts
{
    /// <summary>
    /// 菜单接口
    /// </summary>
    public interface IWapMenuService
    {

        /// <summary>
        /// 获取所有的菜单项列表
        /// </summary>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        IEnumerable<WapMenuDto> SelectAllAppMenus();

        /// <summary>
        /// 获取指定应用的菜单项列表
        /// </summary>
        /// <param name="appKey">APP Key</param>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        IEnumerable<WapMenuDto> SelectAppMenus(string appKey);

        /// <summary>
        /// 通过appIdentity获取指定应用的菜单项列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        IEnumerable<WapMenuDto> SelectAppMenusByAppId(string appIdentity);

        /// <summary>
        /// 通过menukey获取菜单
        /// </summary>
        /// <param name="menuekey"></param>
        /// <returns></returns>
        WapMenuDto GetMenu(string menukey);

        /// <summary>
        /// 通过菜单对象条件查询菜单对象列表 
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        IEnumerable<WapMenuDto> GetMenu(WapMenuDto menu);

        /// <summary>
        /// 通过查询条件获取菜单列表
        /// </summary>
        /// <param name="userkey">用户Key</param>
        /// <param name="appkey">应用Key</param>
        /// <returns></returns>
        IEnumerable<WapMenuDto> GetUserMenu(string userkey, string appkey);

        /// <summary>
        /// 通过功能点Key列表获取菜单列表
        /// </summary>
        /// <param name="funckey"></param>
        /// <returns></returns>
        IEnumerable<WapMenuDto> GetFuncMenu(string funckey);

        /// <summary>
        /// 查询指定应用中对应URL的菜单项
        /// </summary>
        /// <param name="appname">应用名称</param>
        /// <param name="url">URL地址</param>
        /// <returns>返回菜单项列表</returns>
        IEnumerable<WapMenuDto> GetMenuByUrl(string appname, string url);

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu">菜单对象</param>
        /// <returns>是否添加成功</returns>
        WapMenuDto AddMenu(WapMenuDto menu);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menuKey">菜单Key</param>
        /// <param name="menu">菜单对象</param>
        /// <returns>是否修改成功</returns>
        WapMenuDto UpdateMenu(string menuKey, WapMenuDto menu);

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
        ///修改菜单状态
        /// </summary>
        /// <param name="menuKey"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        bool UpdateMenuState(string menuKey, WapStateDto active);

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
        bool ModifySortIndexByKey(Dictionary<string, int> sortIndexes);
    }
}

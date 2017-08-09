using SH3H.SDK.DataAccess.Repo;
using SH3H.SDK.Infrastructure.Caching;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess.Repo
{
    public class WapMenuRepository :
        Repository<IWapMenuStorage>, IWapMenuRepository
    {
        /// <summary>
        /// 获取所有的菜单项列表
        /// </summary>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        public IEnumerable<WapMenu> SelectAllAppMenus()
        {
            return Storage.SelectAllAppMenus();
        }

        /// <summary>
        /// 获取指定应用的菜单项列表
        /// </summary>
        /// <param name="appKey">APP Key</param>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        public IEnumerable<WapMenu> SelectAppMenus(string appKey)
        {
            return Storage.SelectAppMenus(appKey);
        }

        /// <summary>
        /// 通过appIdentity获取指定应用的菜单项列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        public IEnumerable<WapMenu> SelectAppMenusByAppId(string appIdentity)
        {
            //return Storage.SelectAppMenusByAppId(appIdentity);
            var menus = Storage.SelectAppMenusByAppId(appIdentity);

            Dictionary<string, WapMenu> dic = menus.ToDictionary(m => m.MenuKey);
            foreach (var menu in menus)
            {
                WapMenu parent = null;
                if (dic.TryGetValue(menu.ParentKey, out parent))
                    parent.Menus.Add(menu);
            }

            return dic.Values.Where(m => m.ParentKey == "00000000-0000-0000-0000-000000000000");
        }

        /// <summary>
        /// 通过menukey获取菜单
        /// </summary>
        /// <param name="menuekey"></param>
        /// <returns></returns>
        public WapMenu GetMenu(string menukey)
        {
            return Storage.GetMenu(menukey);
        }

        /// <summary>
        /// 通过菜单对象条件查询菜单对象列表 
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public IEnumerable<WapMenu> GetMenu(WapMenu menu)
        {
            return Storage.GetMenu(menu);
        }

        /// <summary>
        /// 通过menukey数组获取菜单列表
        /// </summary>
        /// <param name="menukey"></param>
        /// <returns></returns>
        public IEnumerable<WapMenu> GetMenuList(string[] menukey)
        {
            return Storage.GetMenuList(menukey);
        }

        /// <summary>
        /// 通过查询条件获取菜单列表
        /// </summary>
        /// <param name="userkey">用户Key</param>
        /// <param name="appkey">应用Key</param>
        /// <returns></returns>
        public IEnumerable<WapMenu> GetUserMenu(string userkey, string appkey)
        {
            return Storage.GetUserMenu(userkey, appkey);
        }

        /// <summary>
        /// 通过查询条件获取菜单列表
        /// </summary>
        /// <param name="useraccount">用户账号</param>
        /// <param name="appidentity">应用标识</param>
        /// <returns></returns>
        public IEnumerable<WapMenu> GetMenusByUA(string useraccount, string appidentity)
        {
            return Storage.GetMenusByUA(useraccount, appidentity);
        }

        /// <summary>
        /// 通过功能点Key列表获取菜单列表
        /// </summary>
        /// <param name="funckey"></param>
        /// <returns></returns>
        public IEnumerable<WapMenu> GetFuncMenu(string funckey)
        {
            return Storage.GetFuncMenu(funckey);
        }

        /// <summary>
        /// 查询指定应用中对应URL的菜单项
        /// </summary>
        /// <param name="appname">应用名称</param>
        /// <param name="url">URL地址</param>
        /// <returns>返回菜单项列表</returns>
        public IEnumerable<WapMenu> GetMenuByUrl(string appname, string url)
        {
            return Storage.GetMenuByUrl(appname, url);
        }


        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu">菜单对象</param>
        /// <returns>是否添加成功</returns>
        public bool AddMenu(WapMenu menu)
        {
            return Storage.AddMenu(menu);
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menuKey">菜单Key</param>
        /// <param name="menu">菜单对象</param>
        /// <returns>是否修改成功</returns>
        public bool UpdateMenu(string menuKey, WapMenu menu)
        {
            return Storage.UpdateMenu(menuKey, menu);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuKey">菜单Key</param>
        /// <returns>是否修改成功</returns>
        public bool DeleteMenu(string menuKey)
        {
            return Storage.DeleteMenu(menuKey);
        }

        /// <summary>
        /// 禁用菜单
        /// </summary>
        /// <param name="menuKey">菜单标识</param>
        /// <returns>是否禁用成功</returns>
        public bool DeactiveMenu(string menuKey)
        {
            return Storage.DeactiveMenu(menuKey);
        }

        /// <summary>
        /// 启用菜单
        /// </summary>
        /// <param name="menuKey">菜单标识</param>
        /// <returns>是否启用成功</returns>
        public bool ActiveMenu(string menuKey)
        {
            return Storage.ActiveMenu(menuKey);
        }

        /// <summary>
        /// 修改菜单状态菜单
        /// </summary>
        /// <param name="menuKey"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public bool UpdateMenuState(string menuKey, bool active)
        {
            return Storage.UpdateMenuState(menuKey, active);
        }


        /// <summary>
        /// 根据菜单标识更新父菜单标识
        /// </summary>
        /// <param name="menuKey">菜单标识</param>
        /// <param name="parentMenuKey">父菜单标识</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateMenuParent(string menuKey, string parentMenuKey)
        {
            return Storage.UpdateMenuParent(menuKey, parentMenuKey);

        }

        /// <summary>
        /// 统一更新排序值
        /// </summary>
        /// <param name="sortIndexes">编号索引号字典</param>
        public bool ModifySortIndexByKey(Dictionary<string, int> sortIndexes)
        {
            return Storage.UpdateSortIndexByKey(sortIndexes);
        }

    }
}

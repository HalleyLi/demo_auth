using SH3H.SDK.WebApi.Controllers;
using SH3H.SDK.WebApi.Core;
using SH3H.SDK.WebApi.Core.Models;
using SH3H.WAP.Auth.Contracts;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WapConsts = SH3H.WAP.Share.Consts;

namespace SH3H.WAP.Auth.Controllers
{
    /// <summary>
    /// 定义WAP权限认证菜单服务控制器
    /// </summary>
    [Resource("wapMenuRes")]
    [RoutePrefix(WapConsts.URL_PREFIX_WAP)]
    public class WapMenuController :
        BaseController<IWapMenuService>
    {
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="menu">菜单对象</param>
        /// <returns>是否新增成功</returns>
        [HttpPost]
        [Route("menus")]
        [ActionName("createMenu")]
        public WapResponse<WapMenuDto> AddMenu(WapMenuDto menu)
        {
            return new WapResponse<WapMenuDto>(Service.AddMenu(menu));
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menuKey">菜单Key</param>
        /// <param name="menu">菜单对象</param>
        /// <returns>是否修改成功</returns>
        [HttpPut]
        [Route("menus/{menuKey}")]
        [ActionName("updateMenu")]
        public WapResponse<WapMenuDto> UpdateMenu(string menuKey, WapMenuDto menu)
        {

            var result = Service.UpdateMenu(menuKey, menu);


            return new WapResponse<WapMenuDto>(result);
        }

        /// <summary>
        /// 激活菜单
        /// </summary>
        /// <param name="menuKey">菜单标识</param>
        /// <returns>是否启用成功</returns>
        [HttpPut]
        [Route("{menuKey}/active")]
        [ActionName("activeMenu")]
        private WapBoolean ActiveMenu(string menuKey)
        {
            var result = Service.ActiveMenu(menuKey);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 禁用菜单
        /// </summary>
        /// <param name="menuKey">菜单标识</param>
        /// <returns>是否禁用成功</returns>
        [HttpPut]
        [Route("{menuKey}/deactive")]
        [ActionName("deactiveMenu")]
        private WapBoolean DeactiveMenu(string menuKey)
        {
            var result = Service.DeactiveMenu(menuKey);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 修改菜单状态
        /// </summary>
        /// <param name="menuKey">菜单标识</param>
        /// <param name="active">是否启用</param>
        /// <returns>是否禁用成功</returns>
        [HttpPatch]
        [Route("menus/{menuKey}/state")]
        [ActionName("updateMenuState")]
        public WapBoolean UpdateMenuState(string menuKey, [FromBody]WapStateDto active)
        {
            var result = Service.UpdateMenuState(menuKey, active);
            return new WapBoolean(result);
        }



        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        [HttpGet]
        [Route("menus")]
        [ActionName("getAllMenus")]
        public WapCollection<WapMenuDto> SelectAllAppMenus()
        {
            var result = Service.SelectAllAppMenus();
            return new WapCollection<WapMenuDto>(result);
        }

        /// <summary>
        /// 获取指定应用的所有菜单
        /// </summary>
        /// <param name="appKey">APP Key</param>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        [HttpGet]
        [Route("apps/{appKey}/menus")]
        [ActionName("getMenusByApp")]
        public WapCollection<WapMenuDto> SelectAppMenus(string appKey)
        {
            var result = Service.SelectAppMenus(appKey);
            return new WapCollection<WapMenuDto>(result);
        }

        /// <summary>
        /// 根据应用标识获取所有菜单
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        [HttpGet]
        [Route("menus")]
        [ActionName("getMenusByAppIdentity")]
        public WapCollection<WapMenuDto> SelectAppMenusByAppId(string appIdentity)
        {
            var result = Service.SelectAppMenusByAppId(appIdentity);
            return new WapCollection<WapMenuDto>(result);
        }

        /// <summary>
        /// 获取指定应用和用户的所有菜单
        /// </summary>
        /// <param name="userkey">用户Key</param>
        /// <param name="appKey">应用Key</param>
        /// <returns></returns>
        [HttpGet]
        [Route("menus")]
        [ActionName("getMenusByAppAndUser")]
        public WapCollection<WapMenuDto> GetUserMenu(string userkey, string appKey)
        {
            var result = Service.GetUserMenu(userkey, appKey);
            return new WapCollection<WapMenuDto>(result);
        }


        /// <summary>
        /// 获取指定功能点的所有菜单
        /// </summary>
        /// <param name="funckey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("queryByFunc")]
        [ActionName("getByFunc")]
        private WapCollection<WapMenuDto> GetFuncMenu(string funckey)
        {
            var result = Service.GetFuncMenu(funckey);
            return new WapCollection<WapMenuDto>(result);
        }

        /// <summary>
        /// 获取指定菜单
        /// </summary>
        /// <param name="menuKey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("menus/{menuKey}")]
        [ActionName("getMenuByKey")]
        public WapResponse<WapMenuDto> GetMenu(string menuKey)
        {
            var result = Service.GetMenu(menuKey);
            return new WapResponse<WapMenuDto>(result);
        }


        /// <summary>
        /// 通过菜单对象条件查询菜单对象列表 
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getmenu2")]
        [ActionName("getmenu2")]
        private WapCollection<WapMenuDto> GetMenu(WapMenuDto menu)
        {
            var result = Service.GetMenu(menu);
            return new WapCollection<WapMenuDto>(result);
        }

        /// <summary>
        /// 通过应用名称和Url条件对应URL的菜单项
        /// </summary>
        /// <param name="appname">应用名称</param>
        /// <param name="url">URL地址</param>
        /// <returns>返回菜单项列表</returns>
        [HttpGet]
        [Route("getmenubyurl")]
        [ActionName("getmenubyurl")]
        private WapCollection<WapMenuDto> GetMenuByUrl(string appname, string url)
        {
            var result = Service.GetMenuByUrl(appname, url);
            return new WapCollection<WapMenuDto>(result);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuKey">菜单Key</param>
        /// <returns>是否修改成功</returns>
        [HttpDelete]
        [Route("{menuKey}")]
        [ActionName("deletemenu")]
        private WapBoolean DeleteMenu(string menuKey)
        {
            var result = Service.DeleteMenu(menuKey);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 更新菜单父子关系
        /// </summary>
        /// <param name="menuKey">菜单标识</param>
        /// <param name="parentMenuKey">父菜单标识</param>
        /// <returns>是否更新成功</returns>
        [HttpPut]
        [Route("menus/{parentMenuKey}/child/{menuKey}")]
        [ActionName("updateMenuRelation")]
        public WapBoolean UpdateMenuParent(string menuKey, string parentMenuKey)
        {
            var result = Service.UpdateMenuParent(menuKey, parentMenuKey);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 统一更新排序值
        /// </summary>
        /// <param name="sortIndexes">编号索引号字典</param>
        /// <returns>是否更新成功</returns>
        [HttpPost]
        [Route("menus/sort")]
        [ActionName("sort")]
        public WapBoolean ModifySortIndexByKey([FromBody]Dictionary<string, int> sortIndexes)
        {
            var result = Service.ModifySortIndexByKey(sortIndexes);
            return new WapBoolean(result);
        }

    }
}

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
    /// <summary>
    /// 定义统一登陆应用对象仓库
    /// </summary>
    public class WapAppRepository : Repository<IWapAppStorage>, IWapAppRepository
    {
        /// <summary>
        /// 添加应用
        /// </summary>
        /// <param name="app">应用对象</param>
        /// <returns>添加成功返回添加的对象,失败返回Null</returns>
        public WapApp AddApp(WapApp app)
        {
            return Storage.AddApp(app);
        }
        /// <summary>
        /// 修改应用信息
        /// </summary>
        /// <param name="appKey">应用Key</param>
        /// <param name="app">应用对象</param>
        /// <returns>成功或失败</returns>
        public WapApp ModifyApp(string appKey, WapApp app)
        {
            return Storage.UpdateApp(appKey, app);

            //var cach = CacheManager.Get();
            
            //var result = Storage.UpdateApp(appKey, app);
            //if (result != null)
            //{
            //    cach.Put<WapApp>(appKey, result, Consts.URL_PREFIX_WAP_APP);
            //}
            //return result;
        }

        /// <summary>
        /// 修改应用状态
        /// </summary>
        /// <param name="appKey">KEY</param>
        /// <param name="dto">应用状态对象信息</param>
        /// <returns>修改成功,返回true.否则返回false.</returns>
        public bool ModifyState(string appKey, WapAppState dto)
        {                        
            var result = Storage.ModifyState(appKey, dto);
            //if (result)
            //{
            //    var cach = CacheManager.Get();
            //    if (cach.Exist(appKey, Consts.URL_PREFIX_WAP_APP))
            //    {
            //        var dtoOld = cach.Get<WapApp>(appKey, Consts.URL_PREFIX_WAP_APP);
            //        dtoOld.Active = dto.Active;
            //        cach.Put<WapApp>(appKey, dtoOld, Consts.URL_PREFIX_WAP_APP);
            //    }
            //}
            return result;
        }

        /// <summary>
        /// 获取所有的应用程序列表
        /// </summary>
        /// <returns>返回应用程序列表</returns>
        public IEnumerable<WapApp> GetAllApps()
        {
            return Storage.SelectAll();
            ////1.获取缓存管理器对象
            //var cach = CacheManager.Get();

            ////2.检查缓存是否存在
            //if (cach.Exist("GetAllApps",Consts.URL_PREFIX_WAP_APP))
            //{
            //    return cach.Get<IEnumerable<WapApp>>("GetAllApps", Consts.URL_PREFIX_WAP_APP);
            //}
            //else
            //{
            //    var item = Storage.SelectAll();
            //    if (item != null)
            //    {
            //        //加入缓存
            //        cach.Put<IEnumerable<WapApp>>("GetAllApps", item, Consts.URL_PREFIX_WAP_APP);
            //    }
            //    return item;
            //}            
        }

        /// <summary>
        /// 通过搜索关键字获取应用列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<WapApp> SelectListByKeyword(string keyword)
        {
            return Storage.SelectListByKeyword(keyword);
        }

        /// <summary>
        /// 通过appKey获取应用
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public WapApp SelectAppByKey(string appKey)
        {
            return Storage.SelectAppByKey(appKey);

            //var cach = CacheManager.Get();

            //if (cach.Exist(appKey,Consts.URL_PREFIX_WAP_APP))
            //{
            //    return cach.Get<WapApp>(appKey, Consts.URL_PREFIX_WAP_APP);
            //}
            //else
            //{
            //    var item = Storage.SelectAppByKey(appKey);
            //    if (item != null)
            //    {
            //        cach.Put<WapApp>(appKey, item, Consts.URL_PREFIX_WAP_APP);
            //    }
            //    return item;
            //}            
        }

        /// <summary>
        /// 通过appIdentity获取应用
        /// </summary>
        /// <param name="appIdentify"></param>
        /// <returns></returns>
        public WapApp SelectAppByIdentity(string appIdentity)
        {
            return Storage.SelectAppByIdentity(appIdentity);
            //var cach = CacheManager.Get();

            //if (cach.Exist(appIdentity, Consts.URL_PREFIX_WAP_APP))
            //{
            //    return cach.Get<WapApp>(appIdentity, Consts.URL_PREFIX_WAP_APP);
            //}
            //else
            //{
            //    var item = Storage.SelectAppByIdentity(appIdentity);
            //    if (item != null)
            //    {
            //        cach.Put<WapApp>(appIdentity, item, Consts.URL_PREFIX_WAP_APP);
            //    }
            //    return item;
            //}
        }

        /// <summary>
        /// 根据用户KEY获取APP
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns></returns>
        public IEnumerable<WapApp> GetAppByUserKey(string userKey)
        {
            return Storage.GetAppByUserKey(userKey);
            //var cach = CacheManager.Get();

            //if (cach.Exist(userKey,Consts.URL_PREFIX_WAP_APP))
            //{
            //    return cach.Get<IEnumerable<WapApp>>(userKey, Consts.URL_PREFIX_WAP_APP);
            //}
            //else
            //{
            //    var item = Storage.GetAppByUserKey(userKey);
            //    if (item != null)
            //    {
            //        cach.Put<IEnumerable<WapApp>>(userKey, item, Consts.URL_PREFIX_WAP_APP);
            //    }
            //    return item;
            //}
        }

        /// <summary>
        /// 根据应用的appKey获取指定应用的角色导出脚本
        /// </summary>
        /// <param name="appKey">应用KEY</param>
        /// <returns>返回字符串</returns>
        public string GetRoleString(string appKey)
        {
            return Storage.GetRoleString(appKey);
        }

        /// <summary>
        /// 根据应用的appKey获取导出字符串
        /// </summary>
        /// <param name="appKey">应用KEY</param>
        /// <param name="isJson">是否JSON字符串</param>
        /// <returns>返回字符串</returns>
        public string GetAppString(string appKey, bool isJson)
        {
           return Storage.GetAppString(appKey, isJson);
        }             

        /// <summary>
        /// 删除应用
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <returns>是否删除成功</returns>
        public bool DeleteApp(string appKey)
        {
            return Storage.DeleteApp(appKey);
        }

        /// <summary>
        /// 禁用应用
        /// </summary>
        /// <param name="appKey">应用标识</param>
        /// <returns>是否禁用成功</returns>
        public bool DeactiveApp(string appKey)
        {
            return Storage.DeactiveApp(appKey);
        }

        /// <summary>
        /// 启用应用
        /// </summary>
        /// <param name="appKey">应用标识</param>
        /// <returns>是否启用成功</returns>
        public bool ActiveApp(string appKey)
        {
            return Storage.ActiveApp(appKey);
        }       

        /// <summary>
        /// 获取当前用户授权的应用程序列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回应用程序列表</returns>
        public IEnumerable<WapApp> GetAuthorizedApps(int userId)
        {
            return Storage.SelectAuthorizedApps(userId);
        }       

        /// <summary>
        /// 判断应用标识是否存在
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>如果存在,返回true,否则返回false.</returns>
        public bool IsExitAppIdentity(string appIdentity)
        {
            return Storage.IsExitAppIdentity(appIdentity);
        }
    }
}

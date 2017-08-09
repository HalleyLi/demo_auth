using SH3H.SDK.Definition;
using SH3H.SDK.Definition.Exceptions;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.SDK.Share;
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

namespace SH3H.WAP.Auth.Controllers
{
    /// <summary>
    /// 定义WAP权限认证应用服务控制器
    /// </summary>
    [Resource("wapAppRes")]
    [RoutePrefix("wap/v2")]
    public class WapAppController
        : BaseController<IWapAppService>
    {
        /// <summary>
        /// 添加应用
        /// </summary>
        /// <param name="app">应用对象</param>
        /// <returns>添加成功返回添加的对象,失败返回Null</returns>
        [HttpPost]
        [Route("apps")]
        [ActionName("createApp")]
        public WapResponse<WapAppDto> AddApp([FromBody]WapAppAddDto app)
        {
            var token = base.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                var result = Service.AddApp(app);
                return new WapResponse<WapAppDto>(result);
            }
            else
            {
                //没有权限认证令牌，不执行新增操作，并返回相应的错误码
                return new WapResponse<WapAppDto>(StateCode.CODE_ACCESS_TOKEN_INVALID,
                    "非法的AccessToken");
            }
        }

        /// <summary>
        /// 修改应用信息
        /// </summary>
        /// <param name="appKey">应用Key</param>
        /// <param name="app">应用对象</param>
        /// <returns>返回修改应用对象是否成功或失败</returns>
        [HttpPut]
        [Route("apps/{appKey}")]
        [ActionName("updateApp")]
        public WapResponse<WapAppDto> ModifyApp([FromUri]string appKey, [FromBody]WapAppUpdateDto app)
        {
            var token = base.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                var result = Service.ModifyApp(appKey, app);
                return new WapResponse<WapAppDto>(result);
            }
            else
            {
                //没有权限认证令牌，不执行操作，并返回相应的错误码
                var result = new WapResponse<WapAppDto>(null);
                result.Code = StateCode.CODE_ACCESS_TOKEN_INVALID;
                result.Message = "非法的AccessToken";
                return result;
            }  
        }

        /// <summary>
        /// 修改应用状态
        /// </summary>
        /// <param name="appKey">KEY</param>
        /// <param name="dto">应用状态对象信息</param>
        /// <returns>修改成功,返回true.否则返回false.</returns>
        [HttpPatch]
        [Route("apps/{appKey}/state")]
        [ActionName("updateAppState")]
        public WapBoolean ModifyState([FromUri]string appKey, [FromBody]WapAppStateDto dto)
        {
            var token = base.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                var result = Service.ModifyState(appKey, dto);
                return new WapBoolean(result);
            }
            else
            {
                //没有权限认证令牌，不执行操作，并返回相应的错误码
                var result = new WapBoolean(false);
                result.Code = StateCode.CODE_ACCESS_TOKEN_INVALID;
                result.Message = "非法的AccessToken";
                return result;
            }
        }

        /// <summary>
        /// 获取所有的应用程序列表
        /// </summary>
        /// <returns>返回应用程序列表</returns>
        [HttpGet]
        [Route("apps")]
        [ActionName("getAllApps")]
        public WapCollection<WapAppDto> GetAllApps()
        {
            var result = Service.GetAllApps();
            return new WapCollection<WapAppDto>(result);
        }

        /// <summary>
        /// 通过搜索关键字获取应用列表
        /// </summary>
        /// <param name="keyword">搜索关键字</param>
        /// <returns>返回搜索到的应用列表</returns>
        [HttpGet]
        [Route("apps")]
        [ActionName("getAllAppsByKeyword")]
        public WapCollection<WapAppDto> SelectListByKeyword(string keyword)
        {
            var result = Service.SelectListByKeyword(keyword);
            return new WapCollection<WapAppDto>(result);
        }

        /// <summary>
        /// 通过appKey获取应用
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <returns>返回获取的应用</returns>
        [HttpGet]
        [Route("apps/{appKey}")]
        [ActionName("getAppByKey")]
        public WapResponse<WapAppDto> SelectAppByKey([FromUri]string appKey)
        {
            var result = Service.SelectAppByKey(appKey);
            return new WapResponse<WapAppDto>(result);
        }

        /// <summary>
        /// 通过appIdentity获取应用
        /// </summary>
        /// <param name="appIdentity">应用Identity</param>
        /// <returns>返回获取的应用</returns>
        [HttpGet]
        [Route("apps")]
        [ActionName("getAppByIdentity")]
        public WapResponse<WapAppDto> SelectAppByIdentity([FromUri]string appIdentity)
        {
            var result = Service.SelectAppByIdentity(appIdentity);
            return new WapResponse<WapAppDto>(result);
        }

        /// <summary>
        /// 根据用户userKey获取指定用户授权的应用程序列表
        /// </summary>
        /// <param name="userKey">用户Key</param>
        /// <returns></returns>
        [HttpGet]
        [Route("users/{userKey}/apps")]
        [ActionName("getAppsByUser")]
        public WapCollection<WapAppDto> GetAppByUserKey(string userKey)
        {
            var result = Service.GetAppByUserKey(userKey);
            return new WapCollection<WapAppDto>(result);     
        }

        /// <summary>
        /// 根据应用的appKey获取指定应用的角色导出脚本
        /// </summary>
        /// <param name="appKey">应用KEY</param>
        /// <returns>返回字符串</returns>
        [HttpGet]
        [Route("apps/{appKey}/roleScript")]
        [ActionName("getRoleString")]
        public WapString GetRoleString(string appKey)
        {
            var result = Service.GetRoleString(appKey);
            return new WapString(result);        
        }

        /// <summary>
        /// 根据应用的appKey获取指定应用的应用导出脚本
        /// </summary>
        /// <param name="appKey">应用KEY</param>
        /// <param name="isJson">设定返回的字符串格式类型</param>
        /// <returns>返回字符串</returns>
        [HttpGet]
        [Route("apps/{appKey}/appScript")]
        [ActionName("getAppString")]
        public WapString GetAppString(string appKey, bool isJson)
        {
            var result = Service.GetAppString(appKey, isJson);
            return new WapString(result);  
        }

        /// <summary>
        /// 启用应用
        /// </summary>
        /// <param name="appKey">应用Key</param>
        /// <returns>是否启用成功</returns>
        [HttpPut]
        [Route("{appKey}/active")]
        [ActionName("activeApp")]
        private WapBoolean ActiveApp([FromUri]string appKey)
        {
            var token = base.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                var result = Service.ActiveApp(appKey);
                return new WapBoolean(result);
            }
            else
            {
                //没有权限认证令牌，不执行操作，并返回相应的错误码
                var result = new WapBoolean(false);
                result.Code = StateCode.CODE_ACCESS_TOKEN_INVALID;
                result.Message = "非法的AccessToken";
                return result;
            }
        }

        /// <summary>
        /// 禁用应用
        /// </summary>
        /// <param name="appKey">应用Key</param>
        /// <returns>是否禁用成功</returns>
        [HttpPut]
        [Route("{appKey}/deactive")]
        [ActionName("deactiveApp")]
        private WapBoolean DeactiveApp([FromUri]string appKey)
        {
            var token = base.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                var result = Service.DeactiveApp(appKey);
                return new WapBoolean(result);
            }
            else
            {
                //没有权限认证令牌，不执行操作，并返回相应的错误码
                var result = new WapBoolean(false);
                result.Code = StateCode.CODE_ACCESS_TOKEN_INVALID;
                result.Message = "非法的AccessToken";
                return result;
            }
        }                     

        /// <summary>
        /// 根据用户userId获取当前用户授权的应用程序列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回应用程序列表</returns>
        [HttpGet]
        [Route("queryAuthorizedApps")]
        [ActionName("getAuthorizedApps")]
        private WapCollection<WapApp> GetAuthorizedApps(int userId)
        {
            var result = Service.GetAuthorizedApps(userId);
            return new WapCollection<WapApp>(result);
        }        

        /// <summary>
        /// 删除应用
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <returns>是否删除成功</returns>
        [HttpDelete]
        [Route("{appKey}")]
        [ActionName("deleteApp")]
        private WapBoolean DeleteApp([FromUri]string appKey)
        {
            var token = base.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                var result = Service.DeleteApp(appKey);
                return new WapBoolean(result);
            }
            else
            {
                //没有权限认证令牌，不执行操作，并返回相应的错误码
                var result = new WapBoolean(false);
                result.Code = StateCode.CODE_ACCESS_TOKEN_INVALID;
                result.Message = "非法的AccessToken";
                return result;
            }
        }     
    }
}

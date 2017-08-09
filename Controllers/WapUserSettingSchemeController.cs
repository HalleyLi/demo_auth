using SH3H.SDK.WebApi.Controllers;
using SH3H.SDK.WebApi.Core;
using SH3H.SDK.WebApi.Core.Models;
using SH3H.WAP.Contracts;
using SH3H.WAP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SH3H.WAP.Controllers
{
    /// <summary>
    /// 定义用户设置控制器
    /// </summary>
    [Resource("wapUserSettingSchemeRes")]
    [RoutePrefix(SH3H.WAP.Share.Consts.URL_PREFIX_WAP + "/common")]
    public class WapUserSettingSchemeController: BaseController<IWapUserSettingSchemeService>
    {
        /// <summary>
        /// 添加用户配置参数
        /// </summary>
        /// <returns>新增后的用户配置对象</returns>
        [HttpPost]
        [Route("user_setting_schemes")]
        [ActionName("createUserSettingScheme")]
        public WapResponse<WapUserSettingSchemeDto> Create(WapUserSettingSchemeDto userSettingSchemeDto)
        {
            var result = Service.Add(userSettingSchemeDto);
            return new WapResponse<WapUserSettingSchemeDto>(result);
        }

        /// <summary>
        /// 删除用户配置参数
        /// </summary>
        /// <param name="schemeId">参数编号</param>
        /// <returns>是否删除成功</returns>
        [HttpDelete]
        [Route("user_setting_schemes/{schemeId}")]
        [ActionName("deleteUserSettingScheme")]
        public WapBoolean Remove(int schemeId)
        {
            var result = Service.Remove(schemeId);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 根据参数编号修改用户配置参数
        /// </summary>
        /// <param name="schemeId">参数编号</param>
        /// <param name="userSettingSchemeDto">修改的用户配置参数对象</param>
        /// <returns>修改后的用户配置参数对象</returns>
        [HttpPut]
        [Route("user_setting_schemes/{schemeId}")]
        [ActionName("updateUserSettingScheme")]
        public WapResponse<WapUserSettingSchemeDto> Modify(int schemeId, WapUserSettingSchemeDto userSettingSchemeDto)
        {
            var result = Service.Modify(userSettingSchemeDto);
            return new WapResponse<WapUserSettingSchemeDto>(result);
        }

        /// <summary>
        /// 根据参数编号获取用户配置参数
        /// </summary>
        /// <param name="schemeId">参数编号</param>
        /// <returns>用户配置参数对象</returns>
        [HttpGet]
        [Route("user_setting_schemes/{schemeId}")]
        [ActionName("getUserSettingSchemeById")]
        public WapResponse<WapUserSettingSchemeDto> Get(int schemeId)
        {
            var result = Service.Get(schemeId);
            return new WapResponse<WapUserSettingSchemeDto>(result);
        }
    }
}

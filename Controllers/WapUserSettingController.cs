using SH3H.SDK.WebApi.Controllers;
using SH3H.SDK.WebApi.Core;
using SH3H.SDK.WebApi.Core.Models;
using SH3H.WAP.Contracts;
using SH3H.WAP.Model;
using SH3H.WAP.Model.Dto;
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
    [Resource("wapUserSettingRes")]
    [RoutePrefix(SH3H.WAP.Share.Consts.URL_PREFIX_WAP + "/common")]
    public class WapUserSettingController : BaseController<IWapUserSettingService>
    {
        /// <summary>
        /// 添加用户配置
        /// </summary>
        /// <returns>新增后的用户配置对象</returns>
        [HttpPost]
        [Route("user_settings")]
        [ActionName("createUserSetting")]
        public WapResponse<WapUserSettingDto> Create(WapUserSettingDto userSetting)
        {
            var result = Service.Add(userSetting);
            return new WapResponse<WapUserSettingDto>(result);
        }

        /// <summary>
        /// 添加用户配置组别
        /// </summary>
        /// <returns>新增后的用户配置组别对象</returns>
        [HttpPost]
        [Route("user_settings/group")]
        [ActionName("createUserSettingGroup")]
        public WapResponse<WapUserSettingDto> CreateGroup(AddUserSettingGroupDto addGroup) 
        {
            var result = Service.AddGroup(addGroup);
            return new WapResponse<WapUserSettingDto>(result);
        }

        /// <summary>
        /// 删除用户配置
        /// </summary>
        /// <param name="settingId">用户配置编号</param>
        /// <returns>是否删除成功</returns>
        [HttpDelete]
        [Route("user_settings/{settingId}")]
        [ActionName("removeUserSetting")]
        public WapBoolean Remove(int settingId)
        {
            var result = Service.Remove(settingId);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 根据用户配置编号修改用户配置
        /// </summary>
        /// <param name="settingId">用户配置编号</param>
        /// <param name="userSetting">用户配置对象</param>
        /// <returns>修改后的用户配置对象</returns>
        [HttpPut]
        [Route("user_settings/{settingId}")]
        [ActionName("updateUserSetting")]
        public WapResponse<WapUserSettingDto> Update(int settingId, WapUserSettingDto userSetting)
        {
            var result = Service.Modify(userSetting);
            return new WapResponse<WapUserSettingDto>(result);
        }

         /// <summary>
        /// 根据用户配置编号修改用户配置组别
        /// </summary>
        /// <param name="userSetting">用户配置对象</param>
        /// <returns>修改后的用户配置对象组别</returns>
        [HttpPut]
        [Route("user_settings/{settingId}/group")]
        [ActionName("updateUserSettingGroup")]
        public WapResponse<WapUserSettingDto> UpdateGroup(ModifyUserSettingGroupDto modifyGroup)
        {
            var result = Service.ModifyGroup(modifyGroup);
            return new WapResponse<WapUserSettingDto>(result);
        }

        /// <summary>
        /// 根据用户编号和配置编码修改用户配置
        /// </summary>
        /// <param name="mofifyUserSetting">修改用户配置参数</param>
        /// <returns>更新后的用户配置对象</returns>
        [HttpPut]
        [Route("user_settings/child")]
        [ActionName("updateUserSettingByUserIdAndCode")]
        public WapResponse<WapUserSettingDto> UpdateByUserIdAndCode(WapModifyUserSettingDto mofifyUserSetting)
        {
            var result = Service.ModifyByUserIdAndCode(mofifyUserSetting.UserId, mofifyUserSetting.Code, mofifyUserSetting.UserSetting);
            return new WapResponse<WapUserSettingDto>(result);
        }

        /// <summary>
        ///获取所有用户配置
        /// </summary>
        /// <returns>用户配置对象列表</returns>
        [HttpGet]
        [Route("user_settings")]
        [ActionName("getAllUserSettings")]
        public WapResponse<IEnumerable<WapUserSettingDto>> GetAll()
        {
            var result = Service.GetAll();
            return new WapResponse<IEnumerable<WapUserSettingDto>>(result);
        }

        /// <summary>
        /// 根据配置编号获取用户配置参数
        /// </summary>
        /// <param name="settingId">用户配置对象编号</param>
        /// <returns>用户配置对象列表</returns>
        [HttpGet]
        [Route("user_settings/{settingId}")]
        [ActionName("getUserSettingsBySettingId")]
        public WapResponse<IEnumerable<WapUserSettingDto>> GetByUserSettingId(int settingId)
        {
            var result = Service.GetByUserSettingId(settingId);
            return new WapResponse<IEnumerable<WapUserSettingDto>>(result);
        }

        /// <summary>
        /// 根据用户编号获取用户配置参数
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>用户配置对象列表</returns>
        [HttpGet]
        [Route("user_settings")]
        [ActionName("getUserSettingsByUserId")]
        public WapResponse<IEnumerable<WapUserSettingDto>> GetByUserId(int userId)
        {
            var result = Service.GetByUserId(userId);
            return new WapResponse<IEnumerable<WapUserSettingDto>>(result);
        }

        /// <summary>
        /// 根据用户编号和配置编码查询用户配置
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="code">配置编码</param>
        /// <returns>用户配置对象列表</returns>
        [HttpGet]
        [Route("user_settings")]
        [ActionName("getUserSettingsByUserIdAndCode")]
        public WapResponse<IEnumerable<WapUserSettingDto>> GetByUserIdAndCode(int userId, string code)
        {
            var result = Service.GetByUserIdAndCode(userId, code);
            return new WapResponse<IEnumerable<WapUserSettingDto>>(result);
        }

        /// <summary>
        /// 根据用户编号和应用标识查询用户配置
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>用户配置对象列表</returns>
        [HttpGet]
        [Route("user_settings")]
        [ActionName("getUserSettingsByUserIdAndAppIdentity")]
        public WapResponse<IEnumerable<WapUserSettingDto>> GetByUserIdAndAppIdentity(int userId, string appIdentity)
        {
            var result = Service.GetByUserIdAndAppIdentity(userId, appIdentity);
            return new WapResponse<IEnumerable<WapUserSettingDto>>(result);
        }

        /// <summary>
        /// 根据应用标识查询用户配置
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>用户配置对象列表</returns>
        [HttpGet]
        [Route("user_settings")]
        [ActionName("getUserSettingsByAppIdentity")]
        public WapResponse<IEnumerable<WapUserSettingDto>> GetByAppIdentity(string appIdentity)
        {
            var result = Service.GetByAppIdentity(appIdentity);
            return new WapResponse<IEnumerable<WapUserSettingDto>>(result);
        }
    }
}

using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Share;
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
    /// 定义配置对象控制器
    /// </summary>
    [Resource("wapConfigurationRes")]
    [RoutePrefix(SH3H.WAP.Share.Consts.URL_PREFIX_WAP+"/common")]
    public class WapConfigurationController :
        BaseController<IWapConfigurationService>
    {
        /// <summary>
        /// 新增配置
        /// </summary>
        /// <param name="configuration">系统配置对象</param>
        /// <returns>系统配置对象</returns>
        [HttpPost]
        [Route("configs")]
        [ActionName("createConfig")]
        public WapResponse<WapConfigurationDto> AddConfiguration(WapConfigurationDto configuration)
        {
            var result = Service.AddConfiguration(configuration);
            return new WapResponse<WapConfigurationDto>(result);
        }

        /// <summary>
        ///删除配置
        /// </summary>
        /// <param name="configId">编号</param>
        /// <returns>是否删除成功</returns>
        [HttpDelete]
        [Route("configs/{configId}")]
        [ActionName("deleteConfig")]
        public WapBoolean RemoveConfiguration(int configId)
        {
            var result = Service.RemoveConfiguration(configId);
            return new WapBoolean(result);
        }

        /// <summary>
        ///修改配置
        /// </summary>
        /// <param name="configId">编号</param>
        /// <param name="configuration">系统配置对象</param>
        /// <returns>返回时否修改成功</returns>
        [HttpPut]
        [Route("configs/{configId}")]
        [ActionName("updateConfig")]
        public WapBoolean ModifyConfiguration(int configId, [FromBody]WapConfigurationDto configuration)
        {
            var result = Service.ModifyConfiguration(configId,configuration);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 获取指定配置
        /// </summary>
        /// <param name="configId">编号</param>
        /// <returns>系统配置对象</returns>
        [HttpGet]
        [Route("configs/{configId}")]
        [ActionName("getConfigById")]
        private WapResponse<WapConfigurationDto> GetConfigurationById(int configId)
        {
            var result = Service.GetConfigurationById(configId);
            return new WapResponse<WapConfigurationDto>(result);
        }

        /// <summary>
        /// 获取指定应用指定分组下的配置
        /// </summary>
        /// <param name="app">app编码</param>
        /// <param name="group">配置分组</param>
        /// <returns>系统配置对象</returns>
        [HttpGet]
        [Route("configs")]
        [ActionName("getConfigsByAppAndGroup")]
        public WapCollection<WapConfigurationDto> GetConfigurationByAppAndGroup(string app, string group)
        {
            var result = Service.GetConfigurationByAppAndGroup(app, group);
            return new WapCollection<WapConfigurationDto>(result);
        }


        /// <summary>
        /// 获取指定应用下的配置
        /// </summary>
        /// <param name="appIdentity">app编码</param>
        /// <returns>系统配置对象</returns>
        [HttpGet]
        [Route("apps/{appIdentity}/configs")]
        [ActionName("getConfigsByApp")]
        public  WapCollection<WapConfigurationDto> GetConfigsByAppCode(string appIdentity)
        {
            var result = Service.GetConfigsByAppCode(appIdentity);
            return new WapCollection<WapConfigurationDto>(result);
        }


        /// <summary>
        /// 通过应用获取配置组
        /// </summary>
        /// <param name="appIdentity">应用编码</param>
        /// <returns>系统配置对象</returns>
        [HttpGet]
        [Route("configs/group")]
        [ActionName("getGroupsByApp")]
        public WapCollection<WapConfigurationDto> SelectGroupsByAppCode(string appIdentity)
        {
            var result = Service.SelectGroupsByApp(appIdentity);
            return new WapCollection<WapConfigurationDto>(result);
        }

        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns>系统配置对象列表</returns>
        [HttpGet]
        [Route("configs")]
        [ActionName("getAllConfigs")]
        public WapCollection<WapConfigurationDto> GetAllConfiguration()
        {
            var result = Service.GetAllConfiguration();
            return new WapCollection<WapConfigurationDto>(result);
        }

        /// <summary>
        /// 根据配置code更新配置组code
        /// </summary>
        /// <param name="configCode">配置标识</param>
        /// <param name="group">配置组标识</param>
        /// <returns>是否更新成功</returns>
        [HttpPut]
        [Route("configs/{configCode}/child/{group}")]
        [ActionName("updateConfigRelation")]
        public WapBoolean UpdateConfigParent(string configCode, string group)
        {
            var result = Service.UpdateConfigParent(configCode, group);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 修改配置状态
        /// </summary>
        /// <param name="id">配置ID</param>
        /// <param name="active">激活状态</param>
        /// <returns>是否修改成功</returns>
        [HttpPatch]
        [Route("configs/{id}/state")]
        [ActionName("updateConfigState")]
        public WapBoolean UpdateConfigState(int id, bool active)
        {
            var result = Service.UpdateConfigState(id, active);
            return new WapBoolean(result);
        }

    }
}

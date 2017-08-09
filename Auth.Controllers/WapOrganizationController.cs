using SH3H.SDK.Share;
using SH3H.SDK.WebApi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using SH3H.WAP.Auth.Contracts;
using SH3H.WAP.Auth.Model;
using SH3H.SDK.WebApi.Controllers;
using SH3H.SDK.WebApi.Core.Models;
using SH3H.WAP.Auth.Model.Dto;
namespace SH3H.WAP.Auth.Controllers
{
    /// <summary>
    /// 定义WAP权限认证组织结构服务控制器
    /// </summary>
    [Resource("wapOrganizationRes")]
    [RoutePrefix("wap/v2")]
    public class WapOrganizationController : BaseController<IWapOrganizationService>
    {
        /// <summary>
        /// 新增组织
        /// </summary>
        /// <param name="organization">组织对象</param>
        /// <returns>组织对象</returns>
        [HttpPost]
        [Route("organizations")]
        [ActionName("addOrganization")]
        public WapResponse<WapOrganizationDto> AddOrganization([FromBody]WapOrganizationDto organization)
        {
            var result = Service.AddOrganization(organization);
            return new WapResponse<WapOrganizationDto>(result);
        }
        /// <summary>
        /// 修改组织
        /// </summary>
        /// <param name="organizationKey">组织对象编号</param>
        /// <param name="organization">组织对象</param>
        /// <returns>是否修改成功</returns>
        [HttpPut]
        [Route("organizations/{organizationKey}")]
        [ActionName("updateOrganization")]
        public WapResponse<WapOrganizationDto> ModifyStation(string organizationKey, [FromBody]WapOrganizationDto organization)
        {
            organization.OrganizationKey = organizationKey;
            var result = Service.ModifyOrganizationById(organization);
            return new WapResponse<WapOrganizationDto>(result);
        }
        /// <summary>
        /// 修改组织状态
        /// </summary>
        /// <param name="organizationKey">组织对象编号</param>
        /// <param name="isActive">是否激活</param>
        /// <returns>是否成功</returns>
        [HttpPatch]
        [Route("organizations/{organizationKey}/state")]
        [ActionName("updateOrganizationState")]
        public WapBoolean UpdateOrganizationState(string organizationKey, [FromBody]WapStateDto isActive)
        {

            return new WapBoolean(Service.UpdateOrganizationState(organizationKey, isActive));
        }
        /// <summary>
        /// 起用（state=1）
        /// </summary>
        /// <param name="organizationKey">组织对象编号</param>
        /// <returns>是否成功</returns>
        [HttpPatch]
        [Route("{organizationKey}/active")]
        [ActionName("activeOrganization")]
        private WapBoolean ActiveOrganization(string organizationKey)
        {
            var result = Service.ActiveOrganization(organizationKey);
            return new WapBoolean(result);
        }
        /// <summary>
        /// 删除指定组织
        /// </summary>
        /// <param name="organizationKey">组织对象编号</param>
        /// <returns>是否删除成功</returns>
        [HttpDelete]
        [Route("organizations/{organizationKey}")]
        [ActionName("deleteOrganization")]
        public WapBoolean RemoveOrganization(string organizationKey)
        {
            var result = Service.RemoveOrganization(organizationKey);
            return new WapBoolean(result);
        }
        /// <summary>
        /// 获取所有组织
        /// </summary>
        /// <returns>组织对象列表</returns>
        [HttpGet]
        [Route("organizations")]
        [ActionName("getAllOrganizations")]
        public WapCollection<WapOrganizationDto> GetAllOrganizations()
        {
            var result = Service.GetAllOrganizations();
            return new WapCollection<WapOrganizationDto>(result);
        }
        /// <summary>
        /// 获取指定组织
        /// </summary>
        /// <param name="organizationKey">组织对象编号</param>
        /// <returns>组织对象</returns>
        [HttpGet]
        [Route("organizations/{organizationKey}")]
        [ActionName("getOrganizationByKey")]
        public WapResponse<WapOrganizationDto> GetOrganizationByOrganizationKey(string organizationKey)
        {
            var result = Service.GetOrganizationByOrganizationKey(organizationKey);
            return new WapResponse<WapOrganizationDto>(result);
        }

        /// <summary>
        /// 更新组织顺序
        /// </summary>
        /// <param name="sorts"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("organizations/sort")]
        [ActionName("sortOrganizations")]
        public WapBoolean ModifyOrgIndexByOrganizationKey([FromBody]Dictionary<string, int> sorts)
        {
            var result = Service.ModifyOrgIndexByOrganizationKey(sorts);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 禁用（state=-1）
        /// </summary>
        /// <param name="organizationKey">组织对象编号</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [Route("{organizationKey}/deactive")]
        [ActionName("deactiveOrganization")]
        private WapBoolean DeactiveOrganization(string organizationKey)
        {
            var result = Service.DeactiveOrganization(organizationKey);
            return new WapBoolean(result);
        }
        #region OrganizationUser
        /// <summary>
        /// 获取指定用户的组织
        /// </summary>
        /// <param name="userKey">用户编号</param>
        /// <returns>组织对象集合</returns>
        [HttpGet]
        [Route("users/{userKey}/organization")]
        [ActionName("getUserOrganization")]
        public WapResponse<WapOrganizationDto> GetOrganizationbyUser(string userKey)
        {
            var result = Service.GetOrganizationbyUser(userKey);
            return new WapResponse<WapOrganizationDto>(result.FirstOrDefault());
        }
        /// <summary>
        /// 获取指定用户的组织
        /// </summary>
        /// <param name="userKey">用户编号</param>
        /// <returns></returns>
        [HttpGet]
        [Route("users/{userKey}/organization")]
        [ActionName("getUserOrganization")]
        private WapResponse<Model.Dto.UserOrganizationRelation> GetOrganizationUserRelation(string userKey)
        {
            var result = Service.GetOrganizationUserRelation(userKey);
            return new WapResponse<Model.Dto.UserOrganizationRelation>(result);
        }
        /// <summary>
        /// 创建或更新用户与组织的关联
        /// </summary>
        /// <param name="userKey">用户key</param>
        /// <param name="relationObject">关联对象</param>
        /// <returns>是否操作成功</returns>
        [HttpPost]
        [Route("users/{userKey}/organization/relation")]
        [ActionName("createUserOrgRelation")]
        public WapBoolean CreateOrDeleteOrganizationUserRelation(string userKey, [FromBody]UpdateUserOrganizationRelationObject relationObject)
        {
            var result = Service.CreateOrDeleteOrganizationUserRelation(userKey, relationObject);
            return new WapBoolean(result);
        }


        /// <summary>
        /// 创建或更新用户与组织的关联
        /// </summary>
        /// <param name="relationModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("users/{userKey}/organization")]
        [ActionName("createUserOrgRelation")]
        private WapBoolean UpdateOrganizationUserRelation(Model.Dto.UpdateUserOrganizationRelationModel relationModel)
        {
            var result = Service.UpdateOrganizationUserRelation(relationModel);
            return new WapBoolean(result);
        }


        /// <summary>
        /// 获取所有角色和组织的关联
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("orgUser")]
        [ActionName("getAllOrgUserRel")]
        private WapCollection<Model.Dto.UserOrganizationRelation> GetAllOrganizationUserRelation()
        {
            return new WapCollection<Model.Dto.UserOrganizationRelation>(Service.GetAllOrganizationUserRelation());
        }

        /// <summary>
        ///  凭组织ID获取所有角色和组织的关联
        /// </summary>
        /// <param name="orgKey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("orgUser/byOrg")]
        [ActionName("getAllOrgUserRelByOrgKey")]
        private WapCollection<Model.Dto.UserOrganizationRelation> GetAllOrganizationUserRelationByOrganKey(string orgKey)
        {
            return new WapCollection<Model.Dto.UserOrganizationRelation>(Service.GetAllOrganizationUserRelationByOrganKey(orgKey));
        }

        /// <summary>
        /// 凭用户ID 获取所有角色和组织的关联
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("orgUser/byUser")]
        [ActionName("getAllOrgUserRelByUserKey")]
        private WapCollection<Model.Dto.UserOrganizationRelation> GetAllOrganizationUserRelationByUserKey([FromUri]string userKey)
        {
            return new WapCollection<Model.Dto.UserOrganizationRelation>(Service.GetAllOrganizationUserRelationByUserKey(userKey));
        }

        /// <summary>
        /// 凭组织ID获取所有的用户
        /// </summary>
        /// <param name="orgkey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("queryByOrg")]
        [ActionName("getUserByOrgKey")]
        private WapCollection<WapUser> GetUserByOrganKey(string orgkey)
        {
            return new WapCollection<WapUser>(Service.GetUserByOrganKey(orgkey));
        }

        /// <summary>
        /// 凭组织ID列表获取所有的用户
        /// </summary>
        /// <param name="strOrgKey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("queryByOrgs")]
        [ActionName("getUserByOrgKeys")]
        private WapCollection<WapUser> GetUserByOrganKeys(string strOrgKey)
        {
            if (string.IsNullOrEmpty(strOrgKey))
            {
                return new WapCollection<WapUser>(null);
            }

            string[] keys = strOrgKey.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            return new WapCollection<WapUser>(Service.GetUserByOrganKeys(keys));
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
namespace SH3H.WAP.Auth.Contracts
{
    /// <summary>
    /// 组织结构对象服务接口
    /// </summary>
    public interface IWapOrganizationService
    {
        /// <summary>
        /// 添加组织对象
        /// </summary>
        /// <param name="organization">组织对象</param>
        /// <returns>组织对象</returns>
        WapOrganizationDto AddOrganization(WapOrganizationDto organization);

        /// <summary>
        /// 删除组织对象
        /// </summary>
        /// <param name="organizationKey">组织对象编号</param>
        /// <returns>是否删除成功</returns>
        bool RemoveOrganization(string organizationKey);
        /// <summary>
        /// 更新组织结构状态
        /// </summary>
        /// <param name="organizationKey">组织对象编号</param>
        /// <param name="isActive">是否激活状态</param>
        /// <returns>是否激活成功</returns>
        bool UpdateOrganizationState(string organizationKey,WapStateDto isActive);
        /// <summary>
        /// 获取指定用户的组织
        /// </summary>
        /// <param name="userKey">用户编号</param>
        /// <returns>组织对象集合</returns>
        IEnumerable<WapOrganizationDto> GetOrganizationbyUser(string userKey);
        /// <summary>
        /// 禁用组织对象（state=0）
        /// </summary>
        /// <param name="organizationKey">组织对象编号</param>
        /// <returns>是否成功</returns>
        bool DeactiveOrganization(string organizationKey);
        /// <summary>
        /// 启用组织对象（state=1）
        /// </summary>
        /// <param name="organizationKey">组织对象编号</param>
        /// <returns>是否成功</returns>
        bool ActiveOrganization(string organizationKey);
        /// <summary>
        /// 通过组织对象编号修改站点
        /// </summary>
        /// <param name="organization">组织对象</param>
        /// <returns>返回时否修改成功</returns>
        WapOrganizationDto ModifyOrganizationById(WapOrganizationDto organization);

        /// <summary>
        /// 根据组织对象编号更新词语排序值
        /// </summary>
        /// <param name="sortIndexes">组织对象排序值数组</param>
        /// <returns></returns>
        bool ModifyOrgIndexByOrganizationKey(Dictionary<string, int> sortIndexes);

        /// <summary>
        /// 通过组织对象编号查询站点
        /// </summary>
        /// <param name="organizationKey">组织对象编号</param>
        /// <returns>组织对象对象</returns>
        WapOrganizationDto GetOrganizationByOrganizationKey(string organizationKey);

        /// <summary>
        /// 获取所有组织对象
        /// </summary>
        /// <returns>所有组织对象列表</returns>
        IEnumerable<WapOrganizationDto> GetAllOrganizations();

        #region OrganizationUser

        /// <summary>
        /// 更新用户和组织的关联
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateOrganizationUserRelation(UpdateUserOrganizationRelationModel model);
        /// <summary>
        /// 创建或更新用户与组织的关联
        /// </summary>
        /// <param name="userKey">用户key</param>
        /// <param name="relationObject">关联对象</param>
        /// <returns>是否操作成功</returns>
        bool CreateOrDeleteOrganizationUserRelation(string userKey, UpdateUserOrganizationRelationObject relationObject);
        /// <summary>
        /// 获取用户和组织的关联
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        UserOrganizationRelation GetOrganizationUserRelation(string relationKey);

        /// <summary>
        /// 获取所有用户和组织的关联
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<UserOrganizationRelation> GetAllOrganizationUserRelation();

        /// <summary>
        /// 获取所有用户和组织的关联
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<UserOrganizationRelation> GetAllOrganizationUserRelationByOrganKey(string organKey);

        /// <summary>
        /// 获取所有用户和组织的关联
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<UserOrganizationRelation> GetAllOrganizationUserRelationByUserKey(string userKey);

        /// <summary>
        /// 凭组织ID获取用户列表
        /// </summary>
        /// <param name="organkey"></param>
        /// <returns></returns>
        IEnumerable<WapUser> GetUserByOrganKey(string organkey);

        /// <summary>
        /// 凭组织ID获取用户列表
        /// </summary>
        /// <param name="organkey"></param>
        /// <returns></returns>
        IEnumerable<WapUser> GetUserByOrganKeys(IEnumerable<string> organkeys);

        #endregion

    }
}

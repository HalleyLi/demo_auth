using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SH3H.SDK.DataAccess.Repo;
using SH3H.WAP.Auth.DataAccess;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
namespace SH3H.WAP.Auth.DataAccess.Repo.Contact
{
    public interface IWapOrganizationRepository
    {
        /// <summary>
        /// 添加站点
        /// </summary>
        /// <param name="organization">站点对象</param>
        /// <returns>站点对象</returns>
        WapOrganization AddOrganization(WapOrganization organization);
        /// <summary>
        /// 删除站点
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <returns>是否删除成功</returns>
        bool RemoveOrganization(string organizationKey);

        /// <summary>
        /// 通过站点编号修改站点
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <param name="organization">站点对象</param>
        /// <returns>返回时否修改成功</returns>
        WapOrganization ModifyOrganizationById(WapOrganization organization);
        /// <summary>
        /// 禁用站点（state=-1）
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <returns>是否成功</returns>
        bool DeactiveOrganization(string organizationKey);
        /// <summary>
        /// 启用站点（state=1）
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <returns>是否成功</returns>
        bool ActiveOrganization(string organizationKey);
        /// <summary>
        /// 通过站点编号查询站点
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <returns>站点对象</returns>
        WapOrganization GetOrganizationByOrganizationKey(string organizationKey);
        /// <summary>
        /// 根据站点编号批量更新词语排序值
        /// </summary>
        /// <param name="sortIndexes">站点排序值数组</param>
        /// <returns>是否所有更新都成功</returns>
        bool ModifyOrgIndexByOrganizationKey(Dictionary<string, int> sortIndexes);
        /// <summary>
        /// 根据站点编号更新站点排序值
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <param name="sortIndex">站点排序值</param>
        ///<param name="trans"></param>
        /// <returns>更新是否成功</returns>
        bool ModifyOrgIndexByOrganizationKey(string organizationKey, int sortIndex, System.Data.Common.DbTransaction trans = null);
        /// <summary>
        /// 获取所有站点
        /// </summary>
        /// <returns>所有站点列表</returns>
        IEnumerable<WapOrganization> GetAllOrganizations();

        #region OrganizationUser
        /// <summary>
        /// 获取指定用户的组织
        /// </summary>
        /// <param name="userKey">用户编号</param>
        /// <returns>组织对象集合</returns>
        IEnumerable<WapOrganization> GetOrganizationbyUser(string userKey);
        /// <summary>
        /// 更新用户和组织的关联
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateOrganizationUserRelation(UpdateUserOrganizationRelationModel model);

        /// <summary>
        /// 获取用户和组织的关联
        /// </summary>
        /// <param name="relationKey"></param>
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SH3H.SDK.DataAccess.Repo;
using SH3H.WAP.Auth.DataAccess;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
namespace SH3H.WAP.Auth.DataAccess.Repo
{
    /// <summary>
    /// 组织结构数据仓库
    /// </summary>
    public class WapOrganizationRepository : Repository<IWapOrganizationStorage>, IWapOrganizationRepository
    {
        /// <summary>
        /// 添加站点
        /// </summary>
        /// <param name="organization">站点对象</param>
        /// <returns>站点对象</returns>
        public WapOrganization AddOrganization(WapOrganization organization)
        {
            return Storage.Insert(organization);
        }

        /// <summary>
        /// 删除站点
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <returns>是否删除成功</returns>
        public bool RemoveOrganization(string organizationKey)
        {
            return Storage.Delete(organizationKey);
        }

        /// <summary>
        /// 通过站点编号修改站点
        /// </summary>
        /// <param name="organization">站点对象</param>
        /// <returns>返回时否修改成功</returns>
        public WapOrganization ModifyOrganizationById(WapOrganization organization)
        {
            return Storage.UpdateOrganization(organization.OrganizationKey, organization);
        }
        /// <summary>
        /// 禁用站点（state=-1）
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <returns>是否成功</returns>
        public bool DeactiveOrganization(string organizationKey)
        {
            return Storage.DeactiveOrganization(organizationKey);
        }
        /// <summary>
        /// 启用站点（state=1）
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <returns>是否成功</returns>
        public bool ActiveOrganization(string organizationKey)
        {
            return Storage.ActiveOrganization(organizationKey);
        }
        /// <summary>
        /// 通过站点编号查询站点
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <returns>站点对象</returns>
        public WapOrganization GetOrganizationByOrganizationKey(string organizationKey)
        {
            return Storage.Select(organizationKey);
        }
        /// <summary>
        /// 根据站点编号批量更新词语排序值
        /// </summary>
        /// <param name="sortIndexes">站点排序值数组</param>
        /// <returns>是否所有更新都成功</returns>
        public bool ModifyOrgIndexByOrganizationKey(Dictionary<string, int> sortIndexes)
        {
            return Storage.ModifyOrgIndexByOrganizationKey(sortIndexes);
        }
        /// <summary>
        /// 根据站点编号更新站点排序值
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <param name="sortIndex">站点排序值</param>
        ///<param name="trans"></param>
        /// <returns>更新是否成功</returns>
        public bool ModifyOrgIndexByOrganizationKey(string organizationKey, int sortIndex, System.Data.Common.DbTransaction trans = null)
        {
            return Storage.ModifyOrgIndexByOrganizationKey(organizationKey, sortIndex);
        }
        /// <summary>
        /// 获取所有站点
        /// </summary>
        /// <returns>所有站点列表</returns>
        public IEnumerable<WapOrganization> GetAllOrganizations()
        {
            return Storage.SelectAll();
        }

        #region OrganizationUser
        /// <summary>
        /// 获取指定用户的组织
        /// </summary>
        /// <param name="userKey">用户编号</param>
        /// <returns>组织对象集合</returns>
        public IEnumerable<WapOrganization> GetOrganizationbyUser(string userKey)
        {
            return Storage.GetOrganizationbyUser(userKey);
        }
        /// <summary>
        /// 更新用户和组织的关联
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateOrganizationUserRelation(UpdateUserOrganizationRelationModel model)
        {
            return Storage.UpdateOrganizationUserRelation(model);
        }

        /// <summary>
        /// 获取用户和组织的关联
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public UserOrganizationRelation GetOrganizationUserRelation(string relationKey)
        {
            return Storage.GetOrganizationUserRelation(relationKey);
        }

        /// <summary>
        /// 获取所有用户和组织的关联
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<UserOrganizationRelation> GetAllOrganizationUserRelation()
        {
            return Storage.GetAllOrganizationUserRelation();
        }

        /// <summary>
        /// 获取所有用户和组织的关联
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<UserOrganizationRelation> GetAllOrganizationUserRelationByOrganKey(string organKey)
        {
            return Storage.GetAllOrganizationUserRelationByOrganKey(organKey);
        }

        /// <summary>
        /// 获取所有用户和组织的关联
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<UserOrganizationRelation> GetAllOrganizationUserRelationByUserKey(string userKey)
        {
            return Storage.GetAllOrganizationUserRelationByUserKey(userKey);
        }

        /// <summary>
        /// 凭组织ID获取用户列表
        /// </summary>
        /// <param name="organkey"></param>
        /// <returns></returns>
        public IEnumerable<WapUser> GetUserByOrganKey(string organkey)
        {
            return Storage.GetUserByOrganKey(organkey);
        }

        /// <summary>
        /// 凭组织ID获取用户列表
        /// </summary>
        /// <param name="organkey"></param>
        /// <returns></returns>
        public IEnumerable<WapUser> GetUserByOrganKeys(IEnumerable<string> organkeys)
        {
            return Storage.GetUserByOrganKeys(organkeys);
        }


        #endregion
    }
}


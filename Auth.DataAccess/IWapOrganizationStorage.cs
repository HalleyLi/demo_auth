using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SH3H.SDK.DataAccess.Db.Core;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
namespace SH3H.WAP.Auth.DataAccess
{
    /// <summary>
    /// 定义组织结构操作接口
    /// </summary>
    public interface IWapOrganizationStorage : IDbStorage<WapOrganization,string>
    {
        /// <summary>
        ///  起用（state=1）
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <param name="trans"></param>
        /// <returns>是否成功</returns>
        bool ActiveOrganization(string organizationKey, System.Data.Common.DbTransaction trans = null);
        /// <summary>
        ///  禁用（state=-1）
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <param name="trans"></param>
        /// <returns>是否成功</returns>
        bool DeactiveOrganization(string organizationKey, System.Data.Common.DbTransaction trans = null);
        /// <summary>
        /// 根据站点编号批量更新词语排序值
        /// </summary>
        /// <param name="sortIndexes">站点排序值数组</param>
        /// <returns></returns>
        bool ModifyOrgIndexByOrganizationKey(Dictionary<string, int> sortIndexes);
        /// <summary>
        /// 通过组织id更新组织对象
        /// </summary>
        /// <param name="organizationKey">对象编号</param>
        /// <param name="organization">对象实体</param>
        /// <param name="trans"></param>
        /// <returns>修改后的对象</returns>
        WapOrganization UpdateOrganization(string organizationKey, WapOrganization organization);
        /// <summary>
        /// 根据站点编号更新站点排序值
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <param name="sortIndex">站点排序值</param>
        ///<param name="trans"></param>
        /// <returns></returns>
        bool ModifyOrgIndexByOrganizationKey(string organizationKey, int sortIndex, System.Data.Common.DbTransaction trans = null);

        /// <summary>
        /// 获取所有组织对象
        /// </summary>
        /// <returns>所有站点列表</returns>
        IEnumerable<WapOrganization> SelectAll();

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

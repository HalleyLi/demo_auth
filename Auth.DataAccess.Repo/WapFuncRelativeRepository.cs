using SH3H.SDK.DataAccess.Repo;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using SH3H.WAP.Auth.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess.Repo
{
    public class WapFuncRelativeRepository :
         Repository<IWapFuncRelativeStorage>
        , IWapFuncRelativeRepository
    {

        /// <summary>
        ///获取功能点信息
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="appIdentity"></param>
        /// <returns></returns>
        public IEnumerable<WapFuncRelativeDto> GetRoleFunctionByRoleKeyAndAppIdentity(string roleKey, string appIdentity)
        {
            return Storage.GetRoleFunctionByRoleKeyAndAppIdentity(roleKey, appIdentity);
        }

        /// <summary>
        ///获取功能点信息
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public IEnumerable<WapFuncRelativeDto> GetRoleFunctionByRoleKeyAndAppKey(string roleKey, string appKey)
        {
            return Storage.GetRoleFunctionByRoleKeyAndAppKey(roleKey, appKey);
        }



        /// <summary>
        /// 根据角色获取功能点组信息
        /// </summary>
        /// <param name="roleKey">角色标识</param>
        /// <returns>功能点组列表</returns>
        public IEnumerable<WapFuncRelativeDto> GetRoleFunction(string roleKey)
        {

            return Storage.GetRoleFunction(roleKey);
        }

        /// <summary>
        /// 更新角色功能点关联关系
        /// </summary>
        /// <param name="relationModel">需要关联和需要解绑的功能点角色关系</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateRoleFunctionRelation(IEnumerable<WapRoleFunctionDto> add, IEnumerable<WapRoleFunctionDto> del)
        {
            return Storage.UpdateRoleFunctionRelation(add, del);
        }
    }
}

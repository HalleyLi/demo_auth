using SH3H.SDK.DataAccess.Repo;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess.Repo
{
    public class WapRoleFunctionRepository :
           Repository<IWapRoleFunctionStorage>
          , IWapRoleFunctionRepository
    {

        /// <summary>
        ///获取功能点信息
        /// </summary>
        /// <param name="relationKey"></param>
        /// <returns></returns>
        public WapRoleFunction GetRoleFunctionByKey(string relationKey)
        {
            return Storage.GetRoleFunctionByKey(relationKey);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public IEnumerable<WapRoleFunction> GetAllRoleFunctionByAppKey(string appKey)
        {
            return Storage.GetAllRoleFunctionByAppKey(appKey);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public IEnumerable<WapRoleFunction> GetAllRoleFunctionByAppIdentity(string appIdentity)
        {
            return Storage.GetAllRoleFunctionByAppIdentity(appIdentity);
        }



        public bool UpdateRoleFunctionRelation(IEnumerable<WapRoleFunctionDto> add, IEnumerable<WapRoleFunctionDto> del)
        {
            return Storage.UpdateRoleFunctionRelation(add,del);
        }
    }
}

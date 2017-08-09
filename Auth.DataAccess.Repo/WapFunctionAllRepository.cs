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
    public class WapFunctionAllRepository :
          Repository<IWapFunctionAllStorage>
         , IWapFunctionAllRepository
    {

        /// <summary>
        /// 获取所有功能点组
        /// </summary>
        /// <returns>功能点组列表</returns>
        public IEnumerable<WapFunctionAllDto> GetFunctionAllsByAppIdentity(string appidentity)
        {
            return Storage.GetFunctionAllsByAppIdentity(appidentity);
        }

        /// <summary>
        /// 凭应用主键获取功能点
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public IEnumerable<WapFunctionAllDto> GetFunctionAllsByAppKey(string appKey)
        {
            return Storage.GetFunctionAllsByAppKey(appKey);
        }


        /// <summary>
        /// 获取所有功能点组
        /// </summary>
        /// <returns>功能点组列表</returns>
        public IEnumerable<WapFunctionAllDto> GetFunctionAlls()
        {
            return Storage.GetFunctionAlls();
        }


        /// <summary>
        /// 凭角色获取功能点集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WapFunctionAllDto> GetFunctionAllsByRoles(IEnumerable<string> rolekeys)
        {
            return Storage.GetFunctionAllsByRoles(rolekeys);
        }
    }
}

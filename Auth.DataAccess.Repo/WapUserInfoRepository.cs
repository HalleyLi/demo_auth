using SH3H.SDK.DataAccess.Repo;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using SH3H.WAP.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess.Repo
{
    /// <summary>
    /// 定义用户信息仓库
    /// </summary>
    public class WapUserInfoRepository :
        Repository<IWapUserInfoStorage>, IWapUserInfoRepository
    {
         /// <summary>
        /// 根据用户获取用户组织信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>用户组织信息</returns>
        public WapUserInfo GetUserInfoByUserId(int userId)
        {
            return Storage.SelectUserInfoByUserId(userId);
        }
    }
}

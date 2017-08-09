using SH3H.WAP.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Contracts
{
    /// <summary>
    /// 定义用户信息服务接口
    /// </summary>
    public interface IWapUserInfoService
    {
        /// <summary>
        /// 根据用户获取用户组织信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>用户组织信息</returns>
        WapUserInfoDto GetUserInfoByUserId(int userId);
    }
}

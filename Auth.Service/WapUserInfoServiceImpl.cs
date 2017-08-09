using SH3H.SDK.Service.Core;
using SH3H.WAP.Auth.Contracts;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using SH3H.WAP.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Service
{
    /// <summary>
    /// 用户信息服务实现
    /// </summary>
    public class WapUserInfoServiceImpl : BaseService, IWapUserInfoService
    {
        private IWapUserInfoRepository _repo;

         public WapUserInfoServiceImpl(IWapUserInfoRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 根据用户获取用户组织信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>用户组织信息</returns>
        public WapUserInfoDto GetUserInfoByUserId(int userId)
        {
            var result = _repo.GetUserInfoByUserId(userId);
            if (result != null)
                return WapUserInfoDto.FromModel(result);
            return null;
        }
    }
}

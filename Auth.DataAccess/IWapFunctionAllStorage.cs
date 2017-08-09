using SH3H.WAP.Auth.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess
{
    /// <summary>
    /// 定义功能点数据库存储对象
    /// </summary>
    public interface IWapFunctionAllStorage
    {

        /// <summary>
        /// 获取所有功能点组
        /// </summary>
        /// <returns>功能点组列表</returns>
        IEnumerable<WapFunctionAllDto> GetFunctionAlls();

        /// <summary>
        /// 凭应用标识获取功能点集合
        /// </summary>
        /// <param name="appidentity">应用标识</param>
        /// <returns>功能点集合</returns>
        IEnumerable<WapFunctionAllDto> GetFunctionAllsByAppIdentity(string appidentity);

        /// <summary>
        /// 凭应用主键获取功能点集合
        /// </summary>
        /// <param name="appKey">应用主键</param>
        /// <returns>功能点集合</returns>
        IEnumerable<WapFunctionAllDto> GetFunctionAllsByAppKey(string appKey);

        /// <summary>
        /// 凭角色获取功能点集合
        /// </summary>
        /// <param name="rolekeys">角色主键集合</param> 
        /// <returns>功能点集合</returns>
        IEnumerable<WapFunctionAllDto> GetFunctionAllsByRoles(IEnumerable<string> rolekeys);
    }
}

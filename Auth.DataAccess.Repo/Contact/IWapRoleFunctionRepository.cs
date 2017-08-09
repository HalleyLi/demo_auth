using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess.Repo.Contact
{
    /// <summary>
    /// 定义功能点与角色关联数据库存储对象
    /// </summary>
    public interface IWapRoleFunctionRepository
    {

        /// <summary>
        ///获取功能点和角色的关联
        /// </summary>
        /// <param name="relationKey">主键</param>
        /// <returns>功能点和角色的关联</returns>
        WapRoleFunction GetRoleFunctionByKey(string relationKey);

        /// <summary>
        /// 凭应用标识获取功能点和角色的关联
        /// </summary>
        /// <param name="appKey">应用标识</param>
        /// <returns>功能点和角色的关联列表</returns>
        IEnumerable<WapRoleFunction> GetAllRoleFunctionByAppIdentity(string appIdentity);

        /// <summary>
        /// 凭应用主键获取功能点和角色的关联
        /// </summary>
        /// <param name="appKey">应用主键</param>
        /// <returns>功能点和角色的关联列表</returns>
        IEnumerable<WapRoleFunction> GetAllRoleFunctionByAppKey(string appKey);

        /// <summary>
        /// 更新角色功能点关联关系
        /// </summary>
        /// <param name="relationModel">需要关联和需要解绑的功能点角色关系</param>
        /// <returns>是否更新成功</returns>
        bool UpdateRoleFunctionRelation(IEnumerable<WapRoleFunctionDto> add, IEnumerable<WapRoleFunctionDto> del);
    }
}

using SH3H.WAP.Auth.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess
{
    /// <summary>
    /// 定义功能点及功能组关联数据库存储对象
    /// </summary>
    public interface IWapFuncGroupRelativeStorage
    {

        /// <summary>
        /// 更新角色功能点关联关系
        /// </summary>
        /// <param name="relationModel">需要关联和需要解绑的功能点角色关系</param>
        /// <returns>是否更新成功</returns>
        bool UpdateFunctionGroupRelation(IEnumerable<WapFuncGroupRelativeDto> add, IEnumerable<WapFuncGroupRelativeDto> del);





        /// <summary>
        /// 根据角色获取功能点组信息
        /// </summary>
        /// <param name="roleKey">角色标识</param>
        /// <returns>功能点组列表</returns>
        IEnumerable<WapFuncGroupRelativeDto> GetFunctionGroup(string funcGroupKey);

    }
}

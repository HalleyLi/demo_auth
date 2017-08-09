using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Contracts
{
        /// <summary>
    /// 口径
    /// </summary>
    public interface IWapCaliberService
    {

        /// <summary>
        /// 添加口径
        /// </summary>
        /// <param name="caliber">口径对象</param>
        /// <returns>口径对象</returns>
        WapCaliberDto AddCaliber(WapCaliberDto caliber);

        /// <summary>
        /// 删除口径
        /// </summary>
        /// <param name="id">口径编号</param>
        /// <returns>是否删除成功</returns>
        bool RemoveCaliber(int id);

        /// <summary>
        /// 通过口径编号修改口径
        /// </summary>
        /// <param name="id">口径编号</param>
        /// <param name="caliber">口径对象</param>
        /// <returns>返回时否修改成功</returns>
        WapCaliberDto ModifyCaliberById(int id, WapCaliberDto caliber);

        /// <summary>
        /// 通过口径编号查询口径
        /// </summary>
        /// <param name="id">口径编号</param>
        /// <returns>口径对象</returns>
        WapCaliberDto GetCaliberById(int id);

        /// <summary>
        /// 获取所有口径
        /// </summary>
        /// <returns>所有口径列表</returns>
        IEnumerable<WapCaliberDto> GetAllCalibers();

    }
}

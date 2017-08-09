using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Contracts
{
    /// <summary>
    /// 型号
    /// </summary>
    public interface IWapModelService
    {

        /// <summary>
        /// 添加水表型号
        /// </summary>
        /// <param name="model">水表型号对象</param>
        /// <returns>水表型号对象</returns>
        WapModelDto AddModel(WapModelDto model);

        /// <summary>
        /// 删除水表型号
        /// </summary>
        /// <param name="id">水表型号编号</param>
        /// <returns>是否删除成功</returns>
        bool RemoveModel(int id);

        /// <summary>
        /// 通过水表型号编号修改水表型号
        /// </summary>
        /// <param name="id">水表型号编号</param>
        /// <param name="model">水表型号对象</param>
        /// <returns>返回时否修改成功</returns>
        bool ModifyModelById(int id, WapModelDto model);

        /// <summary>
        /// 通过水表型号编号查询水表型号
        /// </summary>
        /// <param name="id">水表型号编号</param>
        /// <returns>水表型号对象</returns>
        WapModelDto GetModelById(int id);

        /// <summary>
        /// 获取所有水表型号
        /// </summary>
        /// <returns>所有水表型号列表</returns>
        IEnumerable<WapModelDto> GetAllModels();

    }
}

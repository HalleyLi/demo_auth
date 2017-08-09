using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Contracts
{
    /// <summary>
    /// 供应商
    /// </summary>
    public interface IWapProducerService
    {

        /// <summary>
        /// 添加水表厂商
        /// </summary>
        /// <param name="producer">水表厂商对象</param>
        /// <returns>水表厂商对象</returns>
        WapProducerDto AddProducer(WapProducerDto producer);

        /// <summary>
        /// 删除水表厂商
        /// </summary>
        /// <param name="id">水表厂商编号</param>
        /// <returns>是否删除成功</returns>
        bool RemoveProducer(int id);

        /// <summary>
        /// 通过水表厂商编号修改水表厂商
        /// </summary>
        /// <param name="id">水表厂商编号</param>
        /// <param name="producer">水表厂商对象</param>
        /// <returns>返回时否修改成功</returns>
        bool ModifyProducerById(int id, WapProducerDto producer);

        /// <summary>
        /// 通过水表厂商编号查询水表厂商
        /// </summary>
        /// <param name="id">水表厂商编号</param>
        /// <returns>水表厂商对象</returns>
        WapProducerDto GetProducerById(int id);

        /// <summary>
        /// 获取所有水表厂商
        /// </summary>
        /// <returns>所有水表厂商列表</returns>
        IEnumerable<WapProducerDto> GetAllProducers();

    }
}

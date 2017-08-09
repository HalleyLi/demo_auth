using SH3H.WAP.Model;
using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Contracts
{
    /// <summary>
    /// 量程Service接口层
    /// </summary>
    public interface IWapMeterRangeService
    {

        /// <summary>
        /// 新增量程
        /// </summary>
        /// <param name="range">量程对象</param>
        /// <returns>量程对象</returns>
        WapMeterRangeDto AddRange(WapMeterRangeDto range);

        /// <summary>
        /// 修改量程
        /// </summary>
        /// <param name="id">量程id</param>
        /// <param name="range">量程对象</param>
        /// <returns>量程对象</returns>
        WapMeterRangeDto UpdateRange(int id, WapMeterRangeDto range);

        /// <summary>
        /// 获取所有的量程列表
        /// </summary>
        /// <returns>所有的量程列表</returns>
        IEnumerable<WapMeterRangeDto> SelectAllRanges();

        /// <summary>
        /// 通过id获取指定量程
        /// </summary>
        /// <param name="id">量程id</param>
        /// <returns>量程对象</returns>
        WapMeterRangeDto GetRangeById(int id);

        /// <summary>
        /// 删除量程
        /// </summary>
        /// <param name="id">量程id</param>
        /// <returns>是否成功</returns>
        bool DeleteRange(int id);


    }
}

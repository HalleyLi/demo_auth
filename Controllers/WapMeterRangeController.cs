using SH3H.SDK.WebApi.Controllers;
using SH3H.SDK.WebApi.Core;
using SH3H.SDK.WebApi.Core.Models;
using SH3H.WAP.Contracts;
using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SH3H.WAP.Controllers
{
    /// <summary>
    /// 定义量程控制器
    /// </summary>
    [Resource("wapMeterRangeRes")]
    [RoutePrefix(SH3H.WAP.Share.Consts.URL_PREFIX_WAP + "/common")]
    public class WapMeterRangeController :
         BaseController<IWapMeterRangeService>
    {

        /// <summary>
        /// 新增量程
        /// </summary>
        /// <param name="range">量程对象</param>
        /// <returns>量程对象</returns>
        [HttpPost]
        [Route("meter_ranges")]
        [ActionName("createMeterRange")]
        public WapResponse<WapMeterRangeDto> AddRange(WapMeterRangeDto range)
        {
            var result = Service.AddRange(range);
            return new WapResponse<WapMeterRangeDto>(result);
        }

        /// <summary>
        /// 修改量程
        /// </summary>
        /// <param name="id">量程id</param>
        /// <param name="range">量程对象</param>
        /// <returns>量程对象</returns>
        [HttpPut]
        [Route("meter_ranges/{id}")]
        [ActionName("updateMeterRange")]
        public WapResponse<WapMeterRangeDto> UpdateRange([FromUri]int id, [FromBody]WapMeterRangeDto range)
        {
            var result = Service.UpdateRange(id, range);
            return new WapResponse<WapMeterRangeDto>(result);
        }

        /// <summary>
        /// 获取所有的量程列表
        /// </summary>
        /// <returns>所有的量程列表</returns>
        [HttpGet]
        [Route("meter_ranges")]
        [ActionName("getAllMeterRanges")]
        public WapResponse<IEnumerable<WapMeterRangeDto>> SelectAllRanges()
        {
            var result = Service.SelectAllRanges();
            return new WapResponse<IEnumerable<WapMeterRangeDto>>(result);
        }

        /// <summary>
        /// 通过id获取指定量程
        /// </summary>
        /// <param name="id">量程id</param>
        /// <returns>量程对象</returns>
        [HttpGet]
        [Route("meter_ranges/{id}")]
        [ActionName("getMeterRangeById")]
        public WapResponse<WapMeterRangeDto> GetRangeById(int id)
        {
            var result = Service.GetRangeById(id);
            return new WapResponse<WapMeterRangeDto>(result);
        }

        /// <summary>
        /// 删除量程
        /// </summary>
        /// <param name="id">量程id</param>
        /// <returns>是否成功</returns>
        [HttpDelete]
        [Route("meter_ranges/{id}")]
        [ActionName("deleteMeterRange")]
        public WapBoolean DeleteRange([FromUri]int id)
        {
            var result = Service.DeleteRange(id);
            return new WapBoolean(result);
        }

    }
}

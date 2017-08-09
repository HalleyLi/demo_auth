using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Share;
using SH3H.SDK.WebApi.Controllers;
using SH3H.SDK.WebApi.Core;
using SH3H.SDK.WebApi.Core.Models;
using SH3H.WAP.Contracts;
using SH3H.WAP.Model;
using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SH3H.WAP.Controllers
{
    /// <summary>
    /// 定义水表厂商对象控制器
    /// </summary>
    [Resource("wapMeterProducerRes")]
    [RoutePrefix(SH3H.WAP.Share.Consts.URL_PREFIX_WAP + "/common")]
    public class WapProducerController :
        BaseController<IWapProducerService>
    {
        /// <summary>
        /// 添加水表厂商
        /// </summary>
        /// <param name="producer">水表厂商对象</param>
        /// <returns>水表厂商对象</returns>
        [HttpPost]
        [Route("meter_producers")]
        [ActionName("createMeterProducer")]
        public WapResponse<WapProducerDto> AddProducer(WapProducerDto producer)
        {
            var result = Service.AddProducer(producer);
            return new WapResponse<WapProducerDto>(result);
        }

        /// <summary>
        /// 删除厂商#
        /// </summary>
        /// <param name="producerId">水表厂商编号</param>
        /// <returns>是否删除成功</returns>
        [HttpDelete]
        [Route("meter_producers/{producerId}")]
        [ActionName("deleteMeterProducer")]
        public WapBoolean RemoveProducer(int producerId)
        {
            var result = Service.RemoveProducer(producerId);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 修改厂商
        /// </summary>
        /// <param name="producerId">水表厂商编号</param>
        /// <param name="producer">水表厂商对象</param>
        /// <returns>是否修改成功</returns>
        [HttpPut]
        [Route("meter_producers/{producerId}")]
        [ActionName("updateProducer")]
        public WapBoolean ModifyProducer(int producerId, [FromBody]WapProducerDto producer)
        {
            var result = Service.ModifyProducerById(producerId, producer);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 获取指定厂商
        /// </summary>
        /// <param name="producerId">水表厂商编号</param>
        /// <returns>水表厂商对象</returns>
        [HttpGet]
        [Route("meter_producers/{producerId}")]
        [ActionName("getProducer")]
        public WapResponse<WapProducerDto> GetProducer(int producerId)
        {
            var result = Service.GetProducerById(producerId);
            return new WapResponse<WapProducerDto>(result);
        }

        /// <summary>
        /// 获取所有厂商
        /// </summary>
        /// <returns>水表厂商对象列表</returns>
        [HttpGet]
        [Route("meter_producers")]
        [ActionName("getAllMeterProducers")]
        public WapCollection<WapProducerDto> GetAllProducers()
        {
            var result = Service.GetAllProducers();
            return new WapCollection<WapProducerDto>(result);
        }


    }
}

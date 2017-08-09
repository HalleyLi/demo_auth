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
    /// 定义水表型号对象控制器
    /// </summary>
    [Resource("wapMeterModelRes")]
    [RoutePrefix(SH3H.WAP.Share.Consts.URL_PREFIX_WAP+"/common")]
    public class WapModelController :
        BaseController<IWapModelService>
    {
        /// <summary>
        /// 新增型号
        /// </summary>
        /// <param name="model">水表型号对象</param>
        /// <returns>水表型号对象</returns>
        [HttpPost]
        [Route("meter_models")]
        [ActionName("createMeterModel")]
        public WapResponse<WapModelDto> AddModel(WapModelDto model)
        {
            var result = Service.AddModel(model);
            return new WapResponse<WapModelDto>(result);
        }

        /// <summary>
        /// 删除型号
        /// </summary>
        /// <param name="modelId">水表型号编号</param>
        /// <returns>是否删除成功</returns>
        [HttpDelete]
        [Route("meter_models/{modelId}")]
        [ActionName("deleteMeterModel")]
        public WapBoolean RemoveModel(int modelId)
        {
            var result = Service.RemoveModel(modelId);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 修改型号#
        /// </summary>
        /// <param name="modelId">水表型号编号</param>
        /// <param name="model">水表型号对象</param>
        /// <returns>是否修改成功</returns>
        [HttpPut]
        [Route("meter_models/{modelId}")]
        [ActionName("updateMeterModel")]
        public WapBoolean ModifyModel(int modelId, [FromBody]WapModelDto model)
        {
            var result = Service.ModifyModelById(modelId, model);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 获取指定型号
        /// </summary>
        /// <param name="modelId">水表型号编号</param>
        /// <returns>水表型号对象</returns>
        [HttpGet]
        [Route("meter_models/{modelId}")]
        [ActionName("getModel")]
        public WapResponse<WapModelDto> GetModel(int modelId)
        {
            var result = Service.GetModelById(modelId);
            return new WapResponse<WapModelDto>(result);
        }

        /// <summary>
        ///获取所有型号
        /// </summary>
        /// <returns>水表型号对象列表</returns>
        [HttpGet]
        [Route("meter_models")]
        [ActionName("getAllMeterModels")]
        public WapCollection<WapModelDto> GetAllModels()
        {
            var result = Service.GetAllModels();
            return new WapCollection<WapModelDto>(result);
        }


    }
}

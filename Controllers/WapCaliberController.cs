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
    /// 定义口径对象控制器
    /// </summary>
    [Resource("wapCaliberRes")]
    [RoutePrefix(SH3H.WAP.Share.Consts.URL_PREFIX_WAP + "/common")]
    public class WapCaliberController :
        BaseController<IWapCaliberService>
    {
        /// <summary>
        /// 新增口径
        /// </summary>
        /// <param name="caliber">口径对象</param>
        /// <returns>口径对象</returns>
        [HttpPost]
        [Route("meter_calibers")]
        [ActionName("addCaliber")]
        public WapResponse<WapCaliberDto> AddCaliber(WapCaliberDto caliber)
        {
            var result = Service.AddCaliber(caliber);
            return new WapResponse<WapCaliberDto>(result);
        }

        /// <summary>
        /// 修改口径
        /// </summary>
        /// <param name="caliberId">口径对象</param>
        /// <returns>口径对象</returns>
        [HttpPut]
        [Route("meter_calibers/{caliberId}")]
        [ActionName("updateCaliber")]
        public WapResponse<WapCaliberDto> UpdateCaliber(int caliberId, WapCaliberDto caliber)
        {
            var result = Service.ModifyCaliberById(caliberId, caliber);
            return new WapResponse<WapCaliberDto>(result);
        }

        /// <summary>
        /// 删除口径
        /// </summary>
        /// <param name="id">口径编号</param>
        /// <returns>是否删除成功</returns>
        [HttpDelete]
        [Route("meter_calibers/{caliberId}")]
        [ActionName("deleteCaliber")]
        public WapBoolean RemoveCaliber(int caliberId)
        {
            var result = Service.RemoveCaliber(caliberId);
            return new WapBoolean(result);
        }

        /// <summary>
        ///获取指定口径
        /// </summary>
        /// <param name="caliberId">口径编号</param>
        /// <returns>口径对象</returns>
        [HttpGet]
        [Route("meter_calibers/{caliberId}")]
        [ActionName("getCaliber")]
        public WapResponse<WapCaliberDto> GetCaliber(int caliberId)
        {
            var result = Service.GetCaliberById(caliberId);
            return new WapResponse<WapCaliberDto>(result);
        }

        /// <summary>
        /// 获取所有口径
        /// </summary>
        /// <returns>口径对象列表</returns>
        [HttpGet]
        [Route("meter_calibers")]
        [ActionName("getAllCalibers")]
        public WapCollection<WapCaliberDto> GetAllCalibers()
        {
            var result = Service.GetAllCalibers();
            return new WapCollection<WapCaliberDto>(result);
        }


    }
}

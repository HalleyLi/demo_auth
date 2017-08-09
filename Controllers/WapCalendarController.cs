using SH3H.SDK.Share;
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
    /// 定义日历对象控制器
    /// </summary>
    [Resource("wapCalendarRes")]
    [RoutePrefix(SH3H.WAP.Share.Consts.URL_PREFIX_WAP + "/common")]
    public class WapCalendarController :
         BaseController<IWapCalendarService>
    {
        /// <summary>
        /// 新增日历
        /// </summary>
        /// <param name="calendar">日历对象</param>
        /// <returns>日历对象</returns>
        [HttpPost]
        [Route("calendars")]
        [ActionName("createCalendar")]
        public WapResponse<WapCalendarDto> AddCalendar([FromBody]WapCalendarDto calendar)
        {
            var result = Service.Add(calendar);
            return new WapResponse<WapCalendarDto>(result);
        }

        /// <summary>
        /// 删除日历
        /// </summary>
        /// <param name="calendarId">日历编号</param>
        /// <returns>是否删除成功</returns>
        [HttpDelete]
        [Route("calendars/{calendarId}")]
        [ActionName("deleteCalendar")]
        public WapBoolean RemoveCalendar(int calendarId)
        {
            var result = Service.Delete(calendarId);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 修改日历
        /// </summary>
        /// <param name="calendarId">日历编号</param>
        /// <param name="calendar">日历对象</param>
        /// <returns>是否修改成功</returns>
        [HttpPut]
        [Route("calendars/{calendarId}")]
        [ActionName("updateCalendar")]
        public WapBoolean ModifyCalendarById(int calendarId, [FromBody]WapCalendarDto calendar)
        {
            var result = Service.Modify(calendarId, calendar);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 通过日历编号修改日历状态
        /// </summary>
        /// <param name="id">日历编号</param>
        /// <param name="active">日历状态</param>
        /// <returns>返回时否修改成功</returns>
        [HttpPut]
        [Route("calendars/{calendarId}/state")]
        [ActionName("updateCalendarState")]
        public WapBoolean ModifyState(int calendarId, int state)
        {
            var result = Service.ModifyState(calendarId, state);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 通过日历编号查询日历
        /// </summary>
        /// <param name="calendarId">日历编号</param>
        /// <returns>日历对象</returns>
        [HttpGet]
        [Route("calendars/{calendarId}")]
        [ActionName("getCalendarById")]
        public WapResponse<WapCalendarDto> GetCalendar(int calendarId)
        {
            var result = Service.GetCalendar(calendarId);
            return new WapResponse<WapCalendarDto>(result);
        }


        /// <summary>
        /// 通过日历编码查询日历
        /// </summary>
        /// <param name="code">日历编码</param>
        /// <returns>日历对象</returns>
        [HttpGet]
        [Route("calendars")]
        [ActionName("getCalendarByCode")]
        public WapResponse<WapCalendarDto> GetCalendarByCode(string code)
        {
            var result = Service.GetCalendarByCode(code);
            return new WapResponse<WapCalendarDto>(result);
        }


        /// <summary>
        /// 获取所有日历
        /// </summary>
        /// <returns>日历对象</returns>
        [HttpGet]
        [Route("calendars")]
        [ActionName("getAllCalendars")]
        public WapResponse<IEnumerable<WapCalendarDto>> GetAllCalendars()
        {
            var result = Service.GetAllCalendars();
            return new WapResponse<IEnumerable<WapCalendarDto>>(result);
        }



    }
}

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
    /// 定义WapCalendarDateDto控制器
    /// </summary>
    [Resource("wapCalendarDateRes")]
    [RoutePrefix(SH3H.WAP.Share.Consts.URL_PREFIX_WAP+"/common")]
    public class WapCalendarDateController :
         BaseController<IWapCalendarDateService>
    {
        /// <summary>
        /// 新增节假日
        /// </summary>
        /// <param name="calendarDate">WapCalendarDateDto对象</param>
        /// <returns>返回WapCalendarDateDto对象</returns>
        [HttpPost]
        [Route("dates")]
        [ActionName("createCalendarDate")]
        public WapResponse<WapCalendarDateDto> AddCalendarDate([FromBody] WapCalendarDateDto calendarDate)
        {
            var result= Service.AddCalendarDate(calendarDate);
            return new WapResponse<WapCalendarDateDto>(result);
        }

        /// <summary>
        /// 删除日期日历
        /// </summary>
        /// <param name="calendarDateId">日期日历编号</param>
        /// <param name="date">日期</param>
        /// <returns>返回是否删除成功Boolean对象</returns>
        [HttpDelete]
        [Route("dates/{calendarDateId}")]
        [ActionName("deleteCalendarDate")]
        public WapBoolean RemoveCalendarDate(int calendarDateId)
        {
            var result= Service.RemoveCalendarDateById(calendarDateId);
            return new WapBoolean(result);
        }          

        /// <summary>
        /// 修改日期日历
        /// </summary>
        /// <param name="calendarId">日期日历编号</param>
        /// <param name="date">日期</param>
        /// <param name="calendarDate">WapCalendarDateDto对象</param>
        /// <returns>返回是否修改成功Boolean对象</returns>
        [HttpPut]
        [Route("calendar/{calendarId}/{date}")]
        [ActionName("updateCalendarDate")]
        private  WapBoolean ModifyCalendarDate(int calendarId, DateTime date, [FromBody]WapCalendarDateDto calendarDate)
        {
            var reusult= Service.ModifyCalendarDate(calendarId, date, calendarDate);
            return new WapBoolean(reusult);
        }        
        
        /// <summary>
        /// 修改指定节假日
        /// </summary>
        /// <param name="calendarDateId">编号</param>
        /// <param name="calendarDate">WapCalendarDateDto对象</param>
        /// <returns>返回是否修改成功Boolean对象</returns>
        [HttpPut]
        [Route("dates/{calendarDateId}")]
        [ActionName("updateCalendarDate")]
        public WapBoolean ModifyCalendarDate(int calendarDateId, [FromBody]WapCalendarDateDto calendarDate)
        {
            var result= Service.ModifyCalendarDateById(calendarDateId,calendarDate);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 查询日期日历
        /// </summary>
        /// <param name="calendarDateId">日期日历编号</param>
        /// <returns>返回WapCalendarDateDto对象</returns>
        [HttpGet]
        [Route("dates/{calendarDateId}")]
        [ActionName("getCalendarDateById")]
        public WapResponse<WapCalendarDateDto> GetCalendarDate(int calendarDateId)
        {
            var result = Service.GetCalendarDateById(calendarDateId);
            return new WapResponse<WapCalendarDateDto>(result);
        }

        /// <summary>
        /// 获取指定日历和日期的节假日
        /// </summary>
        /// <param name="calendarId">日期日历编号</param>
        /// <param name="date">日期</param>
        /// <returns>返回WapCalendarDateDto对象</returns>
        [HttpGet]
        [Route("calendars/{calendarId}/dates")]
        [ActionName("getCalendarDateByDate")]
        public WapResponse<WapCalendarDateDto> GetCalendarDate(int calendarId, DateTime date)
        {
            var result= Service.GetCalendarDate(calendarId, date);
            return new WapResponse<WapCalendarDateDto>(result);
        }

        /// <summary>
        /// 通过多个日期获得是否为工作日
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="dates">日期集合</param>
        /// <returns>WapCalendarDateDto集合</returns>
        [HttpPost]
        [Route("calendar/{calendarId}")]
        [ActionName("getCalendarDates")]
        private  WapResponse<IEnumerable<WapCalendarDateDto>> GetCalendarDates(int calendarId, [FromBody]IEnumerable<DateTime> dates)
        {
            var result= Service.GetIsHolidayByDates(calendarId,dates);
            return new WapResponse<IEnumerable<WapCalendarDateDto>>(result);
        }

        /// <summary>
        /// 获取指定日历下指定日期范围的节假日
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">截止日期</param>
        /// <returns>wapCalendarDate集合</returns>
        [HttpGet]
        [Route("calendars/{calendarId}/dates")]
        [ActionName("getCalendarDatesByDateRange")]
        public WapResponse<IEnumerable<WapCalendarDateDto>> GetCalendarDatesByDateRange(int calendarId, DateTime startDate, DateTime endDate)
        {
            var result=Service.GetCalendarDateByDateRange(calendarId, startDate, endDate);
            return new WapResponse<IEnumerable<WapCalendarDateDto>>(result);
        }

        /// <summary>
        /// 获取指定日历下的日期是否为工作日#
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="date">指定单个日期</param>
        /// <returns>是否为工作日</returns>
        [HttpGet]
        [Route("calendars/{calendarId}/dates/{date}/check")]
        [ActionName("checkWorkday")]
        public WapBoolean CheckWorkday(int calendarId, DateTime date)
        {
            var result= Service.checkWorkday(calendarId,date);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 通过开始日期和工作日天数获得是否为工作日#
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="offset">工作天数</param>
        /// <returns>截止日期</returns>
        [HttpGet]
        [Route("calendars/{calendarId}/dates")]
        [ActionName("getWorkdayByOffset")]
        public WapResponse<DateTime> GetWorkdayByNum(int calendarId, DateTime startDate, int offset)
        {
            var result= Service.GetWorkdayByNum(calendarId, startDate, offset);
            return new WapResponse<DateTime>(result);
        }


    }
}

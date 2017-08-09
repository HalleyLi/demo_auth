using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Contracts
{
    /// <summary>
    /// 节日
    /// </summary>
    public interface IWapCalendarDateService
    {
        /// <summary>
        /// 添加日历日期对象
        /// </summary>
        /// <param name="entity">WapCalendarDateDto对象</param>
        /// <returns>返回WapCalendarDateDto对象</returns>
        WapCalendarDateDto AddCalendarDate(WapCalendarDateDto entity);

        /// <summary>
        /// 修改日期日历对象
        /// </summary>
        /// <param name="caledarId">更新查询条件为日历日期编号</param>
        /// <param name="date">更新查询条件为日历日期的日期</param>
        /// <param name="calendarDate">CalendarDate对象</param>
        bool ModifyCalendarDate(int caledarId, DateTime date, WapCalendarDateDto calendarDate);

        /// <summary>
        /// 修改日期日历对象
        /// </summary>
        /// <param name="id">新查询条件为编号</param>
        /// <param name="calendarDate">wapCalendarDate对象</param>
        bool ModifyCalendarDateById(int id, WapCalendarDateDto calendarDate);

        /// <summary>
        /// 查询日期日历对象
        /// </summary>
        /// <param name="calendarId">查询条件为日历日期编号</param>
        /// <param name="date">查询条件为日历日期的日期</param>
        WapCalendarDateDto GetCalendarDate(int calendarId, DateTime date);

        /// <summary>
        /// 查询日期日历对象
        /// </summary>
        /// <param name="id">查询条件为日历日期编号</param>
        /// <returns>返回查询后WapCalendarDateDto单个对象</returns>
        WapCalendarDateDto GetCalendarDateById(int id);

        /// <summary>
        /// 删除日期日历对象
        /// </summary>
        /// <param name="calendarId">删除查询条件为日历日期编号</param>
        ///  <param name="date">删除查询条件为日历日期的日期</param>
        bool RemoveCalendarDate(int calendarId, DateTime date);

        /// <summary>
        /// 删除日期日历对象
        /// </summary>
        /// <param name="id">删除查询条件为日历日期编号</param>
        bool RemoveCalendarDateById(int id);

        /// <summary>
        /// 根据年份获取日历日期对象集
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="startDate">一年的开始日期</param>
        /// <param name="endDate">一年的结束日期</param>
        /// <returns>返回可枚举的WapCalendarDateDto对象</returns>
        IEnumerable<WapCalendarDateDto> GetCalendarDateByDateRange(int calendarId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// 通过日期范围获得日期是否为工作日
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="dates">日期集合</param>
        /// <returns></returns>
        IEnumerable<WapCalendarDateDto> GetIsHolidayByDates(int calendarId, IEnumerable<DateTime> dates);

        /// <summary>
        /// 通过单个日期返回是否为工作日
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        bool checkWorkday(int calendarId, DateTime date);

        /// <summary>
        /// 通过开始日期和工作日天数获得是否为工作日
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="date">开始日期</param>
        /// <param name="num">工作天数</param>
        /// <returns>截止日期</returns>
        DateTime GetWorkdayByNum(int calendarId, DateTime date, int num);

    }
}

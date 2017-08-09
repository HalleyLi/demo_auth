using SH3H.SDK.DataAccess.Repo;
using SH3H.WAP.DataAccess;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.DataAccess.Repo
{
    /// <summary>
    /// CalendarDate数据仓库
    /// </summary>
    public class WapCalendarDateRepository 
        : Repository<IWapCalendarDateStorage>
        , IWapCalendarDateRepository
    {
        /// <summary>
        /// 添加WapcalendarDate
        /// </summary>
        /// <param name="calendarDate">WapCalendarDate对象</param>
        /// <returns>WapCalendarDate对象</returns>
        public WapCalendarDate Insert(WapCalendarDate calendarDate)
        {
            return Storage.Insert(calendarDate);
        }

        /// <summary>
        /// 更新WapCalendarDate
        /// </summary>
        /// <param name="calendarId">>更新查询条件为日历日期编号</param>
        /// <param name="date">更新查询条件为日历日期的日期</param>
        /// <param name="calendarDate">更新对象为WapCalendarDate</param>
        /// <returns>返回是否更新成功Boolean对象</returns>
        public bool Update(int calendarId, DateTime date, WapCalendarDate calendarDate)
        {

            return Storage.Update(calendarId, date, calendarDate);
        }

        /// <summary>
        /// 通过日历日期编号更新WapCalendarDate
        /// </summary>
        /// <param name="id">日历日期编号</param>
        /// <param name="calendarDate">WapCalendarDate对象</param>
        /// <returns>返回是否更新成功</returns>
        public bool UpdateById(int id, WapCalendarDate calendarDate)
        {
            return Storage.UpdateById(id,calendarDate);
        }

        /// <summary>
        ///查询WapCalendarDate
        /// </summary>
        /// <param name="calendarId">查询条件为日历日期编号</param>
        /// <param name="date">查询条件为日历日期的日期</param>
        /// <returns>查询对象为CalendarDate</returns>
        public WapCalendarDate Select(int calendarId, DateTime date)
        {
            return Storage.Select(calendarId,date);
        }

        /// <summary>
        /// 通过日历日期编号查询WapCalendarDate
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>返回CalendarDate对象</returns>
        public WapCalendarDate SelectById(int id)
        {
            return Storage.SelectById(id);
        }

        /// <summary>
        /// 删除对象为CalendarDate
        /// </summary>
        /// <param name="calendarId">删除条件为日历日期编号</param>
        /// <param name="date">删除条件为日历日期的日期</param>
        /// <returns>返回是否删除成功</returns>
        public bool Delete(int calendarId, DateTime date)
        {
            return Storage.Delete(calendarId,date);
        }

        /// <summary>
        /// 通过日历日期编号删除WapCalendarDate
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>返回是否删除成功象</returns>
        public bool DeleteById(int id)
        {
            return Storage.DeleteById(id);
        }

        /// <summary>
        /// 根据年份获取日历日期对象集
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="startDate">一年的开始日期</param>
        /// <param name="endDate">一年的结束日期</param>
        /// <returns>返回可枚举的WapCalendarDate对象</returns>
        public IEnumerable<WapCalendarDate> SelectByDateRange(int calendarId, DateTime startDate, DateTime endDate)
        {
            return Storage.SelectByDateRange(calendarId,startDate,endDate);
        }

       /// <summary>
       /// 通过日期范围获得日期是否为工作日
       /// </summary>
       /// <param name="calendarId">日历日期编号</param>
       /// <param name="dates">日期集合</param>
       /// <returns></returns>
        public IEnumerable<WapCalendarDate> SelectIsHoliday(int calendarId, IEnumerable<DateTime> dates)
       {
           return Storage.SelectIsHoliday(calendarId,dates);
       }


    }
}

using SH3H.WAP.Contracts;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Share;

namespace SH3H.WAP.Service
{
    /// <summary>
    /// 节日服务操作
    /// </summary>
    public class WapCalendarDateServiceImpl : IWapCalendarDateService
    {

        private IWapCalendarDateRepository _repo;

        public WapCalendarDateServiceImpl(IWapCalendarDateRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 添加日历日期对象
        /// </summary>
        /// <param name="calendarDate">WapCalendarDateDto对象</param>
        /// <returns>返回WapCalendarDateDto对象</returns>
        public WapCalendarDateDto AddCalendarDate(WapCalendarDateDto calendarDate)
        {
            if (calendarDate == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "日历日期对象不允许为空");
            }
            var cresult = calendarDate.Validate();
            if (!cresult.IsValid)
            {
                throw cresult.BuildException();
            }
            var result = _repo.Insert(calendarDate.ToModel());
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "添加日历日期对象失败");
            }
            return WapCalendarDateDto.FromModel(result);
        }

        /// <summary>
        /// 修改日历日期对象
        /// </summary>
        /// <param name="calendarId">更新查询条件为日历日期编号</param>
        /// <param name="date">更新查询条件为日历日期的日期</param>
        /// <param name="calendarDate">WapCalendarDateDto对象</param>
        /// <returns>返回是否更新成功Boolean对象</returns>
        public bool ModifyCalendarDate(int calendarId, DateTime date, WapCalendarDateDto calendarDate)
        {
            if (calendarDate == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "日历日期对象不允许为空");
            }
            var cresult = calendarDate.Validate();
            if (!cresult.IsValid)
            {
                throw cresult.BuildException();
            }
            if (_repo.SelectById(calendarId) == null)
            {
                throw new WapException(StateCode.CODE_MODEL_NOT_EXIST, "节日不存在");
            }

            return _repo.Update(calendarId, date, calendarDate.ToModel());
        }

        /// <summary>
        /// 修改日历日期对象
        /// </summary>
        /// <param name="id">编号</param>
        /// /// <param name="calendarDate">WapCalendarDateDto对象</param>
        /// <returns>返回是否更新成功Boolean对象</returns>
        public bool ModifyCalendarDateById(int id, WapCalendarDateDto calendarDate)
        {
            return _repo.UpdateById(id, calendarDate.ToModel());
        }

        /// <summary>
        /// 查询日历日期对象
        /// </summary>
        /// <param name="calendarId">查询条件为日历日期编号</param>
        /// <param name="date">查询条件为日历日期的日期</param>
        /// <returns>返回查询后WapCalendarDateDto单个对象</returns>
        public WapCalendarDateDto GetCalendarDate(int calendarId, DateTime date)
        {
            var result = _repo.Select(calendarId, date);
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "查询日历日期对象失败");
            }
            return WapCalendarDateDto.FromModel(result);
        }

        /// <summary>
        /// 查询日历日期对象
        /// </summary>
        /// <param name="id">查询条件为编号</param>
        /// <returns>返回查询后WapCalendarDateDto单个对象</returns>
        public WapCalendarDateDto GetCalendarDateById(int id)
        {
            var result = _repo.SelectById(id);
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, " 查询日历日期对象失败");
            }
            return WapCalendarDateDto.FromModel(result);
        }

        /// <summary>
        /// 删除日历日期对象
        /// </summary>
        /// <param name="calendarId">删除查询条件为日历日期编号</param>
        ///  <param name="date">删除查询条件为日历日期的日期</param>
        /// <returns>返回是否删除成功Boolean对象</returns>
        public bool RemoveCalendarDate(int calendarId, DateTime date)
        {
            if (_repo.SelectById(calendarId) == null)
            {
                throw new WapException(StateCode.CODE_MODEL_NOT_EXIST, "节日不存在");
            }
            return _repo.Delete(calendarId, date);
        }

        /// <summary>
        /// 删除日历日期对象
        /// </summary>
        /// <param name="id">删除查询条件为编号</param>
        /// <returns>返回是否删除成功Boolean对象</returns>
        public bool RemoveCalendarDateById(int calendarId)
        {
            if (_repo.SelectById(calendarId) == null)
            {
                throw new WapException(StateCode.CODE_MODEL_NOT_EXIST, "节日不存在");
            }
            return _repo.DeleteById(calendarId);
        }

        /// <summary>
        /// 根据年份获取日历日期对象集
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>返回可枚举的WapCalendarDateDto对象</returns>
        public IEnumerable<WapCalendarDateDto> GetCalendarDateByDateRange(int calendarId, DateTime startDate, DateTime endDate)
        {
            return _repo.SelectByDateRange(calendarId, startDate, endDate).Select(p => WapCalendarDateDto.FromModel(p)).ToList(); ;
        }


        /// <summary>
        /// 通过日期范围获得日期是否为工作日
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="dates">日期集合</param>
        /// <returns></returns>
        public IEnumerable<WapCalendarDateDto> GetIsHolidayByDates(int calendarId, IEnumerable<DateTime> dates)
        {

            var results = _repo.SelectIsHoliday(calendarId, dates);
            List<WapCalendarDateDto> dateList = results.Select(p => WapCalendarDateDto.FromModel(p)).ToList();
            var list = dates.Where(d => results.All(r => r.CalendarDate != d)).ToList();
            foreach (var d in list)
            {
                WapCalendarDateDto date = new WapCalendarDateDto();
                date.CalendarDate = d;
                date.CalendarId = calendarId;
                date.CalendarDateId = 0;
                if (d.DayOfWeek == DayOfWeek.Saturday ||
                    d.DayOfWeek == DayOfWeek.Sunday)
                {
                    // 双休日
                    date.CalendarDateType = "2";
                    date.CalendarDateName = "双休日";
                }
                else
                {
                    // 工作日
                    date.CalendarDateType = "1";
                    date.CalendarDateName = "工作日";
                }
                date.IsWorkday = (date.CalendarDateType == "1");
                date.IsHoliday = !date.IsWorkday;
                dateList.Add(date);
            }
            return dateList;
        }

        /// <summary>
        /// 通过指定日期获得是否为工作日
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="date">指定单个日期</param>
        /// <returns>是否为工作日</returns>
        public bool checkWorkday(int calendarId, DateTime date)
        {
            IEnumerable<DateTime> dateSingle = new List<DateTime> { date };
            var result = GetIsHolidayByDates(calendarId, dateSingle);
            WapCalendarDateDto dateresult = result.Where(a => a.CalendarDate == date).FirstOrDefault();
            return dateresult.IsWorkday;
        }

        /// <summary>
        /// 通过开始日期和工作日天数获得是否为工作日
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="date">开始日期</param>
        /// <param name="num">工作天数</param>
        /// <returns>截止日期</returns>
        public DateTime GetWorkdayByNum(int calendarId, DateTime date, int num)
        {
            DateTime enddate = date.AddDays(num * 3);
            List<DateTime> datelist = new List<DateTime>();
            while (date < enddate)
            {
                datelist.Add(date);
                date = date.AddDays(1);
            }
            var totalresult = GetIsHolidayByDates(calendarId, datelist).Where(a => a.IsHoliday == false).OrderBy(a => a.CalendarDate).ToList();
            totalresult = totalresult.Take(num).ToList(); ;
            WapCalendarDateDto result = totalresult.LastOrDefault();
            return result.CalendarDate;
        }

        #region 参数检查
        /// <summary>
        /// 检查入参是否是GUID类型字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="fieldName">参数中文名</param>
        /// <returns></returns>
        public static void CheckGuidStr(string str, string fieldName)
        {
            if (!Utils.IsGuid(str))
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_INVALID_ARGUMENTS, "参数 " + fieldName + " 不是合法的GUID类型字符串");
            }
        }

        /// <summary>
        /// 检查入参是否是空字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="fieldName">参数中文名</param>
        /// <returns></returns>
        public static void CheckNullStr(string str, string fieldName)
        {

            if (string.IsNullOrEmpty(str))
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_INVALID_ARGUMENTS, "参数 " + fieldName + " 不能为空");

            }
        }

        /// <summary>
        /// 检查入参是否是空
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static void CheckNull<T>(T obj, string fieldName)
        {
            if (obj == null)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_INVALID_ARGUMENTS, "参数 " + fieldName + " 不能为空");

            }
        }

        #endregion

    }
}

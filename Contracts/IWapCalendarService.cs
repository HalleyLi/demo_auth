using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Contracts
{
    /// <summary>
    /// 日历
    /// </summary>
    public interface IWapCalendarService
    {
        WapCalendarDto Add(WapCalendarDto calendar);

        /// <summary>
        /// 删除日历
        /// </summary>
        /// <param name="id">日历编号</param>
        /// <returns>是否删除成功</returns>
        bool Delete(int id);

        /// <summary>
        /// 通过日历编号修改日历
        /// </summary>
        /// <param name="id">日历编号</param>
        /// <param name="calendar">日历对象</param>
        /// <returns>返回时否修改成功</returns>
        bool Modify(int id, WapCalendarDto calendar);

          /// <summary>
        /// 通过日历编号修改日历状态
        /// </summary>
        /// <param name="calendarId">日历编号</param>
        /// <param name="active">日历状态</param>
        /// <returns>返回时否修改成功</returns>
        bool ModifyState(int calendarId, int state);

        /// <summary>
        /// 通过日历编号查询日历
        /// </summary>
        /// <param name="id">日历编号</param>
        /// <returns>日历对象</returns>
        WapCalendarDto GetCalendar(int id);

        /// <summary>
        /// 获取所有日历
        /// </summary>
        /// <returns>日历对象</returns>
        IEnumerable<WapCalendarDto> GetAllCalendars();


        /// <summary>
        /// 凭编号获取日历
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        WapCalendarDto GetCalendarByCode(string code);
    }
}

using SH3H.SDK.DataAccess.Core;

using SH3H.WAP.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.DataAccess.Repo.Contact
{
    /// <summary>
    /// 定义日历日期操作接口
    /// </summary>
    public interface IWapCalendarDateRepository
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        WapCalendarDate Select(int key1,DateTime key2);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        WapCalendarDate Insert(WapCalendarDate entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        bool Update(int key1, DateTime key2, WapCalendarDate entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        bool Delete(int key1, DateTime key2);

        /// <summary>
        /// 根据编号更新
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="calendarDate"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        bool UpdateById(int id, WapCalendarDate calendarDate);

        /// <summary>
        /// 通过日历日期编号删除
        /// </summary>
        /// <param name="id">日历日期编号</param>
        /// <param name="trans"></param>
        bool DeleteById(int id);

        /// <summary>
        /// 通过日历日期编号查询
        /// </summary>
        /// <param name="id">日历日期编号</param>
        WapCalendarDate SelectById(int id);

        /// <summary>
        /// 根据年份获取日历日期对象集
        /// </summary>
        /// <param name="id">日历日期编号</param>
        /// <param name="startDate">一年的开始日期</param>
        /// <param name="endDate">一年的结束日期</param>
        /// <returns>返回可枚举的WapCalendarDate对象</returns>
        IEnumerable<WapCalendarDate> SelectByDateRange(int id, DateTime startDate, DateTime endDate);

        /// <summary>
        /// 通过日期范围获得日期是否为工作日
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="dates">日期集合</param>
        /// <returns></returns>
        IEnumerable<WapCalendarDate> SelectIsHoliday(int calendarId, IEnumerable<DateTime> dates);

        
    }
}



using SH3H.SDK.DataAccess.Repo;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Model;
using System.Collections.Generic;
namespace SH3H.WAP.DataAccess.Repo
{
    /// <summary>
    /// Calendar 数据仓库
    /// </summary>
    public class WapCalendarRepository
        : Repository<IWapCalendarStorage>
        , IWapCalendarRepository
    {
        public WapCalendar Insert(WapCalendar calendar)
        {
            return Storage.Insert(calendar);
        }

        /// <summary>
        /// 删除日历
        /// </summary>
        /// <param name="id">日历编号</param>
        /// <returns>是否删除成功</returns>
        public bool Delete(int id)
        {
            return Storage.Delete(id);
        }

        /// <summary>
        /// 通过日历编号修改日历
        /// </summary>
        /// <param name="id">日历编号</param>
        /// <param name="calendar">日历对象</param>
        /// <returns>返回时否修改成功</returns>
        public bool Update(int id, WapCalendar calendar)
        {
            return Storage.Update(id,calendar);
        }

         /// <summary>
        /// 通过日历编号修改日历状态
        /// </summary>
        /// <param name="id">日历编号</param>
        /// <param name="state">日历状态</param>
        /// <returns>返回时否修改成功</returns>
        public bool UpdateState(int id, int state) 
        {
            return Storage.UpdateState(id, state);
        }

        /// <summary>
        /// 通过日历编号查询日历
        /// </summary>
        /// <param name="id">日历编号</param>
        /// <returns>日历对象</returns>
        public WapCalendar Select(int id)
        {
            return Storage.Select(id);
        }

        /// <summary>
        /// 获取所有日历
        /// </summary>
        /// <returns>日历对象</returns>
        public IEnumerable<WapCalendar> SelectAll(bool includeBanned)
        {
            return Storage.SelectAll(includeBanned);
        }


        /// <summary>
        /// 凭日历编码获取日历
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public WapCalendar SelectByCode(string code)
        {
            return Storage.SelectByCode(code);
        }
    }
}


using SH3H.SDK.Definition.Auditing;
using SH3H.SDK.Definition.Entities;
using SH3H.SDK.Share;
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
    /// 定义文件资源描述对象
    /// </summary>
    public interface IWapCalendarRepository
    {
        /// <summary>
        /// 获取所有日历
        /// </summary>
        /// <returns>日历对象</returns>
        IEnumerable<WapCalendar> SelectAll(bool includeBanned);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        WapCalendar Select(int key);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        WapCalendar Insert(WapCalendar entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        bool Update(int key, WapCalendar entity);

        /// <summary>
        /// 通过日历编号修改日历状态
        /// </summary>
        /// <param name="id">日历编号</param>
        /// <param name="state">日历状态</param>
        /// <returns>返回时否修改成功</returns>
        bool UpdateState(int id, int state);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        bool Delete(int key);

        /// <summary>
        /// 凭日历编码获取日历
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        WapCalendar SelectByCode(string code);
    }
}

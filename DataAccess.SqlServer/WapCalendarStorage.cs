using SH3H.SDK.DataAccess.Db;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.SDK.Share;
using SH3H.SharpFrame.Data;
using SH3H.WAP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace SH3H.WAP.DataAccess.SqlServer
{
    /// <summary>
    /// 日历数据库操作
    /// </summary>
    public class WapCalendarStorage : BaseAccess<WapCalendar>, IWapCalendarStorage
    {
        public WapCalendarStorage()
            : base(Consts.CONFIGURE_DATABASE_CONNECTION_STRING) { }

        /// <summary>
        /// 添加日历
        /// </summary>
        /// <param name="calendar">日历对象</param>
        /// <returns>日历对象</returns>
        public WapCalendar Insert(WapCalendar calendar)
        {
            try
            {
                string sqlText = @"INSERT INTO CNF_CALENDAR(TENANT_ID,CALENDAR_CODE,CALENDAR_NAME,STATE,REMARK)
                                                    VALUES(@TENANT_ID,@CALENDAR_CODE,@CALENDAR_NAME,@STATE,@REMARK)
                                                    SELECT @@IDENTITY;";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@TENANT_ID", DbType.Int32, calendar.TenantId);
                    Database.AddInParameter(cmd, "@CALENDAR_CODE", DbType.String, calendar.CalendarCode);
                    Database.AddInParameter(cmd, "@CALENDAR_NAME", DbType.String, calendar.CalendarName);
                    Database.AddInParameter(cmd, "@STATE", DbType.Int32, calendar.States);
                    Database.AddInParameter(cmd, "@REMARK", DbType.String, calendar.Remark);
                    int id = ExecuteScalar<int>(cmd);
                    calendar.CalendarId = id;
                    return calendar;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        /// <summary>
        /// 删除日历
        /// </summary>
        /// <param name="id">日历编号</param>
        /// <returns>是否删除成功</returns>
        public bool Delete(int id)
        {
            try
            {
                string sqlText = @"DELETE FROM CNF_CALENDAR
                                  WHERE ID=@ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@ID", DbType.Int32, id);
                    return ExecuteNonQuery(cmd) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }

        /// <summary>
        /// 通过日历编号修改日历
        /// </summary>
        /// <param name="id">日历编号</param>
        /// <param name="calendar">日历对象</param>
        /// <returns>返回时否修改成功</returns>
        public bool Update(int id, WapCalendar calendar)
        {
            try
            {
                string sqlText = @"UPDATE CNF_CALENDAR
                                   SET TENANT_ID=@TENANT_ID,
                                      CALENDAR_CODE=@CALENDAR_CODE,
                                      CALENDAR_NAME=@CALENDAR_NAME,
                                      STATE=@STATE,
                                      REMARK=@REMARK
                                   WHERE ID=@ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@ID", DbType.Int32, id);
                    Database.AddInParameter(cmd, "@TENANT_ID", DbType.Int32, calendar.TenantId);
                    Database.AddInParameter(cmd, "@CALENDAR_CODE", DbType.String, calendar.CalendarCode);
                    Database.AddInParameter(cmd, "@CALENDAR_NAME", DbType.String, calendar.CalendarName);
                    Database.AddInParameter(cmd, "@STATE", DbType.Int32, calendar.States);
                    Database.AddInParameter(cmd, "@REMARK", DbType.String, calendar.Remark);
                    return ExecuteNonQuery(cmd) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }

        /// <summary>
        /// 通过日历编号修改日历状态
        /// </summary>
        /// <param name="id">日历编号</param>
        /// <param name="active">日历状态</param>
        /// <returns>返回时否修改成功</returns>
        public bool UpdateState(int id, int state)
        {
            try
            {
                string sqlText = @"UPDATE CNF_CALENDAR
                                   SET STATE=@STATE
                                   WHERE ID=@ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@ID", DbType.Int32, id);
                    Database.AddInParameter(cmd, "@STATE", DbType.Int32, state);
                    return ExecuteNonQuery(cmd) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }

        /// <summary>
        /// 通过日历编号查询日历
        /// </summary>
        /// <param name="id">日历编号</param>
        /// <returns>日历对象</returns>
        public WapCalendar Select(int id)
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_CALENDAR
                                   WHERE ID=@ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@ID", DbType.Int32, id);
                    return SelectSingle(cmd);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        /// <summary>
        /// 凭日历编码获取日历
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public WapCalendar SelectByCode(string code)
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_CALENDAR
                                   WHERE CALENDAR_CODE=@calendar_code";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@calendar_code", DbType.String, code);
                    return SelectSingle(cmd);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        /// <summary>
        /// 获取所有日历
        /// </summary>
        /// <returns>日历对象</returns>
        public IEnumerable<WapCalendar> SelectAll(bool includeBanned)
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_CALENDAR WHERE STATE=1";
                if (includeBanned == true)
                {
                    sqlText += " OR STATE=0";
                }
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    return SelectList(cmd);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        /// <summary>
        ///  根据<see cref="IDataReader"/>数据读取器构建对象实体
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="calendar">文件描述器对象</param>
        /// <returns>如果构建对象成功返回对象实例，否则返回NULL</returns>
        public override WapCalendar Build(IDataReader reader, WapCalendar calendar)
        {
            calendar.CalendarId = reader.GetReaderValue<int>("ID");
            calendar.TenantId = reader.GetReaderValue<int>("TENANT_ID");
            calendar.CalendarCode = reader.GetReaderValue<string>("CALENDAR_CODE");
            calendar.CalendarName = reader.GetReaderValue<string>("CALENDAR_NAME", null, true);
            calendar.States = reader.GetReaderValue<int>("STATE");
            calendar.Remark = reader.GetReaderValue<string>("REMARK", null, true);
            return base.Build(reader, calendar);
        }


    }
}

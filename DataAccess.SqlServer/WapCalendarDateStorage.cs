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

namespace SH3H.WAP.DataAccess.SqlServer
{
    /// <summary>
    /// 节日数据库操作
    /// </summary>
    public class WapCalendarDateStorage : BaseAccess<WapCalendarDate>, IWapCalendarDateStorage
    {
        public WapCalendarDateStorage()
            : base(Consts.CONFIGURE_DATABASE_CONNECTION_STRING) { }

        public bool CreateSchema()
        {
            throw new NotImplementedException();
        }

        #region CalendarDate

        /// <summary>
        /// 数据库添加操作
        /// </summary>
        /// <param name="entity">添加对象对WapCalendarDate</param>
        /// <param name="trans">返回WapCalendarDate对象</param>
        /// <returns></returns>
        public WapCalendarDate Insert(WapCalendarDate entity)
        {
            try
            {
                string sqlText = @" INSERT INTO CNF_CALENDAR_DATE(CALENDAR_ID,CALENDAR_DATE,CALENDAR_DATE_TYPE,IS_HOLIDAY,Is_WORKDAY,CALENDAR_DATE_NAME,REMARK)
                                    VALUES(@CALENDAR_ID,@CALENDAR_DATE,@CALENDAR_DATE_TYPE,@IS_HOLIDAY,@IS_WORKDAY,@CALENDAR_DATE_NAME,@REMARK)
                                    SELECT @@IDENTITY;";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@CALENDAR_ID", DbType.Int32, entity.CalendarId);
                    Database.AddInParameter(cmd, "@CALENDAR_DATE", DbType.DateTime, entity.CalendarDate);
                    Database.AddInParameter(cmd, "@CALENDAR_DATE_TYPE", DbType.String, entity.CalendarDateType);
                    Database.AddInParameter(cmd, "@IS_HOLIDAY", DbType.Boolean, entity.IsHoliday);
                    Database.AddInParameter(cmd, "@IS_WORKDAY", DbType.Boolean, entity.IsWorkday);
                    Database.AddInParameter(cmd, "@CALENDAR_DATE_NAME", DbType.String, entity.CalendarDateName);
                    Database.AddInParameter(cmd, "@REMARK", DbType.String, entity.Remark);
                    int id = ExecuteScalar<int>(cmd);
                    entity.CalendarId = id;
                    return entity;
                }

            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        /// <summary>
        /// 数据库更新操作
        /// </summary>
        /// <param name="id">更新查询条件为编号</param>
        /// <param name="calendarDate">更新对象为WapCalendarDate</param>
        /// <param name="trans"></param>
        /// <returns>返回是否更新成功Boolean对象</returns>
        public bool UpdateById(int id, WapCalendarDate calendarDate)
        {
            try
            {
                string sqlText = @" UPDATE CNF_CALENDAR_DATE 
                                      SET CALENDAR_DATE=@CALENDAR_DATE ,
                                          CALENDAR_DATE_TYPE=@CALENDAR_DATE_TYPE,
                                          IS_HOLIDAY=@IS_HOLIDAY ,
                                          IS_WORKDAY=@IS_WORKDAY,
                                          CALENDAR_DATE_NAME=@CALENDAR_DATE_NAME,
                                          REMARK=@REMARK WHERE ID=@ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@ID", DbType.Int32, id);
                    Database.AddInParameter(cmd, "@CALENDAR_DATE", DbType.DateTime, calendarDate.CalendarDate);
                    Database.AddInParameter(cmd, "@CALENDAR_DATE_TYPE", DbType.String, calendarDate.CalendarDateType);
                    Database.AddInParameter(cmd, "@IS_HOLIDAY", DbType.Boolean, calendarDate.IsHoliday);
                    Database.AddInParameter(cmd, "@IS_WORKDAY", DbType.Boolean, calendarDate.IsWorkday);
                    Database.AddInParameter(cmd, "@CALENDAR_DATE_NAME", DbType.String, calendarDate.CalendarDateName);
                    Database.AddInParameter(cmd, "@REMARK", DbType.String, calendarDate.Remark);
                    return ExecuteNonQuery(cmd)>0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }

        /// <summary>
        /// 数据库查询操作
        /// </summary>
        /// <param name="id">查询条件为CALENDAR_ID</param>
        /// <returns>返回查询后WapCalendarDate的单个结果</returns>
        public WapCalendarDate SelectById(int id)
        {
            try
            {
                string sqlText = @"SELECT *
                                   FROM CNF_CALENDAR_DATE
                                   WHERE ID = @ID ";
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
        /// 通过日期获取是否为工作日
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="dates">日期集合</param>
        /// <returns>WapCalendarDate集合</returns>
        public IEnumerable<WapCalendarDate> SelectIsHoliday(int calendarId, IEnumerable<DateTime> dates)
        {
            try
            {
                string inClause = ConvertArrayToString<DateTime>(dates, d => "'" + d.ToString("yyyy-MM-dd") + "'");
                string sqlText = @"SELECT * FROM CNF_CALENDAR_DATE WHERE CALENDAR_DATE in (" + inClause + ") AND CALENDAR_ID=@CALENDAR_ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@CALENDAR_ID", DbType.String, calendarId);
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
        /// 数据库删除操作
        /// </summary>
        /// <param name="id">删除范围限定CALENDAR_ID</param>
        /// <param name="trans"></param>
        /// <returns>返回是否删除成功Boolean对象</returns>
        public bool DeleteById(int id)
        {
            try
            {
                string sqlText = @"DELETE FROM CNF_CALENDAR_DATE
                                   WHERE ID = @ID ";
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
        /// CalendarDate查询操作
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="date">日历日期的日期字段</param>
        /// <returns>CalendarDate对象</returns>
        public WapCalendarDate Select(int calendarId, DateTime date)
        {
            try
            {
                string sqlText = @"SELECT *
                                   FROM CNF_CALENDAR_DATE
                                   WHERE CALENDAR_ID = @CALENDAR_ID  
                                   AND CALENDAR_DATE=@CALENDAR_DATE ";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@CALENDAR_ID", DbType.Int32, calendarId);
                    Database.AddInParameter(cmd, "@CALENDAR_DATE", DbType.DateTime, date);
                    var result = SelectSingle(cmd);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        /// <summary>
        /// CalendarDate更新操作
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="date">日期</param>
        /// <param name="calendarDate">WapCalendarDate对象</param>
        /// <param name="trans"></param>
        /// <returns>是否更新成功Boolean对象</returns>
        public bool Update(int calendarId, DateTime date, WapCalendarDate calendarDate)
        {
            try
            {

                string sqlText = @" UPDATE CNF_CALENDAR_DATE 
                                      SET CALENDAR_DATE=@CALENDAR_DATE ,
                                          CALENDAR_DATE_TYPE=@CALENDAR_DATE_TYPE,
                                          IS_HOLIDAY=@IS_HOLIDAY ,
                                          IS_WORKDAY=@IS_WORKDAY ,
                                          CALENDAR_DATE_NAME=@CALENDAR_DATE_NAME,
                                          REMARK=@REMARK WHERE CALENDAR_ID=@CALENDAR_ID AND CALENDAR_DATE=@CALENDAR_DATE2";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@CALENDAR_ID", DbType.Int32, calendarId);
                    Database.AddInParameter(cmd, "@CALENDAR_DATE", DbType.DateTime, calendarDate.CalendarDate);
                    Database.AddInParameter(cmd, "@CALENDAR_DATE_TYPE", DbType.String, calendarDate.CalendarDateType);
                    Database.AddInParameter(cmd, "@IS_HOLIDAY", DbType.Boolean, calendarDate.IsHoliday);
                    Database.AddInParameter(cmd, "@IS_WORKDAY", DbType.Boolean, calendarDate.IsWorkday);
                    Database.AddInParameter(cmd, "@CALENDAR_DATE_NAME", DbType.String, calendarDate.CalendarDateName);
                    Database.AddInParameter(cmd, "@REMARK", DbType.String, calendarDate.Remark);
                    Database.AddInParameter(cmd, "@CALENDAR_DATE2", DbType.DateTime, date);
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
        /// CalendaaDate删除操作
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="date">日期</param>
        /// <param name="trans"></param>
        /// <returns>是否删除成功Boolean对象</returns>
        public bool Delete(int calendarId, DateTime date)
        {
            try
            {
                string sqlText = @"DELETE FROM CNF_CALENDAR_DATE
                                   WHERE CALENDAR_ID = @CALENDAR_ID AND CALENDAR_DATE=@CALENDAR_DATE ";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@CALENDAR_ID", DbType.Int32, calendarId);
                    Database.AddInParameter(cmd, "@CALENDAR_DATE", DbType.DateTime, date);
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
        /// 根据年份获取日历日期对象集
        /// </summary>
        /// <param name="calendarId">日历日期编号</param>
        /// <param name="startDate">一年的开始日期</param>
        /// <param name="endDate">一年的结束日期</param>
        /// <returns>返回可枚举的WapCalendarDate对象</returns>
        public IEnumerable<WapCalendarDate> SelectByDateRange
            (int calendarId, DateTime startDate, DateTime endDate)
        {
            try
            {
                string sqlText = @"SELECT *
                                   FROM CNF_CALENDAR_DATE
                                   WHERE CALENDAR_ID = @CALENDAR_ID  
                                   AND ( CALENDAR_DATE>=@CALENDAR_DATE1 AND CALENDAR_DATE<=@CALENDAR_DATE2) 
                                   ORDER BY CALENDAR_DATE ";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@CALENDAR_ID", DbType.Int32, calendarId);
                    Database.AddInParameter(cmd, "@CALENDAR_DATE1", DbType.DateTime, startDate);
                    Database.AddInParameter(cmd, "@CALENDAR_DATE2", DbType.DateTime, endDate);
                    var result = SelectList(cmd);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        /// <summary>
        /// 根据<see cref="IDataReader"/>数据读取器构建对象实体
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="calendardate">文件描述器对象</param>
        /// <returns>如果构建对象成功返回对象实例，否则返回NULL</returns>
        public override WapCalendarDate Build(IDataReader reader, WapCalendarDate calendardate)
        {
            try
            {
                calendardate.CalendarDate = reader.GetReaderValue<DateTime>("CALENDAR_DATE");
                calendardate.CalendarId = reader.GetReaderValue<int>("CALENDAR_ID");
                calendardate.CalendarDateId = reader.GetReaderValue<int>("ID");
                calendardate.CalendarDateType = reader.GetReaderValue<string>("CALENDAR_DATE_TYPE");
                calendardate.CalendarDateName = reader.GetReaderValue<string>("CALENDAR_DATE_NAME");
                calendardate.IsHoliday = reader.GetReaderValue<bool>("IS_HOLIDAY");
                calendardate.IsWorkday = reader.GetReaderValue<bool>("IS_WORKDAY");
                calendardate.Remark = reader.GetReaderValue<string>("REMARK", null, true);
                return calendardate;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        #endregion

    }
}

using SH3H.SDK.DataAccess.Db;
using SH3H.SDK.Definition.Exceptions;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.WAP.Model;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SH3H.SharpFrame.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SH3H.WAP.DataAccess.SqlServer
{
    /// <summary>
    /// 量程Storage
    /// </summary>
    public class WapMeterRangeStorage : BaseAccess<WapMeterRange>, IWapMeterRangeStorage
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public WapMeterRangeStorage()
            : base(SH3H.SDK.Share.Consts.CONFIGURE_DATABASE_CONNECTION_STRING) { }

        public bool CreateSchema()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 新增量程
        /// </summary>
        /// <param name="range">量程对象</param>
        /// <returns>量程对象</returns>
        public WapMeterRange AddRange(WapMeterRange range)
        {
            try
            {
                string commandText = @"INSERT INTO CNF_METER_RANGE (RANGE_NAME,RANGE_VALUE,RANGE_VOLUME,RANGE_STATE)
                                                     VALUES(@RANGE_NAME,@RANGE_VALUE,@RANGE_VOLUME,@RANGE_STATE) SELECT @@IDENTITY;";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@RANGE_NAME", DbType.String, range.RangeName);
                    Database.AddInParameter(command, "@RANGE_VALUE", DbType.Int32, range.RangeValue);
                    Database.AddInParameter(command, "@RANGE_VOLUME", DbType.Double, range.RangeVolumes);
                    Database.AddInParameter(command, "@RANGE_STATE", DbType.Int32, range.RangeState);
                    int id = ExecuteScalar<int>(command);
                    range.RangeId = id;
                    return range;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "新增量程对象失败");
            }

        }

        /// <summary>
        /// 修改量程
        /// </summary>
        /// <param name="id">量程id</param>
        /// <param name="range">量程对象</param>
        /// <returns>量程对象</returns>
        public WapMeterRange UpdateRange(int id, WapMeterRange range)
        {
            try
            {
                string commandText = @"UPDATE CNF_METER_RANGE SET RANGE_NAME=@RANGE_NAME,
                                                          RANGE_VALUE=@RANGE_VALUE,
                                                          RANGE_VOLUME=@RANGE_VOLUME,
                                                          RANGE_STATE =@RANGE_STATE
                                                       WHERE RANGE_ID=@RANGE_ID";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@RANGE_ID", DbType.Int32, id);
                    Database.AddInParameter(command, "@RANGE_NAME", DbType.String, range.RangeName);
                    Database.AddInParameter(command, "@RANGE_VALUE", DbType.Int32, range.RangeValue);
                    Database.AddInParameter(command, "@RANGE_VOLUME", DbType.Double, range.RangeVolumes);
                    Database.AddInParameter(command, "@RANGE_STATE", DbType.Int32, range.RangeState);
                    if (ExecuteNonQuery(command) > 0)
                        return range;
                    else
                        return null;
                }
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "修改量程对象失败");
            }
        }

        /// <summary>
        /// 获取所有的量程列表
        /// </summary>
        /// <returns>所有的量程列表</returns>
        public IEnumerable<WapMeterRange> SelectAllRanges()
        {
            try
            {
                string commandText = @"SELECT * FROM CNF_METER_RANGE";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    var list = SelectList(command);
                    return (list == null || list.Count() == 0) ? null : list;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "获取所有量程列表失败");
            }
        }

        /// <summary>
        /// 通过id获取指定量程
        /// </summary>
        /// <param name="id">量程id</param>
        /// <returns>量程对象</returns>
        public WapMeterRange GetRangeById(int id)
        {
            try
            {
                string commandText = @"SELECT * FROM CNF_METER_RANGE  WHERE RANGE_ID=@RANGE_ID";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@RANGE_ID", DbType.Int32, id);
                    var list = SelectList(command);
                    return (list == null || list.Count() == 0) ? null : list.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "获取指定量程对象失败");
            }
        }

        /// <summary>
        /// 删除量程
        /// </summary>
        /// <param name="id">量程id</param>
        /// <returns>是否成功</returns>
        public bool DeleteRange(int id)
        {
            try
            {
                string commandText = @"DELETE FROM  CNF_METER_RANGE 
                                       WHERE RANGE_ID=@RANGE_ID";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@RANGE_ID", DbType.Int32, id);
                    if (ExecuteNonQuery(command) > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "删除量程失败");
            }
        }

        /// <summary>
        /// 根据<see cref="IDataReader"/>数据读取器构建实体
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="range">量程对象</param>
        /// <returns>如果构建表格对象成功返回图层实例，否则返回NULL</returns>
        public override WapMeterRange Build(IDataReader reader, WapMeterRange range)
        {
            try
            {
                range.RangeId = reader.GetReaderValue<int>("RANGE_ID");
                range.RangeName = reader.GetReaderValue<string>("RANGE_NAME");
                range.RangeValue = reader.GetReaderValue<int>("RANGE_VALUE");
                range.RangeVolumes = reader.GetReaderValue<float>("RANGE_VOLUME");
                range.RangeState = reader.GetReaderValue<int>("RANGE_STATE");
                return range;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }



    }
}

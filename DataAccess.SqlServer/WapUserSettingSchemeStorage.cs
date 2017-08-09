using SH3H.SDK.DataAccess.Db;
using SH3H.SDK.Definition.Exceptions;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.WAP.Model;
using SH3H.WAP.Share;
using SH3H.SharpFrame.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.DataAccess.SqlServer
{
    /// <summary>
    /// 定义用户配置参数数据库操作
    /// </summary>
    public class WapUserSettingSchemeStorage : BaseAccess<WapUserSettingScheme>, IWapUserSettingSchemeStorage
    {
        public WapUserSettingSchemeStorage()
            : base(SH3H.SDK.Share.Consts.CONFIGURE_DATABASE_CONNECTION_STRING) { }

        /// <summary>
        /// 添加用户配置参数
        /// </summary>
        /// <param name="userSettingScheme">用户配置参数对象</param>
        /// <returns>新增后的用户配置参数对象</returns>
        public WapUserSettingScheme Insert(WapUserSettingScheme userSettingScheme)
        {
            try
            {

                string sqlText = @"INSERT INTO CNF_SCHEME(DATA_TYPE,
                                                            DEFAULT_VALUE,
                                                            MIN_VALUE,
                                                            MAX_VALUE,
                                                            PRECISION,
                                                            DATA_LENGTH,
                                                            WORD_CODE,
                                                            CONTROL_TYPE)
                                                     VALUES(@DATA_TYPE,
                                                            @DEFAULT_VALUE,
                                                            @MIN_VALUE,                                                                                           
                                                            @MAX_VALUE,
                                                            @PRECISION,
                                                            @DATA_LENGTH,
                                                            @WORD_CODE,
                                                            @CONTROL_TYPE)
                                                        SELECT @@IDENTITY;";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@DATA_TYPE", DbType.String, userSettingScheme.DataType);
                    Database.AddInParameter(cmd, "@DEFAULT_VALUE", DbType.String, userSettingScheme.DefaultValue);
                    Database.AddInParameter(cmd, "@MIN_VALUE", DbType.String, userSettingScheme.MinValue);
                    Database.AddInParameter(cmd, "@MAX_VALUE", DbType.String, userSettingScheme.MaxValue);
                    Database.AddInParameter(cmd, "@PRECISION", DbType.String, userSettingScheme.Precision);
                    Database.AddInParameter(cmd, "@DATA_LENGTH", DbType.Int32, userSettingScheme.DataLength);
                    Database.AddInParameter(cmd, "@WORD_CODE", DbType.String, userSettingScheme.WordCode);
                    Database.AddInParameter(cmd, "@CONTROL_TYPE", DbType.String, userSettingScheme.ControlType);
                    int id = ExecuteScalar<int>(cmd);
                    if (id > 0)
                    {
                        userSettingScheme.SchemeId = id;
                        return userSettingScheme;
                    }
                    return null;
                }

            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "新增用户配置参数对象失败");
            }
        }

        /// <summary>
        /// 删除用户配置参数
        /// </summary>
        /// <param name="schemeId">用户配置参数编号</param>
        /// <returns>是否删除成功，true成功，false失败</returns>
        public bool Delete(int schemeId)
        {
            try
            {
                string sqlText = @"DELETE FROM CNF_SCHEME WHERE SCHEME_ID=@SCHEME_ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@SCHEME_ID", DbType.Int32, schemeId);
                    if (ExecuteNonQuery(cmd) > 0)
                        return true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "删除用户配置参数对象失败");
            }
        }

        /// <summary>
        /// 根据参数编号修改用户配置参数
        /// </summary>
        /// <param name="userSettingScheme">用户配置参数对象</param>
        /// <returns>更新成功，返回更新后的对象;更新失败，返回空</returns>
        public WapUserSettingScheme Update(WapUserSettingScheme userSettingScheme)
        {
            try
            {
                string sqlText = @"UPDATE CNF_SCHEME
                                   SET DATA_TYPE=@DATA_TYPE,
                                       DEFAULT_VALUE=@DEFAULT_VALUE,
                                       MIN_VALUE=@MIN_VALUE,
                                       MAX_VALUE=@MAX_VALUE,
                                       PRECISION=@PRECISION,
                                       DATA_LENGTH=@DATA_LENGTH,
                                       WORD_CODE=@WORD_CODE,
                                       CONTROL_TYPE=@CONTROL_TYPE
                                 WHERE SCHEME_ID=@SCHEME_ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@DATA_TYPE", DbType.String, userSettingScheme.DataType);
                    Database.AddInParameter(cmd, "@DEFAULT_VALUE", DbType.String, userSettingScheme.DefaultValue);
                    Database.AddInParameter(cmd, "@MIN_VALUE", DbType.String, userSettingScheme.MinValue);
                    Database.AddInParameter(cmd, "@MAX_VALUE", DbType.String, userSettingScheme.MaxValue);
                    Database.AddInParameter(cmd, "@PRECISION", DbType.String, userSettingScheme.Precision);
                    Database.AddInParameter(cmd, "@DATA_LENGTH", DbType.Int32, userSettingScheme.DataLength);
                    Database.AddInParameter(cmd, "@SCHEME_ID", DbType.Int32, userSettingScheme.SchemeId);
                    Database.AddInParameter(cmd, "@WORD_CODE", DbType.String, userSettingScheme.WordCode);
                    Database.AddInParameter(cmd, "@CONTROL_TYPE", DbType.String, userSettingScheme.ControlType);
                    if (ExecuteNonQuery(cmd) > 0)
                        return userSettingScheme;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "修改用户配置参数对象失败");
            }
        }

        /// <summary>
        /// 根据参数编号获取用户配置参数
        /// </summary>
        /// <param name="schemeId">参数编号</param>
        /// <returns>用户配置参数对象</returns>
        public WapUserSettingScheme Select(int schemeId)
        {
            try
            {
                string sqlText = @"SELECT SCHEME_ID,
                                          DATA_TYPE,
                                          DEFAULT_VALUE,
                                          MIN_VALUE,
                                          MAX_VALUE,
                                          PRECISION,
                                          DATA_LENGTH,
                                          WORD_CODE,
                                          CONTROL_TYPE
                                    FROM CNF_SCHEME                             
                                   WHERE SCHEME_ID=@SCHEME_ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@SCHEME_ID", DbType.Int32, schemeId);
                    return SelectSingle(cmd);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "根据参数编号获取用户配置参数对象失败");
            }
        }

        /// <summary>
        /// 根据<see cref="IDataReader"/>数据读取器构建对象实体
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="userSettingScheme">文件描述器对象</param>
        /// <returns>如果构建对象成功返回对象实例，否则返回NULL</returns>
        public override WapUserSettingScheme Build(IDataReader reader, WapUserSettingScheme userSettingScheme)
        {
            try
            {
                userSettingScheme.SchemeId = reader.GetReaderValue<Int32>("SCHEME_ID");
                userSettingScheme.DataType = reader.GetReaderValue<string>("DATA_TYPE");
                userSettingScheme.DefaultValue = reader.GetReaderValue<string>("DEFAULT_VALUE", null, true);
                userSettingScheme.MinValue = reader.GetReaderValue<string>("MIN_VALUE", null, true);
                userSettingScheme.MaxValue = reader.GetReaderValue<string>("MAX_VALUE", null, true);
                userSettingScheme.Precision = reader.GetReaderValue<string>("PRECISION", null, true);
                userSettingScheme.DataLength = reader.GetReaderValue<Int32>("DATA_LENGTH");
                userSettingScheme.WordCode = reader.GetReaderValue<string>("WORD_CODE", null, true);
                userSettingScheme.ControlType = reader.GetReaderValue<string>("CONTROL_TYPE", null, true);
                return userSettingScheme;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }
    }
}

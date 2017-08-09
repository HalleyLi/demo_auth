using Microsoft.Practices.EnterpriseLibrary.Data;
using SH3H.SDK.DataAccess.Db;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.WAP.Model;
using SH3H.SharpFrame.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Share;

namespace SH3H.WAP.DataAccess.SqlServer
{
    /// <summary>
    /// 定义用户配置数据库操作
    /// </summary>
    public class WapUserSettingStorage : BaseAccess<WapUserSetting>, IWapUserSettingStorage
    {
        public WapUserSettingStorage()
            : base(SH3H.SDK.Share.Consts.CONFIGURE_DATABASE_CONNECTION_STRING) { }

        /// <summary>
        /// 添加用户配置
        /// </summary>
        /// <returns>新增后的用户配置对象</returns>
        public WapUserSetting Insert(WapUserSetting userSetting)
        {
            try
            {

                string sqlText = @"INSERT INTO CNF_USER_SETTING(USER_ID,
                                                            IS_GROUP,
                                                            USER_SETTING_CODE,
                                                            USER_SETTING_VALUE,
                                                            APP_IDENTITY,
                                                            USER_SETTING_TYPE,
                                                            USER_SETTING_TEXT,
                                                            SCHEME_ID,
                                                            CREATOR_ID,
                                                            CREATE_TIME,
                                                            MODIFIER_ID,
                                                            MODITY_TIME,
                                                            REMARK)
                                                        VALUES(@USER_ID,
                                                               @IS_GROUP,
                                                               @USER_SETTING_CODE,
                                                               @USER_SETTING_VALUE,                                                                                           
                                                               @APP_IDENTITY,
                                                               @USER_SETTING_TYPE,
                                                               @USER_SETTING_TEXT,
                                                               @SCHEME_ID,
                                                               @CREATOR_ID,  
                                                               getdate(),
                                                               @MODIFIER_ID,
                                                               getdate(),
                                                               @REMARK)
                                                        SELECT @@IDENTITY;";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@USER_ID", DbType.Int32, userSetting.UserId);
                    Database.AddInParameter(cmd, "@USER_SETTING_CODE", DbType.String, userSetting.UserSettingCode);
                    Database.AddInParameter(cmd, "@USER_SETTING_VALUE", DbType.String, userSetting.UserSettingValue);
                    Database.AddInParameter(cmd, "@APP_IDENTITY", DbType.String, userSetting.AppIdentity);
                    Database.AddInParameter(cmd, "@USER_SETTING_TYPE", DbType.String, userSetting.UserSettingType);
                    Database.AddInParameter(cmd, "@USER_SETTING_TEXT", DbType.String, userSetting.UserSettingText);
                    Database.AddInParameter(cmd, "@SCHEME_ID", DbType.Int32, userSetting.SchemeId);
                    Database.AddInParameter(cmd, "@CREATOR_ID", DbType.Int32, userSetting.CreatorId);
                    Database.AddInParameter(cmd, "@MODIFIER_ID", DbType.Int32, userSetting.CreatorId);
                    Database.AddInParameter(cmd, "@REMARK", DbType.String, userSetting.Remark);
                    Database.AddInParameter(cmd, "@IS_GROUP", DbType.String, bool.FalseString);
                    int id = ExecuteScalar<int>(cmd);
                    if (id > 0)
                    {
                        userSetting.UserSettingId = id;
                        return userSetting;
                    }
                    return null;
                }

            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "新增用户配置对象失败");
            }
        }

        /// <summary>
        /// 添加用户配置组别
        /// </summary>
        /// <returns>新增后的用户配置组别对象</returns>
        public WapUserSetting InsertGroup(WapUserSetting userSetting)
        {
            try
            {
                string sqlText = @"INSERT INTO CNF_USER_SETTING(USER_ID,
                                                            IS_GROUP,
                                                            USER_SETTING_CODE,
                                                            USER_SETTING_VALUE,
                                                            APP_IDENTITY,
                                                            USER_SETTING_TYPE,
                                                            USER_SETTING_TEXT,
                                                            SCHEME_ID,
                                                            CREATOR_ID,
                                                            CREATE_TIME,
                                                            MODIFIER_ID,
                                                            MODITY_TIME,
                                                            REMARK)
                                                        VALUES(@USER_ID,
                                                               @IS_GROUP,
                                                               @USER_SETTING_CODE,
                                                               @USER_SETTING_VALUE,                                                                                           
                                                               @APP_IDENTITY,
                                                               @USER_SETTING_TYPE,
                                                               @USER_SETTING_TEXT,
                                                               @SCHEME_ID,
                                                               @CREATOR_ID,  
                                                               getdate(),
                                                               @MODIFIER_ID,
                                                               getdate(),
                                                               @REMARK)
                                                        SELECT @@IDENTITY;";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@USER_ID", DbType.Int32, userSetting.UserId);
                    Database.AddInParameter(cmd, "@USER_SETTING_CODE", DbType.String, String.Empty);
                    Database.AddInParameter(cmd, "@USER_SETTING_VALUE", DbType.String, String.Empty);
                    Database.AddInParameter(cmd, "@APP_IDENTITY", DbType.String, userSetting.AppIdentity);
                    Database.AddInParameter(cmd, "@USER_SETTING_TYPE", DbType.String, userSetting.UserSettingType);
                    Database.AddInParameter(cmd, "@USER_SETTING_TEXT", DbType.String, userSetting.UserSettingText);
                    Database.AddInParameter(cmd, "@SCHEME_ID", DbType.Int32, 0);
                    Database.AddInParameter(cmd, "@CREATOR_ID", DbType.Int32, userSetting.CreatorId);
                    Database.AddInParameter(cmd, "@MODIFIER_ID", DbType.Int32, userSetting.CreatorId);
                    Database.AddInParameter(cmd, "@REMARK", DbType.String, userSetting.Remark);
                    Database.AddInParameter(cmd, "@IS_GROUP", DbType.String, bool.TrueString);
                    int id = ExecuteScalar<int>(cmd);
                    if (id > 0)
                    {
                        userSetting.UserSettingId = id;
                        return userSetting;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "新增用户配置对象失败");
            }
        }

        /// <summary>
        /// 删除用户配置
        /// </summary>
        /// <param name="userSettingId">用户配置编号</param>
        /// <returns>是否删除成功</returns>
        public bool Delete(int userSettingId)
        {
            try
            {
                string sqlText = @"DELETE FROM CNF_USER_SETTING
                                   WHERE　USER_SETTING_ID=@USER_SETTING_ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@USER_SETTING_ID", DbType.Int32, userSettingId);
                    if (ExecuteNonQuery(cmd) > 0) { return true; }
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "删除用户配置对象失败");
            }
        }

        /// <summary>
        /// 根据用户配置编号修改用户配置
        /// </summary>
        /// <param name="userSetting">用户配置对象</param>
        /// <returns>修改后的用户配置对象</returns>
        public WapUserSetting Update(WapUserSetting userSetting)
        {
            try
            {

                string sqlText = @"UPDATE CNF_USER_SETTING
                                        SET 
                                            USER_SETTING_CODE=@USER_SETTING_CODE,
                                            USER_SETTING_VALUE=@USER_SETTING_VALUE,
                                            USER_SETTING_TYPE=@USER_SETTING_TYPE,
                                            USER_SETTING_TEXT=@USER_SETTING_TEXT,
                                            SCHEME_ID=@SCHEME_ID,
                                            REMARK=@REMARK,
                                            MODIFIER_ID=@MODIFIER_ID,
                                            MODITY_TIME=getdate()
                                      WHERE　USER_SETTING_ID=@USER_SETTING_ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@USER_SETTING_ID", DbType.Int32, userSetting.UserSettingId);
                    Database.AddInParameter(cmd, "@USER_SETTING_CODE", DbType.String, userSetting.UserSettingCode);
                    Database.AddInParameter(cmd, "@USER_SETTING_VALUE", DbType.String, userSetting.UserSettingValue);
                    Database.AddInParameter(cmd, "@USER_SETTING_TYPE", DbType.String, userSetting.UserSettingType);
                    Database.AddInParameter(cmd, "@USER_SETTING_TEXT", DbType.String, userSetting.UserSettingText);
                    Database.AddInParameter(cmd, "@SCHEME_ID", DbType.Int32, userSetting.SchemeId);
                    Database.AddInParameter(cmd, "@REMARK", DbType.String, userSetting.Remark);
                    Database.AddInParameter(cmd, "@MODIFIER_ID", DbType.Int32, userSetting.ModifierId);
                    if (ExecuteNonQuery(cmd) > 0) { return userSetting; }
                    return null;
                }

            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "修改用户配置对象失败");
            }
        }

        /// <summary>
        /// 根据用户配置编号修改用户配置组别
        /// </summary>
        /// <param name="userSetting">用户配置对象</param>
        /// <returns>修改后的用户配置对象组别</returns>
        public WapUserSetting UpdateGroup(WapUserSetting userSetting)
        {
            try
            {

                string sqlText = @"UPDATE CNF_USER_SETTING
                                        SET 
                                            USER_SETTING_TYPE=@USER_SETTING_TYPE,
                                            USER_SETTING_TEXT=@USER_SETTING_TEXT,
                                            REMARK=@REMARK,
                                            MODIFIER_ID=@MODIFIER_ID,
                                            MODITY_TIME=getdate()
                                      WHERE　USER_SETTING_ID=@USER_SETTING_ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@USER_SETTING_ID", DbType.Int32, userSetting.UserSettingId);
                    Database.AddInParameter(cmd, "@USER_SETTING_TYPE", DbType.String, userSetting.UserSettingType);
                    Database.AddInParameter(cmd, "@USER_SETTING_TEXT", DbType.String, userSetting.UserSettingText);
                    Database.AddInParameter(cmd, "@REMARK", DbType.String, userSetting.Remark);
                    Database.AddInParameter(cmd, "@MODIFIER_ID", DbType.Int32, userSetting.ModifierId);
                    if (ExecuteNonQuery(cmd) > 0) { return userSetting; }
                    return null;
                }

            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "修改用户配置对象组别失败");
            }
        }

        /// <summary>
        /// 根据用户编号和配置编码修改用户配置
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="code">用户配置编码</param>
        /// <param name="userSetting">用户配置对象</param>
        /// <returns>更新后的用户配置对象</returns>
        public WapUserSetting UpdateByUserIdAndCode(int userId, string code, WapUserSetting userSetting)
        {
            try
            {

                string sqlText = @"UPDATE CNF_USER_SETTING
                                        SET 
                                            USER_SETTING_VALUE=@USER_SETTING_VALUE,
                                            MODIFIER_ID=@MODIFIER_ID,
                                            MODITY_TIME=getdate()
                                      WHERE　USER_ID=@USER_ID AND USER_SETTING_CODE=@USER_SETTING_CODE";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@USER_ID", DbType.Int32, userId);
                    Database.AddInParameter(cmd, "@USER_SETTING_CODE", DbType.String, code);
                    Database.AddInParameter(cmd, "@USER_SETTING_VALUE", DbType.String, userSetting.UserSettingValue);
                    Database.AddInParameter(cmd, "@MODIFIER_ID", DbType.Int32, userSetting.ModifierId);
                    if (ExecuteNonQuery(cmd) > 0) { return userSetting; }
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "根据用户编号和配置编码修改用户配置对象失败");
            }
        }

        /// <summary>
        ///获取所有用户配置
        /// </summary>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSetting> SelectAll()
        {
            try
            {
                string sqlText = @"SELECT USER_SETTING_ID,
                                          USER_ID,
                                          USER_SETTING_CODE,
                                          USER_SETTING_VALUE,
                                          APP_IDENTITY,
                                          USER_SETTING_TYPE,
                                          USER_SETTING_TEXT,
                                          SCHEME_ID,
                                          CREATOR_ID,
                                          CREATE_TIME,
                                          MODIFIER_ID,
                                          MODITY_TIME,
                                          REMARK,
                                           IS_GROUP
                                   FROM CNF_USER_SETTING";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    return SelectList(cmd);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "获取所有用户配置对象失败");
            }
        }

        /// <summary>
        /// 根据配置编号获取用户配置参数
        /// </summary>
        /// <param name="userSettingId">用户配置对象编号</param>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSetting> SelectByUserSettingId(int userSettingId)
        {
            try
            {
                string sqlText = @" SELECT USER_SETTING_ID,
                                           USER_ID,
                                           USER_SETTING_CODE,
                                           USER_SETTING_VALUE,
                                           APP_IDENTITY,
                                           USER_SETTING_TYPE,
                                           USER_SETTING_TEXT,
                                           SCHEME_ID,
                                           CREATOR_ID,
                                           CREATE_TIME,
                                           MODIFIER_ID,
                                           MODITY_TIME,
                                           REMARK,
                                           IS_GROUP
                                        FROM CNF_USER_SETTING
                                   WHERE USER_SETTING_ID=@USER_SETTING_ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@USER_SETTING_ID", DbType.Int32, userSettingId);
                    return SelectList(cmd);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, " 根据配置编号获取指定用户配置对象失败");
            }
        }

        /// <summary>
        /// 根据配置编号获取用户配置参数
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSetting> SelectByUserId(int userId)
        {
            try
            {
                string sqlText = @" SELECT USER_SETTING_ID,
                                           USER_ID,
                                           USER_SETTING_CODE,
                                           USER_SETTING_VALUE,
                                           APP_IDENTITY,
                                           USER_SETTING_TYPE,
                                           USER_SETTING_TEXT,
                                           SCHEME_ID,
                                           CREATOR_ID,
                                           CREATE_TIME,
                                           MODIFIER_ID,
                                           MODITY_TIME,
                                           REMARK,
                                           IS_GROUP
                                       FROM CNF_USER_SETTING
                                   WHERE USER_ID=@USER_ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@USER_ID", DbType.Int32, userId);
                    return SelectList(cmd);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "根据配置编号获取指定用户配置失败");
            }
        }

        /// <summary>
        /// 根据用户编号和配置编码查询用户配置
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="code">配置编码</param>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSetting> SelectByUserIdAndCode(int userId, string code)
        {
            try
            {
                string sqlText = @" SELECT USER_SETTING_ID,
                                           USER_ID,
                                           USER_SETTING_CODE,
                                           USER_SETTING_VALUE,
                                           APP_IDENTITY,
                                           USER_SETTING_TYPE,
                                           USER_SETTING_TEXT,
                                           SCHEME_ID,
                                           CREATOR_ID,
                                           CREATE_TIME,
                                           MODIFIER_ID,
                                           MODITY_TIME,
                                           REMARK,
                                           IS_GROUP
                                       FROM CNF_USER_SETTING
                                   WHERE USER_ID=@USER_ID AND USER_SETTING_CODE=@USER_SETTING_CODE";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@USER_ID", DbType.Int32, userId);
                    Database.AddInParameter(cmd, "@USER_SETTING_CODE", DbType.String, code);
                    return SelectList(cmd);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "根据用户编号和配置编码获取指定用户配置对象失败");
            }
        }

        /// <summary>
        /// 根据用户编号和应用标识查询用户配置
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSetting> SelectByUserIdAndAppIdentity(int userId, string appIdentity)
        {
            try
            {
                string sqlText = @" SELECT USER_SETTING_ID,
                                           USER_ID,
                                           USER_SETTING_CODE,
                                           USER_SETTING_VALUE,
                                           APP_IDENTITY,
                                           USER_SETTING_TYPE,
                                           USER_SETTING_TEXT,
                                           SCHEME_ID,
                                           CREATOR_ID,
                                           CREATE_TIME,
                                           MODIFIER_ID,
                                           MODITY_TIME,
                                           REMARK,
                                           IS_GROUP
                                        FROM CNF_USER_SETTING
                                   WHERE USER_ID=@USER_ID AND APP_IDENTITY=@APP_IDENTITY";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@USER_ID", DbType.Int32, userId);
                    Database.AddInParameter(cmd, "@APP_IDENTITY", DbType.String, appIdentity);
                    return SelectList(cmd);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "根据用户编号和应用标识获取指定用户配置对象失败");
            }
        }

        /// <summary>
        /// 根据应用标识查询用户配置
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSetting> SelectByAppIdentity(string appIdentity)
        {
            try
            {
                string sqlText = @" SELECT USER_SETTING_ID,
                                           USER_ID,
                                           USER_SETTING_CODE,
                                           USER_SETTING_VALUE,
                                           APP_IDENTITY,
                                           USER_SETTING_TYPE,
                                           USER_SETTING_TEXT,
                                           SCHEME_ID,
                                           CREATOR_ID,
                                           CREATE_TIME,
                                           MODIFIER_ID,
                                           MODITY_TIME,
                                           REMARK,
                                           IS_GROUP
                                        FROM CNF_USER_SETTING
                                   WHERE APP_IDENTITY=@APP_IDENTITY";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@APP_IDENTITY", DbType.String, appIdentity);
                    return SelectList(cmd);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "根据应用标识获取指定用户配置对象失败");
            }
        }

        /// <summary>
        /// 根据<see cref="IDataReader"/>数据读取器构建对象实体
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="userSetting">文件描述器对象</param>
        /// <returns>如果构建对象成功返回对象实例，否则返回NULL</returns>
        public override WapUserSetting Build(IDataReader reader, WapUserSetting userSetting)
        {
            try
            {
                userSetting.UserSettingId = reader.GetReaderValue<Int32>("USER_SETTING_ID");
                userSetting.UserId = reader.GetReaderValue<Int32>("USER_ID");
                userSetting.IsGroup = reader.GetReaderValue<bool>("IS_GROUP");
                userSetting.UserSettingCode = reader.GetReaderValue<string>("USER_SETTING_CODE");
                userSetting.UserSettingValue = reader.GetReaderValue<string>("USER_SETTING_VALUE");
                userSetting.AppIdentity = reader.GetReaderValue<string>("APP_IDENTITY");
                userSetting.UserSettingType = reader.GetReaderValue<string>("USER_SETTING_TYPE");
                userSetting.UserSettingText = reader.GetReaderValue<string>("USER_SETTING_TEXT");
                userSetting.SchemeId = reader.GetReaderValue<int>("SCHEME_ID");
                userSetting.CreatorId = reader.GetReaderValue<Int32>("CREATOR_ID");
                userSetting.CreateTime = reader.GetReaderValue<DateTime>("CREATE_TIME");
                userSetting.ModifierId = reader.GetReaderValue<Int32>("MODIFIER_ID");
                userSetting.ModifyTime = reader.GetReaderValue<DateTime>("MODITY_TIME");
                userSetting.Remark = reader.GetReaderValue<string>("REMARK", null, true);
                return userSetting;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }
    }
}

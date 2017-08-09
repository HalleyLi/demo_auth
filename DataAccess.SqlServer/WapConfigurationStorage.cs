using SH3H.SDK.DataAccess.Db;
using SH3H.SDK.Definition.Exceptions;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.SDK.Share;
using SH3H.SharpFrame;
using SH3H.SharpFrame.Data;
using SH3H.WAP.Model;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.DataAccess.SqlServer
{
    /// <summary>
    /// 系统配置表的操作
    /// </summary>
    public class WapConfigurationStorage : BaseAccess<WapConfiguration>, IWapConfigurationStorage
    {
        public WapConfigurationStorage()
            : base(SH3H.SDK.Share.Consts.CONFIGURE_DATABASE_CONNECTION_STRING) { }

        public bool CreateSchema()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 添加系统配置
        /// </summary>
        /// <param name="configuration">系统配置对象</param>
        /// <param name="trans"></param>
        /// <returns>口径对象</returns>
        public WapConfiguration Insert(WapConfiguration configuration)
        {
            try
            {
                if (GetConfigByCode(configuration) != null)
                {
                    throw new WapException(StateCode.CODE_ARGUMENT_DATA_REPEAT, "数据表中已有该code的配置项");
                }
                else
                {
                    string sqlText = @"INSERT INTO CNF_CONFIGURATION(CONFIG_NAME,CONFIG_CODE,CONFIG_TYPE,CONFIG_VALUE,CONFIG_DEFAULT,CONFIG_APP,CONFIG_GROUP,CONFIG_STATE,REMARK,SCHEME_ID)
                                  VALUES(@CONFIG_NAME,@CONFIG_CODE,@CONFIG_TYPE,@CONFIG_VALUE,@CONFIG_DEFAULT,@CONFIG_APP,@CONFIG_GROUP,@CONFIG_STATE,@REMARK,@SCHEME_ID)
                                   SELECT @@IDENTITY;";
                    using (var cmd = Database.GetSqlStringCommand(sqlText))
                    {
                        Database.AddInParameter(cmd, "@CONFIG_NAME", DbType.String, configuration.ConfigName);
                        Database.AddInParameter(cmd, "@CONFIG_CODE", DbType.String, configuration.ConfigCode);
                        Database.AddInParameter(cmd, "@CONFIG_TYPE", DbType.Int32, configuration.ConfigType);
                        Database.AddInParameter(cmd, "@CONFIG_VALUE", DbType.String, configuration.ConfigValue);
                        Database.AddInParameter(cmd, "@CONFIG_DEFAULT", DbType.String, configuration.ConfigDefault);
                        Database.AddInParameter(cmd, "@CONFIG_APP", DbType.String, configuration.ConfigApp);
                        Database.AddInParameter(cmd, "@CONFIG_GROUP", DbType.String, configuration.ConfigGroup);
                        Database.AddInParameter(cmd, "@CONFIG_STATE", DbType.Int32, configuration.ConfigState);
                        Database.AddInParameter(cmd, "@REMARK", DbType.String, configuration.Remark);
                        Database.AddInParameter(cmd, "@SCHEME_ID", DbType.Int32, configuration.SchemeId);
                        int id = ExecuteScalar<int>(cmd);
                        configuration.Id = id;
                        return configuration;
                    }
                }

            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "新增配置对象失败");
            }

        }

        /// <summary>
        /// 判断数据库是否已有该标识的配置信息
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public WapConfiguration GetConfigByCode(WapConfiguration config)
        {
            try
            {
                string commandText = @"SELECT * FROM CNF_CONFIGURATION WHERE CONFIG_CODE=@CONFIG_CODE AND CONFIG_APP=@CONFIG_APP";
                using (var command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@CONFIG_CODE", DbType.String, config.ConfigCode);
                    Database.AddInParameter(command, "@CONFIG_APP", DbType.String, config.ConfigApp);
                    var list = SelectList(command);
                    return (list == null || list.Count() == 0) ? null : list.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        /// <summary>
        /// 删除系统配置
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="trans"></param>
        /// <returns>是否删除成功</returns>
        public bool Delete(int id)
        {
            try
            {
                string sqlText = @"DELETE FROM CNF_CONFIGURATION
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
        /// 修改系统配置
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="configuration">系统配置对象</param>
        /// <param name="trans"></param>
        /// <returns>返回时否修改成功</returns>
        public bool Update(int id, WapConfiguration configuration)
        {
            try
            {
                WapConfiguration config = GetConfigByCode(configuration);
                if (config != null && config.Id != id)
                {
                    throw new WapException(StateCode.CODE_ARGUMENT_DATA_REPEAT, "数据表中已有该code的配置项");
                }
                string sqlText = @"UPDATE CNF_CONFIGURATION 
                                         SET CONFIG_NAME=@CONFIG_NAME,
                                         CONFIG_CODE=@CONFIG_CODE,
                                         CONFIG_TYPE=@CONFIG_TYPE,
                                         CONFIG_VALUE=@CONFIG_VALUE,
                                         CONFIG_DEFAULT=@CONFIG_DEFAULT,
                                         CONFIG_APP=@CONFIG_APP,
                                         CONFIG_GROUP=@CONFIG_GROUP,
                                         CONFIG_STATE=@CONFIG_STATE,
                                         REMARK=@REMARK,
                                         SCHEME_ID=@SCHEME_ID 
                                      WHERE ID=@ID";
                using (DbCommand cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@CONFIG_NAME", DbType.String, configuration.ConfigName);
                    Database.AddInParameter(cmd, "@CONFIG_CODE", DbType.String, configuration.ConfigCode);
                    Database.AddInParameter(cmd, "@CONFIG_TYPE", DbType.Int32, configuration.ConfigType);
                    Database.AddInParameter(cmd, "@CONFIG_VALUE", DbType.String, configuration.ConfigValue);
                    Database.AddInParameter(cmd, "@CONFIG_DEFAULT", DbType.String, configuration.ConfigDefault);
                    Database.AddInParameter(cmd, "@CONFIG_APP", DbType.String, configuration.ConfigApp);
                    Database.AddInParameter(cmd, "@CONFIG_GROUP", DbType.String, configuration.ConfigGroup);
                    Database.AddInParameter(cmd, "@CONFIG_STATE", DbType.Int32, configuration.ConfigState);
                    Database.AddInParameter(cmd, "@REMARK", DbType.String, configuration.Remark);
                    Database.AddInParameter(cmd, "@SCHEME_ID", DbType.Int32, configuration.SchemeId);
                    Database.AddInParameter(cmd, "@ID", DbType.Int32, id);
                    if (ExecuteNonQuery(cmd) > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "修改配置对象失败");
            }
        }

        /// <summary>
        /// 通过编号查询系统配置
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>系统配置对象</returns>
        public WapConfiguration SelectById(int id)
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_CONFIGURATION
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
        /// 通过配置编码和分组获取系统配置列表
        /// </summary>
        /// <param name="appCode">配置编码</param>
        /// <param name="group">配置分组</param>
        /// <returns>系统配置对象</returns>
        public IEnumerable<WapConfiguration> SelectByAppAndGroup(string appCode, string group)
        {
            try
            {
                string whereString = "";
                if (appCode != null)
                    whereString += " CONFIG_APP=@CONFIG_APP AND";
                if (!string.IsNullOrEmpty(group))
                    whereString += " CONFIG_GROUP=@CONFIG_GROUP AND";
                if (Guard.IsNotNullOrEmpty(whereString))
                {
                    whereString = " AND  " + whereString.Remove(whereString.Length - 4);
                }

                string sqlText = @"SELECT * FROM CNF_CONFIGURATION WHERE CONFIG_TYPE=0 " + whereString;
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    if (appCode != null)
                    {
                        Database.AddInParameter(cmd, "@CONFIG_APP", DbType.String, appCode);
                    }
                    if (!string.IsNullOrEmpty(group))
                    {
                        Database.AddInParameter(cmd, "@CONFIG_GROUP", DbType.String, group);
                    }
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
        /// 获取指定应用下的配置
        /// </summary>
        /// <param name="appCode">配置编码</param>
        /// <returns>系统配置对象</returns>
        public IEnumerable<WapConfiguration> GetConfigsByAppCode(string appCode)
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_CONFIGURATION
                                   WHERE CONFIG_APP=@CONFIG_APP";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@CONFIG_APP", DbType.String, appCode);
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
        /// 通过配置编码获取分组
        /// </summary>
        /// <param name="appCode">应用编码</param>
        /// <returns>系统配置对象</returns>
        public IEnumerable<WapConfiguration> SelectGroupsByApp(string appCode)
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_CONFIGURATION
                                   WHERE CONFIG_APP=@CONFIG_APP AND CONFIG_TYPE=1";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@CONFIG_APP", DbType.String, appCode);
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
        /// 根据配置code更新配置组code
        /// </summary>
        /// <param name="configCode">配置标识</param>
        /// <param name="group">配置组标识</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateConfigParent(string configCode, string group)
        {
            try
            {
                string sqlText = @"UPDATE CNF_CONFIGURATION 
                                        SET CONFIG_GROUP = @CONFIG_GROUP
                                      WHERE CONFIG_CODE=@CONFIG_CODE";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@CONFIG_CODE", DbType.String, configCode);
                    Database.AddInParameter(cmd, "@CONFIG_GROUP", DbType.String, group);
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
        /// 修改配置状态
        /// </summary>
        /// <param name="id">配置ID</param>
        /// <param name="active">激活状态</param>
        /// <returns>是否修改成功</returns>
        public bool UpdateConfigState(int id, bool active)
        {
            try
            {
                string commandText = @"UPDATE CNF_CONFIGURATION 
                                       SET  CONFIG_STATE=@CONFIG_STATE
                                      where ID = @ID";
                using (var command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@ID", DbType.Int32, id);
                    Database.AddInParameter(command, "@CONFIG_STATE", DbType.Boolean, active);
                    return ExecuteNonQuery(command) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }

        /// <summary>
        /// 获取所有系统配置
        /// </summary>
        /// <returns>系统配置对象列表</returns>
        public IEnumerable<WapConfiguration> SelectAll()
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_CONFIGURATION ";
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

        public override WapConfiguration Build(IDataReader reader, WapConfiguration configuration)
        {
            configuration.Id = reader.GetReaderValue<int>("ID");
            configuration.ConfigName = reader.GetReaderValue<string>("CONFIG_NAME");
            configuration.ConfigCode = reader.GetReaderValue<string>("CONFIG_CODE");
            configuration.ConfigType = reader.GetReaderValue<int>("CONFIG_TYPE");
            configuration.ConfigValue = reader.GetReaderValue<string>("CONFIG_VALUE", null, true);
            configuration.ConfigDefault = reader.GetReaderValue<string>("CONFIG_DEFAULT", null, true);
            configuration.ConfigApp = reader.GetReaderValue<string>("CONFIG_APP");
            configuration.ConfigGroup = reader.GetReaderValue<string>("CONFIG_GROUP", null, true);
            configuration.ConfigState = reader.GetReaderValue<int>("CONFIG_STATE");
            configuration.Remark = reader.GetReaderValue<string>("REMARK", null, true);
            configuration.SchemeId = reader.GetReaderValue<int>("SCHEME_ID");
            return base.Build(reader, configuration);
        }
    }
}

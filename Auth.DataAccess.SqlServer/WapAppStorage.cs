using SH3H.SDK.DataAccess.Db;
using SH3H.SDK.Definition.Exceptions;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.SDK.Share;
using SH3H.SharpFrame.Data;
using SH3H.WAP.Auth.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess.SqlServer
{
    /// <summary>
    /// 定义统一权限应用程序SqlServer数据库访问对象
    /// </summary>
    public class WapAppStorage : BaseAccess<WapApp>, IWapAppStorage
    {
        /// <summary>
        /// 构造函数
        /// </summary>        
        public WapAppStorage()
            : base(Consts.AUTH_DATABASE_CONNECTION_STRING) { }

        #region WapAppStorage        

        /// <summary>
        /// 添加应用
        /// </summary>
        /// <param name="app">应用对象</param>
        /// <returns>添加成功返回添加的对象,失败返回Null</returns>
        public WapApp AddApp(WapApp app)
        {
            try
            {
                string commandText = @"INSERT INTO AUTH_APP(APP_KEY,APP_IDENTITY,APP_NAME,APP_PYCODE,APP_ACTIVE,APP_SORTSN,APP_COMMENT,APP_TYPE,EXTEND,APP_ICON,APP_DEFAULT_INDEX)
                                                     VALUES(@APP_KEY,@APP_IDENTITY,@APP_NAME,@APP_PYCODE,@APP_ACTIVE,@APP_SORTSN,
                                    @APP_COMMENT,@APP_TYPE,@EXTEND,@APP_ICON,@APP_DEFAULT_INDEX)";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@APP_KEY", DbType.String, app.AppKey);
                    Database.AddInParameter(command, "@APP_IDENTITY", DbType.String, app.AppIdentity);
                    Database.AddInParameter(command, "@APP_NAME", DbType.String, app.AppName);
                    Database.AddInParameter(command, "@APP_PYCODE", DbType.String, app.PyCode);
                    Database.AddInParameter(command, "@APP_ACTIVE", DbType.Boolean, app.Active);
                    Database.AddInParameter(command, "@APP_SORTSN", DbType.Int32, app.SortSn);
                    Database.AddInParameter(command, "@APP_COMMENT", DbType.String, app.Comment);
                    Database.AddInParameter(command, "@APP_TYPE", DbType.Int32, app.Type);
                    Database.AddInParameter(command, "@EXTEND", DbType.String, app.Extend);
                    Database.AddInParameter(command, "@APP_ICON", DbType.String, app.Icon);
                    Database.AddInParameter(command, "@APP_DEFAULT_INDEX", DbType.String, app.DefaultIndex);
                    var ret = ExecuteNonQuery(command);
                    return ret > 0 ? app : null;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "插入应用失败");
            }            
        }

        /// <summary>
        /// 修改应用信息
        /// </summary>
        /// <param name="appKey">应用Key</param>
        /// <param name="app">应用对象</param>
        /// <returns>成功或失败</returns>
        public WapApp UpdateApp(string appKey, WapApp app)
        {
            try
            {
                string commandText = @"UPDATE AUTH_APP 
                                       SET  APP_IDENTITY=@APP_IDENTITY,
                                            APP_NAME =@APP_NAME,
                                            APP_ACTIVE=@APP_ACTIVE,
                                            APP_SORTSN=@APP_SORTSN,
                                            APP_COMMENT=@APP_COMMENT,
                                            APP_TYPE=@APP_TYPE,
                                            EXTEND=@EXTEND,
                                            APP_ICON=@APP_ICON,
                                            APP_DEFAULT_INDEX=@APP_DEFAULT_INDEX

                                      where APP_KEY = @APP_KEY";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@APP_IDENTITY", DbType.String, app.AppIdentity);
                    Database.AddInParameter(command, "@APP_NAME", DbType.String, app.AppName);
                    Database.AddInParameter(command, "@APP_ACTIVE", DbType.Boolean, app.Active);
                    Database.AddInParameter(command, "@APP_SORTSN", DbType.Int32, app.SortSn);
                    Database.AddInParameter(command, "@APP_COMMENT", DbType.String, app.Comment);
                    Database.AddInParameter(command, "@APP_TYPE", DbType.Int32, app.Type);
                    Database.AddInParameter(command, "@EXTEND", DbType.String, app.Extend);
                    Database.AddInParameter(command, "@APP_KEY", DbType.String, appKey);
                    Database.AddInParameter(command, "@APP_ICON", DbType.String, app.Icon);
                    Database.AddInParameter(command, "@APP_DEFAULT_INDEX", DbType.String, app.DefaultIndex);
                    int line = ExecuteNonQuery(command);
                    if (line <= 0)
                    {
                        throw new WapException(SH3H.WAP.Share.StateCode.CODE_APP_INEXIST, "应用标识不存在");
                    }
                }

                string commandText_select = @"SELECT * FROM AUTH_APP WHERE APP_KEY = @APP_KEY;";
                using (DbCommand command_select = Database.GetSqlStringCommand(commandText_select))
                {
                    Database.AddInParameter(command_select, "@APP_KEY", DbType.String, appKey);
                    var list = SelectList(command_select);
                    return list.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "更新应用信息失败");
            }            
        }

        /// <summary>
        /// 修改应用状态
        /// </summary>
        /// <param name="appKey">KEY</param>
        /// <param name="model">应用状态对象信息</param>
        /// <returns>修改成功,返回true.否则返回false.</returns>
        public bool ModifyState(string appKey, WapAppState model)
        {
            try
            {
                string commandText = @"UPDATE AUTH_APP 
                                       SET  APP_ACTIVE=@APP_ACTIVE
                                      where APP_KEY = @APP_KEY";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@APP_KEY", DbType.String, appKey);
                    Database.AddInParameter(command, "@APP_ACTIVE", DbType.Boolean, model.Active);

                    int line = ExecuteNonQuery(command);
                    if (line <= 0)
                    {
                        throw new WapException(SH3H.WAP.Share.StateCode.CODE_APP_INEXIST, "应用标识不存在");
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "修改应用状态失败");
            }          
        }

        /// <summary>
        /// 获取所有应用程序
        /// </summary>
        /// <returns>应用程序对象列表</returns>
        public IEnumerable<WapApp> SelectAll()
        {
            try
            {
                string sqlText = @"SELECT * FROM AUTH_APP ";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    return SelectList(cmd);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "获取所有应用信息失败");
            }           
        }

        /// <summary>
        /// 通过搜索关键字获取应用列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<WapApp> SelectListByKeyword(string keyword)
        {
            try
            {
                StringBuilder commandText = new StringBuilder(@"SELECT * FROM AUTH_APP WHERE 1=1 ");
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    commandText.AppendLine(@"  AND (APP_PYCODE  LIKE +'%'+@search_text+'%'
	                                             OR APP_NAME  LIKE +'%'+@search_text+'%'
                                               	OR APP_IDENTITY  LIKE +'%'+@search_text+'%')");
                }
                commandText.AppendLine("  order by APP_SORTSN");

                using (DbCommand command = Database.GetSqlStringCommand(commandText.ToString()))
                {
                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        Database.AddInParameter(command, "@search_text", DbType.String, keyword);
                    }
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "获取应用列表失败");
            }           
        }

        /// <summary>
        /// 通过appKey获取应用
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public WapApp SelectAppByKey(string appKey)
        {
            try
            {
                string commandText = "SELECT * FROM AUTH_APP WHERE APP_KEY = @APP_KEY";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@APP_KEY", DbType.String, appKey);
                    var list = SelectList(command);
                    if ((list == null || list.Count() == 0))
                    {
                        throw new WapException(SH3H.WAP.Share.StateCode.CODE_APP_INEXIST, "应用标识不存在");
                    }
                    return list.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "通过appKey获取应用失败");
            }            
        }

        /// <summary>
        /// 通过appIdentity获取应用
        /// </summary>
        /// <param name="appIdentity"></param>
        /// <returns></returns>
        public WapApp SelectAppByIdentity(string appIdentity)
        {
            try
            {
                string commandText = "SELECT * FROM AUTH_APP WHERE APP_IDENTITY = @APP_IDENTITY";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@APP_IDENTITY", DbType.String, appIdentity);
                    var list = SelectList(command);
                    if ((list == null || list.Count() == 0))
                    {
                        throw new WapException(SH3H.WAP.Share.StateCode.CODE_APP_INEXIST, "应用标识不存在");
                    }
                    return list.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "通过appIdentify获取应用失败");
            }          
        }

        /// <summary>
        /// 根据用户KEY获取APP
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns></returns>
        public IEnumerable<WapApp> GetAppByUserKey(string userKey)
        {
            try
            {
                string commandText = @"SELECT * FROM AUTH_APP WHERE APP_KEY IN (
                                        SELECT GRP.FUNC_APP_KEY FROM AUTH_FUNCTION_GROUP GRP 
                                        INNER JOIN AUTH_FUNCTION FUNC ON FUNC.FUNC_GROUP_KEY = GRP.FUNC_GROUP_KEY
                                        INNER JOIN AUTH_ROLE_FUNCTION FR ON FR.FUNC_KEY = FUNC.FUNC_KEY
                                        INNER JOIN AUTH_ROLE R ON R.ROLE_KEY = FR.ROLE_KEY
                                        INNER JOIN AUTH_USER_ROLE UR ON UR.ROLE_KEY = R.ROLE_KEY
                                        INNER JOIN AUTH_USER U ON U.USER_KEY = UR.USER_KEY
                                        WHERE U.USER_KEY = @UserKey                                        
                                    ) ORDER BY APP_SORTSN";

                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@UserKey", DbType.String, userKey);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "根据用户Key获取应用失败");
            }          
        }

        /// <summary>
        /// 根据应用的appKey获取指定应用的角色导出脚本
        /// </summary>
        /// <param name="appKey">应用KEY</param>
        /// <returns>返回字符串</returns>
        public string GetRoleString(string appKey)
        {
            try
            {
                List<string> tableNames = new List<string>();
                tableNames.Add("AUTH_ROLE");
                tableNames.Add("AUTH_ROLE_FUNCTION");

                string commandText = string.Format(@"
                                                SELECT * 
                                                FROM AUTH_ROLE;
                                                
                                                SELECT * 
                                                FROM AUTH_ROLE_FUNCTION 
                                                WHERE FUNC_KEY IN 
                                                (
	                                                SELECT FUNC_KEY FROM AUTH_FUNCTION WHERE FUNC_APP_KEY='{0}'
                                                );", appKey);

                DataSet ds = Database.ExecuteDataSet(CommandType.Text, commandText);
                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < tableNames.Count(); i++)
                {
                    sb.Append(BuildJsonString(ds.Tables[i], tableNames[i])).Append(i == tableNames.Count() - 1 ? "" : ",");
                }
                return "{" + sb.ToString() + "\r\n}";
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "根据appKey获取角色脚本失败");
            }           
        }

        /// <summary>
        /// 根据应用的appKey获取导出字符串
        /// </summary>
        /// <param name="appKey">应用KEY</param>
        /// <param name="isJson">是否JSON字符串</param>
        /// <returns>返回字符串</returns>
        public string GetAppString(string appKey, bool isJson)
        {
            try
            {
                List<string> tableNames = new List<string>();
                tableNames.Add("AUTH_APP");
                tableNames.Add("AUTH_FUNCTION");
                tableNames.Add("AUTH_FUNCTION_GROUP");
                tableNames.Add("AUTH_MENU");

                List<string> tableCNames = new List<string>();
                tableCNames.Add("应用表");
                tableCNames.Add("功能表");
                tableCNames.Add("功能组表");
                tableCNames.Add("菜单表");

                string commandText = string.Format(@"SELECT * FROM 
                                    AUTH_APP 
                                    WHERE APP_KEY='{0}';
                                    SELECT AF.* 
                                    FROM AUTH_FUNCTION AF
                                    INNER JOIN AUTH_FUNCTION_GROUP AFG
                                    ON AFG.FUNC_GROUP_KEY=AF.FUNC_GROUP_KEY
                                    WHERE AFG.FUNC_APP_KEY='{0}';
                                    SELECT * 
                                    FROM AUTH_FUNCTION_GROUP 
                                    WHERE FUNC_APP_KEY='{0}';
                                    SELECT * 
                                    FROM AUTH_MENU 
                                    WHERE APP_KEY='{0}'", appKey);

                DataSet ds = Database.ExecuteDataSet(CommandType.Text, commandText);
                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < tableNames.Count(); i++)
                {
                    if (isJson)
                    {
                        sb.Append(BuildJsonString(ds.Tables[i], tableNames[i])).Append(i == tableNames.Count() - 1 ? "" : ",");
                    }
                    else
                    {
                        if (sb.Length == 0) sb.Append("-- 敏捷平台数据导出");
                        sb.Append("\r\n-- ").Append(tableCNames[i]);
                        sb.Append(BuildInsertString(ds.Tables[i], tableNames[i]));
                    }
                }
                return isJson ? "{" + sb.ToString() + "\r\n}" : sb.ToString();
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "根据appKey获取应用脚本失败");
            }          
        }

        /// <summary>
        /// 删除应用
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <returns>是否删除成功</returns>
        public bool DeleteApp(string appKey)
        {
            try
            {
                try
                {
                    string commandText = @"DELETE FROM AUTH_APP
                                      where APP_KEY = @APP_KEY";
                    using (DbCommand command = Database.GetSqlStringCommand(commandText))
                    {
                        Database.AddInParameter(command, "@APP_KEY", DbType.String, appKey);

                        return ExecuteNonQuery(command) > 0;
                    }
                }
                catch (Exception ex)
                {
                    LogManager.Get().Throw(ex);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "根据appKey删除应用失败");
            }           
        }

        /// <summary>
        /// 禁用应用
        /// </summary>
        /// <param name="appKey">应用标识</param>
        /// <returns>是否禁用成功</returns>
        public bool DeactiveApp(string appKey)
        {
            try
            {
                string commandText = @"UPDATE AUTH_APP 
                                       SET  APP_ACTIVE=0
                                      where APP_KEY = @APP_KEY";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@APP_KEY", DbType.String, appKey);

                    return ExecuteNonQuery(command) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "禁用应用失败");
            }
        }

        /// <summary>
        /// 启用应用
        /// </summary>
        /// <param name="appKey">应用标识</param>
        /// <returns>是否启用成功</returns>
        public bool ActiveApp(string appKey)
        {
            try
            {
                string commandText = @"UPDATE AUTH_APP 
                                       SET  APP_ACTIVE=1
                                      where APP_KEY = @APP_KEY";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@APP_KEY", DbType.String, appKey);

                    return ExecuteNonQuery(command) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "启用应用失败");
            }
        }            

        /// <summary>
        /// 查找当前用户被授权的应用程序
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>
        /// 返回应用程序列表
        /// </returns>
        public IEnumerable<WapApp> SelectAuthorizedApps(int userId)
        {
            try
            {
                string commandText = @" SELECT * 
                                        FROM AUTH_APP 
                                        WHERE APP_KEY IN (
                                            SELECT GRP.FUNC_APP_KEY 
                                            FROM AUTH_FUNCTION_GROUP GRP 
                                            INNER JOIN AUTH_FUNCTION FUNC 
                                            ON FUNC.FUNC_GROUP_KEY = GRP.FUNC_GROUP_KEY
                                            INNER JOIN AUTH_ROLE_FUNCTION FR 
                                            ON FR.FUNC_KEY = FUNC.FUNC_KEY
                                            INNER JOIN AUTH_ROLE R  
                                            ON R.ROLE_KEY = FR.ROLE_KEY
                                            INNER JOIN AUTH_USER_ROLE UR  
                                            ON UR.ROLE_KEY = R.ROLE_KEY
                                            INNER JOIN AUTH_USER U  
                                            ON U.USER_KEY = UR.USER_KEY
                                            WHERE U.USER_ID = @UserId                                        
                                        ) ORDER BY APP_SORTSN";

                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@UserId", DbType.Int32, userId);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "查找当前用户被授权的应用程序失败");
            }
        }

        /// <summary>
        /// 判断应用标识是否存在
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>如果存在,返回true,否则返回false.</returns>
        public bool IsExitAppIdentity(string appIdentity)
        {
            try
            {
                string commandText = "SELECT TOP 1 * FROM AUTH_APP WHERE APP_IDENTITY = @APP_IDENTITY";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@APP_IDENTITY", DbType.String, appIdentity);
                    var list = SelectList(command);
                    return list.Count() > 0 ? true : false;
                }
                
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR,"检查应用标识是否存在时失败!");
                throw;
            }
        }

        /// <summary>
        /// 根据数据表构建JSON语句
        /// </summary>
        /// <param name="data">数据表</param>
        /// <param name="tableName">表名</param>
        /// <returns>返回字符串</returns>
        private string BuildJsonString(DataTable data, string tableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n\"").Append(tableName).Append("\":[");
            for (int x = 0; x < data.Rows.Count; x++)
            {
                sb.Append("{\r\n");
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    sb.Append("\"").Append(data.Columns[i].ColumnName).Append("\":\"").Append(data.Rows[x][i]).Append(i == data.Columns.Count - 1 ? "\"" : "\",");
                }
                sb.Append(x == data.Rows.Count - 1 ? "\r\n}" : "\r\n},");
            }
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// 根据数据表构建INSERT语句
        /// </summary>
        /// <param name="data">数据表</param>
        /// <param name="tableName">表名</param>
        /// <returns>返回字符串</returns>
        private string BuildInsertString(DataTable data, string tableName)
        {
            StringBuilder cols = new StringBuilder();
            for (var x = 0; x < data.Columns.Count; x++)
            {
                cols.Append(data.Columns[x].ColumnName).Append(x == data.Columns.Count - 1 ? "" : ",");
            }
            string comStr = "\r\nINSERT " + tableName + " (" + cols.ToString() + ") VALUES ({0});";
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in data.Rows)
            {
                StringBuilder ia = new StringBuilder();
                foreach (var a in dr.ItemArray)
                {
                    var prefix = "";
                    if (a.GetType().Name == "String") prefix = "N";
                    ia.Append(a == null ? "NULL" : prefix + "'" + a.ToString() + "',");
                }
                //sb.Append(string.Format(comStr, ConvertArrayToString<object>(dr.ItemArray, a => "'" + a.ToString() + "'")));         
                sb.Append(string.Format(comStr, ia.ToString().TrimEnd(',')));
            }
            return sb.ToString();
        }            

        #endregion

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <returns>返回对象实例</returns>
        protected override WapApp CreateObject(IDataReader reader)
        {
            return new WapApp();
        }

        public bool CreateSchema()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据<see cref="IDataReader"/>数据读取器构建图层实体
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="app">应用程序对象</param>
        /// <returns>如果构建应用程序对象成功返回图层实例，否则返回NULL</returns>
        public override WapApp Build(IDataReader reader, WapApp app)
        {
            try
            {
                app.AppKey = reader.GetReaderValue<string>("APP_KEY");
                app.AppIdentity = reader.GetReaderValue<string>("APP_IDENTITY");
                app.AppName = reader.GetReaderValue<string>("APP_NAME");
                app.PyCode = reader.GetReaderValue<string>("APP_PYCODE",null,true);
                app.Active = reader.GetReaderValue<bool>("APP_ACTIVE");
                app.SortSn = reader.GetReaderValue<int>("APP_SORTSN");
                app.Comment = reader.GetReaderValue<string>("APP_COMMENT",null,true);
                app.Type = reader.GetReaderValue<int>("APP_TYPE");
                app.Extend = reader.GetReaderValue<string>("EXTEND", null, true);
                app.Icon = reader.GetReaderValue<string>("APP_ICON", null, true);
                app.DefaultIndex = reader.GetReaderValue<string>("APP_DEFAULT_INDEX", null, true);
                return app;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }
    }
}

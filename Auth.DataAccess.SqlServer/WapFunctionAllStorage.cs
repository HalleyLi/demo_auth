using SH3H.SDK.DataAccess.Db;
using SH3H.SDK.Definition.Exceptions;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.WAP.Auth.Model.Dto;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SH3H.SharpFrame.Data;

namespace SH3H.WAP.Auth.DataAccess.SqlServer
{
    /// <summary>
    /// 定义功能点附菜单SqlServer数据库访问对象
    /// </summary>
    public class WapFunctionAllStorage :
        BaseAccess<WapFunctionAllDto>, IWapFunctionAllStorage
    {
                //构造函数
        public WapFunctionAllStorage() : base(SH3H.SDK.Share.Consts.CONFIGURE_DATABASE_CONNECTION_STRING) { }

        public bool CreateSchema()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 获取所有功能点组
        /// </summary>
        /// <returns>功能点组列表</returns>
        public IEnumerable<WapFunctionAllDto> GetFunctionAlls()
        {
            try
            {
                string commandText = @"SELECT AF.FUNC_KEY
                                            ,AF.FUNC_NAME
                                            ,AF.FUNC_CODE
                                            ,AF.FUNC_PYCODE
                                            ,AF.FUNC_SORTSN
                                            ,AF.FUNC_ACTIVE
                                            ,AF.FUNC_COMMENT
                                            ,AFG.FUNC_GROUP_KEY
	                                        ,AFG.FUNC_GROUP_NAME
	                                        ,AA.APP_KEY
	                                        ,AA.APP_NAME
                                        FROM AUTH_FUNCTION AF
                                        LEFT JOIN AUTH_FUNCTION_GROUP AFG
                                        ON AFG.FUNC_GROUP_KEY=AF.FUNC_GROUP_KEY
                                        LEFT JOIN AUTH_APP AA
                                        ON AF.FUNC_APP_KEY=AA.APP_KEY
                                        WHERE AF.FUNC_ACTIVE = 1
                                          AND AFG.FUNC_GROUP_ACTIVE = 1
                                          AND AA.APP_ACTIVE = 1";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "失败");
            }
        }

        /// <summary>
        ///  凭应用标识获取所有功能点组
        /// </summary>
        /// <param name="appidentity">应用标识</param>
        /// <returns>功能点组列表</returns>
        public IEnumerable<WapFunctionAllDto> GetFunctionAllsByAppIdentity(string appidentity)
        {
            try
            {
                string commandText = @"SELECT AF.FUNC_KEY
                                            ,AF.FUNC_NAME
                                            ,AF.FUNC_CODE
                                            ,AF.FUNC_PYCODE
                                            ,AF.FUNC_SORTSN
                                            ,AF.FUNC_ACTIVE
                                            ,AF.FUNC_COMMENT
                                            ,AFG.FUNC_GROUP_KEY
	                                        ,AFG.FUNC_GROUP_NAME
	                                        ,AA.APP_KEY
	                                        ,AA.APP_NAME
                                        FROM AUTH_FUNCTION AF
                                        LEFT JOIN AUTH_FUNCTION_GROUP AFG
                                        ON AFG.FUNC_GROUP_KEY=AF.FUNC_GROUP_KEY
                                        LEFT JOIN AUTH_APP AA
                                        ON AF.FUNC_APP_KEY=AA.APP_KEY
                                        WHERE AF.FUNC_ACTIVE = 1
                                          AND AFG.FUNC_GROUP_ACTIVE = 1
                                          AND AA.APP_ACTIVE = 1
                                          AND AA.APP_IDENTITY = @app_identity";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "app_identity", DbType.String, appidentity);

                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "失败");
            }
        }

        /// <summary>
        ///  凭应用主键获取所有功能点组
        /// </summary>
        /// <param name="appKey">应用主键</param>
        /// <returns>功能点组列表</returns>
        public IEnumerable<WapFunctionAllDto> GetFunctionAllsByAppKey(string appKey)
        {
            try
            {
                string commandText = @"SELECT AF.FUNC_KEY
                                            ,AF.FUNC_NAME
                                            ,AF.FUNC_CODE
                                            ,AF.FUNC_PYCODE
                                            ,AF.FUNC_SORTSN
                                            ,AF.FUNC_ACTIVE
                                            ,AF.FUNC_COMMENT
                                            ,AFG.FUNC_GROUP_KEY
	                                        ,AFG.FUNC_GROUP_NAME
	                                        ,AA.APP_KEY
	                                        ,AA.APP_NAME
                                        FROM AUTH_FUNCTION AF
                                        LEFT JOIN AUTH_FUNCTION_GROUP AFG
                                        ON AFG.FUNC_GROUP_KEY=AF.FUNC_GROUP_KEY
                                        LEFT JOIN AUTH_APP AA
                                        ON AF.FUNC_APP_KEY=AA.APP_KEY
                                        WHERE AF.FUNC_ACTIVE = 1
                                          AND AFG.FUNC_GROUP_ACTIVE = 1
                                          AND AA.APP_ACTIVE = 1
                                          AND AA.APP_KEY = @app_key";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "app_key", DbType.String, appKey);

                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "失败");
            }
        }

        /// <summary>
        /// 凭角色主键列表获取所有功能点组
        /// </summary>
        /// <param name="rolekeys">角色主键列表</param>
        /// <returns>功能点组列表</returns>
        public IEnumerable<WapFunctionAllDto> GetFunctionAllsByRoles(IEnumerable<string> rolekeys)
        {
            if (rolekeys == null || rolekeys.Count() == 0)
            {
                return new List<WapFunctionAllDto>();
            }

            try
            {
                string commandText = @"SELECT AF.FUNC_KEY
                                            ,AF.FUNC_NAME
                                            ,AF.FUNC_CODE
                                            ,AF.FUNC_PYCODE
                                            ,AF.FUNC_SORTSN
                                            ,AF.FUNC_ACTIVE
                                            ,AF.FUNC_COMMENT
                                            ,AFG.FUNC_GROUP_KEY
	                                        ,AFG.FUNC_GROUP_NAME
	                                        ,AA.APP_KEY
	                                        ,AA.APP_NAME
                                        FROM AUTH_FUNCTION AF
                                        INNER JOIN AUTH_ROLE_FUNCTION ARF
                                        ON ARF.FUNC_KEY = AF.FUNC_KEY
                                        LEFT JOIN AUTH_FUNCTION_GROUP AFG
                                        ON AFG.FUNC_GROUP_KEY=AF.FUNC_GROUP_KEY
                                        LEFT JOIN AUTH_APP AA
                                        ON AF.FUNC_APP_KEY=AA.APP_KEY
                                        WHERE AF.FUNC_ACTIVE = 1
                                          AND AFG.FUNC_GROUP_ACTIVE = 1
                                          AND AA.APP_ACTIVE = 1
                                          AND ARF.ROLE_KEY IN (
                                          ";
                int length = rolekeys.Count();
                int index = 0;
                for (; index < length; index++)
                {
                    commandText += "@role_key" + index + ",";
                }
                if (commandText.EndsWith(","))
                {
                    commandText = commandText.Substring(0, commandText.Length - 1);
                }
                commandText += ")";

                index = 0;
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    foreach (var rolekey in rolekeys)
                    {
                        string paramname = "role_key" + index;
                        Database.AddInParameter(command, paramname, DbType.String, rolekey);
                        index++;
                    }

                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "失败");
            }
        }

        /// <summary>
        /// 根据<see cref="IDataReader"/>数据读取器构建图层实体
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="funcAll">功能点组对象</param>
        /// <returns>如果构建角色对象成功返回图层实例，否则返回NULL</returns>
        public override WapFunctionAllDto Build(IDataReader reader, WapFunctionAllDto funcAll)
        {
            try
            {
                funcAll.Key = reader.GetReaderValue<string>("FUNC_KEY");
                funcAll.Name = reader.GetReaderValue<string>("FUNC_NAME");
                funcAll.Code = reader.GetReaderValue<string>("FUNC_CODE");
                funcAll.Pycode = reader.GetReaderValue<string>("FUNC_PYCODE");
                funcAll.Sortsn = reader.GetReaderValue<decimal>("FUNC_SORTSN");
                funcAll.Comment = reader.GetReaderValue<string>("FUNC_COMMENT",null,true);
                funcAll.Active = reader.GetReaderValue<bool>("FUNC_ACTIVE");

                funcAll.AppKey = reader.GetReaderValue<string>("APP_KEY");
                funcAll.AppName = reader.GetReaderValue<string>("APP_NAME");

                funcAll.FuncGroupKey = reader.GetReaderValue<string>("FUNC_GROUP_KEY");
                funcAll.FuncGroupName = reader.GetReaderValue<string>("FUNC_GROUP_NAME");
                return funcAll;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "失败");
            }
        }

       

    }
}

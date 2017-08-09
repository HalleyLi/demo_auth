using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SH3H.SharpFrame.Data;
using SH3H.SDK.DataAccess.Db;
using SH3H.WAP.Auth.Model;
using SH3H.SDK.Share;
using System.Data;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.WAP.Auth.Model.Dto;
using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Share;

namespace SH3H.WAP.Auth.DataAccess.SqlServer
{
    /// <summary>
    /// 定义功能点SqlServer数据库访问对象
    /// </summary>
    public class WapFunctionStorage :
        BaseAccess<WapFunction>, IWapFunctionStorage
    {
        /// <summary>
        /// 构造函数
        /// </summary>        
        public WapFunctionStorage()
            : base(SH3H.SDK.Share.Consts.AUTH_DATABASE_CONNECTION_STRING) { }

        /// <summary>
        /// 根据<see cref="IDataReader"/>数据读取器构建图层实体
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="fucntion">功能点组对象</param>
        /// <returns>如果构建角色对象成功返回图层实例，否则返回NULL</returns>
        public override WapFunction Build(IDataReader reader, WapFunction fucntion)
        {
            try
            {
                fucntion.Key = reader.GetReaderValue<string>("FUNC_KEY");
                fucntion.Name = reader.GetReaderValue<string>("FUNC_NAME");
                fucntion.Code = reader.GetReaderValue<string>("FUNC_CODE");
                fucntion.Pycode = reader.GetReaderValue<string>("FUNC_PYCODE");
                fucntion.Sortsn = reader.GetReaderValue<decimal>("FUNC_SORTSN");
                fucntion.Comment = reader.GetReaderValue<string>("FUNC_COMMENT",null,true);
                fucntion.Active = reader.GetReaderValue<bool>("FUNC_ACTIVE");
                //fucntion.GroupName = reader.GetReaderValue<string>("FUNC_GROUP_NAME");
                fucntion.TemplateKey = reader.GetReaderValue<string>("FUNC_TEMPLATE_KEY",null,true);
                fucntion.Extend = reader.GetReaderValue<string>("EXTEND", null, true);
                fucntion.AppKey = reader.GetReaderValue<string>("FUNC_APP_KEY");
                fucntion.FuncGroupKey = reader.GetReaderValue<string>("FUNC_GROUP_KEY");
                return fucntion;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_MODEL_CONVERT_ERROR, "功能点模型转换失败");
            }
        }


        /// <summary>
        /// 获取功能点
        /// </summary>
        /// <param name="functionkey">功能点主键</param>
        /// <returns>功能点</returns>
        public Model.WapFunction GetFunction(string functionkey)
        {
            try
            {
                string commandText = @"SELECT AF.FUNC_KEY
                                            ,AF.FUNC_NAME
                                            ,AF.FUNC_CODE
                                            ,AF.FUNC_TEMPLATE_KEY
                                            ,AF.FUNC_PYCODE
                                            ,AF.FUNC_SORTSN
                                            ,AF.FUNC_ACTIVE
                                            ,AF.FUNC_COMMENT
                                            ,AF.EXTEND
                                            ,AF.FUNC_APP_KEY
                                            ,AF.FUNC_GROUP_KEY
	                                        ,AFG.FUNC_GROUP_NAME
                                        FROM AUTH_FUNCTION AF
                                        LEFT JOIN AUTH_FUNCTION_GROUP AFG
                                        ON AFG.FUNC_GROUP_KEY=AF.FUNC_GROUP_KEY
                                        WHERE AF.FUNC_KEY = @func_key";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@func_key", System.Data.DbType.String, functionkey);
                    return SelectSingle(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "获取功能点失败");
            }
        }

        /// <summary>
        /// 获取功能点(附菜单信息)列表
        /// </summary>
        /// <returns>功能点(附菜单信息)列表</returns>
        public IEnumerable<Model.WapFunction> GetAllFunction()
        {
            try
            {
                string commandText = @"SELECT AF.FUNC_KEY
                                            ,AF.FUNC_NAME
                                            ,AF.FUNC_CODE
                                            ,AF.FUNC_TEMPLATE_KEY
                                            ,AF.FUNC_PYCODE
                                            ,AF.FUNC_SORTSN
                                            ,AF.FUNC_ACTIVE
                                            ,AF.FUNC_COMMENT
                                            ,AF.EXTEND
                                            ,AF.FUNC_APP_KEY
                                            ,AF.FUNC_GROUP_KEY
	                                        ,AFG.FUNC_GROUP_NAME
                                        FROM AUTH_FUNCTION AF
                                        LEFT JOIN AUTH_FUNCTION_GROUP AFG
                                        ON AFG.FUNC_GROUP_KEY=AF.FUNC_GROUP_KEY";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "获取功能点(附菜单信息)列表失败");
            }
        }

        /// <summary>
        /// 新增功能点
        /// </summary>
        /// <param name="function">功能点</param>
        /// <returns>是否成功</returns>
        public bool AddFunction(Model.WapFunction function)
        {
            int result = -1;

            if (function.TemplateKey == null)
            {
                function.TemplateKey = "";
            }

            if (function.Name == null)
            {
                function.Name = "";
            }

            if (function.Comment == null)
            {
                function.Comment = "";
            }

            if (function.Pycode == null)
            {
                function.Pycode = "";
            }


            try
            {
                string addFunctionCommandText = @"INSERT INTO  AUTH_FUNCTION 
                                                        (FUNC_KEY
                                                       ,FUNC_NAME
                                                       ,FUNC_CODE
                                                       ,FUNC_TEMPLATE_KEY
                                                       ,FUNC_PYCODE
                                                       ,FUNC_SORTSN
                                                       ,FUNC_ACTIVE
                                                       ,FUNC_COMMENT
                                                       ,EXTEND
                                                       ,FUNC_APP_KEY
                                                       ,FUNC_GROUP_KEY)
                                                    VALUES
                                                        (@func_key 
                                                        , @func_name 
                                                        , @func_code 
                                                        , @func_template_key
                                                        , @func_pycode 
                                                        , @func_sortsn 
                                                        , @func_active 
                                                        , @func_comment
                                                        , @extend 
                                                        , @func_app_key
                                                        , @func_group_key );";
                using (DbCommand addFunctionCommand = Database.GetSqlStringCommand(addFunctionCommandText))
                {
                    Database.AddInParameter(addFunctionCommand, "@func_key", System.Data.DbType.String, function.Key);
                    Database.AddInParameter(addFunctionCommand, "@func_name", System.Data.DbType.String, function.Name);
                    Database.AddInParameter(addFunctionCommand, "@func_code", System.Data.DbType.String, function.Code);
                    //TODO:保留字段
                    Database.AddInParameter(addFunctionCommand, "@func_template_key", System.Data.DbType.String, function.TemplateKey);
                    Database.AddInParameter(addFunctionCommand, "@func_pycode", System.Data.DbType.String, function.Pycode);
                    Database.AddInParameter(addFunctionCommand, "@func_sortsn", System.Data.DbType.Decimal, function.Sortsn);
                    Database.AddInParameter(addFunctionCommand, "@func_comment", System.Data.DbType.String, function.Comment);
                    Database.AddInParameter(addFunctionCommand, "@func_active", System.Data.DbType.Byte, function.Active);
                    Database.AddInParameter(addFunctionCommand, "@extend", System.Data.DbType.String, function.Extend);
                    Database.AddInParameter(addFunctionCommand, "@func_app_key", System.Data.DbType.String, function.AppKey);
                    Database.AddInParameter(addFunctionCommand, "@func_group_key", System.Data.DbType.String, function.FuncGroupKey);

                    result = ExecuteNonQuery(addFunctionCommand);
                }
                return result >= 0;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "新增功能点失败");
            }

        }

        /// <summary>
        /// 向某个功能点组添加一个功能点
        /// </summary>
        /// <param name="relative">功能点组关联</param>
        /// <returns>是否成功</returns>
        public bool AddFunction(WapFuncGroupRelativeDto relative)
        {
            try
            {
                if (relative.TemplateKey == null)
                {
                    relative.TemplateKey = "";
                }

                if (relative.Name == null)
                {
                    relative.Name = "";
                }

                if (relative.Comment == null)
                {
                    relative.Comment = "";
                }

                if (relative.Pycode == null)
                {
                    relative.Pycode = "";
                }

                string addFunctionCommandText = @"INSERT INTO  AUTH_FUNCTION 
                                                                        (FUNC_KEY
                                                                       ,FUNC_NAME
                                                                       ,FUNC_CODE
                                                                       ,FUNC_TEMPLATE_KEY
                                                                       ,FUNC_PYCODE
                                                                       ,FUNC_SORTSN
                                                                       ,FUNC_ACTIVE
                                                                       ,FUNC_COMMENT
                                                                       ,EXTEND
                                                                       ,FUNC_APP_KEY
                                                                       ,FUNC_GROUP_KEY)
                                                                    VALUES
                                                                        (@func_key 
                                                                        , @func_name 
                                                                        , @func_code 
                                                                        , @func_template_key
                                                                        , @func_pycode 
                                                                        , @func_sortsn 
                                                                        , @func_active 
                                                                        , @func_comment 
                                                                        , @extend
                                                                        , @func_app_key
                                                                        , @func_group_key);";

                string sqlText = addFunctionCommandText;

                using (DbCommand addFunctionCommand = Database.GetSqlStringCommand(sqlText))
                {
                    //功能点
                    Database.AddInParameter(addFunctionCommand, "@func_key", System.Data.DbType.String, relative.Key);
                    Database.AddInParameter(addFunctionCommand, "@func_name", System.Data.DbType.String, relative.Name);
                    Database.AddInParameter(addFunctionCommand, "@func_code", System.Data.DbType.String, relative.Code);
                    Database.AddInParameter(addFunctionCommand, "@func_template_key", System.Data.DbType.String, relative.TemplateKey);
                    Database.AddInParameter(addFunctionCommand, "@func_pycode", System.Data.DbType.String, relative.Pycode);
                    Database.AddInParameter(addFunctionCommand, "@func_sortsn", System.Data.DbType.Decimal, relative.Sortsn);
                    Database.AddInParameter(addFunctionCommand, "@func_comment", System.Data.DbType.String, relative.Comment);
                    Database.AddInParameter(addFunctionCommand, "@func_active", System.Data.DbType.Byte, relative.Active);
                    Database.AddInParameter(addFunctionCommand, "@extend", System.Data.DbType.String, relative.Extend);
                    Database.AddInParameter(addFunctionCommand, "@func_app_key", System.Data.DbType.String, relative.AppKey);
                    Database.AddInParameter(addFunctionCommand, "@func_group_key", System.Data.DbType.String, relative.FuncGroupKey);


                    int result = ExecuteNonQuery(addFunctionCommand);
                    if (result == 1) return true;
                    else throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "向某个功能点组添加一个功能点失败");
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "向某个功能点组添加一个功能点失败");
            }
        }

        /// <summary>
        /// 通过appKey和funcCode获取功能点
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <param name="funcCode">功能点Code</param>
        /// <returns>存在返回true\不存在返回false</returns>
        public bool IsHaveFuncByFuncCodeAndAppKey(string appKey, string funcCode)
        {
            try
            {
                string sqlText = @"SELECT * 
                                   FROM AUTH_FUNCTION  
                                   WHERE FUNC_CODE = @FUNC_CODE AND FUNC_APP_KEY=@APP_KEY";

                using (DbCommand command = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(command, "@APP_KEY", System.Data.DbType.String, appKey);
                    Database.AddInParameter(command, "@FUNC_CODE", System.Data.DbType.String, funcCode);
                    var list = SelectList(command);
                    return (list == null || list.Count() == 0) ? false : true;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, " 获取功能点对象失败");
            }

        }

        /// <summary>
        /// 删除功能点
        /// </summary>
        /// <param name="functionkey">功能点主键</param>
        /// <returns>是否成功</returns>
        public bool DeleteFunction(string functionkey)
        {
            try
            {
                string commandText = @"UPDATE  AUTH_FUNCTION SET FUNC_ACTIVE = 0  WHERE FUNC_KEY = @func_key";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@func_key", System.Data.DbType.String, functionkey);
                    return ExecuteNonQuery(command) >= 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "删除功能点失败");
            }
        }

        /// <summary>
        /// 修改功能点
        /// </summary>
        /// <param name="function">功能点</param>
        /// <returns>是否成功</returns>
        public bool UpdateFunction(Model.WapFunction function)
        {

            Model.WapFunction baseModel = GetFunction(function.Key);

            ///原模型不存在
            if (baseModel == null)
            {
                throw new WapException(StateCode.CODE_MODEL_NOT_EXIST, "修改的功能点不存在");
            }

            if (function.TemplateKey == null)
            {
                function.TemplateKey = "";
            }

            int result = -1;
            try
            {
                //修改方法
                string modifyFunctionCommandText = @"UPDATE  AUTH_FUNCTION 
                                           SET  FUNC_NAME  = @func_name
                                              , FUNC_CODE  = @func_code
                                              , FUNC_PYCODE  = @func_pycode
                                              , FUNC_SORTSN  = @func_sortsn
                                              , FUNC_ACTIVE  = @func_active
                                              , FUNC_COMMENT  = @func_comment
                                              , EXTEND  = @extend
                                              , FUNC_TEMPLATE_KEY  = @func_template_key
                                              , FUNC_APP_KEY  = @func_app_key
                                              , FUNC_GROUP_KEY  = @func_group_key
                                         WHERE FUNC_KEY=@func_key";
                using (DbCommand modifyFunctionCommand = Database.GetSqlStringCommand(modifyFunctionCommandText))
                {

                    Database.AddInParameter(modifyFunctionCommand, "@func_key", System.Data.DbType.String, function.Key);
                    Database.AddInParameter(modifyFunctionCommand, "@func_name", System.Data.DbType.String, function.Name);
                    Database.AddInParameter(modifyFunctionCommand, "@func_code", System.Data.DbType.String, function.Code);
                    Database.AddInParameter(modifyFunctionCommand, "@func_pycode", System.Data.DbType.String, function.Pycode);
                    Database.AddInParameter(modifyFunctionCommand, "@func_sortsn", System.Data.DbType.Decimal, function.Sortsn);
                    Database.AddInParameter(modifyFunctionCommand, "@func_comment", System.Data.DbType.String, function.Comment);
                    Database.AddInParameter(modifyFunctionCommand, "@func_active", System.Data.DbType.Boolean, function.Active);
                    Database.AddInParameter(modifyFunctionCommand, "@func_template_key", System.Data.DbType.String, function.TemplateKey);
                    Database.AddInParameter(modifyFunctionCommand, "@extend", System.Data.DbType.String, function.Extend);
                    Database.AddInParameter(modifyFunctionCommand, "@func_app_key", System.Data.DbType.String, function.AppKey);
                    Database.AddInParameter(modifyFunctionCommand, "@func_group_key", System.Data.DbType.String, function.FuncGroupKey);

                    result = ExecuteNonQuery(modifyFunctionCommand);
                    return result >= 0;
                }
            }

            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "修改功能点失败");
            }

        }

        /// <summary>
        /// 修改功能点激活状态
        /// </summary>
        /// <param name="key">功能点主键</param>
        /// <param name="active">激活状态</param>
        /// <returns>是否成功</returns>
        public bool UpdateFunctionActive(string key, bool active)
        {

            Model.WapFunction baseModel = GetFunction(key);

            ///原模型不存在
            if (baseModel == null)
            {
                throw new WapException(StateCode.CODE_MODEL_NOT_EXIST, "修改的功能点不存在");
            }

            int result = -1;
            try
            {
                //修改方法
                string modifyFunctionCommandText = @"UPDATE  AUTH_FUNCTION 
                                           SET  FUNC_ACTIVE  = @func_active
                                         WHERE FUNC_KEY=@func_key";
                using (DbCommand modifyFunctionCommand = Database.GetSqlStringCommand(modifyFunctionCommandText))
                {

                    Database.AddInParameter(modifyFunctionCommand, "@func_key", System.Data.DbType.String, baseModel.Key);
                    Database.AddInParameter(modifyFunctionCommand, "@func_active", System.Data.DbType.Boolean, active);

                    result = ExecuteNonQuery(modifyFunctionCommand);
                    return result >= 0;
                }
            }

            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "修改功能点激活状态失败");
            }
        }

        /// <summary>
        /// 凭Code获取功能点
        /// </summary>
        /// <param name="code">Code</param>
        /// <returns>功能点</returns>
        public WapFunction GetFunctionByCode(string code)
        {
            try
            {
                string commandText = @"SELECT AF.FUNC_KEY
                                            ,AF.FUNC_NAME
                                            ,AF.FUNC_CODE
                                            ,AF.FUNC_TEMPLATE_KEY
                                            ,AF.FUNC_PYCODE
                                            ,AF.FUNC_SORTSN
                                            ,AF.FUNC_ACTIVE
                                            ,AF.FUNC_COMMENT
                                            ,AF.EXTEND
                                            ,AF.FUNC_APP_KEY
                                            ,AF.FUNC_GROUP_KEY
	                                        ,AFG.FUNC_GROUP_NAME
                                        FROM AUTH_FUNCTION AF
                                        LEFT JOIN AUTH_FUNCTION_GROUP AFG
                                        ON AFG.FUNC_GROUP_KEY=AF.FUNC_GROUP_KEY
                                        WHERE AF.FUNC_CODE = @func_code
                                        AND AF.FUNC_ACTIVE = 1";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@func_code", System.Data.DbType.String, code);
                    return SelectSingle(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "失败");
            }
        }


        /// <summary>
        /// 创建方案
        /// </summary>
        /// <returns>是否成功</returns>
        public bool CreateSchema()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 功能点模糊查询
        /// </summary>
        /// <param name="searchText">搜索关键字</param>
        /// <returns>功能点列表</returns>
        public IEnumerable<WapFunction> FuzzySearch(string searchText)
        {
            try
            {
                string sqlText = @"SELECT * FROM AUTH_FUNCTION WHERE FUNC_NAME LIKE N'%'+@TEXT+'%'
                                                                        OR FUNC_CODE LIKE N'%'+@TEXT+'%'
                                                                        OR FUNC_PYCODE LIKE '%'+@TEXT+'%'";
                using (DbCommand command = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(command, "@TEXT", System.Data.DbType.String, searchText);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }
    }
}

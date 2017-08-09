using SH3H.SDK.DataAccess.Db;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.SDK.Share;
using SH3H.WAP.Auth.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SH3H.SharpFrame.Data;
using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Share;

namespace SH3H.WAP.Auth.DataAccess.SqlServer
{
    /// <summary>
    /// 定义功能组程序SqlServer数据库访问对象
    /// </summary>
    public class WapFuncGroupStorage :
         BaseAccess<WapFunctionGroup>, IWapFunctionGroupStorage
    {
        /// <summary>
        /// 构造函数
        /// </summary>        
        public WapFuncGroupStorage()
            : base(SH3H.SDK.Share.Consts.AUTH_DATABASE_CONNECTION_STRING) { }

        /// <summary>
        /// 根据<see cref="IDataReader"/>数据读取器构建图层实体
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="funcGroup">功能点组对象</param>
        /// <returns>如果构建角色对象成功返回图层实例，否则返回NULL</returns>
        public override WapFunctionGroup Build(IDataReader reader, WapFunctionGroup funcGroup)
        {
            try
            {
                funcGroup.FuncGroupKey = reader.GetReaderValue<Guid>("FUNC_GROUP_KEY");
                funcGroup.FuncGroupName = reader.GetReaderValue<string>("FUNC_GROUP_NAME");
                funcGroup.FuncGroupPycode = reader.GetReaderValue<string>("FUNC_GROUP_PYCODE");
                funcGroup.FuncGroupSortsn = reader.GetReaderValue<int>("FUNC_GROUP_SORTSN");
                funcGroup.FuncAppKey = reader.GetReaderValue<string>("FUNC_APP_KEY");
                funcGroup.ParentFuncGroupKey = reader.GetReaderValue<Guid>("PARENT_FUNC_GROUP_KEY");
                funcGroup.FuncGroupComment = reader.GetReaderValue<string>("FUNC_GROUP_COMMENT",null,true);
                funcGroup.FuncGroupActive = reader.GetReaderValue<bool>("FUNC_GROUP_ACTIVE");
                funcGroup.Extend = reader.GetReaderValue<string>("EXTEND",null,true);
                return funcGroup;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_MODEL_NOT_EXIST, "功能组模型转换失败");
            }
        }

        /// <summary>
        /// 凭功能组主键获取功能组
        /// </summary>磨
        /// <param name="functionGroupKey">功能组主键</param>
        /// <returns>功能组</returns>
        public Model.WapFunctionGroup GetFunctionGroup(string functionGroupKey)
        {
            try
            {
                string commandText = @"SELECT FUNC_GROUP_KEY
                                              ,FUNC_GROUP_NAME
                                              ,FUNC_GROUP_PYCODE
                                              ,FUNC_GROUP_SORTSN
                                              ,FUNC_APP_KEY
                                              ,PARENT_FUNC_GROUP_KEY
                                              ,FUNC_GROUP_ACTIVE
                                              ,FUNC_GROUP_COMMENT
                                              ,EXTEND
                                          FROM AUTH_FUNCTION_GROUP
                                          WHERE FUNC_GROUP_KEY = @func_group_key";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@func_group_key", System.Data.DbType.String, functionGroupKey);
                    return SelectSingle(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_MODEL_CONVERT_ERROR, "凭功能组主键获取功能组失败");
            }
        }

        /// <summary>
        /// 获取所有功能点组
        /// </summary>
        /// <returns>功能点组</returns>
        public IEnumerable<Model.WapFunctionGroup> GetAllFunctionGroup()
        {
            try
            {
                string commandText = @"SELECT FUNC_GROUP_KEY
                                              ,FUNC_GROUP_NAME
                                              ,FUNC_GROUP_PYCODE
                                              ,FUNC_GROUP_SORTSN
                                              ,FUNC_APP_KEY
                                              ,PARENT_FUNC_GROUP_KEY
                                              ,FUNC_GROUP_ACTIVE
                                              ,FUNC_GROUP_COMMENT
                                              ,EXTEND
                                          FROM AUTH_FUNCTION_GROUP";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_MODEL_CONVERT_ERROR, "获取所有功能点组列表失败");
            }
        }

        /// <summary>
        /// 凭应用标识获取功能点组列表
        /// </summary>
        /// <param name="appidentity">应用标识</param>
        /// <returns>功能点组列表</returns>
        public IEnumerable<Model.WapFunctionGroup> GetAllFunctionGroupByAppIdentity(string appidentity)
        {
            try
            {
                string commandText = @"SELECT AFG.FUNC_GROUP_KEY
                                              ,AFG.FUNC_GROUP_NAME
                                              ,AFG.FUNC_GROUP_PYCODE
                                              ,AFG.FUNC_GROUP_SORTSN
                                              ,AFG.FUNC_APP_KEY
                                              ,AFG.PARENT_FUNC_GROUP_KEY
                                              ,AFG.FUNC_GROUP_ACTIVE
                                              ,AFG.FUNC_GROUP_COMMENT
                                              ,AFG.EXTEND
                                          FROM AUTH_FUNCTION_GROUP AFG
                                          INNER JOIN AUTH_APP AP
                                          ON AP.APP_KEY=AFG.FUNC_APP_KEY
                                          WHERE AP.APP_IDENTITY=@app_identity";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "app_identity", DbType.String, appidentity);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_MODEL_CONVERT_ERROR, "凭应用标识获取功能点组列表失败");
            }
        }

        /// <summary>
        /// 凭应用主键获取功能点组列表
        /// </summary>
        /// <param name="appkey">应用主键</param>
        /// <returns>功能点组列表</returns>
        public IEnumerable<Model.WapFunctionGroup> GetAllFunctionGroupByAppKey(string appkey)
        {
            try
            {
                string commandText = @"SELECT AFG.FUNC_GROUP_KEY
                                              ,AFG.FUNC_GROUP_NAME
                                              ,AFG.FUNC_GROUP_PYCODE
                                              ,AFG.FUNC_GROUP_SORTSN
                                              ,AFG.FUNC_APP_KEY
                                              ,AFG.PARENT_FUNC_GROUP_KEY
                                              ,AFG.FUNC_GROUP_ACTIVE
                                              ,AFG.FUNC_GROUP_COMMENT
                                              ,AFG.EXTEND
                                          FROM AUTH_FUNCTION_GROUP AFG
                                          INNER JOIN AUTH_APP AP
                                          ON AP.APP_KEY=AFG.FUNC_APP_KEY
                                          WHERE AFG.FUNC_APP_KEY=@func_app_key";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "func_app_key", DbType.String, appkey);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_MODEL_CONVERT_ERROR, "凭应用主键获取功能点组列表失败");
            }
        }

        /// <summary>
        /// 新增功能点组
        /// </summary>
        /// <param name="functionGroup">功能点组</param>
        /// <returns>是否成功</returns>
        public bool AddFunctionGroup(Model.WapFunctionGroup functionGroup)
        {
            int result = -1;
            try
            {
                string addFunctionGroupCommandText = @"INSERT INTO  AUTH_FUNCTION_GROUP 
                                                        (FUNC_GROUP_KEY
                                                       ,FUNC_GROUP_NAME
                                                       ,FUNC_GROUP_PYCODE
                                                       ,FUNC_GROUP_SORTSN
                                                       ,FUNC_APP_KEY
                                                       ,PARENT_FUNC_GROUP_KEY
                                                       ,FUNC_GROUP_ACTIVE
                                                       ,FUNC_GROUP_COMMENT
                                                       ,EXTEND)
                                                    VALUES
                                                        (@func_group_key 
                                                        , @func_group_name 
                                                        , @func_group_pycode
                                                        , @func_group_sortsn
                                                        , @func_app_key
                                                        , @parent_func_group_key
                                                        , @func_group_active
                                                        , @func_group_comment
                                                        , @extend);";
                using (DbCommand addFunctionGroupCommand = Database.GetSqlStringCommand(addFunctionGroupCommandText))
                {
                    Database.AddInParameter(addFunctionGroupCommand, "@func_group_key", System.Data.DbType.Guid, functionGroup.FuncGroupKey);
                    Database.AddInParameter(addFunctionGroupCommand, "@func_group_name", System.Data.DbType.String, functionGroup.FuncGroupName);
                    Database.AddInParameter(addFunctionGroupCommand, "@func_group_pycode", System.Data.DbType.String, functionGroup.FuncGroupPycode);
                    Database.AddInParameter(addFunctionGroupCommand, "@func_group_sortsn", System.Data.DbType.Decimal, functionGroup.FuncGroupSortsn);
                    Database.AddInParameter(addFunctionGroupCommand, "@func_app_key", System.Data.DbType.String, functionGroup.FuncAppKey);
                    Database.AddInParameter(addFunctionGroupCommand, "@parent_func_group_key", System.Data.DbType.Guid, functionGroup.ParentFuncGroupKey);
                    Database.AddInParameter(addFunctionGroupCommand, "@func_group_active", System.Data.DbType.Byte, functionGroup.FuncGroupActive);
                    Database.AddInParameter(addFunctionGroupCommand, "@func_group_comment", System.Data.DbType.String, functionGroup.FuncGroupComment);
                    Database.AddInParameter(addFunctionGroupCommand, "@extend", System.Data.DbType.String, functionGroup.Extend);

                    result = ExecuteNonQuery(addFunctionGroupCommand);
                }
                return result >= 0;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_MODEL_CONVERT_ERROR, "新增功能点组失败");
            }

        }

        /// <summary>
        /// 删除供电组
        /// </summary>
        /// <param name="functionGroupKey">功能点组主键</param>
        /// <returns>是否成功</returns>
        public bool DeleteFunctionGroup(string functionGroupKey)
        {
            try
            {
                string commandText = @"UPDATE  AUTH_FUNCTION_GROUP 
                                       SET FUNC_GROUP_ACTIVE = 0  
                                       WHERE FUNC_GROUP_KEY = @func_group_key";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@func_group_key", System.Data.DbType.String, functionGroupKey);
                    return ExecuteNonQuery(command) >= 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_MODEL_CONVERT_ERROR, "删除功能点组失败");
            }
        }

        /// <summary>
        /// 修改功能点组
        /// </summary>
        /// <param name="functionGroup">功能点组</param>
        /// <returns>是否成功</returns>
        public bool UpdateFunctionGroup(Model.WapFunctionGroup functionGroup)
        {

            Model.WapFunctionGroup baseModel = GetFunctionGroup(functionGroup.FuncGroupKey.ToString());

            ///原模型不存在
            if (baseModel == null)
            {
                throw new WapException(StateCode.CODE_MODEL_NOT_EXIST, "修改的动能点组不存在");
            }

            int result = -1;
            try
            {
                //修改方法
                string modifyFunctionGroupCommandText = @"UPDATE  AUTH_FUNCTION_GROUP 
                                           SET  FUNC_GROUP_NAME  = @func_group_name
                                              , FUNC_GROUP_PYCODE  = @func_group_pycode
                                              , FUNC_GROUP_SORTSN  = @func_group_sortsn
                                              , FUNC_APP_KEY  = @func_app_key
                                              , PARENT_FUNC_GROUP_KEY  = @parent_func_group_key
                                              , FUNC_GROUP_ACTIVE  = @func_group_active
                                              , FUNC_GROUP_COMMENT  = @func_group_comment
                                              , EXTEND  = @extend
                                         WHERE FUNC_GROUP_KEY=@func_group_key";
                using (DbCommand modifyFunctionGroupCommand = Database.GetSqlStringCommand(modifyFunctionGroupCommandText))
                {
                    Database.AddInParameter(modifyFunctionGroupCommand, "@func_group_key", System.Data.DbType.String, functionGroup.FuncGroupKey.ToString());
                    Database.AddInParameter(modifyFunctionGroupCommand, "@func_group_name", System.Data.DbType.String, functionGroup.FuncGroupName);
                    Database.AddInParameter(modifyFunctionGroupCommand, "@func_group_pycode", System.Data.DbType.String, functionGroup.FuncGroupPycode);
                    Database.AddInParameter(modifyFunctionGroupCommand, "@func_group_sortsn", System.Data.DbType.Decimal, functionGroup.FuncGroupSortsn);
                    Database.AddInParameter(modifyFunctionGroupCommand, "@func_app_key", System.Data.DbType.String, functionGroup.FuncAppKey);
                    Database.AddInParameter(modifyFunctionGroupCommand, "@parent_func_group_key", System.Data.DbType.Guid, functionGroup.ParentFuncGroupKey);
                    Database.AddInParameter(modifyFunctionGroupCommand, "@func_group_active", System.Data.DbType.Byte, functionGroup.FuncGroupActive);
                    Database.AddInParameter(modifyFunctionGroupCommand, "@func_group_comment", System.Data.DbType.String, functionGroup.FuncGroupComment);
                    Database.AddInParameter(modifyFunctionGroupCommand, "@extend", System.Data.DbType.String, functionGroup.Extend);

                    result = ExecuteNonQuery(modifyFunctionGroupCommand);
                    return result >= 0;
                }
            }

            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_MODEL_CONVERT_ERROR, "修改功能点组失败");
            }

        }

        /// <summary>
        /// 修改功能点组激活状态
        /// </summary>
        /// <param name="key">功能点组主键</param>
        /// <param name="active">激活状态</param>
        /// <returns>是否成功</returns>
        public bool UpdateFunctionGroupActive(string key, bool active)
        {

            Model.WapFunctionGroup baseModel = GetFunctionGroup(key);

            ///原模型不存在
            if (baseModel == null)
            {
                throw new WapException(StateCode.CODE_MODEL_NOT_EXIST, "修改的动能点组不存在");
            }

            int result = -1;
            try
            {
                //修改方法
                string modifyFunctionGroupCommandText = @"UPDATE  AUTH_FUNCTION_GROUP 
                                           SET  FUNC_GROUP_ACTIVE  = @func_group_active
                                         WHERE FUNC_GROUP_KEY=@func_group_key";
                using (DbCommand modifyFunctionGroupCommand = Database.GetSqlStringCommand(modifyFunctionGroupCommandText))
                {
                    Database.AddInParameter(modifyFunctionGroupCommand, "@func_group_key", System.Data.DbType.String, key);
                    Database.AddInParameter(modifyFunctionGroupCommand, "@func_group_active", System.Data.DbType.Byte, active);

                    result = ExecuteNonQuery(modifyFunctionGroupCommand);
                    return result >= 0;
                }
            }

            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_MODEL_CONVERT_ERROR, "修改功能点组激活状态失败");
            }

        }

        /// <summary>
        /// 凭功能组名获取功能组主键
        /// </summary>
        /// <param name="name">能组名</param>
        /// <returns>功能组主键</returns>
        public string GetKeyByName(string name)
        {
            try
            {
                string funcGroupKey = null;
                string queryCommandText = @"SELECT  FUNC_GROUP_KEY 
                                          FROM  AUTH_FUNCTION_GROUP 
                                          WHERE FUNC_GROUP_NAME = @func_group_name";
                using (DbCommand queryCommand = Database.GetSqlStringCommand(queryCommandText))
                {
                    Database.AddInParameter(queryCommand, "func_group_name", DbType.String, name);
                    IDataReader dr = ExecuteReader(queryCommand);
                    funcGroupKey = dr.GetReaderValue<string>("FUNC_GROUP_KEY");
                }
                return funcGroupKey;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_MODEL_CONVERT_ERROR, "凭功能组名获取功能组主键失败");
            }


        }

        /// <summary>
        /// 通过appKey和groupName获取功能点组(该功能点组是否存在)
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <param name="groupName">功能点组名称</param>
        /// <returns>存在返回true\不存在返回false</returns>
        public bool IsHaveFuncGroupByNameAndAppKey(string appKey, string groupName)
        {
            try
            {
                string sqlText = @"SELECT  *
                                          FROM  AUTH_FUNCTION_GROUP 
                                          WHERE FUNC_GROUP_NAME = @FUNC_GROUP_NAME AND FUNC_APP_KEY=@APP_KEY";

                using (DbCommand command = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(command, "@APP_KEY", System.Data.DbType.String, appKey);
                    Database.AddInParameter(command, "@FUNC_GROUP_NAME", System.Data.DbType.String, groupName);
                    var list = SelectList(command);
                    return (list == null || list.Count() == 0) ? false : true;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, " 获取功能点组对象失败");
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
    }
}

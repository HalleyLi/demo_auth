using SH3H.SDK.DataAccess.Db;
using SH3H.SDK.Definition.Exceptions;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SH3H.SharpFrame.Data;
using SH3H.WAP.Auth.Model.Dto;

namespace SH3H.WAP.Auth.DataAccess.SqlServer
{
    /// <summary>
    /// 定义角色功能点SqlServer数据库访问对象
    /// </summary>
    public class WapRoleFunctionStorage :
        BaseAccess<WapRoleFunction>, IWapRoleFunctionStorage
    {
        //构造函数
        public WapRoleFunctionStorage() : base(SH3H.SDK.Share.Consts.CONFIGURE_DATABASE_CONNECTION_STRING) { }

        public bool CreateSchema()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 凭应用Key获取角色和功能点的关联
        /// </summary>
        /// <param name="appKey">应用主键</param>
        /// <returns>功能点(附功角色)列表</returns>
        public IEnumerable<WapRoleFunction> GetAllRoleFunctionByAppKey(string appKey)
        {

            /// 因为既需要菜单对应的功能点 又需要所有拥有的权限 
            /// 所以需要额外内关联一个菜单表作为约束
            try
            {
                string commandText = @"SELECT DISTINCT ARF.RELATION_KEY
                                            ,ARF.ROLE_KEY
                                            ,ARF.FUNC_KEY
                                    FROM AUTH_ROLE_FUNCTION ARF
                                    INNER JOIN AUTH_FUNCTION AF
                                    ON AF.FUNC_KEY=ARF.FUNC_KEY
                                    INNER JOIN AUTH_FUNCTION_GROUP AFG
                                    ON AFG.FUNC_GROUP_KEY=AF.FUNC_GROUP_KEY
                                    INNER JOIN AUTH_MENU AM
                                    ON AM.APP_KEY=AFG.FUNC_APP_KEY
                                    AND AM.FUNC_KEY=ARF.FUNC_KEY
                                    WHERE AFG.FUNC_APP_KEY=@func_app_key";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "func_app_key", DbType.String, appKey);

                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "凭应用Key获取角色和功能点的关联失败");
            }
        }

        /// <summary>
        /// 凭应用Key获取角色和功能点的关联
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>功能点(附功角色)列表</returns>
        public IEnumerable<WapRoleFunction> GetAllRoleFunctionByAppIdentity(string appIdentity)
        {

            /// 因为既需要菜单对应的功能点 又需要所有拥有的权限 
            /// 所以需要额外内关联一个菜单表作为约束
            try
            {
                string commandText = @"SELECT DISTINCT ARF.RELATION_KEY
                                            ,ARF.ROLE_KEY
                                            ,ARF.FUNC_KEY
                                    FROM AUTH_ROLE_FUNCTION ARF
                                    INNER JOIN AUTH_FUNCTION AF
                                    ON AF.FUNC_KEY=ARF.FUNC_KEY
                                    INNER JOIN AUTH_FUNCTION_GROUP AFG
                                    ON AFG.FUNC_GROUP_KEY=AF.FUNC_GROUP_KEY
                                    INNER JOIN AUTH_MENU AM
                                    ON AM.APP_KEY=AFG.FUNC_APP_KEY
                                    AND AM.FUNC_KEY=ARF.FUNC_KEY
                                    INNER JOIN AUTH_APP AP
                                    ON AP.APP_KEY=AFG.FUNC_APP_KEY
                                    WHERE AP.APP_IDENTITY=@func_app_identity";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "func_app_identity", DbType.String, appIdentity);

                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, " 凭应用Key获取角色和功能点的关联失败");
            }
        }

        /// <summary>
        /// 凭关联主键获取功能点(附功角色)
        /// </summary>
        /// <param name="relationKey">关联主键</param>
        /// <returns>功能点(附功角色)</returns>
        public WapRoleFunction GetRoleFunctionByKey(string relationKey)
        {
            try
            {
                string commandText = @"SELECT RELATION_KEY
                                              ,ROLE_KEY
                                              ,FUNC_KEY
                                        FROM AUTH_ROLE_FUNCTION 
                                        WHERE RELATION_KEY=@relation_key";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "relation_key", DbType.String, relationKey);

                    return SelectSingle(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "凭关联主键获取功能点失败");
            }
        }

        /// <summary>
        /// 根据<see cref="IDataReader"/>数据读取器构建图层实体
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="funcRelative">功能点(附角色)对象</param>
        /// <returns>如果构建角色对象成功返回图层实例，否则返回NULL</returns>
        public override WapRoleFunction Build(IDataReader reader, WapRoleFunction model)
        {
            try
            {
                model.RelationKey = reader.GetReaderValue<string>("RELATION_KEY");
                model.RoleKey = reader.GetReaderValue<string>("ROLE_KEY");
                model.FuncKey = reader.GetReaderValue<string>("FUNC_KEY");
                model.RoleLevel = 0;
                model.RolePath = "";

                return model;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_MODEL_CONVERT_ERROR, "角色功能模型转换失败");
            }
        }


        /// <summary>
        /// 新增角色和功能组的关联
        /// </summary>
        /// <param name="model">功能点和角色关联模型</param>
        /// <param name="trans">事物</param>
        /// <returns>是否成功</returns>
        public bool AddRoleFunctionRelation(WapRoleFunctionDto model, DbTransaction trans)
        {
            int result = -1;
            try
            {
                string addFunctionCommandText = @"INSERT INTO AUTH_ROLE_FUNCTION
                                                               (RELATION_KEY
                                                               ,ROLE_KEY
                                                               ,FUNC_KEY)
                                                         VALUES
                                                               (@relation_key
                                                               ,@role_key
                                                               ,@func_key);";
                using (DbCommand addFunctionCommand = Database.GetSqlStringCommand(addFunctionCommandText))
                {
                    Database.AddInParameter(addFunctionCommand, "@relation_key", System.Data.DbType.String, model.RelationKey);
                    Database.AddInParameter(addFunctionCommand, "@role_key", System.Data.DbType.String, model.RoleKey);
                    Database.AddInParameter(addFunctionCommand, "@func_key", System.Data.DbType.String, model.FuncKey);

                    result = ExecuteNonQuery(addFunctionCommand, trans);
                }
                return result >= 0;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "新增角色和功能组的关联失败");
            }
        }

        /// <summary>
        /// 删除角色和功能组的关联
        /// </summary>
        /// <param name="relationkey">关联主键</param>
        /// <param name="trans">事物</param>
        /// <returns>是否成功</returns>
        public bool DeleteRoleFunctionRelation(string relationkey, DbTransaction trans)
        {
            try
            {
                string commandText = @"DELETE  AUTH_ROLE_FUNCTION  WHERE RELATION_KEY = @relation_key";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@relation_key", System.Data.DbType.String, relationkey);
                    return ExecuteNonQuery(command, trans) >= 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "删除角色和功能组的关联失败");
            }
        }

        /// <summary>
        /// 删除角色和功能组的关联
        /// </summary>
        /// <param name="rolekey">角色主键</param>
        /// <param name="funckey">功能点主键</param>
        /// <param name="trans">事物</param>
        /// <returns>是否成功</returns>
        public bool DeleteRoleFunctionRelation(string rolekey, string funckey, DbTransaction trans)
        {
            try
            {
                string commandText = @"DELETE  AUTH_ROLE_FUNCTION  
                                         WHERE ROLE_KEY = @role_key 
                                           AND FUNC_KEY = @func_key";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@role_key", System.Data.DbType.String, rolekey);
                    Database.AddInParameter(command, "@func_key", System.Data.DbType.String, funckey);
                    return ExecuteNonQuery(command, trans) >= 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "删除角色和功能组的关联失败");
            }
        }


        /// <summary>
        /// 更新角色和功能组的关联
        /// </summary>
        /// <param name="relationModel">功能点和角色关联模型</param>
        /// <returns>是否成功</returns>
        public bool UpdateRoleFunctionRelation(IEnumerable<WapRoleFunctionDto> add, IEnumerable<WapRoleFunctionDto> del)
        {
            return base.Transact(trans =>
            {
                if (del != null && del.Count() > 0)
                {
                    foreach (var item in del)
                    {
                        if (string.IsNullOrEmpty(item.RelationKey) || item.RelationKey == new Guid().ToString())
                        {
                            //凭 roleKey和funcKye删除
                            if (!DeleteRoleFunctionRelation(item.RoleKey, item.FuncKey, trans))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            //凭 relationKey 删除
                            if (!DeleteRoleFunctionRelation(item.RelationKey, trans))
                            {
                                return false;
                            }
                        }
                    }
                }

                if (add != null && add.Count() > 0)
                {
                    foreach (var item in add)
                    {
                        if (!AddRoleFunctionRelation(item, trans))
                        {
                            throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "新增角色和功能组的关联失败");
                        }
                    }
                }
                return true;
            });
        }

    }
}

using SH3H.SharpFrame.Data;
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
using SH3H.WAP.Auth.Model.Dto;
using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Share;

namespace SH3H.WAP.Auth.DataAccess.SqlServer
{
    /// <summary>
    /// 定义角色数据访问对象
    /// </summary>
    public class WapRoleStorage
        : BaseAccess<WapRole>, IWapRoleStorage
    {
        /// <summary>
        /// 构造函数
        /// </summary>        
        public WapRoleStorage()
            : base(SH3H.SDK.Share.Consts.AUTH_DATABASE_CONNECTION_STRING) { }

        #region 2.0

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="entity">角色对象</param>
        /// <returns>是否添加成功</returns>
        public WapRole CreateRole(WapRole entity)
        {
            try
            {
                string sqlText = @"INSERT INTO AUTH_ROLE (ROLE_KEY,ROLE_NAME,PARENT_ROLE_KEY,ROLE_PYCODE,ROLE_SORTSN,ROLE_ACTIVE,ROLE_COMMENT,EXTEND)
                                  VALUES(@ROLE_KEY,@ROLE_NAME,@PARENT_ROLE_KEY,@ROLE_PYCODE,@ROLE_SORTSN,@ROLE_ACTIVE,@ROLE_COMMENT,@EXTEND);";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@ROLE_KEY", DbType.String, entity.RoleKey);
                    Database.AddInParameter(cmd, "@ROLE_NAME", DbType.String, entity.RoleName);
                    Database.AddInParameter(cmd, "@PARENT_ROLE_KEY", DbType.String, entity.ParentRoleKey);
                    Database.AddInParameter(cmd, "@ROLE_PYCODE", DbType.String, entity.RolePycode);
                    Database.AddInParameter(cmd, "@ROLE_SORTSN", DbType.Int32, entity.RoleSortsn);
                    Database.AddInParameter(cmd, "@ROLE_ACTIVE", DbType.Boolean, entity.RoleActive);
                    Database.AddInParameter(cmd, "@ROLE_COMMENT", DbType.String, entity.RoleComment);
                    Database.AddInParameter(cmd, "@EXTEND", DbType.String, entity.Extend);
                    ExecuteNonQuery(cmd);
                    return entity;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "新增角色失败");
            }
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WapRole UpdateRole(string roleKey, WapRole entity)
        {
            try
            {
                string sqlText = @" UPDATE AUTH_ROLE 
                      SET ROLE_NAME=@ROLE_NAME,
                          PARENT_ROLE_KEY=@PARENT_ROLE_KEY,
                          ROLE_PYCODE=@ROLE_PYCODE ,
                          ROLE_SORTSN=@ROLE_SORTSN,
                          ROLE_ACTIVE=@ROLE_ACTIVE ,
                          ROLE_COMMENT=@ROLE_COMMENT ,
                          EXTEND=@EXTEND
                          WHERE ROLE_KEY=@ROLE_KEY";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@ROLE_KEY", DbType.String, roleKey);
                    Database.AddInParameter(cmd, "@ROLE_NAME", DbType.String, entity.RoleName);
                    Database.AddInParameter(cmd, "@PARENT_ROLE_KEY", DbType.String, entity.ParentRoleKey);
                    Database.AddInParameter(cmd, "@ROLE_PYCODE", DbType.String, entity.RolePycode);
                    Database.AddInParameter(cmd, "@ROLE_SORTSN", DbType.Int32, entity.RoleSortsn);
                    Database.AddInParameter(cmd, "@ROLE_ACTIVE", DbType.Boolean, entity.RoleActive);
                    Database.AddInParameter(cmd, "@ROLE_COMMENT", DbType.String, entity.RoleComment);
                    Database.AddInParameter(cmd, "@EXTEND", DbType.String, entity.Extend);

                    ExecuteNonQuery(cmd);
                    return entity;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "修改角色失败");
            }
        }

        /// <summary>
        /// 修改角色信息服务
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="activeState"></param>
        /// <returns></returns>
        public bool UpdateRoleState(string roleKey, bool activeState)
        {
            try
            {
                string sqlText = @"UPDATE AUTH_ROLE 
                                   SET ROLE_ACTIVE=@ROLE_ACTIVE WHERE ROLE_KEY=@ROLE_KEY";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@ROLE_ACTIVE", DbType.Boolean, activeState);
                    Database.AddInParameter(cmd, "@ROLE_KEY", DbType.String, roleKey);
                    return ExecuteNonQuery(cmd) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "新增角色状态失败");
            }
        }

        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns>角色信息对象</returns>
        public IEnumerable<Model.WapRole> GetAllRoles()
        {
            try
            {
                string commandText = @"SELECT * FROM AUTH_ROLE";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "获取所有角色信息失败");
            }
        }

        /// <summary>
        /// 根据角色KEY查询角色
        /// </summary>
        /// <param name="roleKey">角色KEY</param>
        /// <returns>角色对象</returns>
        public WapRole GetRoleByRoleKey(string roleKey)
        {
            try
            {
                string commandText = @"SELECT * FROM AUTH_ROLE WHERE ROLE_KEY = @ROLR_KEY";

                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@ROLR_KEY", DbType.String, roleKey);
                    return SelectSingle(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "根据角色KEY查询角色失败");
            }
        }

        /// <summary>
        /// 根据用户KEY查询该用户所关联的所有角色
        /// </summary>
        /// <param name="userKey">用户KEY</param>
        /// <returns>返回角色对象列表</returns>
        public IEnumerable<WapRole> GetRolesByUserKey(string userKey)
        {
            try
            {
                string commandText = @"SELECT * FROM AUTH_ROLE WHERE ROLE_KEY IN (SELECT ROLE_KEY FROM AUTH_USER_ROLE WHERE USER_KEY=@USER_KEY)";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@USER_KEY", DbType.String, userKey);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "根据用户KEY查询该用户所关联的所有角色失败");
            }
        }

        /// <summary>
        /// 根据角色拼音码查询角色
        /// </summary>
        /// <param name="pyCode">角色拼音码</param>
        /// <returns>角色对象</returns>
        public WapRole GetRoleByPyCode(string pyCode)
        {
            try
            {
                string commandText = @"SELECT * FROM AUTH_ROLE WHERE ROLE_PYCODE = @ROLE_PYCODE";

                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@ROLE_PYCODE", DbType.String, pyCode);
                    return SelectSingle(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "根据角色拼音码查询角色失败");
            }
        }

        /// <summary>
        /// 获取指定角色名的角色
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <returns>角色对象</returns>
        public WapRole GetRoleByRoleName(string roleName)
        {
            try
            {
                string commandText = @"SELECT * FROM AUTH_ROLE WHERE ROLE_NAME = @ROLE_NAME";

                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@ROLE_NAME", DbType.String, roleName);
                    return SelectSingle(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "获取指定角色名的角色失败");
            }
        }

        /// <summary>
        /// 根据角色标识更新父角色标识
        /// </summary>
        /// <param name="roleKey">角色标识</param>
        /// <param name="parentRoleKey">父角色标识</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateParentRoleKeyByRoleKey(string roleKey, string parentRoleKey)
        {
            try
            {
                string sqlCommand = @"UPDATE AUTH_ROLE 
                                        SET PARENT_ROLE_KEY=@PARENT_ROLE_KEY
                                      WHERE ROLE_KEY=@ROLE_KEY";
                using (DbCommand command = Database.GetSqlStringCommand(sqlCommand))
                {
                    Database.AddInParameter(command, "@PARENT_ROLE_KEY", DbType.String, parentRoleKey);
                    Database.AddInParameter(command, "@ROLE_KEY", DbType.String, roleKey);
                    return ExecuteNonQuery(command) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "根据角色标识更新父角色标识失败");
            }

        }

        /// <summary>
        /// 更新用户对应角色关系
        /// </summary>
        /// <param name="userKey">用户key</param>
        /// <param name="deleteArr">待删除的关系</param>
        /// <param name="AddArr">待添加的关系</param>
        /// <returns>是否更新成功</returns>
        public bool CreateOrUpdateUserRoleRelation(string userKey, IEnumerable<WapRoleUserRelationDto> deleteArr, IEnumerable<WapRoleUserRelationDto> AddArr)
        {
            return base.Transact(transact =>
                {
                    //删除角色关系
                    string sqlText = "DELETE FROM AUTH_USER_ROLE WHERE ROLE_KEY =@ROLE_KEY AND USER_KEY = @USER_KEY";

                    if (deleteArr.Count() > 0)
                    {
                        foreach (var item in deleteArr)
                        {
                            try
                            {
                                using (var cmd = Database.GetSqlStringCommand(sqlText))
                                {
                                    Database.AddInParameter(cmd, "@ROLE_KEY", DbType.String, item.RoleKey);
                                    Database.AddInParameter(cmd, "@USER_KEY", DbType.String, userKey);
                                    var result = ExecuteNonQuery(cmd);
                                    if (result <= 0)
                                    {
                                        return false;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                LogManager.Get().Throw(ex);
                                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "用户角色关系更新失败");
                            }
                        }
                    }

                    //添加的角色关系
                    string addSqlText = "INSERT INTO AUTH_USER_ROLE (RELATION_KEY,USER_KEY,ROLE_KEY) VALUES (@RELATION_KEY,@USER_KEY,@ROLE_KEY)";

                    if (AddArr.Count() > 0)
                    {
                        foreach (var arr in AddArr)
                        {
                            try
                            {
                                using (var cmd = Database.GetSqlStringCommand(addSqlText))
                                {
                                    Database.AddInParameter(cmd, "@RELATION_KEY", DbType.String, arr.RelationKey);
                                    Database.AddInParameter(cmd, "@USER_KEY", DbType.String, userKey);
                                    Database.AddInParameter(cmd, "@ROLE_KEY", DbType.String, arr.RoleKey);
                                    var result = ExecuteNonQuery(cmd);
                                    if (result <= 0)
                                    {
                                        return false;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                LogManager.Get().Throw(ex);
                                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "用户角色关系更新失败");
                            }
                        }
                    }
                    return true;
                });
        }

        /// <summary>
        /// 角色模糊查询
        /// </summary>
        /// <param name="searchText">模糊查询关键字</param>
        /// <returns>角色列表信息</returns>
        public IEnumerable<WapRole> FuzzySearch(string searchText)
        {
            try
            {
                string commandText = @"SELECT * FROM AUTH_ROLE WHERE　ROLE_PYCODE LIKE N'%'+@TEXT+'%' 
                                                OR ROLE_NAME LIKE N'%'+@TEXT+'%' ";

                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@TEXT", DbType.String, searchText);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        /// <summary>
        /// 根据<see cref="IDataReader"/>数据读取器构建图层实体
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="role">角色对象</param>
        /// <returns>如果构建角色对象成功返回图层实例，否则返回NULL</returns>
        public override WapRole Build(IDataReader reader, WapRole role)
        {
            try
            {
                role.RoleKey = reader.GetReaderValue<string>("ROLE_KEY");
                role.RoleName = reader.GetReaderValue<string>("ROLE_NAME");
                role.RolePycode = reader.GetReaderValue<string>("ROLE_PYCODE");
                role.ParentRoleKey = reader.GetReaderValue<string>("PARENT_ROLE_KEY");
                role.RoleActive = reader.GetReaderValue<bool>("ROLE_ACTIVE");
                //role.ParentRoleName = reader.GetReaderValue<string>("parent_role_name", null, true);
                role.RoleComment = reader.GetReaderValue<string>("ROLE_COMMENT", null, true);
                role.RoleSortsn = reader.GetReaderValue<int>("ROLE_SORTSN");
                role.Extend = reader.GetReaderValue<string>("EXTEND", null, true);
                return role;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_MODEL_CONVERT_ERROR, "角色模型转换失败");
            }
        }
        #endregion


        #region 1.6

        /// <summary>
        /// 激活角色
        /// </summary>
        /// <param name="roleKey"></param>
        /// <returns></returns>
        public bool ActiveRole(string roleKey)
        {
            try
            {
                string sqlText = @"UPDATE auth_role 
                                   SET role_active=1 WHERE role_key=@role_key";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@role_key", DbType.String, roleKey);
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
        /// 禁用角色
        /// </summary>
        /// <param name="roleKey"></param>
        /// <returns></returns>
        public bool DeActiveRole(string roleKey)
        {
            try
            {
                string sqlText = @"UPDATE auth_role 
                                   SET role_active=0 WHERE role_key=@role_key";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@role_key", DbType.String, roleKey);
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
        /// 更新角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateRole(WapRole entity)
        {
            try
            {
                string sqlText = @" UPDATE AUTH_ROLE 
                      SET ROLE_NAME=@ROLE_NAME,
                          PARENT_ROLE_KEY=@PARENT_ROLE_KEY,
                          ROLE_PYCODE=@ROLE_PYCODE ,
                          ROLE_SORTSN=@ROLE_SORTSN,
                          ROLE_ACTIVE=@ROLE_ACTIVE ,
                          ROLE_COMMENT=@ROLE_COMMENT ,
                          EXTEND=@EXTEND
                          WHERE ROLE_KEY=@ROLE_KEY";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@ROLE_KEY", DbType.String, entity.RoleKey);
                    Database.AddInParameter(cmd, "@ROLE_NAME", DbType.String, entity.RoleName);
                    Database.AddInParameter(cmd, "@PARENT_ROLE_KEY", DbType.String, entity.ParentRoleKey);
                    Database.AddInParameter(cmd, "@ROLE_PYCODE", DbType.String, entity.RolePycode);
                    Database.AddInParameter(cmd, "@ROLE_SORTSN", DbType.Int32, entity.RoleSortsn);
                    Database.AddInParameter(cmd, "@ROLE_ACTIVE", DbType.Boolean, entity.RoleActive);
                    Database.AddInParameter(cmd, "@ROLE_COMMENT", DbType.String, entity.RoleComment);
                    Database.AddInParameter(cmd, "@EXTEND", DbType.String, entity.Extend);

                    ExecuteNonQuery(cmd);
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }

        /// <summary>
        /// 统一更新角色排序值
        /// </summary>
        /// <param name="sortIndexes">角色编号索引号字典</param>
        public bool UpdateSortIndexByKey(Dictionary<string, int> sortIndexes)
        {
            return base.Transact(trans =>
            {
                foreach (KeyValuePair<string, int> index in sortIndexes)
                {
                    if (!UpdateIndexByRoleKey(index.Key, index.Value))
                        return false;
                }
                return true;
            });
        }

        /// <summary>
        /// 根据角色KEY更新角色排序值
        /// </summary>
        /// <param name="roleKey">角色关键码</param>
        /// <param name="sortIndex">排序索引值</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateIndexByRoleKey(string roleKey, int sortIndex)
        {
            try
            {
                string sqlText = @"UPDATE auth_role 
                                   SET role_sortsn=@role_sortsn
                                   WHERE role_key=@role_key";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@role_sortsn", DbType.Int32, sortIndex);
                    Database.AddInParameter(cmd, "@role_key", DbType.String, roleKey);
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
        /// 根据用户编号查询该用户所关联的所有角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回角色对象列表</returns>
        public IEnumerable<WapRole> GetRolesByUserId(int userId)
        {
            try
            {
                string commandText = @"SELECT pr.role_name as parent_role_name,t_role.role_name,t_role.role_key,t_role.parent_role_key,t_role.ROLE_PYCODE
                                       FROM (SELECT r.* FROM auth_role r
                                            INNER JOIN auth_user_role ur ON r.role_key = ur.role_key
                                            INNER JOIN auth_user u ON u.user_key = ur.user_key
                                            WHERE u.user_id = @UserId) t_role
                                        left join auth_role pr  on pr.role_key=t_role.parent_role_key
                                         ORDER BY t_role.role_sortsn";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@UserId", DbType.String, userId);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        /// <summary>
        /// 激活角色状态
        /// </summary>
        /// <param name="roleKeys">角色标识符列表</param>
        /// <param name="trans">事务对象</param>
        /// <returns>角色状态是否激活成功</returns>
        public bool ActiveRole(IEnumerable<string> roleKeys, DbTransaction trans = null)
        {
            try
            {
                string roleKeysStr = ConvertArrayToString(roleKeys);
                string sqlText = @"UPDATE auth_role 
                                   SET role_active=1 WHERE ";
                string sqlwhere = "";
                if (roleKeys.Count() == 1)
                    sqlwhere = "role_key=@role_key";
                else if (roleKeys.Count() > 1)
                    sqlwhere = "role_key in (@role_key)";
                else
                    sqlText = "";

                sqlText += sqlwhere;
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@role_key", DbType.String, roleKeysStr);
                    return ExecuteNonQuery(cmd, trans) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }

        /// <summary>
        /// 禁用角色状态
        /// </summary>
        /// <param name="roleKeys">角色标识符列表</param>
        /// <param name="trans">事务对象</param>
        /// <returns>角色状态是否禁用成功</returns>
        public bool DeactiveRole(IEnumerable<string> roleKeys, DbTransaction trans = null)
        {
            try
            {
                string roleKeysStr = ConvertArrayToString(roleKeys);
                string sqlText = @"UPDATE auth_role 
                                   SET role_active=0 WHERE ";
                string sqlwhere = "";
                if (roleKeys.Count() == 1)
                    sqlwhere = "role_key=@role_key";
                else if (roleKeys.Count() > 1)
                    sqlwhere = "role_key in (@role_key)";
                else
                    sqlText = "";

                sqlText += sqlwhere;
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@role_key", DbType.String, roleKeysStr);
                    return ExecuteNonQuery(cmd, trans) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }

        /// <summary>
        /// 根据用户编号查询该用户所关联的所有角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回角色树对象列表</returns>
        public IEnumerable<Model.Dto.WapRoleOutputDto> GetRolesOutputByUserId(int userId)
        {
            return BindTree(GetAllRolesByUserId(userId), userId);
        }

        public IEnumerable<Model.Dto.WapRoleRelativeDto> getAllRoleRelations()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取某一用户所关联的角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IEnumerable<Model.WapRole> GetAllRolesByUserId(int userId)
        {
            try
            {
                string commandText = @"SELECT * FROM auth_role WHERE role_key in 
                                        (SELECT role_key FROM　auth_user_role WHERE user_key in (
                                            SELECT user_key FROM auth_user WHERE user_id=@UserId))";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@UserId", DbType.Int32, userId);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        /// <summary>
        /// 递归获取树形结构
        /// </summary>
        /// <param name="rolelist">数据源</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        private List<Model.Dto.WapRoleOutputDto> BindTree(IEnumerable<Model.WapRole> rolelist, int userid)
        {
            if (rolelist != null && rolelist.Count() > 0)
            {
                List<Model.Dto.WapRoleOutputDto> modelList = new List<Model.Dto.WapRoleOutputDto>(rolelist.Count());
                foreach (var waprole in rolelist)
                {
                    Model.Dto.WapRoleOutputDto model = new Model.Dto.WapRoleOutputDto();
                    string currentKey = waprole.RoleKey.ToString();//当前角色Key
                    model.RoleKey = currentKey;
                    model.RoleName = waprole.RoleName;
                    model.ParentRoleKey = waprole.ParentRoleKey;
                    model.RolePycode = waprole.RolePycode;
                    model.RoleSortsn = waprole.RoleSortsn;
                    model.RoleActive = waprole.RoleActive;
                    model.RoleComment = waprole.RoleComment;
                    model.Extend = waprole.Extend;
                    var childrolelist = GetChildRoles(currentKey, userid);
                    if (childrolelist != null && childrolelist.Count() > 0)
                    {
                        model.Roles = BindTree(childrolelist, userid);
                    }
                    modelList.Add(model);
                }
                return modelList;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取某一父节点下的所有子节点
        /// </summary>
        /// <param name="parentRoleKey"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        private IEnumerable<Model.WapRole> GetChildRoles(string parentRoleKey, int userid)
        {
            try
            {
                string commandText = @"SELECT * FROM auth_role WHERE parent_role_key=@ParentRoleKey AND role_key in 
                                        (SELECT role_key FROM　auth_user_role WHERE user_key in (
                                            SELECT user_key FROM auth_user WHERE user_id=@UserId)) order by ROLE_SORTSN";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@ParentRoleKey", DbType.String, parentRoleKey);
                    Database.AddInParameter(command, "@UserId", DbType.Int32, userid);
                    return SelectList(command);
                }
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

using SH3H.SDK.DataAccess.Db;
using SH3H.SDK.Definition.Exceptions;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.SDK.Share;
using SH3H.SharpFrame.Data;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
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
    /// 定义用户数据访问
    /// </summary>
    public class WapUserStorage 
        : BaseAccess<WapUser>, IWapUserStorage
    {
        /// <summary>
        /// 构造函数
        /// </summary>        
        public WapUserStorage()
            : base(Consts.AUTH_DATABASE_CONNECTION_STRING) { }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns>用户对象</returns>
        public WapUser Insert(WapUser user)
        {
            try
            {
                string commandText = @"INSERT AUTH_USER (USER_KEY,
                                                         USER_ID,
                                                         USER_NAME,
                                                         USER_WORKNO,
                                                         USER_ACCOUNT,
                                                         USER_PWD,
                                                         USER_PYCODE,
                                                         USER_SORTSN,
                                                         USER_ACTIVE,
                                                         USER_COMMENT,
                                                         USER_PHONE,
                                                         USER_CELLPHONE,
                                                         USER_EMAIL,
                                                         USER_IDCARD,
                                                         USER_BIRTHDATE,
                                                         USER_SEX,
                                                         USER_ADDRESS,
                                                         USER_POSTNUM,
                                                         EXTEND,
                                                         USER_DOMAIN_ACCOUNT,
                                                         FILE_HASH)
                                                 VALUES(@USER_KEY,
                                                        @USER_ID,
                                                        @USER_NAME,
                                                        @USER_WORKNO,
                                                        @USER_ACCOUNT,
                                                        @USER_PWD,
                                                        @USER_PYCODE,
                                                        @USER_SORTSN,
                                                        @USER_ACTIVE,
                                                        @USER_COMMENT,
                                                        @USER_PHONE,
                                                        @USER_CELLPHONE,
                                                        @USER_EMAIL,
                                                        @USER_IDCARD,
                                                        @USER_BIRTHDATE,
                                                        @USER_SEX,
                                                        @USER_ADDRESS,
                                                        @USER_POSTNUM,
                                                        @EXTEND,
                                                        @USER_DOMAIN_ACCOUNT,
                                                        @FILE_HASH)";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@USER_KEY", DbType.String, user.UserKey);
                    Database.AddInParameter(command, "@USER_ID", DbType.String, user.Id);
                    Database.AddInParameter(command, "@USER_NAME", DbType.String, user.Name);
                    Database.AddInParameter(command, "@USER_WORKNO", DbType.String, user.JobNumber);
                    Database.AddInParameter(command, "@USER_ACCOUNT", DbType.String, user.Account);
                    Database.AddInParameter(command, "@USER_PWD", DbType.String, user.Password);
                    Database.AddInParameter(command, "@USER_PYCODE", DbType.String, user.Code);
                    Database.AddInParameter(command, "@USER_SORTSN", DbType.Decimal, user.SortSn);
                    Database.AddInParameter(command, "@USER_ACTIVE", DbType.Boolean, user.Active);
                    Database.AddInParameter(command, "@USER_COMMENT", DbType.String, user.Comment);
                    Database.AddInParameter(command, "@USER_PHONE", DbType.String, user.Phone);
                    Database.AddInParameter(command, "@USER_CELLPHONE", DbType.String, user.Cellphone);
                    Database.AddInParameter(command, "@USER_EMAIL", DbType.String, user.Email);
                    Database.AddInParameter(command, "@USER_IDCARD", DbType.String, user.IdCard);
                    Database.AddInParameter(command, "@USER_BIRTHDATE", DbType.DateTime, SH3H.WAP.Share.Utils.UTCToDateTime(user.Birthday));
                    Database.AddInParameter(command, "@USER_SEX", DbType.Int32, user.Sex);
                    Database.AddInParameter(command, "@USER_ADDRESS", DbType.String, user.Address);
                    Database.AddInParameter(command, "@USER_POSTNUM", DbType.String, user.PostNo);
                    Database.AddInParameter(command, "@EXTEND", DbType.String, user.Extend);
                    Database.AddInParameter(command, "@USER_DOMAIN_ACCOUNT", DbType.String, user.DomainAccount);
                    Database.AddInParameter(command, "@FILE_HASH", DbType.String, user.FileHash);
                    if (ExecuteNonQuery(command) > 0)
                        return user;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "添加用户信息失败");
            }           
        }


        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userKey">KEY</param>
        /// <param name="user">用户对象</param>
        /// <returns>返回是否更新成功</returns>
        public WapUser UpdateUser(string userKey ,WapUser user)
        {
            try
            {
                string commandText = @"update AUTH_USER 
                                        set USER_NAME=@USER_NAME,
                                            USER_WORKNO =@USER_WORKNO,
                                            USER_ACCOUNT=@USER_ACCOUNT,
                                            USER_ACTIVE=@USER_ACTIVE,
                                            USER_PHONE=@USER_PHONE,
                                            USER_CELLPHONE=@USER_CELLPHONE,
                                            USER_EMAIL=@USER_EMAIL,
                                            USER_IDCARD=@USER_IDCARD,
                                            USER_BIRTHDATE=@USER_BIRTHDATE,
                                            USER_SEX=@USER_SEX,
                                            USER_ADDRESS=@USER_ADDRESS,
                                            USER_POSTNUM=@USER_POSTNUM,
                                            USER_COMMENT=@USER_COMMENT,
                                            EXTEND=@EXTEND,
                                            FILE_HASH=@FILE_HASH
                                      where USER_KEY = @USER_KEY";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@USER_NAME", DbType.String, user.Name);
                    Database.AddInParameter(command, "@USER_WORKNO", DbType.String, user.JobNumber);
                    Database.AddInParameter(command, "@USER_ACCOUNT", DbType.String, user.Account);
                    Database.AddInParameter(command, "@USER_ACTIVE", DbType.Boolean, user.Active);
                    Database.AddInParameter(command, "@USER_PHONE", DbType.String, user.Phone);
                    Database.AddInParameter(command, "@USER_CELLPHONE", DbType.String, user.Cellphone);
                    Database.AddInParameter(command, "@USER_EMAIL", DbType.String, user.Email);
                    Database.AddInParameter(command, "@USER_IDCARD", DbType.String, user.IdCard);
                    Database.AddInParameter(command, "@USER_BIRTHDATE", DbType.DateTime, SH3H.WAP.Share.Utils.UTCToDateTime(user.Birthday));
                    Database.AddInParameter(command, "@USER_SEX", DbType.Int32, user.Sex);
                    Database.AddInParameter(command, "@USER_ADDRESS", DbType.String, user.Address);
                    Database.AddInParameter(command, "@USER_POSTNUM", DbType.String, user.PostNo);
                    Database.AddInParameter(command, "@USER_COMMENT", DbType.String, user.Comment);
                    Database.AddInParameter(command, "@EXTEND", DbType.String, user.Extend);
                    Database.AddInParameter(command, "@USER_KEY", DbType.String, userKey);
                    Database.AddInParameter(command, "@FILE_HASH", DbType.String, user.FileHash);
                    int line = ExecuteNonQuery(command);
                    if (line <= 0)
                    {
                        throw new WapException(SH3H.WAP.Share.StateCode.CODE_APP_INEXIST, "应用标识不存在");
                    }
                }

                string commandText_select = @"SELECT * FROM AUTH_USER WHERE USER_KEY = @USER_KEY";
                using (DbCommand command_select = Database.GetSqlStringCommand(commandText_select))
                {
                    Database.AddInParameter(command_select, "@USER_KEY", DbType.String, userKey);
                    var list = SelectList(command_select);
                    return list.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "更新用户信息失败");
            }            
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns>用户对象列表</returns>
        public IEnumerable<WapUser> GetAllUsers()
        {
            try
            {
                string commandText = @"SELECT * FROM  AUTH_USER WHERE USER_ACCOUNT != 'root'";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "获取所有用户失败");
            }          
        }

        /// <summary>
        /// 根据账号返回用户信息
        /// </summary>
        /// <param name="userKey">用户KEY</param>
        /// <returns>用户信息</returns>
        public WapUser GetUserByUserKey(string userKey)
        {
            try
            {
                string commandText = @"SELECT * FROM  AUTH_USER 
                                       WHERE USER_KEY = @USER_KEY ";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@USER_KEY", DbType.String, userKey);
                    var list = SelectList(command);
                    if (list.Count() <= 0)
                    {
                        throw new WapException(SH3H.WAP.Share.StateCode.CODE_USER_UNKNOWN, "未知的用户");
                    }
                    return list.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "根据userKey获得用户信息失败");
            }           
        }

        /// <summary>
        /// 根据用户编号获取用户信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>用户对象</returns>
        public WapUser GetUserByUserId(int userId)
        {
            try
            {
                string commandText = @"SELECT * FROM AUTH_USER 
                                       WHERE USER_ID = @USER_ID ";

                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@USER_ID", DbType.Int32, userId);
                    var list = SelectList(command);
                    if (list.Count() <= 0)
                    {
                        throw new WapException(SH3H.WAP.Share.StateCode.CODE_USER_UNKNOWN, "未知的用户");
                    }
                    return list.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "根据用户编号获取用户信息失败");
            }           
        }

        /// <summary>
        /// 根据角色KEY获取用户信息
        /// </summary>
        /// <param name="roleKey">角色KEY</param>
        /// <returns>用户对象列表</returns>
        public IEnumerable<WapUser> SelectUsersByRoleKey(string roleKey)
        {
            try
            {
                string commandText = @"SELECT users.* FROM AUTH_USER users
                                LEFT JOIN AUTH_USER_ROLE relation ON relation.USER_KEY=users.USER_KEY
                                LEFT JOIn AUTH_ROLE roles ON roles.ROLE_KEY=relation.ROLE_KEY
                                WHERE roles.ROLE_KEY=@ROLE_KEY ";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@ROLE_KEY", DbType.String, roleKey);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "根据角色KEY获取用户信息失败");
            }          
        }

         /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userKey">用户KEY</param>
        /// <param name="reset">是否重置，重置则将密码设为"0000",否则修改密码</param>
        /// <param name="dto">新旧密码信息</param>
        /// <returns>修改成功返回true.否则返回false.</returns>
        public bool UpdatePassword(string userKey, bool reset, ChangePasswordInputDto dto)
        {
            try
            {
                string commandText_checkSame = "SELECT USER_PWD FROM AUTH_USER WHERE USER_KEY = @USER_KEY";
                using (DbCommand command_checkSame = Database.GetSqlStringCommand(commandText_checkSame))
                {
                    Database.AddInParameter(command_checkSame, "@USER_KEY", DbType.String, userKey);
                    IDataReader reader = ExecuteReader(command_checkSame);
                    if (reader.Read())
                    {
                        string pwd = reader["USER_PWD"].ToString();
                        if (pwd != dto.OldPassword)
                            throw new WapException(SH3H.WAP.Share.StateCode.CODE_PASSWORD_ERROR, "旧密码输入错误!");
                    }
                    else
                    {
                        throw new WapException(SH3H.WAP.Share.StateCode.CODE_ACCOUNT_INEXIST, "账号不存在!");
                    }
                }
                string commandText = @"UPDATE AUTH_USER 
                                SET USER_PWD=@newPassword
                                where USER_KEY = @USER_KEY and USER_PWD=@oldPassword";

                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@newPassword", DbType.String, reset ? "0000" : dto.NewPassword);
                    Database.AddInParameter(command, "@oldPassword", DbType.String, dto.OldPassword);
                    Database.AddInParameter(command, "@USER_KEY", DbType.String, userKey);
                    int line = ExecuteNonQuery(command);
                    return line > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(WapException))
                    throw new WapException(ex.GetHashCode(), ex.Message);
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "修改用户密码失败");
            }           
        }

        /// <summary>
        /// 根据userKey修改用户状态
        /// </summary>
        /// <param name="userKey">用户Key</param>
        /// <param name="state">用户状态信息</param>
        /// <returns>更新成功返回true.否则返回false.</returns>
        public bool UpdateUserStateByUserKey(string userKey, WapUserState state)
        {
            try
            {
                string commandText = @"UPDATE AUTH_USER SET USER_ACTIVE = @USER_ACTIVE WHERE USER_KEY = @USER_KEY ";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@USER_ACTIVE", DbType.Boolean, state.Active);
                    Database.AddInParameter(command, "@USER_KEY", DbType.String, userKey);

                    int line = ExecuteNonQuery(command);
                    if (line <= 0)
                    {
                        throw new WapException(SH3H.WAP.Share.StateCode.CODE_USER_UNKNOWN, "未知的用户");
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "根据userKey修改用户状态失败");
            }           
        }

        /// <summary>
        /// 根据组织KEY获取指定的用户
        /// </summary>
        /// <param name="organizationKey">组织KEY</param>
        /// <returns>返回用户列表.</returns>
        public IEnumerable<WapUser> SelectUserByOrganizationKey(string organizationKey)
        {
            try
            {
                string commandText = @"  SELECT * FROM AUTH_USER_ORGANIZATION O 
                                     LEFT JOIN AUTH_USER A ON O.USER_KEY = A.USER_KEY
                                     WHERE O.ORGANIZATION_KEY = @ORGANIZATION_KEY ";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@ORGANIZATION_KEY", DbType.String, organizationKey);

                    var list = SelectList(command);
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "根据组织KEY获取指定的用户失败");
            }           
        }

        /// <summary>
        /// 根据账号获得用户信息列表
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns>用户信息</returns>
        public IEnumerable<WapUser> SelectUserByAccount(string account)
        {           
            try
            {
                string commandText = @"SELECT * FROM  AUTH_USER 
                                       WHERE USER_ACCOUNT = @USER_ACCOUNT ";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@USER_ACCOUNT", DbType.String, account);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "根据账号获得用户信息列表失败");
            }
        }

        /// <summary>
        /// 根据用户Key返回用户信息
        /// </summary>
        /// <param name="userKey">用户KEY</param>
        /// <returns>用户信息</returns>
        public IEnumerable<WapUser> SelectUserByUserKey(string userKey)
        {           
            try
            {
                string commandText = @"SELECT * FROM  AUTH_USER 
                                       WHERE USER_KEY = @USER_KEY ";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@USER_KEY", DbType.String, userKey);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "根据用户Key返回用户信息失败");
            }
        }

        /// <summary>
        /// 删除用户角色关系
        /// </summary>
        /// <param name="userKey">用户KEY</param>
        /// <param name="roleKey">角色KEY</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public bool DeleteRoleUserRelation(string userKey, string roleKey, System.Data.Common.DbTransaction trans = null) 
        {            
            try
            {
                string commandText = @"DELETE FROM AUTH_USER_ROLE WHERE ROLE_KEY =@ROLE_KEY AND USER_KEY = @USER_KEY";

                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@USER_KEY", DbType.String, userKey);
                    Database.AddInParameter(command, "@ROLE_KEY", DbType.String, roleKey);
                    return ExecuteNonQuery(command) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "删除用户角色关系失败");
            }
        }

        /// <summary>
        /// 新增用户角色关系
        /// </summary>
        /// <param name="userKey">用户KEY</param>
        /// <param name="roleKey">角色KEY</param>
        /// <param name="relationKey">关系KEY</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public bool AddRoleUserRelation(string userKey, string roleKey, string relationKey, System.Data.Common.DbTransaction trans = null)
        {
            try
            {
                string commandText = @"INSERT INTO AUTH_USER_ROLE(USER_KEY,ROLE_KEY,RELATION_KEY)
                                    SELECT A.USER_KEY,A.ROLE_KEY,A.RELATION_KEY
                                    FROM (
	                                    SELECT TOP 0 NULL AS USER_KEY,NULL AS ROLE_KEY,NULL AS RELATION_KEY
	                                    UNION
	                                    SELECT @USER_KEY,@ROLE_KEY,@RELATION_KEY
                                    ) A LEFT JOIN AUTH_USER_ROLE B ON A.USER_KEY = B.USER_KEY AND A.ROLE_KEY = B.ROLE_KEY
                                    WHERE B.RELATION_KEY IS NULL";

                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@USER_KEY", DbType.String, userKey);
                    Database.AddInParameter(command, "@ROLE_KEY", DbType.String, roleKey);
                    Database.AddInParameter(command, "@RELATION_KEY", DbType.String, relationKey);
                    return ExecuteNonQuery(command) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "新增用户角色关系失败");
            }
        }

        /// <summary>
        /// 更新用户角色信息
        /// </summary>
        /// <param name="deleteArr">删除的用户角色关系</param>
        /// <param name="addArr">新增的用户角色关系</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateRoleUserRelation(IEnumerable<RoleUserRelation> deleteArr, IEnumerable<RoleUserRelation> addArr)
        {
            return base.Transact(trans =>
            {
                if (deleteArr != null)
                {
                    foreach (var del in deleteArr)
                    {
                        if (!DeleteRoleUserRelation(del.UserKey, del.RoleKey, trans))
                            return false;
                    }
                }
                if (addArr != null)
                {
                    foreach (var add in addArr)
                    {
                        if (!AddRoleUserRelation(add.UserKey, add.RoleKey, add.RelationKey, trans))
                            return false;
                    }
                }
                return true;
            });
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="userkey">用户KEY</param>
        /// <param name="userName">用户名称</param>
        /// <returns>false为不存在,true为存在</returns>
        public bool IsNameExist(string userkey, string userName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                Dictionary<string, object> paramArr = new Dictionary<string, object>();

                sb.Append(@"SELECT TOP 1 USER_KEY FROM AUTH_USER WHERE USER_ACTIVE = 1 AND USER_NAME =@USER_NAME ");
                paramArr.Add("@USER_NAME", userName);

                if (!string.IsNullOrEmpty(userkey))
                {
                    sb.Append("and USER_KEY <>@USER_KEY");
                    paramArr.Add("@USER_KEY", userkey);
                }
                string sqlText = sb.ToString();
                using (DbCommand command = Database.GetSqlStringCommand(sqlText))
                {
                    var result = SelectList(command);
                    bool blnExist = result.Count() == 0 ? false : true;
                    return blnExist;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "检查用户名是否存在时失败");
            }           
        }

        /// <summary>
        /// 检查帐户名是否存在
        /// </summary>
        /// <param name="userkey">用户KEY</param>
        /// <param name="account">账号</param>
        /// <returns>false为不存在,true为存在</returns>
        public bool IsAccountExist(string userkey, string account)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                Dictionary<string, object> paramArr = new Dictionary<string, object>();

                sb.Append(@"SELECT TOP 1 USER_KEY FROM AUTH_USER WHERE USER_ACTIVE = 1 AND USER_ACCOUNT =@USER_ACCOUNT ");
                paramArr.Add("@USER_ACCOUNT", account);

                if (!string.IsNullOrEmpty(userkey))
                {
                    sb.Append("and USER_KEY <>@USER_KEY");
                    paramArr.Add("@USER_KEY", userkey);
                }

                string sqlText = sb.ToString();
                using (DbCommand command = Database.GetSqlStringCommand(sqlText))
                {
                    var result = SelectList(command);
                    bool blnExist = result.Count() == 0 ? false : true;
                    return blnExist;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "检查帐户名是否存在时失败");
            }           
        }

        /// <summary>
        /// 检查工号是否存在
        /// </summary>
        /// <param name="userkey">用户KEY</param>
        /// <param name="workno">工号</param>
        /// <returns>false为不存在,true为存在</returns>
        public bool IsWorknoExist(string userkey, string workno)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                Dictionary<string, object> paramArr = new Dictionary<string, object>();
                sb.Append(@"SELECT TOP 1 USER_KEY FROM SH3H_USER WHERE USER_ACTIVE = 1 AND USER_WORKNO =@USER_WORKNO ");
                paramArr.Add("@USER_WORKNO", workno);
                if (!string.IsNullOrEmpty(userkey))
                {
                    sb.Append("and USER_KEY <>@USER_KEY");
                    paramArr.Add("@USER_KEY", userkey);
                }
                string sqlText = sb.ToString();
                using (DbCommand command = Database.GetSqlStringCommand(sqlText))
                {
                    var result = SelectList(command);
                    bool blnExist = result.Count() == 0 ? false : true;
                    return blnExist;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "检查工号是否存在时失败");
            }          
        }

        /// <summary>
        /// 根据<see cref="IDataReader"/>数据读取器构建图层实体
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="user">用户对象</param>
        /// <returns>如果构建角色对象成功返回图层实例，否则返回NULL</returns>
        public override WapUser Build(IDataReader reader, WapUser user)
        {
            try
            {
                user.UserKey = reader.GetReaderValue<string>("USER_KEY");
                user.Id = reader.GetReaderValue<int>("USER_ID");
                user.Name = reader.GetReaderValue<string>("USER_NAME");
                user.JobNumber = reader.GetReaderValue<string>("USER_WORKNO");
                user.Account = reader.GetReaderValue<string>("USER_ACCOUNT");
                user.DomainAccount = reader.GetReaderValue<string>("USER_DOMAIN_ACCOUNT");
                user.Password = reader.GetReaderValue<string>("USER_PWD");
                user.Code = reader.GetReaderValue<string>("USER_PYCODE");
                user.SortSn = reader.GetReaderValue<int>("USER_SORTSN");
                user.Active = reader.GetReaderValue<bool>("USER_ACTIVE");
                user.Comment = reader.GetReaderValue<string>("USER_COMMENT",null,true);
                user.Phone = reader.GetReaderValue<string>("USER_PHONE",null,true);
                user.Cellphone = reader.GetReaderValue<string>("USER_CELLPHONE",null,true);
                user.Email = reader.GetReaderValue<string>("USER_EMAIL",null,true);
                user.IdCard = reader.GetReaderValue<string>("USER_IDCARD",null,true);
                user.Birthday = SH3H.WAP.Share.Utils.DateTimeToUTC(reader.GetReaderValue<DateTime>("USER_BIRTHDATE"));
                user.Sex = reader.GetReaderValue<int>("USER_SEX");
                user.Address = reader.GetReaderValue<string>("USER_ADDRESS",null,true);
                user.PostNo = reader.GetReaderValue<string>("USER_POSTNUM",null,true);
                user.Extend = reader.GetReaderValue<string>("EXTEND",null,true);
                user.FileHash = reader.GetReaderValue<string>("FILE_HASH", null, true);
                return user;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }
      
    } 
}

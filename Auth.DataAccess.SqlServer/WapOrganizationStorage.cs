using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SH3H.SDK.DataAccess.Db;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.DataAccess;
using SH3H.SDK.Share;
using SH3H.SharpFrame.Data;
using System.Data;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.WAP.Auth.Model.Dto;
using System.Data.Common;
namespace SH3H.WAP.Auth.DataAccess.SqlServer
{
    /// <summary>
    /// 组织结构数据库操作
    /// </summary>
    public class WapOrganizationStorage : BaseAccess<WapOrganization>, IWapOrganizationStorage
    {
        //构造函数
        public WapOrganizationStorage() : base(Consts.CONFIGURE_DATABASE_CONNECTION_STRING) { }

        public bool CreateSchema()
        {
            throw new NotImplementedException();
        }
        //构建组织对象
        public override WapOrganization Build(IDataReader reader, WapOrganization organization)
        {
            organization.OrganizationKey = reader.GetReaderValue<Guid>("ORGANIZATION_KEY").ToString();
            organization.ParentOrganizationKey = reader.GetReaderValue<Guid>("PARENT_ORGANIZATION_KEY").ToString();
            organization.OrganizationCode = reader.GetReaderValue<string>("ORGANIZATION_CODE");
            organization.OrganizationName = reader.GetReaderValue<string>("ORGANIZATION_NAME");
            organization.OrganizationType = reader.GetReaderValue<int>("ORGANIZATION_TYPE");
            organization.OrganizationAddress = reader.GetReaderValue<string>("ORGANIZATION_ADDRESS", null, true);
            organization.OrganizationDutyMan = reader.GetReaderValue<string>("ORGANIZATION_DUTY_MAN", null, true);
            organization.OrganizationTel = reader.GetReaderValue<string>("ORGANIZATION_TEL", null, true);
            organization.SortIndex = reader.GetReaderValue<int>("SORT_INDEX");
            organization.State = reader.GetReaderValue<int>("STATE");
            organization.TenantId = reader.GetReaderValue<int>("TENANT_ID");
            organization.Extend = reader.GetReaderValue<string>("EXTEND", null, true);
            return base.Build(reader, organization);
        }

        /// <summary>
        /// 添加组织结构
        /// </summary>
        /// <param name="organization">站点对象</param>
        /// <param name="trans"></param>
        /// <returns>组织对象</returns>
        public WapOrganization Insert(WapOrganization organization, System.Data.Common.DbTransaction trans = null)
        {
            try
            {
                string sqlText = @"INSERT INTO AUTH_ORGANIZATION(ORGANIZATION_KEY,ORGANIZATION_CODE,ORGANIZATION_NAME,ORGANIZATION_ADDRESS,PARENT_ORGANIZATION_KEY,ORGANIZATION_DUTY_MAN,ORGANIZATION_TEL,SORT_INDEX,STATE,ORGANIZATION_TYPE,TENANT_ID,EXTEND)
                                  VALUES(@ORGANIZATION_KEY,@ORGANIZATION_CODE,@ORGANIZATION_NAME,@ORGANIZATION_ADDRESS,@PARENT_ORGANIZATION_KEY,@ORGANIZATION_DUTY_MAN,@ORGANIZATION_TEL,@SORT_INDEX,@STATE,@ORGANIZATION_TYPE,@TENANT_ID,@EXTEND)
                                   SELECT @@IDENTITY;";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    if (!string.IsNullOrEmpty(organization.OrganizationKey) && !string.IsNullOrEmpty(organization.ParentOrganizationKey))
                    {
                        Guid newGuid = Guid.Parse(organization.OrganizationKey);
                        Guid parentGuid = Guid.Parse(organization.ParentOrganizationKey);

                        Database.AddInParameter(cmd, "@ORGANIZATION_KEY", DbType.Guid, newGuid);
                        Database.AddInParameter(cmd, "@ORGANIZATION_CODE", DbType.String, organization.OrganizationCode);
                        Database.AddInParameter(cmd, "@ORGANIZATION_NAME", DbType.String, organization.OrganizationName);
                        Database.AddInParameter(cmd, "@ORGANIZATION_ADDRESS", DbType.String, organization.OrganizationAddress);
                        Database.AddInParameter(cmd, "@PARENT_ORGANIZATION_KEY", DbType.Guid, parentGuid);
                        Database.AddInParameter(cmd, "@ORGANIZATION_DUTY_MAN", DbType.String, organization.OrganizationDutyMan);
                        Database.AddInParameter(cmd, "@ORGANIZATION_TEL", DbType.String, organization.OrganizationTel);
                        Database.AddInParameter(cmd, "@SORT_INDEX", DbType.Int32, organization.SortIndex);
                        Database.AddInParameter(cmd, "@STATE", DbType.Int32, organization.State);
                        Database.AddInParameter(cmd, "@ORGANIZATION_TYPE", DbType.Int32, organization.OrganizationType);
                        Database.AddInParameter(cmd, "@TENANT_ID", DbType.Int32, organization.TenantId);
                        Database.AddInParameter(cmd, "@EXTEND", DbType.String, organization.Extend);
                        int i = ExecuteNonQuery(cmd, trans);
                        if (i > 0)
                        {
                            organization.OrganizationKey = newGuid.ToString();
                            return organization;
                        }
                        else
                            return null;
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }

        }

        /// <summary>
        /// 删除组织
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <param name="trans"></param>
        /// <returns>是否删除成功</returns>
        public bool Delete(string organizationKey, System.Data.Common.DbTransaction trans = null)
        {
            try
            {
                string sqlText = @"DELETE FROM AUTH_ORGANIZATION
                                  WHERE ORGANIZATION_KEY=@ORGANIZATION_KEY";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Guid guid = Guid.Parse(organizationKey);
                    Database.AddInParameter(cmd, "@ORGANIZATION_KEY", DbType.Guid, guid);
                    int i = ExecuteNonQuery(cmd, trans);
                    if (i > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }
        /// <summary>
        /// 通过站点编号查询站点
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <returns>站点对象</returns>
        public WapOrganization Select(string organizationKey)
        {
            try
            {
                string sqlText = @"SELECT * FROM AUTH_ORGANIZATION
                                   WHERE ORGANIZATION_KEY=@ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Guid guid = Guid.Parse(organizationKey);
                    Database.AddInParameter(cmd, "@ID", DbType.Guid, guid);
                    WapOrganization o = SelectSingle(cmd);
                    return o;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        /// <summary>
        /// 通过组织id修改组织对象
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <param name="organization">站点对象</param>
        /// <param name="trans"></param>
        /// <returns>返回时否修改成功</returns>
        public bool Update(string organizationKey, WapOrganization organization, System.Data.Common.DbTransaction trans = null)
        {
            return false;
            try
            {
                string sqlText = @"UPDATE AUTH_ORGANIZATION 
                                   SET ORGANIZATION_CODE=@ORGANIZATION_CODE,ORGANIZATION_NAME=@ORGANIZATION_NAME,ORGANIZATION_ADDRESS=@ORGANIZATION_ADDRESS,PARENT_ORGANIZATION_KEY=@PARENT_ORGANIZATION_KEY,ORGANIZATION_DUTY_MAN=@ORGANIZATION_DUTY_MAN,ORGANIZATION_TEL=@ORGANIZATION_TEL,SORT_INDEX=@SORT_INDEX,STATE=@STATE,ORGANIZATION_TYPE=@ORGANIZATION_TYPE,TENANT_ID=@TENANT_ID,EXTEND=@EXTEND WHERE ORGANIZATION_KEY=@ORGANIZATION_KEY";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Guid guid = Guid.Parse(organizationKey);
                    Guid parentGuid = Guid.Parse(organization.ParentOrganizationKey);

                    Database.AddInParameter(cmd, "@ORGANIZATION_KEY", DbType.Guid, guid);
                    Database.AddInParameter(cmd, "@ORGANIZATION_CODE", DbType.String, organization.OrganizationCode);
                    Database.AddInParameter(cmd, "@ORGANIZATION_NAME", DbType.String, organization.OrganizationName);
                    Database.AddInParameter(cmd, "@ORGANIZATION_ADDRESS", DbType.String, organization.OrganizationAddress);
                    Database.AddInParameter(cmd, "@PARENT_ORGANIZATION_KEY", DbType.Guid, parentGuid);
                    Database.AddInParameter(cmd, "@ORGANIZATION_DUTY_MAN", DbType.String, organization.OrganizationDutyMan);
                    Database.AddInParameter(cmd, "@ORGANIZATION_TEL", DbType.String, organization.OrganizationTel);
                    Database.AddInParameter(cmd, "@SORT_INDEX", DbType.Int32, organization.SortIndex);
                    Database.AddInParameter(cmd, "@STATE", DbType.Int32, organization.State);
                    Database.AddInParameter(cmd, "@ORGANIZATION_TYPE", DbType.Int32, organization.OrganizationType);
                    Database.AddInParameter(cmd, "@TENANT_ID", DbType.Int32, organization.TenantId);
                    Database.AddInParameter(cmd, "@EXTEND", DbType.String, organization.Extend);
                    int i = ExecuteNonQuery(cmd, trans);
                    if (i > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }
        /// <summary>
        /// 通过组织id更新组织对象
        /// </summary>
        /// <param name="organizationKey">对象编号</param>
        /// <param name="organization">对象实体</param>
        /// <param name="trans"></param>
        /// <returns>修改后的对象</returns>
        public WapOrganization UpdateOrganization(string organizationKey, WapOrganization organization)
        {
            try
            {
                string sqlText = @"UPDATE AUTH_ORGANIZATION 
                                   SET ORGANIZATION_CODE=@ORGANIZATION_CODE,ORGANIZATION_NAME=@ORGANIZATION_NAME,ORGANIZATION_ADDRESS=@ORGANIZATION_ADDRESS,PARENT_ORGANIZATION_KEY=@PARENT_ORGANIZATION_KEY,ORGANIZATION_DUTY_MAN=@ORGANIZATION_DUTY_MAN,ORGANIZATION_TEL=@ORGANIZATION_TEL,SORT_INDEX=@SORT_INDEX,STATE=@STATE,ORGANIZATION_TYPE=@ORGANIZATION_TYPE,TENANT_ID=@TENANT_ID,EXTEND=@EXTEND WHERE ORGANIZATION_KEY=@ORGANIZATION_KEY";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Guid guid = Guid.Parse(organizationKey);
                    Guid parentGuid = Guid.Parse(organization.ParentOrganizationKey);

                    Database.AddInParameter(cmd, "@ORGANIZATION_KEY", DbType.Guid, guid);
                    Database.AddInParameter(cmd, "@ORGANIZATION_CODE", DbType.String, organization.OrganizationCode);
                    Database.AddInParameter(cmd, "@ORGANIZATION_NAME", DbType.String, organization.OrganizationName);
                    Database.AddInParameter(cmd, "@ORGANIZATION_ADDRESS", DbType.String, organization.OrganizationAddress);
                    Database.AddInParameter(cmd, "@PARENT_ORGANIZATION_KEY", DbType.Guid, parentGuid);
                    Database.AddInParameter(cmd, "@ORGANIZATION_DUTY_MAN", DbType.String, organization.OrganizationDutyMan);
                    Database.AddInParameter(cmd, "@ORGANIZATION_TEL", DbType.String, organization.OrganizationTel);
                    Database.AddInParameter(cmd, "@SORT_INDEX", DbType.Int32, organization.SortIndex);
                    Database.AddInParameter(cmd, "@STATE", DbType.Int32, organization.State);
                    Database.AddInParameter(cmd, "@ORGANIZATION_TYPE", DbType.Int32, organization.OrganizationType);
                    Database.AddInParameter(cmd, "@TENANT_ID", DbType.Int32, organization.TenantId);
                    Database.AddInParameter(cmd, "@EXTEND", DbType.String, organization.Extend);
                    int i = ExecuteNonQuery(cmd);
                    if (i > 0)
                        return organization;
                }
                return null;//代表失败
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }
        /// <summary>
        /// 禁用（state=-1）
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <param name="trans"></param>
        /// <returns>是否成功</returns>
        public bool DeactiveOrganization(string organizationKey, System.Data.Common.DbTransaction trans = null)
        {
            try
            {
                string sqlText = @"UPDATE AUTH_ORGANIZATION SET STATE=0
                                  WHERE ORGANIZATION_KEY=@ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Guid guid = Guid.Parse(organizationKey);
                    Database.AddInParameter(cmd, "@ID", DbType.Guid, guid);
                    int i = ExecuteNonQuery(cmd, trans);
                    if (i > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }
        /// <summary>
        /// 起用（state=1）
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <param name="trans"></param>
        /// <returns>是否成功</returns>
        public bool ActiveOrganization(string organizationKey, System.Data.Common.DbTransaction trans = null)
        {
            try
            {
                string sqlText = @"UPDATE AUTH_ORGANIZATION SET STATE=1
                                  WHERE ORGANIZATION_KEY=@ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Guid guid = Guid.Parse(organizationKey);
                    Database.AddInParameter(cmd, "@ID", DbType.Guid, guid);
                    int i = ExecuteNonQuery(cmd, trans);
                    if (i > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }
        /// <summary>
        /// 根据站点编号更新词语排序值
        /// </summary>
        /// <param name="sortIndexes">站点排序值数组</param>
        /// <returns></returns>
        public bool ModifyOrgIndexByOrganizationKey(Dictionary<string, int> sortIndexes)
        {
            return base.Transact(trans =>
            {
                foreach (KeyValuePair<string, int> index in sortIndexes)
                {
                    if (!ModifyOrgIndexByOrganizationKey(index.Key, index.Value, trans))
                        return false;
                }
                return true;
            });
        }
        /// <summary>
        /// 根据站点编号更新站点排序值
        /// </summary>
        /// <param name="organizationKey">站点编号</param>
        /// <param name="sortIndex">站点排序值</param>
        ///<param name="trans"></param>
        /// <returns></returns>
        public bool ModifyOrgIndexByOrganizationKey(string organizationKey, int sortIndex, System.Data.Common.DbTransaction trans = null)
        {
            try
            {
                string sqlText = @"UPDATE AUTH_ORGANIZATION 
                                   SET SORT_INDEX=@SORT_INDEX
                                   WHERE ORGANIZATION_KEY=@ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Guid guid = Guid.Parse(organizationKey);
                    Database.AddInParameter(cmd, "@SORT_INDEX", DbType.Int32, sortIndex);
                    Database.AddInParameter(cmd, "@ID", DbType.Guid, guid);
                    int i = ExecuteNonQuery(cmd, trans);
                    if (i > 0)
                        return true;
                    else
                        return false;

                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }

        /// <summary>
        /// 获取所有组织对象
        /// </summary>
        /// <returns>站点对象列表</returns>
        public IEnumerable<WapOrganization> SelectAll()
        {
            try
            {
                string sqlText = @"SELECT * FROM AUTH_ORGANIZATION where STATE<>-1";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    var list = SelectList(cmd);
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        #region OrganizationUser
        /// <summary>
        /// 获取指定用户的组织
        /// </summary>
        /// <param name="userKey">用户编号</param>
        /// <returns>组织对象集合</returns>
        public IEnumerable<WapOrganization> GetOrganizationbyUser(string userKey)
        {
            try
            {
                string sqlText = @"SELECT a.ORGANIZATION_KEY,
		                                 a.PARENT_ORGANIZATION_KEY,
		                                 a.ORGANIZATION_CODE,
		                                 a.ORGANIZATION_NAME,
		                                 a.ORGANIZATION_TYPE,
		                                 a.ORGANIZATION_ADDRESS,
		                                 a.ORGANIZATION_DUTY_MAN,
		                                 a.ORGANIZATION_TEL,
		                                 a.SORT_INDEX,
		                                 a.STATE,
		                                 a.TENANT_ID,
		                                 a.EXTEND 
        FROM AUTH_ORGANIZATION a,(SELECT * FROM AUTH_USER_ORGANIZATION WHERE USER_KEY=@USERKEY) as b WHERE a.ORGANIZATION_KEY=b.ORGANIZATION_KEY";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Guid guid = Guid.Parse(userKey);
                    Database.AddInParameter(cmd, "@USERKEY", DbType.Guid, guid);
                    IEnumerable<WapOrganization> organizations= SelectList(cmd);
                    return organizations;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        public UserOrganizationRelation OrganizationUserRelationBuild(IDataReader reader, UserOrganizationRelation mode)
        {
            try
            {
                mode.RelationKey = reader.GetReaderValue<string>("RELATION_KEY");
                mode.UserKey = reader.GetReaderValue<string>("USER_KEY");
                mode.OrganKey = reader.GetReaderValue<string>("ORGANIZATION_KEY");
                return mode;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        public bool UpdateOrganizationUserRelation(Model.Dto.UpdateUserOrganizationRelationModel relationModel)
        {
            if (relationModel == null)
            {
                return false;
            }
            return base.Transact(trans =>
            {

                if (relationModel.DeletedArr != null && relationModel.DeletedArr.Count() > 0)
                {
                    foreach (var item in relationModel.DeletedArr)
                    {
                        if (string.IsNullOrEmpty(item.RelationKey) || item.RelationKey == new Guid().ToString())
                        {
                            if (!DeleteOrganizationUserRelation(item.UserKey, item.OrganKey, trans))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (!DeleteOrganizationUserRelation(item.RelationKey, trans))
                            {
                                return false;
                            }
                        }
                    }
                }

                if (relationModel.AddArr != null && relationModel.AddArr.Count() > 0)
                {
                    foreach (var item in relationModel.AddArr)
                    {
                        if (!AddOrganizationUserRelation(item, trans))
                        {
                            return false;
                        }
                    }
                }
                return true;
            });
        }

        private bool DeleteOrganizationUserRelation(string relationKey, DbTransaction trans)
        {
            try
            {
                string commandText = @"DELETE  AUTH_USER_ORGANIZATION WHERE RELATION_KEY = @relation_key";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@relation_key", System.Data.DbType.String, relationKey);
                    return ExecuteNonQuery(command, trans) >= 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }

        private bool DeleteOrganizationUserRelation(string userkey, string orgckey, DbTransaction trans)
        {
            try
            {
                string commandText = @"DELETE  AUTH_USER_ORGANIZATION  
                                         WHERE USER_KEY = @user_key 
                                           AND ORGANIZATION_KEY = @organization_key";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@user_key", System.Data.DbType.String, userkey);
                    Database.AddInParameter(command, "@organization_key", System.Data.DbType.String, orgckey);
                    return ExecuteNonQuery(command, trans) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }


        private bool AddOrganizationUserRelation(UserOrganizationRelation model, DbTransaction trans)
        {
            int result = -1;
            try
            {
                string addFunctionCommandText = @"INSERT INTO AUTH_USER_ORGANIZATION
                                                               (RELATION_KEY
                                                                ,USER_KEY
                                                                ,ORGANIZATION_KEY)
                                                         VALUES
                                                               (@relation_key
                                                               ,@user_key
                                                               ,@organization_key);";
                using (DbCommand addFunctionCommand = Database.GetSqlStringCommand(addFunctionCommandText))
                {
                    Database.AddInParameter(addFunctionCommand, "@relation_key", System.Data.DbType.String, model.RelationKey);
                    Database.AddInParameter(addFunctionCommand, "@user_key", System.Data.DbType.String, model.UserKey);
                    Database.AddInParameter(addFunctionCommand, "@organization_key", System.Data.DbType.String, model.OrganKey);

                    result = ExecuteNonQuery(addFunctionCommand, trans);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }

        public Model.Dto.UserOrganizationRelation GetOrganizationUserRelation(string relationKey)
        {
            try
            {
                string commandText = @"SELECT RELATION_KEY
                                              ,USER_KEY
                                              ,ORGANIZATION_KEY
                                          FROM AUTH_USER_ORGANIZATION
                                          WHERE RELATION_KEY=@relationKey";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "relationKey", DbType.String, relationKey);

                    return SelectSingle<UserOrganizationRelation>(command, new Func<IDataReader, UserOrganizationRelation>(p =>
                    {
                        UserOrganizationRelation dto = new UserOrganizationRelation();
                        OrganizationUserRelationBuild(p, dto);
                        return dto;
                    }));
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        public IEnumerable<Model.Dto.UserOrganizationRelation> GetAllOrganizationUserRelation()
        {
            try
            {
                string commandText = @"SELECT RELATION_KEY
                                              ,USER_KEY
                                              ,ORGANIZATION_KEY
                                          FROM AUTH_USER_ORGANIZATION";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    return SelectList<UserOrganizationRelation>(command, new Func<IDataReader, UserOrganizationRelation>(p =>
                    {
                        UserOrganizationRelation dto = new UserOrganizationRelation();
                        OrganizationUserRelationBuild(p, dto);
                        return dto;
                    }));
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        public IEnumerable<Model.Dto.UserOrganizationRelation> GetAllOrganizationUserRelationByOrganKey(string organKey)
        {
            try
            {
                string commandText = @"SELECT RELATION_KEY
                                              ,USER_KEY
                                              ,ORGANIZATION_KEY
                                          FROM AUTH_USER_ORGANIZATION
                                          WHERE ORGANIZATION_KEY=@organKey";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {

                    Database.AddInParameter(command, "organKey", DbType.String, organKey);

                    return SelectList<UserOrganizationRelation>(command, new Func<IDataReader, UserOrganizationRelation>(p =>
                    {
                        UserOrganizationRelation dto = new UserOrganizationRelation();
                        OrganizationUserRelationBuild(p, dto);
                        return dto;
                    }));
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        public IEnumerable<Model.Dto.UserOrganizationRelation> GetAllOrganizationUserRelationByUserKey(string userKey)
        {
            try
            {
                string commandText = @"SELECT RELATION_KEY
                                              ,USER_KEY
                                              ,ORGANIZATION_KEY
                                          FROM AUTH_USER_ORGANIZATION
                                          WHERE USER_KEY=@userKey";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {

                    Database.AddInParameter(command, "userKey", DbType.String, userKey);

                    return SelectList<UserOrganizationRelation>(command, new Func<IDataReader, UserOrganizationRelation>(p =>
                    {
                        UserOrganizationRelation dto = new UserOrganizationRelation();
                        OrganizationUserRelationBuild(p, dto);
                        return dto;
                    }));
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        public IEnumerable<WapUser> GetUserByOrganKey(string organkey)
        {
            try
            {
                string commandText = @" SELECT AU.USER_KEY
                                              ,AU.USER_ID
                                              ,AU.USER_NAME
                                              ,AU.USER_WORKNO
                                              ,AU.USER_ACCOUNT
                                              ,AU.USER_DOMAIN_ACCOUNT
                                              ,AU.USER_PWD
                                              ,AU.USER_PYCODE
                                              ,AU.USER_SORTSN
                                              ,AU.USER_ACTIVE
                                              ,AU.USER_COMMENT
                                              ,AU.USER_PHONE
                                              ,AU.USER_CELLPHONE
                                              ,AU.USER_EMAIL
                                              ,AU.USER_IDCARD
                                              ,AU.USER_BIRTHDATE
                                              ,AU.USER_SEX
                                              ,AU.USER_ADDRESS
                                              ,AU.USER_POSTNUM
                                              ,AU.EXTEND
                                          FROM AUTH_USER AU
                                          INNER JOIN AUTH_USER_ORGANIZATION AUO
                                          ON AUO.USER_KEY=AU.USER_KEY
                                          WHERE AUO.ORGANIZATION_KEY=@organkey";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {

                    Database.AddInParameter(command, "organkey", DbType.String, organkey);

                    return SelectList<WapUser>(command, new Func<IDataReader, WapUser>(p =>
                    {
                        WapUser dto = new WapUser();
                        WapUserBuild(p, dto);
                        return dto;
                    }));
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        public IEnumerable<WapUser> GetUserByOrganKeys(IEnumerable<string> organkey)
        {
            try
            {
                string commandText = @" SELECT AU.USER_KEY
                                              ,AU.USER_ID
                                              ,AU.USER_NAME
                                              ,AU.USER_WORKNO
                                              ,AU.USER_ACCOUNT
                                              ,AU.USER_DOMAIN_ACCOUNT
                                              ,AU.USER_PWD
                                              ,AU.USER_PYCODE
                                              ,AU.USER_SORTSN
                                              ,AU.USER_ACTIVE
                                              ,AU.USER_COMMENT
                                              ,AU.USER_PHONE
                                              ,AU.USER_CELLPHONE
                                              ,AU.USER_EMAIL
                                              ,AU.USER_IDCARD
                                              ,AU.USER_BIRTHDATE
                                              ,AU.USER_SEX
                                              ,AU.USER_ADDRESS
                                              ,AU.USER_POSTNUM
                                              ,AU.EXTEND
                                          FROM AUTH_USER AU
                                          INNER JOIN AUTH_USER_ORGANIZATION AUO
                                          ON AUO.USER_KEY=AU.USER_KEY
                                          WHERE AUO.ORGANIZATION_KEY IN (";
                int length = organkey.Count();
                int index = 0;
                for (; index < length; index++)
                {
                    commandText += "@organ_key" + index + ",";
                }
                if (commandText.EndsWith(","))
                {
                    commandText = commandText.Substring(0, commandText.Length - 1);
                }

                commandText += ")";

                index = 0;

                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    foreach (var organ in organkey)
                    {
                        string paramname = "organ_key" + index;
                        Database.AddInParameter(command, paramname, DbType.String, organ);
                        index++;
                    }

                    return SelectList<WapUser>(command, new Func<IDataReader, WapUser>(p =>
                    {
                        WapUser dto = new WapUser();
                        WapUserBuild(p, dto);
                        return dto;
                    }));
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }


        public WapUser WapUserBuild(IDataReader reader, WapUser user)
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
                user.Comment = reader.GetReaderValue<string>("USER_COMMENT", null, true);
                user.Phone = reader.GetReaderValue<string>("USER_PHONE", null, true);
                user.Cellphone = reader.GetReaderValue<string>("USER_CELLPHONE", null, true);
                user.Email = reader.GetReaderValue<string>("USER_EMAIL", null, true);
                user.IdCard = reader.GetReaderValue<string>("USER_IDCARD", null, true);
                user.Birthday = SH3H.WAP.Share.Utils.DateTimeToUTC(reader.GetReaderValue<DateTime>("USER_BIRTHDATE"));
                user.Sex = reader.GetReaderValue<int>("USER_SEX");
                user.Address = reader.GetReaderValue<string>("USER_ADDRESS", null, true);
                user.PostNo = reader.GetReaderValue<string>("USER_POSTNUM", null, true);
                user.Extend = reader.GetReaderValue<string>("EXTEND", null, true);
                return user;
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

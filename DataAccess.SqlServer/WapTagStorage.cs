using SH3H.SDK.DataAccess.Db;
using SH3H.SDK.Definition.Exceptions;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.WAP.Model;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SH3H.SharpFrame.Data;

namespace SH3H.WAP.DataAccess.SqlServer
{
    /// <summary>
    /// 标签Storage
    /// </summary>
    public class WapTagStorage : BaseAccess<WapTag>, IWapTagStorage
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WapTagStorage()
            : base(SH3H.SDK.Share.Consts.CONFIGURE_DATABASE_CONNECTION_STRING) { }

        public bool CreateSchema()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="tag">标签对象</param>
        /// <returns>标签对象</returns>
        public WapTag AddTag(WapTag tag)
        {
            try
            {
                if (GetTagByTag(tag) != null)
                {
                    throw new WapException(StateCode.CODE_ARGUMENT_DATA_REPEAT, "数据表中已有该名称的标签");
                }
                else
                {
                    string commandText = @"INSERT INTO CNF_TAG (TAG_NAME,TAG_GROUP_CODE,TAG_APP_IDENTITY,TAG_CREATE_USERID,TAG_COLOR,TAG_REFERENCE_COUNT,TAG_COMMENT,TAG_CREATE_DATE)
                                                     VALUES(@TAG_NAME,@TAG_GROUP_CODE,@TAG_APP_IDENTITY,@TAG_CREATE_USERID,@TAG_COLOR,@TAG_REFERENCE_COUNT,@TAG_COMMENT,getdate()) SELECT @@IDENTITY;";
                    using (DbCommand command = Database.GetSqlStringCommand(commandText))
                    {
                        Database.AddInParameter(command, "@TAG_NAME", DbType.String, tag.TagName);
                        Database.AddInParameter(command, "@TAG_GROUP_CODE", DbType.String, tag.TagGroupCode);
                        Database.AddInParameter(command, "@TAG_APP_IDENTITY", DbType.String, tag.AppIdentity);
                        Database.AddInParameter(command, "@TAG_CREATE_USERID", DbType.Int32, tag.UserId);
                        Database.AddInParameter(command, "@TAG_COLOR", DbType.String, tag.Color);
                        Database.AddInParameter(command, "@TAG_REFERENCE_COUNT", DbType.Int32, tag.ReferenceCount);
                        Database.AddInParameter(command, "@TAG_COMMENT", DbType.String, tag.Comment);
                        //Database.AddInParameter(command, "@TAG_CREATE_DATE", DbType.DateTime, tag.CreateTime);
                        int id = ExecuteScalar<int>(command);
                        tag.TagId = id;
                        return tag;
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
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "新增标签对象失败");
            }

        }


        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <param name="tag">标签对象</param>
        /// <returns>标签对象</returns>
        public WapTag UpdateTag(int id, WapTag tag)
        {
            try
            {
                WapTag tempTag = GetTagByTag(tag);
                if (tempTag != null && tempTag.TagId != id)
                {
                    throw new WapException(StateCode.CODE_ARGUMENT_DATA_REPEAT, "数据表中已有该名称的标签");
                }
                else
                {
                    string commandText = @"UPDATE CNF_TAG SET TAG_NAME=@TAG_NAME,
                                                          TAG_GROUP_CODE=@TAG_GROUP_CODE,
                                                          TAG_APP_IDENTITY=@TAG_APP_IDENTITY,
                                                          TAG_CREATE_USERID=@TAG_CREATE_USERID,
                                                          TAG_COLOR=@TAG_COLOR,
                                                          TAG_REFERENCE_COUNT=@TAG_REFERENCE_COUNT,
                                                          TAG_COMMENT=@TAG_COMMENT,
                                                          TAG_UPDATE_DATE = getdate()
                                                       WHERE TAG_ID=@TAG_ID";
                    using (DbCommand command = Database.GetSqlStringCommand(commandText))
                    {
                        Database.AddInParameter(command, "@TAG_ID", DbType.Int32, id);
                        Database.AddInParameter(command, "@TAG_NAME", DbType.String, tag.TagName);
                        Database.AddInParameter(command, "@TAG_GROUP_CODE", DbType.String, tag.TagGroupCode);
                        Database.AddInParameter(command, "@TAG_APP_IDENTITY", DbType.String, tag.AppIdentity);
                        Database.AddInParameter(command, "@TAG_CREATE_USERID", DbType.Int32, tag.UserId);
                        Database.AddInParameter(command, "@TAG_COLOR", DbType.String, tag.Color);
                        Database.AddInParameter(command, "@TAG_REFERENCE_COUNT", DbType.Int32, tag.ReferenceCount);
                        Database.AddInParameter(command, "@TAG_COMMENT", DbType.String, tag.Comment);
                        //Database.AddInParameter(command, "@TAG_UPDATE_DATE", DbType.DateTime, tag.UpdateTime);
                        if (ExecuteNonQuery(command) > 0)
                            return tag;
                        else
                            return null;
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
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "修改标签对象失败");
            }
        }

        /// <summary>
        /// 根据应用获取标签列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>标签列表</returns>
        public IEnumerable<WapTag> SelectTagsByAppCode(string appIdentity)
        {
            try
            {
                string commandText = @"SELECT * FROM CNF_TAG where TAG_APP_IDENTITY=@TAG_APP_IDENTITY";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@TAG_APP_IDENTITY", DbType.String, appIdentity);
                    var list = SelectList(command);
                    return (list == null || list.Count() == 0) ? null : list;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "根据应用获取标签列表失败");
            }
        }


        /// <summary>
        /// 根据应用获取所有的标签组Code
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>标签组Code列表</returns>
        public IEnumerable<string> SelectTagGroupsByAppCode(string appIdentity)
        {
            try
            {
                string commandText = @"SELECT DISTINCT TAG_GROUP_CODE FROM CNF_TAG WHERE TAG_APP_IDENTITY=@TAG_APP_IDENTITY";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@TAG_APP_IDENTITY", DbType.String, appIdentity);
                    IList<string> list = new List<string>();

                    IDataReader reader = SelectReader(command);
                    while (reader.Read())
                    {
                        list.Add(reader[0].ToString());
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "根据应用获取标签组Code列表失败");
            }
        }

        /// <summary>
        /// 判断数据库是否已有该标识的标签
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public WapTag GetTagByTag(WapTag tag)
        {
            try
            {
                string commandText = @"SELECT * FROM CNF_TAG WHERE TAG_GROUP_CODE=@TAG_GROUP_CODE AND TAG_NAME=@TAG_NAME AND TAG_APP_IDENTITY=@TAG_APP_IDENTITY";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@TAG_NAME", DbType.String, tag.TagName);
                    Database.AddInParameter(command, "@TAG_GROUP_CODE", DbType.String, tag.TagGroupCode);
                    Database.AddInParameter(command, "@TAG_APP_IDENTITY", DbType.String, tag.AppIdentity);
                    var list = SelectList(command);
                    return (list == null || list.Count() == 0) ? null : list.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "获取失败");
            }
        }


        /// <summary>
        /// 通过id获取指定标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <returns>标签对象</returns>
        public WapTag GetTagById(int id)
        {
            try
            {
                string commandText = @"SELECT * FROM CNF_TAG  WHERE TAG_ID=@TAG_ID";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@TAG_ID", DbType.Int32, id);
                    var list = SelectList(command);
                    return (list == null || list.Count() == 0) ? null : list.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "获取指定标签对象失败");
            }
        }

        /// <summary>
        /// 指定应用和标签组code获取标签列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <param name="tagGroupCode">标签组code</param>
        /// <returns>标签列表</returns>
        public IEnumerable<WapTag> SelectTagsByTagGroupCode(string appIdentity, string tagGroupCode)
        {
            try
            {
                string commandText = @"SELECT * FROM CNF_TAG WHERE TAG_APP_IDENTITY=@TAG_APP_IDENTITY AND TAG_GROUP_CODE=@TAG_GROUP_CODE";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@TAG_APP_IDENTITY", DbType.String, appIdentity);
                    Database.AddInParameter(command, "@TAG_GROUP_CODE", DbType.String, tagGroupCode);
                    var list = SelectList(command);
                    return (list == null || list.Count() == 0) ? null : list;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "指定应用和标签组code获取标签列表失败");
            }
        }


        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <returns>是否成功</returns>
        public bool DeleteTag(int id)
        {
            try
            {
                string commandText = @"DELETE FROM  CNF_TAG 
                                       WHERE TAG_ID=@TAG_ID";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@TAG_ID", DbType.Int32, id);
                    if (ExecuteNonQuery(command) > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "标签删除失败");
            }
        }

        /// <summary>
        /// 修改标签引用次数
        /// </summary>
        /// <param name="id">标签id</param>
        /// <param name="referenceCount">标签引用次数</param>
        /// <returns>是否成功</returns>
        public bool UpdateReferenceCount(int id,int referenceCount)
        {
            try
            {
                string commandText = @"UPDATE  CNF_TAG
                                       SET  TAG_REFERENCE_COUNT =@TAG_REFERENCE_COUNT  
                                       WHERE TAG_ID=@TAG_ID";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@TAG_ID", DbType.Int32, id);
                    Database.AddInParameter(command, "@TAG_REFERENCE_COUNT", DbType.Int32, referenceCount);
                    if (ExecuteNonQuery(command) > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "修改标签引用次数失败");
            }
        }

        /// <summary>
        /// 点击标签引用次数加1
        /// </summary>
        /// <param name="id">标签ID</param>
        /// <returns></returns>
        public int ReferenceAdd(int id)
        {
            try
            {
                string commandText = @"UPDATE  CNF_TAG
                                       SET  TAG_REFERENCE_COUNT =TAG_REFERENCE_COUNT+1  
                                       WHERE TAG_ID=@TAG_ID";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@TAG_ID", DbType.Int32, id);
                    ExecuteNonQuery(command);
                    string commandTextSel = @"SELECT TAG_REFERENCE_COUNT 
                                                FROM   CNF_TAG
                                               WHERE TAG_ID=@TAG_ID";
                    using (DbCommand commandSel = Database.GetSqlStringCommand(commandTextSel))
                    {
                        Database.AddInParameter(commandSel, "@TAG_ID", DbType.Int32, id);
                        return int.Parse(ExecuteScalar(commandSel).ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "操作失败");
            }
        }


        /// <summary>
        /// 根据<see cref="IDataReader"/>数据读取器构建实体
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="tag">标签对象</param>
        /// <returns>如果构建表格对象成功返回图层实例，否则返回NULL</returns>
        public override WapTag Build(IDataReader reader, WapTag tag)
        {
            try
            {
                tag.TagId = reader.GetReaderValue<int>("TAG_ID");
                tag.TagName = reader.GetReaderValue<string>("TAG_NAME");
                tag.TagGroupCode = reader.GetReaderValue<string>("TAG_GROUP_CODE");
                tag.AppIdentity = reader.GetReaderValue<string>("TAG_APP_IDENTITY");
                tag.UserId = reader.GetReaderValue<int>("TAG_CREATE_USERID");
                tag.Color = reader.GetReaderValue<string>("TAG_COLOR");
                tag.ReferenceCount = reader.GetReaderValue<int>("TAG_REFERENCE_COUNT");
                tag.Comment = reader.GetReaderValue<string>("TAG_COMMENT", null, true);
                tag.CreateTime = reader.GetReaderValue<DateTime?>("TAG_CREATE_DATE", null, true);
                tag.UpdateTime = reader.GetReaderValue<DateTime?>("TAG_CREATE_DATE", null, true);
                return tag;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

    }
}

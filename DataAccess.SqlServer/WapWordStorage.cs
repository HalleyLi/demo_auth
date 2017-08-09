using SH3H.SDK.DataAccess.Db;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.SDK.Share;
using SH3H.SharpFrame.Data;
using SH3H.WAP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace SH3H.WAP.DataAccess.SqlServer
{
    /// <summary>
    /// 词汇数据库操作
    /// </summary>
    public class WapWordStorage : BaseAccess<WapWord>, IWapWordStorage
    {
        public WapWordStorage()
            : base(Consts.CONFIGURE_DATABASE_CONNECTION_STRING) { }

        public bool CreateSchema()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 根据WORD_GROUP_KEY字段获取词语集合
        /// </summary>
        /// <param name="key">分组key</param>
        /// <returns>词语集合</returns>
        public IEnumerable<WapWord> GetWordsByGroupKey(string key)
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_WORD WHERE WORD_GROUP_KEY=@WORD_GROUP_KEY";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@WORD_GROUP_KEY", DbType.String, key);
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
        /// 添加词语
        /// </summary>
        /// <param name="word">词语对象</param>
        /// <param name="trans"></param>
        /// <returns>词语对象</returns>
        public WapWord Insert(WapWord word)
        {
            try
            {

                string sqlText = @"INSERT INTO CNF_WORD(PARENT_ID,
                                                        WORD_CODE,
                                                        WORD_TEXT,
                                                        WORD_VALUE,
                                                        WORD_APP,
                                                        SORT_INDEX,
                                                        STATE,
                                                        TENANT_ID,
                                                        REMARK,
                                                        WORD_GROUP_KEY,
                                                        WORD_PYCODE,
                                                        IS_EXTERNAL_VISIBLE)
                                                  VALUES(@PARENT_ID,
                                                       @WORD_CODE,
                                                       @WORD_TEXT,
                                                       @WORD_VALUE,
                                                       @WORD_APP,
                                                       @SORT_INDEX,
                                                       @STATE,
                                                       @TENANT_ID,
                                                       @REMARK,
                                                       @WORD_GROUP_KEY,
                                                       @WORD_PYCODE,
                                                       @IS_EXTERNAL_VISIBLE)
                                                  SELECT @@IDENTITY;";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@PARENT_ID", DbType.Int32, word.ParentId);
                    Database.AddInParameter(cmd, "@WORD_CODE", DbType.String, word.WordCode);
                    Database.AddInParameter(cmd, "@WORD_TEXT", DbType.String, word.WordText);
                    Database.AddInParameter(cmd, "@WORD_VALUE", DbType.String, word.WordValue);
                    Database.AddInParameter(cmd, "@WORD_APP", DbType.String, word.App);
                    Database.AddInParameter(cmd, "@SORT_INDEX", DbType.Int32, word.WordSortIndex);
                    Database.AddInParameter(cmd, "@STATE", DbType.Int32, word.WordState);
                    Database.AddInParameter(cmd, "@TENANT_ID", DbType.Int32, word.TenentId);
                    Database.AddInParameter(cmd, "@REMARK", DbType.String, word.Remark);
                    Database.AddInParameter(cmd, "@WORD_GROUP_KEY", DbType.String, word.WordGroupKey);
                    Database.AddInParameter(cmd, "@WORD_PYCODE", DbType.String, word.WordPYCode);
                    Database.AddInParameter(cmd, "@IS_EXTERNAL_VISIBLE", DbType.Boolean, word.IsExternalVisible);
                    int id = ExecuteScalar<int>(cmd);
                    if (id > 0)
                    {
                        word.WordId = id;
                        return word;
                    }
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
        /// 删除词语
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <param name="trans"></param>
        /// <returns>是否删除成功</returns>
        public bool Delete(int id)
        {
            try
            {
                string sqlText = @"DELETE FROM CNF_WORD
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
        /// 通过词语编号修改词语
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <param name="word">词语对象</param>
        /// <param name="trans"></param>
        /// <returns>返回修改后的词语信息</returns>
        public WapWord Update(int id, WapWord word)
        {
            DbTransaction transaction = null;
            DbConnection connection = null;
            try
            {
                connection = this.Database.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                WapWord model = null;
                string sqlText = @"UPDATE CNF_WORD 
                                   SET PARENT_ID=@PARENT_ID,
                                       WORD_CODE=@WORD_CODE,
                                       WORD_TEXT=@WORD_TEXT,
                                       WORD_VALUE=@WORD_VALUE,                                       
                                       STATE=@STATE,                                       
                                       REMARK=@REMARK, 
                                        WORD_GROUP_KEY=@WORD_GROUP_KEY,
                                        WORD_PYCODE=@WORD_PYCODE
                                        WHERE ID=@ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@ID", DbType.Int32, id);
                    Database.AddInParameter(cmd, "@PARENT_ID", DbType.Int32, word.ParentId);
                    Database.AddInParameter(cmd, "@WORD_CODE", DbType.String, word.WordCode);
                    Database.AddInParameter(cmd, "@WORD_TEXT", DbType.String, word.WordText);
                    Database.AddInParameter(cmd, "@WORD_VALUE", DbType.String, word.WordValue);
                    //Database.AddInParameter(cmd, "@SORT_INDEX", DbType.Int32, word.WordSortIndex);
                    Database.AddInParameter(cmd, "@STATE", DbType.Int32, word.WordState);
                    //Database.AddInParameter(cmd, "@TENANT_ID", DbType.Int32, word.TenentId);
                    Database.AddInParameter(cmd, "@REMARK", DbType.String, word.Remark);
                    Database.AddInParameter(cmd, "@WORD_GROUP_KEY", DbType.String, word.WordGroupKey);
                    Database.AddInParameter(cmd, "@WORD_PYCODE", DbType.String, word.WordPYCode);
                    //Database.AddInParameter(cmd, "@IS_EXTERNAL_VISIBLE", DbType.Boolean, word.IsExternalVisible);

                    cmd.Transaction = transaction;
                    int line = ExecuteNonQuery(cmd, transaction);
                    if (line < 1)
                    {
                        transaction.Rollback();
                        transaction.Dispose();
                        connection.Close();
                        connection.Dispose();
                        return null;
                    }
                }

                string commandText = "SELECT * FROM CNF_WORD WHERE ID = @ID";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@ID", DbType.Int32, id);
                    command.Transaction = transaction;
                    IDataReader reader = ExecuteReader(command, transaction);
                    if (reader.Read())
                    {
                        model = new WapWord();
                        model.WordId = reader.GetReaderValue<int>("ID");
                        model.ParentId = reader.GetReaderValue<int>("PARENT_ID");
                        model.WordGroupKey = reader.GetReaderValue<string>("WORD_GROUP_KEY");
                        model.WordText = reader.GetReaderValue<string>("WORD_TEXT");
                        model.WordCode = reader.GetReaderValue<string>("WORD_CODE");
                        model.WordValue = reader.GetReaderValue<string>("WORD_VALUE", null, true);
                        model.WordPYCode = reader.GetReaderValue<string>("WORD_PYCODE", null, true);
                        model.App = reader.GetReaderValue<string>("WORD_APP");
                        model.WordSortIndex = reader.GetReaderValue<int>("SORT_INDEX");
                        model.WordState = reader.GetReaderValue<int>("STATE");
                        model.IsExternalVisible = reader.GetReaderValue<bool>("IS_EXTERNAL_VISIBLE");
                        model.TenentId = reader.GetReaderValue<int>("TENANT_ID");
                        model.Remark = reader.GetReaderValue<string>("REMARK", null, true);

                        reader.Close();
                        reader.Dispose();
                        transaction.Commit();
                        transaction.Dispose();
                        connection.Close();
                        connection.Dispose();
                        return model;
                    }
                    else
                    {
                        reader.Close();
                        reader.Dispose();
                        transaction.Rollback();
                        transaction.Dispose();
                        connection.Close();
                        connection.Dispose();
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

        /// <summary>
        /// 根据词语编号改词语父编号
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <param name="parentId">词语父编号</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateParentId(int id, int parentId)
        {
            try
            {
                string sqlText = @"UPDATE  CNF_WORD 
                                  SET PARENT_ID =@PARENT_ID
                                   WHERE ID=@ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@ID", DbType.Int32, id);
                    Database.AddInParameter(cmd, "@PARENT_ID", DbType.Int32, parentId);
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
        /// 更新词语状态
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <param name="state">词语操作</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateStateById(int id, int state)
        {
            try
            {
                string sqlText = "UPDATE CNF_WORD SET STATE = @STATE where ID =@ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@STATE", DbType.Int32, state);
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
        /// 通过词语父级编号查询词语
        /// </summary>
        /// <param name="pid">词语父级编号</param>
        /// <returns>词语对象</returns>
        public IEnumerable<WapWord> SelectByPerentId(int pid)
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_WORD WHERE PARENT_ID=@PID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@PID", DbType.Int32, pid);
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
        /// 通过词语父级编号查询词语
        /// </summary>
        /// <param name="pcode">词语父级编号</param>
        /// <returns>词语对象</returns>
        public IEnumerable<WapWord> SelectByPerentCode(string pcode)
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_WORD WHERE PARENT_ID=(SELECT ID FROM CNF_WORD WHERE WORD_CODE=@PCODE)";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@PCODE", DbType.String, pcode);
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
        /// 通过词语父级编号查询词语
        /// </summary>
        /// <param name="code">词语父级编号</param>
        /// <param name="value">词语编号</param>
        /// <returns>词语对象</returns>
        public WapWord SelectByCodeAndValue(string code, string value)
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_WORD WHERE PARENT_ID=(SELECT ID FROM CNF_WORD WHERE WORD_CODE=@CODE) and WORD_VALUE=@VALUE";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@CODE", DbType.String, code);
                    Database.AddInParameter(cmd, "@VALUE", DbType.String, value);
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
        /// 统一更新词语排序值
        /// </summary>
        /// <param name="sortIndexes">词语编号索引号字典</param>
        public bool UpdateSortIndexById(Dictionary<int, int> sortIndexes)
        {
            return base.Transact(trans =>
            {
                foreach (KeyValuePair<int, int> index in sortIndexes)
                {
                    if (!UpdateSortIndexById(index.Key, index.Value))
                        return false;
                }
                return true;
            });
        }

        /// <summary>
        /// 根据词语编号更新词语排序值
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <param name="sortIndex">词语排序值</param>
        ///<param name="trans"></param>
        /// <returns></returns>
        public bool UpdateSortIndexById(int id, int sortIndex)
        {
            try
            {
                string sqlText = @"UPDATE CNF_WORD 
                                   SET SORT_INDEX=@SORT_INDEX
                                   WHERE ID=@ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@SORT_INDEX", DbType.Int32, sortIndex);
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
        /// 通过词语编号查询词语
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <returns>词语对象</returns>
        public WapWord Select(int id)
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_WORD
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
        /// 获取指定应用下的词语
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>词语对象</returns>
        public IEnumerable<WapWord> GetAllWordsByApp(string appIdentity)
        {
            try
            {
                string sqlText = @"SELECT [ID]
                                          ,[PARENT_ID]
                                          ,[WORD_GROUP_KEY]
                                          ,[WORD_CODE]
                                          ,[WORD_TEXT]
                                          ,[WORD_VALUE]
                                          ,[WORD_PYCODE]
                                          ,[WORD_APP]
                                          ,[SORT_INDEX]
                                          ,[STATE]
                                          ,[IS_EXTERNAL_VISIBLE]
                                          ,[TENANT_ID]
                                          ,[REMARK]
                                      FROM [dbo].[CNF_WORD]
                                      WHERE [WORD_APP] = @word_app";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@word_app", DbType.String, appIdentity);
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
        /// 获取指定应用指定词语组的词语
        /// </summary>
        /// <param name="app">应用标识</param>
        /// <param name="group">词语组ID</param>
        /// <returns>词语对象</returns>
        public IEnumerable<WapWord> GetAllWordsByAppAndGroup(string app, string group)
        {
            try
            {
                string sqlText = @"SELECT [ID]
                                          ,[PARENT_ID]
                                          ,[WORD_GROUP_KEY]
                                          ,[WORD_CODE]
                                          ,[WORD_TEXT]
                                          ,[WORD_VALUE]
                                          ,[WORD_PYCODE]
                                          ,[WORD_APP]
                                          ,[SORT_INDEX]
                                          ,[STATE]
                                          ,[IS_EXTERNAL_VISIBLE]
                                          ,[TENANT_ID]
                                          ,[REMARK]
                                      FROM [dbo].[CNF_WORD]
                                      WHERE [WORD_APP] = @word_app
                                      AND  [WORD_GROUP_KEY]=@word_group_key";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@word_app", DbType.String, app);
                    Database.AddInParameter(cmd, "@word_group_key", DbType.String, group);
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
        /// 根据词语编码查找
        /// </summary>
        /// <param name="wordCodes">词语编码集合</param>
        /// <returns>词语集合</returns>
        public IEnumerable<WapWord> SelectByWordCodes(IEnumerable<string> wordCodes)
        {
            try
            {
                string inClause = ConvertArrayToString(wordCodes, d => "'" + d + "'");
                string sqlText = "SELECT * FROM CNF_WORD WHERE STATE > =0  ";
                string orderBy = " ORDER BY SORT_INDEX, ID";
                if (wordCodes.Count() == 0)
                {
                    sqlText += orderBy;
                }
                else
                {
                    if (wordCodes.Count() == 1)
                    {
                        sqlText += " AND  WORD_CODE = " + inClause + " " + orderBy;
                    }
                    if (wordCodes.Count() > 1)
                    {
                        sqlText += " AND   WORD_CODE in (" + inClause + ") " + orderBy;
                    }
                }

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

        /// <summary>
        ///  根据<see cref="IDataReader"/>数据读取器构建对象实体
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="word">文件描述器对象</param>
        /// <returns>如果构建对象成功返回对象实例，否则返回NULL</returns>
        public override WapWord Build(IDataReader reader, WapWord word)
        {
            word.WordId = reader.GetReaderValue<int>("ID");
            word.ParentId = reader.GetReaderValue<int>("PARENT_ID");
            word.WordCode = reader.GetReaderValue<string>("WORD_CODE");
            word.WordText = reader.GetReaderValue<string>("WORD_TEXT");
            word.App = reader.GetReaderValue<string>("WORD_APP");
            word.WordValue = reader.GetReaderValue<string>("WORD_VALUE", null, true);
            word.WordSortIndex = reader.GetReaderValue<int>("SORT_INDEX");
            word.WordState = reader.GetReaderValue<int>("STATE");
            word.TenentId = reader.GetReaderValue<int>("TENANT_ID");
            word.Remark = reader.GetReaderValue<string>("REMARK", null, true);
            word.WordGroupKey = reader.GetReaderValue<string>("WORD_GROUP_KEY", null, true);
            word.WordPYCode = reader.GetReaderValue<string>("WORD_PYCODE", null, true);
            word.IsExternalVisible = reader.GetReaderValue<bool>("IS_EXTERNAL_VISIBLE", true, false);

            return base.Build(reader, word);
        }
    }
}

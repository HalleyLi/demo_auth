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

namespace SH3H.WAP.DataAccess.SqlServer
{
    /// <summary>
    /// 口径数据库操作
    /// </summary>
    public class WapCaliberStorage : BaseAccess<WapCaliber>, IWapCaliberStorage
    {
        public WapCaliberStorage()
            : base(Consts.CONFIGURE_DATABASE_CONNECTION_STRING) { }

        public bool CreateSchema()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 添加口径
        /// </summary>
        /// <param name="caliber">口径对象</param>
        /// <param name="trans"></param>
        /// <returns>口径对象</returns>
        public WapCaliber Insert(WapCaliber caliber)
        {
            try
            {

                string sqlText = @"INSERT INTO CNF_CALIBER(CALIBER_NAME,CALIBER_VALUE,TENANT_ID)
                                  VALUES(@CALIBER_NAME,@CALIBER_VALUE,@TENANT_ID)
                                   SELECT @@IDENTITY;";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@CALIBER_NAME", DbType.String, caliber.CaliberName);
                    Database.AddInParameter(cmd, "@CALIBER_VALUE", DbType.String, caliber.CaliberValue);
                    Database.AddInParameter(cmd, "@TENANT_ID", DbType.Int32, caliber.TenantId);
                    int id = ExecuteScalar<int>(cmd);
                    if (id > 0)
                    {
                        caliber.CaliberId = id;
                        return caliber;
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
        /// 删除口径
        /// </summary>
        /// <param name="id">口径编号</param>
        /// <param name="trans"></param>
        /// <returns>是否删除成功</returns>
        public bool Delete(int id)
        {
            try
            {
                string sqlText = @"DELETE FROM CNF_CALIBER
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
        /// 通过口径编号修改口径
        /// </summary>
        /// <param name="id">口径编号</param>
        /// <param name="caliber">口径对象</param>
        /// <param name="trans"></param>
        /// <returns>返回时否修改成功</returns>
        public WapCaliber Update(int id, WapCaliber caliber)
        {
            try
            {
                string sqlText = @"UPDATE CNF_CALIBER 
                                   SET CALIBER_NAME=@CALIBER_NAME,CALIBER_VALUE=@CALIBER_VALUE,TENANT_ID=@TENANT_ID WHERE ID=@ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@ID", DbType.Int32, id);
                    Database.AddInParameter(cmd, "@CALIBER_NAME", DbType.String, caliber.CaliberName);
                    Database.AddInParameter(cmd, "@CALIBER_VALUE", DbType.String, caliber.CaliberValue);
                    Database.AddInParameter(cmd, "@TENANT_ID", DbType.Int32, caliber.TenantId);
                    if (ExecuteNonQuery(cmd) > 0)
                    {
                        return caliber;
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
        /// 通过口径编号查询口径
        /// </summary>
        /// <param name="id">口径编号</param>
        /// <returns>口径对象</returns>
        public WapCaliber Select(int id)
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_CALIBER
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
        /// 获取所有口径
        /// </summary>
        /// <returns>口径对象列表</returns>
        public IEnumerable<WapCaliber> SelectAll()
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_CALIBER ";
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

        public override WapCaliber Build(IDataReader reader, WapCaliber caliber)
        {
            caliber.CaliberId = reader.GetReaderValue<int>("ID");
            caliber.CaliberName = reader.GetReaderValue<string>("CALIBER_NAME");
            caliber.CaliberValue = reader.GetReaderValue<string>("CALIBER_VALUE");
            caliber.TenantId = reader.GetReaderValue<int>("TENANT_ID");
            return base.Build(reader, caliber);
        }
    }
}

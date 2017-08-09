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
    /// 水表型号数据库操作
    /// </summary>
    public class WapModelStorage : BaseAccess<WapModel>, IWapModelStorage
    {
        public WapModelStorage()
            : base(Consts.CONFIGURE_DATABASE_CONNECTION_STRING) { }

        public bool CreateSchema()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 添加水表型号
        /// </summary>
        /// <param name="model">水表型号对象</param>
        /// <param name="trans"></param>
        /// <returns>水表型号对象</returns>
        public WapModel Insert(WapModel model)
        {
            try
            {

                string sqlText = @"INSERT INTO CNF_MODEL(MODEL_NAME,DEVICE_TYPE,REMARK,TENANT_ID)
                                  VALUES(@MODEL_NAME,@DEVICE_TYPE,@REMARK,@TENANT_ID)
                                   SELECT @@IDENTITY;";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@MODEL_NAME", DbType.String, model.ModelName);
                    Database.AddInParameter(cmd, "@DEVICE_TYPE", DbType.Int32, model.DeviceType);
                    Database.AddInParameter(cmd, "@REMARK", DbType.String, model.Remark);
                    Database.AddInParameter(cmd, "@TENANT_ID", DbType.Int32, model.TenantId);
                    int id = ExecuteScalar<int>(cmd);
                    model.ModelId = id;
                    return model;
                }

            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }

        }

        /// <summary>
        /// 删除水表型号
        /// </summary>
        /// <param name="id">水表型号编号</param>
        /// <param name="trans"></param>
        /// <returns>是否删除成功</returns>
        public bool Delete(int id)
        {
            try
            {
                string sqlText = @"DELETE FROM CNF_MODEL
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
        /// 通过水表型号编号修改水表型号
        /// </summary>
        /// <param name="id">水表型号编号</param>
        /// <param name="model">水表型号对象</param>
        /// <param name="trans"></param>
        /// <returns>返回时否修改成功</returns>
        public bool Update(int id, WapModel model)
        {
            try
            {

                string sqlText = @"UPDATE CNF_MODEL 
                                                    SET MODEL_NAME=@MODEL_NAME,DEVICE_TYPE=@DEVICE_TYPE,REMARK=@REMARK,TENANT_ID=@TENANT_ID WHERE ID=@ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@ID", DbType.Int32, id);
                    Database.AddInParameter(cmd, "@MODEL_NAME", DbType.String, model.ModelName);
                    Database.AddInParameter(cmd, "@DEVICE_TYPE", DbType.Int32, model.DeviceType);
                    Database.AddInParameter(cmd, "@REMARK", DbType.String, model.Remark);
                    Database.AddInParameter(cmd, "@TENANT_ID", DbType.Int32, model.TenantId);
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
        /// 通过水表型号编号查询水表型号
        /// </summary>
        /// <param name="id">水表型号编号</param>
        /// <returns>水表型号对象</returns>
        public WapModel Select(int id)
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_MODEL
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
        /// 获取所有水表型号
        /// </summary>
        /// <returns>水表型号对象列表</returns>
        public IEnumerable<WapModel> SelectAll()
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_MODEL ";
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

        public override WapModel Build(IDataReader reader, WapModel model)
        {
            model.ModelId = reader.GetReaderValue<int>("ID");
            model.ModelName = reader.GetReaderValue<string>("MODEL_NAME");
            model.DeviceType = reader.GetReaderValue<int>("DEVICE_TYPE");
            model.Remark = reader.GetReaderValue<string>("REMARK", null, true);
            model.TenantId = reader.GetReaderValue<int>("TENANT_ID");
            return base.Build(reader, model);
        }
    }
}

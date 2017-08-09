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
    /// 水表厂商数据库操作
    /// </summary>
    public class WapProducerStorage : BaseAccess<WapProducer>, IWapProducerStorage
    {
        public WapProducerStorage()
            : base(Consts.CONFIGURE_DATABASE_CONNECTION_STRING) { }

        public bool CreateSchema()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 添加水表厂商
        /// </summary>
        /// <param name="producer">水表厂商对象</param>
        /// <param name="trans"></param>
        /// <returns>水表厂商对象</returns>
        public WapProducer Insert(WapProducer producer)
        {
            try
            {

                string sqlText = @"INSERT INTO CNF_PRODUCER(PRODUCER_NAME,DEVICE_TYPE,PRODUCER_ADDRESS,PRODUCER_CONTACT,PRODUCER_TELEPHONE,REMARK,TENANT_ID)
                                  VALUES(@PRODUCER_NAME,@DEVICE_TYPE,@PRODUCER_ADDRESS,@PRODUCER_CONTACT,@PRODUCER_TELEPHONE,@REMARK,@TENANT_ID)
                                   SELECT @@IDENTITY;";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@PRODUCER_NAME", DbType.String, producer.ProducerName);
                    Database.AddInParameter(cmd, "@DEVICE_TYPE", DbType.Int32, producer.DeviceType);
                    Database.AddInParameter(cmd, "@PRODUCER_ADDRESS", DbType.String, producer.ProducerAddress);
                    Database.AddInParameter(cmd, "@PRODUCER_CONTACT", DbType.String, producer.ProducerContact);
                    Database.AddInParameter(cmd, "@PRODUCER_TELEPHONE", DbType.String, producer.ProducerTelephone);
                    Database.AddInParameter(cmd, "@REMARK", DbType.String, producer.Remark);
                    Database.AddInParameter(cmd, "@TENANT_ID", DbType.Int32, producer.TenantId);
                    int id = ExecuteScalar<int>(cmd);
                    if (id > 0)
                    {
                        producer.ProducerId = id;
                        return producer;
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
        /// 删除水表厂商
        /// </summary>
        /// <param name="id">水表厂商编号</param>
        /// <param name="trans"></param>
        /// <returns>是否删除成功</returns>
        public bool Delete(int id)
        {
            try
            {
                string sqlText = @"DELETE FROM CNF_PRODUCER
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
        /// 通过水表厂商编号修改水表厂商
        /// </summary>
        /// <param name="id">水表厂商编号</param>
        /// <param name="producer">水表厂商对象</param>
        /// <param name="trans"></param>
        /// <returns>返回时否修改成功</returns>
        public bool Update(int id, WapProducer producer)
        {
            try
            {
                string sqlText = @"UPDATE CNF_PRODUCER 
                                   SET PRODUCER_NAME=@PRODUCER_NAME,DEVICE_TYPE=@DEVICE_TYPE,PRODUCER_ADDRESS=@PRODUCER_ADDRESS,PRODUCER_CONTACT=@PRODUCER_CONTACT,PRODUCER_TELEPHONE=@PRODUCER_TELEPHONE,REMARK=@REMARK,TENANT_ID=@TENANT_ID WHERE ID=@ID";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@ID", DbType.Int32, id);
                    Database.AddInParameter(cmd, "@PRODUCER_NAME", DbType.String, producer.ProducerName);
                    Database.AddInParameter(cmd, "@DEVICE_TYPE", DbType.Int32, producer.DeviceType);
                    Database.AddInParameter(cmd, "@PRODUCER_ADDRESS", DbType.String, producer.ProducerAddress);
                    Database.AddInParameter(cmd, "@PRODUCER_CONTACT", DbType.String, producer.ProducerContact);
                    Database.AddInParameter(cmd, "@PRODUCER_TELEPHONE", DbType.String, producer.ProducerTelephone);
                    Database.AddInParameter(cmd, "@REMARK", DbType.String, producer.Remark);
                    Database.AddInParameter(cmd, "@TENANT_ID", DbType.Int32, producer.TenantId);
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
        /// 通过水表厂商编号查询水表厂商
        /// </summary>
        /// <param name="id">水表厂商编号</param>
        /// <returns>水表厂商对象</returns>
        public WapProducer Select(int id)
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_PRODUCER
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
        /// 获取所有水表厂商
        /// </summary>
        /// <returns>水表厂商对象列表</returns>
        public IEnumerable<WapProducer> SelectAll()
        {
            try
            {
                string sqlText = @"SELECT * FROM CNF_PRODUCER ";
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

        public override WapProducer Build(IDataReader reader, WapProducer producer)
        {
            producer.ProducerId = reader.GetReaderValue<int>("ID");
            producer.ProducerName = reader.GetReaderValue<string>("PRODUCER_NAME");
            producer.DeviceType = reader.GetReaderValue<int>("DEVICE_TYPE");
            producer.ProducerAddress = reader.GetReaderValue<string>("PRODUCER_ADDRESS", null, true);
            producer.ProducerContact = reader.GetReaderValue<string>("PRODUCER_CONTACT", null, true);
            producer.ProducerTelephone = reader.GetReaderValue<string>("PRODUCER_TELEPHONE", null, true);
            producer.Remark = reader.GetReaderValue<string>("REMARK", null, true);
            producer.TenantId = reader.GetReaderValue<int>("TENANT_ID");
            return base.Build(reader, producer);
        }
    }
}

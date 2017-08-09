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

namespace SH3H.WAP.Auth.DataAccess.SqlServer
{

    /// <summary>
    /// 定义统一权限应用程序SqlServer数据库访问对象
    /// </summary>
    public class WapAuthSeqStorage
        : BaseAccess<WapAuthSequence>, IWapAuthSeqStorage
    {
        /// <summary>
        /// 构造函数
        /// </summary>        
        public WapAuthSeqStorage()
            : base(Consts.AUTH_DATABASE_CONNECTION_STRING) { }

        /// <summary>
        /// 根据请求标识创建对象的唯一标识
        /// </summary>
        /// <param name="sequenceKey">对象种类标识</param>
        /// <returns>返回对应的序列号对象</returns>
        public WapAuthSequence CreateSequence(string sequenceKey)
        {
            var sequence = new WapAuthSequence();

            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("uspCreateAusSequence"))
                {
                    Database.AddOutParameter(command, "@nextsn", DbType.Int32, 8);
                    Database.AddInParameter(command, "@seqidentity", DbType.String, sequenceKey);

                    ExecuteNonQuery(command);
                    var sn = int.Parse(command.Parameters["@nextsn"].Value.ToString());
                    sequence.Sn = sn;
                    sequence.IdentityKey = Guid.NewGuid().ToString().ToUpper();
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
            }

            return sequence;
        }
    }
}
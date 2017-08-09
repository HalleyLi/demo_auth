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
    public class WapAuthAuditStorage
        : BaseAccess<WapAuthAudit>, IWapAuthAuditStorage
    {
        /// <summary>
        /// 构造函数
        /// </summary>        
        public WapAuthAuditStorage()
            : base(Consts.AUTH_DATABASE_CONNECTION_STRING) { }


        /// <summary>
        /// 新增数据审计对象
        /// </summary>
        /// <param name="audit">数据审计对象</param>
        /// <returns>返回是否插入成功</returns>
        public bool AddAudit(WapAuthAudit audit)
        {
            try
            {
                string commandText = @"INSERT INTO AUTH_AUDIT(KEYWORD_1,KEYWORD_2,KEYWORD_3,TRACKING_GUID,
                                            USER_ACCOUNT,OPERATE_DATE,OPERATE_FUNC,OPERATE_CONTENT,CLIENT_IP,ACK_RESULT)
                                       VALUES(@KEYWORD_1,@KEYWORD_2,@KEYWORD_3,@TRACKING_GUID,@USER_ACCOUNT,@OPERATE_DATE,
                                                @OPERATE_FUNC,@OPERATE_CONTENT,@CLIENT_IP,@ACK_RESULT)";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@KEYWORD_1", DbType.String, audit.Keyword1);
                    Database.AddInParameter(command, "@KEYWORD_2", DbType.String, audit.Keyword2);
                    Database.AddInParameter(command, "@KEYWORD_3", DbType.String, audit.Keyword3);
                    Database.AddInParameter(command, "@TRACKING_GUID", DbType.String, audit.TrackingGuid);
                    Database.AddInParameter(command, "@USER_ACCOUNT", DbType.String, audit.UserAccount);
                    Database.AddInParameter(command, "@OPERATE_DATE", DbType.DateTime, audit.OperateDate);
                    Database.AddInParameter(command, "@OPERATE_FUNC", DbType.String, audit.OperateFunc);
                    Database.AddInParameter(command, "@OPERATE_CONTENT", DbType.String, audit.OperateContent);
                    Database.AddInParameter(command, "@CLIENT_IP", DbType.String, audit.ClientIp);
                    Database.AddInParameter(command, "@ACK_RESULT", DbType.Int32, audit.AckResult);

                    return ExecuteNonQuery(command) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }
    }
}

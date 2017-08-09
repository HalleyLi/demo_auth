using SH3H.SDK.DataAccess.Repo;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using SH3H.WAP.Auth.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess.Repo
{ /// <summary>
    /// 定义统一登陆应用对象仓库
    /// </summary>
    public class WapAuthAuditRepository
        : Repository<IWapAuthAuditStorage>, IWapAuthAuditRepository
    {
        /// <summary>
        /// 新增数据审计对象
        /// </summary>
        /// <param name="audit">数据审计对象</param>
        /// <returns>返回是否插入成功</returns>
        public bool AddAudit(WapAuthAudit audit)
        {
            try
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(1);
                var method = sf.GetMethod();
                string methodFullname = string.Format("{0}.{1}", method.ReflectedType.FullName, method.Name);
                string methodName = methodFullname.Substring(methodFullname.LastIndexOf(".") + 1);

                if (audit.OperateContent.Length > 4000) audit.OperateContent = audit.OperateContent.Substring(0, 4000);
                audit.OperateDate = DateTime.Now;

                if (string.IsNullOrEmpty(audit.TrackingGuid)) audit.TrackingGuid = Guid.NewGuid().ToString();
                if (string.IsNullOrEmpty(audit.OperateFunc)) audit.OperateFunc = methodName;
                if (string.IsNullOrEmpty(audit.UserAccount)) audit.UserAccount = "";
                if (string.IsNullOrEmpty(audit.ClientIp)) audit.ClientIp = "";
                if (string.IsNullOrEmpty(audit.Keyword1)) audit.Keyword1 = "";
                if (string.IsNullOrEmpty(audit.Keyword2)) audit.Keyword2 = "";
                if (string.IsNullOrEmpty(audit.Keyword3)) audit.Keyword3 = "";
                if (!audit.AckResult.HasValue) audit.AckResult = 1;

                return Storage.AddAudit(audit);
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex);
                return false;
            }
        }
    }
}
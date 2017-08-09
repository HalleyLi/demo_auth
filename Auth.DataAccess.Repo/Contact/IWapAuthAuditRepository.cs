using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess.Repo.Contact
{
    /// <summary>
    /// 定义权限认证系统数据审计数据仓储
    /// </summary>
    public interface IWapAuthAuditRepository
    {
        /// <summary>
        /// 添加审计记录
        /// </summary>
        /// <param name="audit"></param>
        bool AddAudit(Model.WapAuthAudit audit);
    }
}

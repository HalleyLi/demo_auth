using SH3H.WAP.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess
{
    /// <summary>
    /// 定义权限认证系统数据审计数据库存储对象
    /// </summary>
    public interface IWapAuthAuditStorage
    {
        /// <summary>
        /// 新增数据审计对象
        /// </summary>
        /// <param name="audit">数据审计对象</param>
        /// <returns>返回是否插入成功</returns>
        bool AddAudit(WapAuthAudit audit);
    }
}

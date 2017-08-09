using SH3H.WAP.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess.Repo.Contact
{
    /// <summary>
    /// 定义权限认证应用序列号数据仓储
    /// </summary>
    public interface IAuthSeqRepository
    {
        WapAuthSequence CreateSequence(string sequenceKey);

    }
}

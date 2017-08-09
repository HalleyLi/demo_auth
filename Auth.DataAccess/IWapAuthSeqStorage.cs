using SH3H.WAP.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess
{
    /// <summary>
    /// 定义权限认证应用序列号数据库存储对象
    /// </summary>
    public interface IWapAuthSeqStorage
    {
        /// <summary>
        /// 根据请求标识创建对象的唯一标识
        /// </summary>
        /// <param name="sequenceKey">对象种类标识</param>
        /// <returns>返回对应的序列号对象</returns>
        WapAuthSequence CreateSequence(string sequenceKey);
    }
}

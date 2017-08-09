using SH3H.SDK.DataAccess.Repo;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using SH3H.WAP.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SH3H.WAP.Auth.DataAccess.Repo
{
    /// <summary>
    /// 定义统一登陆应用对象仓库
    /// </summary>
    public class WapAuthSeqRepository
        : Repository<IWapAuthSeqStorage>, IAuthSeqRepository
    {
        private readonly object lockObj = new object();

        /// <summary>
        /// 根据请求标识创建对象的唯一标识
        /// </summary>
        /// <param name="sequenceKey">对象种类标识</param>
        /// <returns>返回对应的序列号对象</returns>
        public WapAuthSequence CreateSequence(string sequenceKey)
        {
            lock (lockObj)
            {
                return Storage.CreateSequence(sequenceKey);
            }
        }


    }
}
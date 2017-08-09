using SH3H.WAP.Auth.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Core
{
    /// <summary>
    /// 票据信息封装
    /// </summary>
    [Serializable]
    public class TicketObject
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public WapUser User { get; set; }

        /// <summary>
        /// 票据
        /// </summary>
        public string Ticket { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Timeout { get; set; }

        public WapApp App { get; set; }

        /// <summary>
        /// a key identity client
        /// </summary>
        public string ClientKey { get; set; }

        /// <summary>
        /// 最后一次心跳时间
        /// </summary>
        public DateTime LastHeartbeat { get; set; }

        public TicketObject Copy()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, this);
                ms.Seek(0, SeekOrigin.Begin);
                TicketObject copy = bf.Deserialize(ms) as TicketObject;
                return copy;
            }
        }
    }
}

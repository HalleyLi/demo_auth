using SH3H.SDK.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Model
{
    /// <summary>
    /// 定义权限应用序列号对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/Auth")]
    public class WapAuthSequence
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [DataMember]
        public string IdentityKey { get; set; }

        /// <summary>
        /// 流水号
        /// </summary>
        [DataMember]
        public int Sn { get; set; }
    }
}

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
    /// 定义应用程序对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/Auth")]
    public class WapAppState
    {
        public WapAppState()
        { 
        
        }

        /// <summary>
        /// 应用状态
        /// </summary>
        [DataMember]
        public bool Active { get; set; }
    }
}

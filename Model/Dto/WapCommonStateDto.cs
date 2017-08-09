using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Model.Dto
{
    /// <summary>
    /// 定义模型对象状态Dto
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model")]
    public class WapCommonStateDto
    {
        /// <summary>
        /// 激活状态
        /// </summary>
        [DataMember(Name = "active")]
        public bool Active { get; set; }

    }
}

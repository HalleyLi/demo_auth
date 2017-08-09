using SH3H.SDK.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Model
{
    /// <summary>
    /// 水表量程对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/config")]
    public class WapMeterRange
    {

        /// <summary>
        /// 量程id
        /// </summary>
        [DataMember]
        public int RangeId { get; set; }

        /// <summary>
        /// 量程名称
        /// </summary>
        [DataMember]
        public string RangeName { get; set; }

        /// <summary>
        /// 量程值
        /// </summary>
        [DataMember]
        public int RangeValue { get; set; }

        /// <summary>
        /// 量程系数
        /// </summary>
        [DataMember]
        public float RangeVolumes{ get; set; }

        /// <summary>
        /// 量程状态
        /// </summary>
        [DataMember]
        public int RangeState { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapMeterRange)) return false;
            return ((WapMeterRange)obj).RangeId == this.RangeId;
        }
    }
}

using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Model.Dto
{
    /// <summary>
    /// 水表量程Dto对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/config")]
    public class WapMeterRangeDto
    {

        /// <summary>
        /// 量程id
        /// </summary>
        [DataMember(Name = "id")]
        public int RangeId { get; set; }

        /// <summary>
        /// 量程名称
        /// </summary>
        [DataMember(Name = "name")]
        public string RangeName { get; set; }

        /// <summary>
        /// 量程值
        /// </summary>
        [DataMember(Name = "value")]
        public int RangeValue { get; set; }

        /// <summary>
        /// 量程系数
        /// </summary>
        [DataMember(Name = "volumes")]
        public float RangeVolumes { get; set; }

        /// <summary>
        /// 量程状态
        /// </summary>
        [DataMember(Name = "state")]
        public int RangeState { get; set; }


        #region 模块装换

        /// <summary>
        /// model转DTO对象
        /// </summary>
        /// <param name="model">model对象</param>
        /// <returns>dto对象</returns>
        public static WapMeterRangeDto FromModel(WapMeterRange model)
        {
            if (model == null)
                return null;
            WapMeterRangeDto result = new WapMeterRangeDto()
            {
                RangeId = model.RangeId,
                RangeName = model.RangeName,
                RangeValue = model.RangeValue,
                RangeVolumes = model.RangeVolumes,
                RangeState = model.RangeState
            };
            return result;
        }

        /// <summary>
        /// dto转model
        /// </summary>
        /// <returns>model对象</returns>
        public WapMeterRange ToModel()
        {
            WapMeterRange result = new WapMeterRange()
            {
                RangeId = this.RangeId,
                RangeName = this.RangeName,
                RangeValue = this.RangeValue,
                RangeVolumes = this.RangeVolumes,
                RangeState = this.RangeState
            };

            return result;
        }
        #endregion

        /// <summary>
        /// 验证模型
        /// </summary>
        /// <returns>True: 验证通过; False:验证失败</returns>
        public ValidateResult Validate()
        {
            ValidateResult result = new ValidateResult();
            if (string.IsNullOrEmpty(this.RangeName) || this.RangeName.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "量程名不能为空 长度不能超过50");
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapMeterRange)) return false;
            return ((WapMeterRange)obj).RangeId == this.RangeId;
        }

    }
}

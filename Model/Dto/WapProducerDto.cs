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
    /// 水表厂商对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/config")]
    public class WapProducerDto
    {


        /// <summary>
        /// 获取或设置制造商名称
        /// </summary>
        [DataMember(Name = "producerId")]
        public int ProducerId { get; set; }

        /// <summary>
        /// 获取或设置制造商名称
        /// </summary>
        [DataMember(Name = "producerName")]
        public string ProducerName { get; set; }

        /// <summary>
        /// 获取或设置生产商类型
        /// </summary>
        [DataMember(Name = "type")]
        public int DeviceType { get; set; }

        /// <summary>
        /// 获取或设置生产商地址
        /// </summary>
        [DataMember(Name = "address")]
        public string ProducerAddress { get; set; }

        /// <summary>
        /// 获取或设置联系人
        /// </summary>
        [DataMember(Name = "contact")]
        public string ProducerContact { get; set; }

        /// <summary>
        /// 获取或设置联系电话
        /// </summary>
        [DataMember(Name = "tel")]
        public string ProducerTelephone { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        [DataMember(Name = "remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置租户ID
        /// </summary>
        [DataMember(Name = "tenantId")]
        public int TenantId { get; set; }



        #region 模转
        /// <summary>
        /// 返回DTO对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static WapProducerDto FromModel(WapProducer model)
        {
            WapProducerDto result = new WapProducerDto()
            {
                DeviceType = model.DeviceType,
                ProducerAddress = model.ProducerAddress,
                ProducerContact = model.ProducerContact,
                ProducerId = model.ProducerId,
                ProducerName = model.ProducerName,
                ProducerTelephone = model.ProducerTelephone,
                Remark = model.Remark,
                TenantId = model.TenantId
            };

            return result;
        }


        /// <summary>
        /// 以当前对象状态生成一个Model对象
        /// </summary>
        /// <returns></returns>
        public WapProducer ToModel()
        {
            WapProducer result = new WapProducer()
            {
                DeviceType = this.DeviceType,
                ProducerAddress = this.ProducerAddress,
                ProducerContact = this.ProducerContact,
                ProducerId = this.ProducerId,
                ProducerName = this.ProducerName,
                ProducerTelephone = this.ProducerTelephone,
                Remark = this.Remark,
                TenantId = this.TenantId
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

            //名字不能为空 长度不能超过50
            if (string.IsNullOrEmpty(this.ProducerName) || this.ProducerName.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "名字不能为空 长度不能超过50");
            }

            //地址不能为空 长度不能超过200
            if (!string.IsNullOrEmpty(this.ProducerAddress) && this.ProducerAddress.Length > 200)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "地址长度不能超过200");
            }

            //内容长度不能超过20
            if (!string.IsNullOrEmpty(this.ProducerContact) && this.ProducerContact.Length > 20)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "内容长度不能超过20");
            }

            //手机长度不能超过20
            if (!string.IsNullOrEmpty(this.ProducerTelephone) && this.ProducerTelephone.Length > 20)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "手机长度不能超过20");
            }

            //备注不能超过100个字符
            if (!string.IsNullOrEmpty(this.Remark) && this.Remark.Length > 500)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "配置默认值不能超过500个字符");
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapProducerDto)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapProducerDto>(this, obj as WapProducerDto);
        }
    }
}

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
    /// 水表型号对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/config")]
    public class WapModelDto
    {

        /// <summary>
        /// 获取或设置型号名称
        /// </summary>
        [DataMember(Name = "modelId")]
        public int ModelId { get; set; }

        /// <summary>
        /// 获取或设置型号名称
        /// </summary>
        [DataMember(Name = "modelName")]
        public string ModelName { get; set; }

        /// <summary>
        /// 获取或设置型号类型
        /// </summary>
        [DataMember(Name = "type")]
        public int DeviceType { get; set; }

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
        public static WapModelDto FromModel(WapModel model)
        {
            WapModelDto result = new WapModelDto()
            {
                DeviceType = model.DeviceType,
                ModelId = model.ModelId,
                ModelName = model.ModelName,
                Remark = model.Remark,
                TenantId = model.TenantId
            };

            return result;
        }


        /// <summary>
        /// 以当前对象状态生成一个Model对象
        /// </summary>
        /// <returns></returns>
        public WapModel ToModel()
        {
            WapModel result = new WapModel()
            {
                DeviceType = this.DeviceType,
                ModelId = this.ModelId,
                ModelName = this.ModelName,
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

            //名字不能为空 长度不能超过20
            if (string.IsNullOrEmpty(this.ModelName) || this.ModelName.Length > 20)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "名字不能为空 长度不能超过20");
            }

            //名字不能为空 长度不能超过500
            if (!string.IsNullOrEmpty(this.Remark) && this.Remark.Length > 500)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "备注长度不能超过500");
            }

            return result;
        }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapModelDto)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapModelDto>(this, obj as WapModelDto);
        }
    }
}

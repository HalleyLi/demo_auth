
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
    /// 系统配置对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/config")]
    public class WapCaliberDto
    {
        /// <summary>
        /// 获取或设置编号
        /// </summary>
        [DataMember(Name = "caliberId")]
        public int CaliberId { get; set; }

        /// <summary>
        /// 获取或设置口径名称
        /// </summary>
        [DataMember(Name = "caliberName")]
        public string CaliberName { get; set; }

        /// <summary>
        /// 获取或设置口径值
        /// </summary>
        [DataMember(Name = "caliberValue")]
        public string CaliberValue { get; set; }

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
        public static WapCaliberDto FromModel(WapCaliber model)
        {
            WapCaliberDto result = new WapCaliberDto()
            {
                CaliberId = model.CaliberId,
                CaliberName = model.CaliberName,
                CaliberValue = model.CaliberValue,
                TenantId = model.TenantId
            };

            return result;
        }


        /// <summary>
        /// 以当前对象状态生成一个Model对象
        /// </summary>
        /// <returns></returns>
        public WapCaliber ToModel()
        {
            WapCaliber result = new WapCaliber()
            {
                CaliberId = this.CaliberId,
                CaliberName = this.CaliberName,
                CaliberValue = this.CaliberValue,
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
            if (string.IsNullOrEmpty(this.CaliberName) || this.CaliberName.Length > 20)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "名字不能为空 长度不能超过50");
            }

            return result;
        }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapCaliberDto)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapCaliberDto>(this, obj as WapCaliberDto);
        }
    }
}

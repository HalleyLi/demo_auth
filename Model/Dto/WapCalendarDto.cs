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
    public class WapCalendarDto
    {

        /// <summary>
        /// 编号
        /// </summary> 
        [DataMember(Name = "calendarId")]
        public int CalendarId { get; set; }


        /// <summary>
        /// 租户编号
        /// </summary> 
        [DataMember(Name = "tenantId")]
        public int TenantId { get; set; }

        /// <summary>
        /// 编码
        /// </summary> 
        [DataMember(Name = "calendarCode")]
        public string CalendarCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary> 
        [DataMember(Name = "calendarName")]
        public string CalendarName { get; set; }

        /// <summary>
        /// 状态
        /// </summary> 
        [DataMember(Name = "active")]
        public bool States { get; set; }

        /// <summary>
        /// 备注
        /// </summary> 
        [DataMember(Name = "remark")]
        public string Remark { get; set; }

        #region 模转
        /// <summary>
        /// 返回DTO对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static WapCalendarDto FromModel(WapCalendar model)
        {
            WapCalendarDto result = new WapCalendarDto()
            {
                CalendarCode = model.CalendarCode,
                CalendarName = model.CalendarName,
                TenantId = model.TenantId,
                CalendarId = model.CalendarId,
                States = model.States == 1,
                Remark = model.Remark
            };

            return result;
        }


        /// <summary>
        /// 以当前对象状态生成一个Model对象
        /// </summary>
        /// <returns></returns>
        public WapCalendar ToModel()
        {
            WapCalendar result = new WapCalendar()
            {
                CalendarCode = this.CalendarCode,
                CalendarName = this.CalendarName,
                TenantId = this.TenantId,
                CalendarId = this.CalendarId,
                States = this.States ? 1 : 0,
                Remark = this.Remark
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

            //编码不能为空 长度不能超过20
            if (string.IsNullOrEmpty(this.CalendarCode) || this.CalendarCode.Length > 20)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "编码不能为空 长度不能超过20");
            }

            //名字不能为空 长度不能超过20
            if (string.IsNullOrEmpty(this.CalendarName) || this.CalendarName.Length > 20)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "名字不能为空 长度不能超过20");
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
            if (!(obj is WapCalendarDto)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapCalendarDto>(this, obj as WapCalendarDto);
        }
    }
}

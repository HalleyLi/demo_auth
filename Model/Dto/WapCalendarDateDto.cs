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
    /// 节假日
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/config")]
    public class WapCalendarDateDto
    {

        /// <summary>
        /// 获取或设置日历编号
        /// </summary> 
        [DataMember(Name = "calendarDateId")]
        public int CalendarDateId { get; set; }

        /// <summary>
        /// 获取或设置日历编号
        /// </summary> 
        [DataMember(Name = "calendarId")]
        public int CalendarId { get; set; }

        /// <summary>
        /// 获取或设置日历日期
        /// </summary> 
        [DataMember(Name = "calendarDate")]
        public DateTime CalendarDate { get; set; }

        /// <summary>
        /// 获取或设置日历日期类型
        /// </summary> 
        [DataMember(Name = "type")]
        public string CalendarDateType { get; set; }

        /// <summary>
        /// 获取或设置是否为假期的信息
        /// </summary> 
        [DataMember(Name = "isHoliday")]
        public bool IsHoliday { get; set; }

        /// <summary>
        /// 获取或设置是否为工作日的信息
        /// </summary> 
        [DataMember(Name = "isWorkday")]
        public bool IsWorkday { get; set; }

        /// <summary>
        /// 获取或设置假期名称
        /// </summary> 
        [DataMember(Name = "calendarDateName")]
        public string CalendarDateName { get; set; }

        /// <summary>
        /// 获取或设置备注信息
        /// </summary> 
        [DataMember(Name = "remark")]
        public string Remark { get; set; }



        #region 模转
        /// <summary>
        /// 返回DTO对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static WapCalendarDateDto FromModel(WapCalendarDate model)
        {
            WapCalendarDateDto result = new WapCalendarDateDto()
            {
                CalendarDate = model.CalendarDate,
                CalendarDateId = model.CalendarDateId,
                CalendarDateName = model.CalendarDateName,
                CalendarDateType = model.CalendarDateType,
                IsHoliday = model.IsHoliday,
                IsWorkday = model.IsWorkday,
                CalendarId = model.CalendarId,
                Remark = model.Remark
            };

            return result;
        }


        /// <summary>
        /// 以当前对象状态生成一个Model对象
        /// </summary>
        /// <returns></returns>
        public WapCalendarDate ToModel()
        {
            WapCalendarDate result = new WapCalendarDate()
            {
                CalendarDate = this.CalendarDate,
                CalendarDateId = this.CalendarDateId,
                CalendarDateName = this.CalendarDateName,
                CalendarDateType = this.CalendarDateType,
                IsHoliday = this.IsHoliday,
                IsWorkday = this.IsWorkday,
                CalendarId = this.CalendarId,
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

            //类型不能为空 长度不能超过50
            if (string.IsNullOrEmpty(this.CalendarDateType) || this.CalendarDateType.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "类型不能为空 长度不能超过50");
            }

            //名字不能为空 长度不能超过20
            if (string.IsNullOrEmpty(this.CalendarDateName) || this.CalendarDateName.Length > 20)
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
            if (!(obj is WapCalendarDateDto)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapCalendarDateDto>(this, obj as WapCalendarDateDto);
        }
    }
}

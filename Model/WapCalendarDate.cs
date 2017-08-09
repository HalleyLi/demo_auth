using SH3H.SDK.Definition.Auditing;
using SH3H.SDK.Definition.Entities;
using SH3H.SDK.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Linq;

namespace SH3H.WAP.Model
{
    /// <summary>
    /// 定义文件资源描述对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/config")]
    public class WapCalendarDate
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

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapCalendarDate)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapCalendarDate>(this, obj as WapCalendarDate);
        }
    }
}

using SH3H.SDK.Definition.Auditing;
using SH3H.SDK.Definition.Entities;
using SH3H.SDK.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace SH3H.WAP.Model
{
    /// <summary>
    /// 定义文件资源描述对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/config")]
    public class WapCalendar 
    {

        /// <summary>
        /// 获取或设置用户ID
        /// </summary> 
        [DataMember(Name = "calendarId")]
        public int CalendarId { get; set; }


        /// <summary>
        /// 获取或设置租户编号
        /// </summary> 
        [DataMember(Name = "tenantId")]
        public int TenantId { get; set; }

        /// <summary>
        /// 获取或设置日历编码
        /// </summary> 
        [DataMember(Name = "calendarCode")]
        public string CalendarCode { get; set; }

        /// <summary>
        /// 获取或设置日历名称
        /// </summary> 
        [DataMember(Name = "calendarName")]
        public string CalendarName { get; set; }

        /// <summary>
        /// 获取或设置是否放假状态
        /// </summary> 
        [DataMember(Name = "active")]
        public int States { get; set; }

        /// <summary>
        /// 获取或设置备注信息
        /// </summary> 
        [DataMember(Name = "remark")]
        public string Remark { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapCalendar)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapCalendar>(this, obj as WapCalendar);
        }
    }
}

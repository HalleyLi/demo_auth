using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Model.Dto
{
    /// <summary>
    /// 存储获取的角色相关字段信息
    /// </summary>
    public class WapRoleRelativeDto : WapRole
    {
        /// <summary>
        /// 是否关联
        /// </summary>
        [DataMember]
        public bool IsRelative { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string RoleComment { get; set; }

        /// <summary>
        /// 角色排序
        /// </summary>
        [DataMember]
        public decimal RoleSortsn { get; set; }

        /// <summary>
        /// 角色等级
        /// </summary>
        [DataMember]
        public int RoleLevel { get; set; }

        /// <summary>
        /// 角色路径
        /// </summary>
        [DataMember]
        public string RolePath { get; set; }
    }
}

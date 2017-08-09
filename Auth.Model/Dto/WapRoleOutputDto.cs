using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Model.Dto
{
    /// <summary>
    /// 定义角色输出对象
    /// </summary>
    public class WapRoleOutputDto : WapRole
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WapRoleOutputDto()
        {
            this.Roles = new List<WapRoleOutputDto>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="role">角色对象</param>
        public WapRoleOutputDto(WapRole role)
            : this()
        {
            this.RoleKey = role.RoleKey;
            this.RolePycode = role.RolePycode;
            this.RoleName = role.RoleName;
            this.ParentRoleKey = role.ParentRoleKey;
            this.RoleActive = role.RoleActive;
            this.RoleComment = role.RoleComment;
            this.RoleSortsn = role.RoleSortsn;
        }

        /// <summary>
        /// 获取或设置子角色对象列表
        /// </summary>    
        [DataMember]
        public List<WapRoleOutputDto> Roles { get; set; }
    }
}

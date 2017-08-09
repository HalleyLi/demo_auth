using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Model.Dto
{
    /// <summary>
    /// 角色对象DTO
    /// </summary>  
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/Auth")]
    public class WapRoleDto
    {
        /// <summary>
        /// 获取或设置角色KEY
        /// </summary>        
        [DataMember(Name = "roleKey")]
        public string RoleKey { get; set; }

        /// <summary>
        /// 获取或设置角色名称
        /// </summary>        
        [DataMember(Name = "roleName")]
        public string RoleName { get; set; }

        /// <summary>
        /// 获取或设置角色代码
        /// </summary>        
        [DataMember(Name = "pyCode")]
        public string RolePycode { get; set; }

        /// <summary>
        /// 获取或设置父角色KEY
        /// </summary>        
        [DataMember(Name = "parentRoleKey")]
        public string ParentRoleKey { get; set; }

        /// <summary>
        /// 获取或设置当前角色是否为活动角色
        /// </summary>
        [DataMember(Name = "active")]
        public bool RoleActive { get; set; }

        /// <summary>
        /// 获取或设置角色备注
        /// </summary>
        [DataMember(Name = "comment")]
        public string RoleComment { get; set; }

        /// <summary>
        /// 获取或设置角色排序值
        /// </summary>
        [DataMember(Name = "sortSn")]
        public int RoleSortsn { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        [DataMember(Name = "extend")]
        public string Extend { get; set; }

        #region Override

        public override int GetHashCode()
        {
            return RoleKey.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapRole)) return false;
            return ((WapRole)obj).RoleKey == this.RoleKey;
        }

        public override string ToString()
        {
            return RoleName;
        }

        #endregion

        /// <summary>
        /// 数据库模型转换为DTO
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static WapRoleDto FromModel(WapRole role)
        {
            WapRoleDto roledto = new WapRoleDto();
            roledto.RoleKey = role.RoleKey;
            roledto.RoleName = role.RoleName;
            roledto.RolePycode = role.RolePycode;
            roledto.ParentRoleKey = role.ParentRoleKey;
            roledto.RoleActive = role.RoleActive;
            roledto.RoleComment = role.RoleComment;
            roledto.RoleSortsn = role.RoleSortsn;
            roledto.Extend = role.Extend;
            return roledto;
        }
        /// <summary>
        /// DTO转化为数据库模型
        /// </summary>
        /// <returns></returns>
        public WapRole ToModel()
        {
            WapRole role = new WapRole();
            role.RoleKey = this.RoleKey;
            role.RoleName = this.RoleName;
            role.RolePycode = this.RolePycode;
            role.ParentRoleKey = this.ParentRoleKey;
            role.RoleActive = this.RoleActive;
            role.RoleComment = this.RoleComment;
            role.RoleSortsn = this.RoleSortsn;
            role.Extend = this.Extend;
            return role;
        }
        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (string.IsNullOrEmpty(this.RoleName))
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "角色名称不能为空");
            }
            if (!string.IsNullOrEmpty(this.RoleName) && this.RoleName.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "角色名称长度不能超过50字符");
            }
            if (!String.IsNullOrEmpty(this.RolePycode) && this.RolePycode.Length > 10)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "角色拼音码长度不能超过10字符");
            }
            if (!String.IsNullOrEmpty(this.RoleComment) && this.RoleComment.Length > 500)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "角色备注长度不能超过500字符");
            }

            return result;
        }
    }
}

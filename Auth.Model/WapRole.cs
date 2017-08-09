using SH3H.SDK.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Model
{
    /// <summary>
    /// 定义角色对象，角色对象可以嵌套多个子角色对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model")]
    public class WapRole
    {
        /// <summary>
        /// 获取或设置角色KEY
        /// </summary>        
        [DataMember]
        public string RoleKey { get; set; }

        /// <summary>
        /// 获取或设置角色名称
        /// </summary>        
        [DataMember]
        public string RoleName { get; set; }

        /// <summary>
        /// 获取或设置角色代码
        /// </summary>        
        [DataMember]
        public string RolePycode { get; set; }

        /// <summary>
        /// 获取或设置父角色KEY
        /// </summary>        
        [DataMember]
        public string ParentRoleKey { get; set; }

        /// <summary>
        /// 获取或设置当前角色是否为活动角色
        /// </summary>
        [DataMember]
        public bool RoleActive { get; set; }

        /// <summary>
        /// 获取或设置角色备注
        /// </summary>
        [DataMember]
        public string RoleComment { get; set; }

        /// <summary>
        /// 获取或设置角色排序值
        /// </summary>
        [DataMember]
        public int RoleSortsn { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        [DataMember]
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
    }
}

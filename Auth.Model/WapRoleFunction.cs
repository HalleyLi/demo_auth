using SH3H.SDK.Share;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Model
{
    /// <summary>
    /// 角色功能点关联
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/Auth")]
    public class WapRoleFunction
    {
        /// <summary>
        /// 角色key
        /// </summary>
        [DataMember(Name = "roleKey")]
        public string RoleKey { get; set; }

        /// <summary>
        /// 功能点key
        /// </summary>
        [DataMember(Name = "funcKey")]
        public string FuncKey { get; set; }


        /// <summary>
        /// 关系key
        /// </summary>
        [DataMember(Name = "relationKey")]
        public string RelationKey { get; set; }

        /// <summary>
        /// 角色层级，绝对层级，相对于根节点的层级
        /// </summary>
        [DataMember(Name = "roleLevel")]
        public int RoleLevel { get; set; }

        /// <summary>
        /// 角色路径，绝对路径，相对于根节点的路径
        /// </summary>
        [DataMember(Name = "rolePath")]
        public string RolePath { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapRoleFunction)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapRoleFunction>(this, obj as WapRoleFunction);
        }
    }
}

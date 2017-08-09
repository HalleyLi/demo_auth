using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Model.Dto
{
    /// <summary>
    /// 角色与用户关系模型
    /// </summary>
    [DataContract]
    public class RoleUserRelation
    {
        /// <summary>
        /// 用户key
        /// </summary>
        [DataMember]
        public string UserKey { get; set; }

        /// <summary>
        /// 角色key
        /// </summary>
        [DataMember]
        public string RoleKey { get; set; }

        /// <summary>
        /// 关系key
        /// </summary>
        [DataMember]
        public string RelationKey { get; set; }
    }
}

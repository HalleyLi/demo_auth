using SH3H.SDK.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Model.Dto
{
    /// <summary>
    /// 更新角色关系
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/Auth")]
    public class WapUpdateRoleRelationDto
    {
        /// <summary>
        /// 删除的关系
        /// </summary>
        [DataMember(Name = "delRoles")]
        public IEnumerable<string> DelRoles { get; set; }

        /// <summary>
        /// 添加的关系
        /// </summary>
        [DataMember(Name = "addRoles")]
        public IEnumerable<string> AddRoles { get; set; }

        public WapUpdateRoleRelationDto()
        {
            DelRoles = new List<string>();
            AddRoles = new List<string>();
        }
    }
}

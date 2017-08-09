using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Model.Dto
{
    public class UpdateUserOrganizationRelationModel
    {
        /// <summary>
        /// 解除的角色关系
        /// </summary>
        [DataMember]
        public IEnumerable<UserOrganizationRelation> DeletedArr { get; set; }

        /// <summary>
        /// 新增的组织关系
        /// </summary>
        [DataMember]
        public IEnumerable<UserOrganizationRelation> AddArr { get; set; }

        public UpdateUserOrganizationRelationModel()
        {
            DeletedArr = new List<UserOrganizationRelation>();
            AddArr = new List<UserOrganizationRelation>();
        }
    }
}

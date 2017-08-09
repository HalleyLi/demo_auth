using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Model.Dto
{
    public class UserOrganizationRelation
    {
        /// <summary>
        /// 用户key
        /// </summary>
        [DataMember]
        public string UserKey { get; set; }

        /// <summary>
        /// 组织key
        /// </summary>
        [DataMember]
        public string OrganKey { get; set; }

        /// <summary>
        /// 关系key
        /// </summary>
        [DataMember]
        public string RelationKey { get; set; }
    }
}

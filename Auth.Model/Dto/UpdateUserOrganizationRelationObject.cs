using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Model.Dto
{
    //与指定userkey关联的添加或者删除的组织结构集合对象
    public class UpdateUserOrganizationRelationObject
    {
        /// <summary>
        /// 与指定用户对应的需要删除关联的组织结构
        /// </summary>
        [DataMember]
        public IEnumerable<string> delOrg { get; set; }
        /// <summary>
        /// 与指定用户对应的需要添加的关联组织结构
        /// </summary>
        [DataMember]
        public IEnumerable<string> addOrg { get; set; }

        public UpdateUserOrganizationRelationObject()
        {
            delOrg = new List<string>();
            addOrg = new List<string>();
        }
    }
}

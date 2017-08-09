using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SH3H.SDK.Share;
using SH3H.SDK.Definition.Entities;
using System.Runtime.Serialization;
namespace SH3H.WAP.Auth.Model
{
    /// <summary>
    /// 定义组织结构对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/Auth")]
    public class WapOrganization
    {
        /// <summary>
        /// 组织对象主键
        /// </summary>
        [DataMember]
        public string OrganizationKey { get; set; }
        /// <summary>
        /// 获取或设置父ID
        /// </summary>
        [DataMember]
        public string ParentOrganizationKey { get; set; }

        /// <summary>
        /// 获取或设置站点编码
        /// </summary>
        [DataMember]
        public string OrganizationCode { get; set; }

        /// <summary>
        /// 获取或设置站点名称
        /// </summary>
        [DataMember]
        public string OrganizationName { get; set; }

        /// <summary>
        /// 获取或设置类型
        /// </summary>
        [DataMember]
        public int OrganizationType { get; set; }

        /// <summary>
        /// 获取或设置地址
        /// </summary>
        [DataMember]
        public string OrganizationAddress { get; set; }

        /// <summary>
        /// 获取或设置责任人
        /// </summary>
        [DataMember]
        public string OrganizationDutyMan { get; set; }

        /// <summary>
        /// 获取或设置联系电话
        /// </summary>
        [DataMember]
        public string OrganizationTel { get; set; }

        /// <summary>
        /// 获取或设置排序
        /// </summary>
        [DataMember]
        public int SortIndex { get; set; }

        /// <summary>
        /// 获取或设置状态
        /// </summary>
        [DataMember]
        public int State { get; set; }


        /// <summary>
        /// 获取或设置租户ID
        /// </summary>
        [DataMember]
        public int TenantId { get; set; }

        /// <summary>
        /// 扩展
        /// </summary>
        [DataMember]
        public string Extend { get; set; }

        #region Override

        public override int GetHashCode()
        {
            return OrganizationKey.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapOrganization)) return false;
            return ((WapOrganization)obj).OrganizationKey == this.OrganizationKey;
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", OrganizationCode, OrganizationKey);
        }

        #endregion
    }
}

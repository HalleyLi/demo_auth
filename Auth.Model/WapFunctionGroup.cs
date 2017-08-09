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
    /// 功能组下功能点
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model")]
    public class WapFunctionGroup
    {
        /// <summary>
        /// 功能点组主键
        /// </summary>
        [DataMember(Name = "funcGroupKey")]
        public Guid FuncGroupKey { get; set; }

        /// <summary>
        /// 工能点组名
        /// </summary>
        [DataMember(Name = "funcGroupName")]
        public string FuncGroupName { get; set; }

        /// <summary>
        /// 拼音Code
        /// </summary>
        [DataMember(Name = "funcGroupPycode")]
        public string FuncGroupPycode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DataMember(Name = "funcGroupSortsn")]
        public decimal FuncGroupSortsn { get; set; }

        /// <summary>
        /// 所属应用
        /// </summary>
        [DataMember(Name = "funcAppKey")]
        public string FuncAppKey { get; set; }


        /// <summary>
        /// 上级功能点组
        /// </summary>
        [DataMember(Name = "parentFuncGroupKey")]
        public Guid ParentFuncGroupKey { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        [DataMember(Name = "funcGroupActive")]
        public bool? FuncGroupActive { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember(Name = "runcGroupComment")]
        public string FuncGroupComment { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember(Name = "extend")]
        public string Extend { get; set; }

        public WapFunctionGroup()
        {
            this.FuncAppKey = Guid.Empty.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapFunctionGroup)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapFunctionGroup>(this, obj as WapFunctionGroup);
        }
    }
}

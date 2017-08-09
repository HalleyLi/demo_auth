using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using SH3H.SDK.Share;
using SH3H.SDK.Definition.Entities;

namespace SH3H.WAP.Model
{

    /// <summary>
    /// 词语对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/config")]
    public class WapWord 
    {
        /// <summary>
        /// 编号
        /// </summary>
        [DataMember(Name = "wordId")]
        public int WordId { get; set; }

        /// <summary>
        /// 父编号
        /// </summary>
        [DataMember(Name = "parentId")]
        public int ParentId { get; set; }


        /// <summary>
        /// 编码
        /// </summary>
        [DataMember(Name = "wordCode")]
        public string WordCode { get; set; }

        /// <summary>
        /// 词语文本
        /// </summary>
        [DataMember(Name = "wordText")]
        public string WordText { get; set; }

        /// <summary>
        /// 词语值
        /// </summary>
        [DataMember(Name = "wordValue")]
        public string WordValue { get; set; }

        /// <summary>
        /// 所属应用标识
        /// </summary>
        [DataMember(Name = "app")]
        public string App { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        [DataMember(Name = "sort")]
        public int WordSortIndex { get; set; }

        /// <summary>
        /// 获取或设置词语状态
        /// </summary>
        [DataMember(Name = "active")]
        public int WordState { get; set; }

        /// <summary>
        /// 获取或设置租户编号
        /// </summary>
        [DataMember(Name = "tenentId")]
        public int TenentId { get; set; }

        /// <summary>
        /// 获取或设置词语备注
        /// </summary>
        [DataMember(Name = "remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 词语分组字段
        /// </summary>
        [DataMember(Name = "groupKey")]
        public string WordGroupKey { get; set; }

        /// <summary>
        /// 词语拼音码
        /// </summary>
        [DataMember(Name = "pyCode")]
        public string WordPYCode{ get; set; }

        /// <summary>
        /// 是否外部可见
        /// </summary>
        [DataMember(Name = "isExternalVisible")]
        public bool IsExternalVisible { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapWord)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapWord>(this, obj as WapWord);
        }
    }
}

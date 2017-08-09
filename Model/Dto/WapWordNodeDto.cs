using SH3H.SDK.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Model.Dto
{
    /// <summary>
    /// 分层处理
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/config")]
    public class WapWordNodeDto : WapWordDto
    {
          /// <summary>
        /// 新建类型为WapWordNodeDto列表
        /// </summary>
        public WapWordNodeDto()
        {
            this.Nodes = new List<WapWordNodeDto>();
        }

        /// <summary>
        /// 复制WapWord对象
        /// </summary>
        /// <param name="word">WapWord对象</param>
        public WapWordNodeDto(WapWordDto word)
            : this()
        {
            this.WordId = word.WordId;
            this.ParentId = word.ParentId;
            this.Remark = word.Remark;
            this.TenentId = word.TenentId;
            this.WordCode = word.WordCode;
            this.WordGroupKey = word.WordGroupKey;
            this.WordSortIndex = word.WordSortIndex;
            this.WordState = word.WordState;
            this.WordText = word.WordText;
            this.WordValue = word.WordValue;
        }

        /// <summary>
        /// 获取或设置子节点
        /// </summary>
        [DataMember]
        public List<WapWordNodeDto> Nodes { get; set; }

        /// <summary>
        /// 建立WapWordNodeDto树
        /// </summary>
        /// <param name="words">WapWord对象集合</param>
        /// <returns>WapWordNodeDto树</returns>
        public static IEnumerable<WapWordNodeDto> Create(IEnumerable<WapWordDto> words)
        {
            var dicNodes = words
                .Select(word => new WapWordNodeDto(word))
                .ToDictionary(node => node.WordId);

            foreach (var node in dicNodes.Values)
            {
                if (node.ParentId == 0) continue;
                WapWordNodeDto parentNode = null;
                if (dicNodes.TryGetValue(node.ParentId, out parentNode))
                    parentNode.Nodes.Add(node);
            }
            return dicNodes.Values.Where(node => node.ParentId == 0);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapWordNodeDto)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapWordNodeDto>(this, obj as WapWordNodeDto);
        }
    }
}

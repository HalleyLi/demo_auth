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
    /// 功能点
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/Auth")]
    public class WapFunction
    {
        /// <summary>
        /// 获取或设置功能点关键字
        /// </summary>
        [DataMember(Name = "key")]
        public string Key { get; set; }

        /// <summary>
        /// 获取或设置功能点代码
        /// </summary>
        [DataMember(Name = "code")]
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置功能点名称
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置当前功能点所在分组
        /// </summary>
        //[DataMember]
        //public string GroupName { get; set; }

        /// <summary>
        /// 获取或设置功能点拼音码
        /// </summary>
        [DataMember(Name = "pycode")]
        public string Pycode { get; set; }

        /// <summary>
        /// 获取或设置功能点排序
        /// </summary>
        [DataMember(Name = "sortsn")]
        public decimal Sortsn { get; set; }

        /// <summary>
        /// 功能点组是否激活状态
        /// </summary>
        [DataMember(Name = "active")]
        public bool? Active { get; set; }

        /// <summary>
        /// 功能点备注信息
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// 临时KEY
        /// </summary>
        [DataMember(Name = "templateKey")]
        public string TemplateKey { get; set; }

        /// <summary>
        /// 额外信息
        /// </summary>
        [DataMember(Name = "extend")]
        public string Extend { get; set; }

        /// <summary>
        /// 所属应用主键
        /// </summary>
        [DataMember(Name = "appKey")]
        public string AppKey { get; set; }


        /// <summary>
        /// 功能点组Key
        /// </summary>
        [DataMember(Name = "funcGroupKey")]
        public string FuncGroupKey { get; set; }

        /// <summary>
        /// 复制功能点对象
        /// </summary>
        /// <returns>返回新的功能点对象</returns>
        public WapFunction Clone()
        {
            WapFunction func = new WapFunction();
            func.Key = this.Key;
            func.Code = this.Code;
            func.Name = this.Name;
            //func.GroupName = this.GroupName;
            func.Pycode = this.Pycode;
            func.Extend = this.Extend;
            return func;
        }

        #region Override

        public override string ToString()
        {
            return string.Format("{0}({1})", Name, Code);
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapFunction)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapFunction>(this, obj as WapFunction);
        }
    }
}

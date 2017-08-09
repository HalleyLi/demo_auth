using SH3H.SDK.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Model
{   /// <summary>
    /// 定义应用程序对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/Auth")]
    public class WapApp
    {
        /// <summary>
        /// 获取或设置应用程序Key
        /// </summary>
        [DataMember]
        public string AppKey { get; set; }

        /// <summary>
        /// 获取或设置应用程序标识
        /// </summary>
        [DataMember]
        public string AppIdentity { get; set; }

        /// <summary>
        /// 获取或设置应用程序名称
        /// </summary>
        [DataMember]
        public string AppName { get; set; }

        /// <summary>
        /// 获取或设置应用拼音码
        /// </summary>
        [DataMember]
        public string PyCode { get; set; }

        /// <summary>
        /// 获取或设置应用排序
        /// </summary>
        [DataMember]
        public int SortSn { get; set; }
        /// <summary>
        /// 获取或设置应用程序是否激活
        /// </summary>
        [DataMember]
        public bool? Active { get; set; }

        /// <summary>
        /// 获取或设置应用类型
        /// </summary>
        [DataMember]
        public int Type { get; set; }

        /// <summary>
        /// 获取或设置应用程序描述
        /// </summary>
        [DataMember]
        public string Comment { get; set; }

        /// <summary>
        /// 获取或设置应用图标
        /// </summary>
        [DataMember]
        public string Icon { get; set; }

        /// <summary>
        /// 获取或设置应用首页地址
        /// </summary>
        [DataMember]
        public string DefaultIndex { get; set; }

        /// <summary>
        /// 获取或设置应用程序扩展信息
        /// </summary>
        [DataMember]
        public string Extend { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapApp)) return false;
            return ((WapApp)obj).AppKey == this.AppKey;
        }        
    }
}
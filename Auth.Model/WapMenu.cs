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
    /// 菜单
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model")]
    public class WapMenu
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WapMenu()
        {
            this.Menus = new List<WapMenu>();
        }
        /// <summary>
        /// 获取或设置当前菜单的子菜单列表
        /// </summary>
        [DataMember(Name = "menus")]
        public List<WapMenu> Menus { get; set; }

        /// <summary>
        /// 获取或设置菜单主键
        /// </summary>
        [DataMember(Name = "menuKey")]
        public string MenuKey { get; set; }

        /// <summary>
        /// 获取或设置菜单名称
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置当前菜单的父菜单主键
        /// </summary>
        [DataMember(Name = "parentKey")]
        public string ParentKey { get; set; }

        /// <summary>
        /// 获取或设置当前菜单排列序号
        /// </summary>
        [DataMember(Name = "sortsn")]
        public int Sortsn { get; set; }

        /// <summary>
        /// 获取或设置当前菜单是否为活动菜单
        /// </summary>
        [DataMember(Name = "isActive")]
        public bool IsActive { get; set; }

        /// <summary>
        /// 获取或设置当前菜单备注
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// 获取或设置功能点Key
        /// </summary>
        [DataMember(Name = "funKey")]
        public string FunKey { get; set; }

        /// <summary>
        /// 获取或设置Url
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// 获取或设置当前菜单对应的组件编号
        /// </summary>
        [DataMember(Name = "componentId")]
        public string ComponentId { get; set; }

        /// <summary>
        /// 获取或设置当前菜单应用的CSS样式名称
        /// </summary>
        [DataMember(Name = "css")]
        public string Css { get; set; }

        /// <summary>
        /// 获取或设置应用Key
        /// </summary>
        [DataMember(Name = "appKey")]
        public string AppKey { get; set; }

        /// <summary>
        /// 获取或设置当前菜单是否显示
        /// </summary>
        [DataMember(Name = "isShow")]
        public bool IsShow { get; set; }

        /// <summary>
        /// 获取或设置参数
        /// </summary>
        [DataMember(Name = "parameter")]
        public string Parameter { get; set; }

        /// <summary>
        /// 获取或设置扩展信息
        /// </summary>
        [DataMember(Name = "extend")]
        public string Extend { get; set; }

        /// <summary>
        /// 获取功能点Code
        /// </summary>
        [DataMember(Name = "funCode")]
        public string FunCode { get; set; }

        /// <summary>
        /// 获取功能点可用状态
        /// </summary>
        [DataMember(Name = "funActive")]
        public bool? FunActive { get; set; }

        /// <summary>
        /// 是否是权限功能
        /// </summary>
        [DataMember(Name = "isFuncActive")]
        public bool IsFuncActive { get; set; }

        /// <summary>
        /// 获取或设置应用Identity
        /// </summary>
        [DataMember(Name = "appIdentity")]
        public string AppIdentitfy { get; set; }

        #region Override

        public override string ToString()
        {
            return Name;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapMenu)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapMenu>(this, obj as WapMenu);
        }
    }
}

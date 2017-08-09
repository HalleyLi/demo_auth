using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Model.Dto
{
    /// <summary>
    /// 修改用户配置对象传入参数
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/config")]
    public class WapModifyUserSettingDto
    {
        /// <summary>
        /// 获取或设置用户编号
        /// </summary>
        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        /// <summary>
        /// 获取或设置用户配置编码
        /// </summary>
        [DataMember(Name = "settingCode")]
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置用户配置更新实体
        /// </summary>
        [DataMember(Name = "userSetting")]
        public WapUserSettingDto UserSetting { get; set; }
    }
}

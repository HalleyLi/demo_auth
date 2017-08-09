using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using SH3H.SDK.Share;
namespace SH3H.WAP.Auth.Model
{
    /// <summary>
    /// 定义用户对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/Auth")]
    public class WapUser
    {
        /// <summary>
        /// 获取或设置用户唯一标识
        /// </summary>
        [DataMember]
        public string UserKey { get; set; }

        /// <summary>
        /// 获取或设置用户编号
        /// </summary>        
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置用户姓名
        /// </summary>        
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置用户编码(拼音码)
        /// </summary>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置用户账号
        /// </summary>
        [DataMember]
        public string Account { get; set; }

        /// <summary>
        /// 获取或设置用户工号
        /// </summary>
        [DataMember]
        public string JobNumber { get; set; }

        /// <summary>
        /// 获取或设置用户手机号
        /// </summary>
        [DataMember]
        public string Cellphone { get; set; }
        /// <summary>
        /// 获取或设置用户电话
        /// </summary>
        [DataMember]
        public string Phone { get; set; }
        /// <summary>
        /// 获取或设置用户状态
        /// </summary>
        [DataMember]
        public bool? Active { get; set; }

        /// <summary>
        /// 获取或设置用户域帐户
        /// </summary>
        [DataMember]
        public string DomainAccount { get; set; }

        /// <summary>
        /// 获取或设置用户密码
        /// </summary>
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置用户序号
        /// </summary>
        [DataMember]
        public int SortSn { get; set; }

        /// <summary>
        /// 获取或设置用户备注
        /// </summary>
        [DataMember]
        public string Comment { get; set; }

        /// <summary>
        /// 获取或设置用户邮箱
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置用户身份证号
        /// </summary>
        [DataMember]
        public string IdCard { get; set; }

        /// <summary>
        /// 获取或设置用户生日
        /// </summary>
        [DataMember]
        public long? Birthday { get; set; }

        /// <summary>
        /// 获取或设置用户性别,1:男，2:女
        /// </summary>
        [DataMember]
        public int? Sex { get; set; }

        /// <summary>
        /// 获取或设置用户地址
        /// </summary>
        [DataMember]
        public string Address { get; set; }

        /// <summary>
        /// 获取或设置用户邮编
        /// </summary>
        [DataMember]
        public string PostNo { get; set; }

        /// <summary>
        /// 获取或设置扩展信息
        /// </summary>
        [DataMember]
        public string Extend { get; set; }

        /// <summary>
        /// 获取或设置文件MD5哈希值
        /// </summary>
        [DataMember]
        public string FileHash { get; set; }

        #region Override

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapUser)) return false;
            return ((WapUser)obj).Id == this.Id;
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", Name, Id);
        }

        

        #endregion
    }
}

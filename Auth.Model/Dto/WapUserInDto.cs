using AutoMapper;
using SH3H.SDK.Share;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Model.Dto
{
    /// <summary>
    /// 定义应用程序对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/Auth")]
    public class WapUserInDto
    {
        /// <summary>
        /// 获取或设置用户姓名
        /// </summary>        
        [DataMember(Name = "userName")]
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置用户工号
        /// </summary>
        [DataMember(Name = "jobNumber")]
        public string JobNumber { get; set; }

        /// <summary>
        /// 获取或设置用户账号
        /// </summary>
        [DataMember(Name = "account")]
        public string Account { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [DataMember(Name="password")]
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置用户编码(拼音码)
        /// </summary>
        [DataMember(Name = "pyCode")]
        public string PyCode { get; set; }

        /// <summary>
        /// 获取或设置用户状态
        /// </summary>
        [DataMember(Name = "active")]
        public bool? Active { get; set; }

        /// <summary>
        /// 获取或设置用户备注
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// 获取或设置用户邮箱
        /// </summary>
        [DataMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置用户身份证号
        /// </summary>
        [DataMember(Name = "idCard")]
        public string IdCard { get; set; }

        /// <summary>
        /// 获取或设置用户生日
        /// </summary>
        [DataMember(Name = "birthday")]
        public long? Birthday { get; set; }

        /// <summary>
        /// 获取或设置用户性别,1:男，2:女
        /// </summary>
        [DataMember(Name = "sex")]
        public int? Sex { get; set; }

        /// <summary>
        /// 获取或设置用户电话
        /// </summary>
        [DataMember(Name = "phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 获取或设置用户手机号
        /// </summary>
        [DataMember(Name = "cellphone")]
        public string Cellphone { get; set; }

        /// <summary>
        /// 获取或设置用户地址
        /// </summary>
        [DataMember(Name = "address")]
        public string Address { get; set; }

        /// <summary>
        /// 获取或设置用户邮编
        /// </summary>
        [DataMember(Name = "postNo")]
        public string PostNo { get; set; }

        /// <summary>
        /// 获取或设置扩展信息
        /// </summary>
        [DataMember(Name = "extend")]
        public string Extend { get; set; }

        /// <summary>
        /// 获取或设置文件哈希值
        /// </summary>
        [DataMember(Name = "fileHash")]
        public string FileHash { get; set; }

        /// <summary>
        /// 从WapUserInDto对象转换为WapUser对象
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static WapUser ToModel(WapUserInDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            WapUser model = new WapUser()
            {
                Name=dto.UserName,
                JobNumber=dto.JobNumber,
                Account=dto.Account,
                Password=dto.Password,
                Code=dto.PyCode,
                Active=dto.Active,
                Comment=dto.Comment,
                Email=dto.Email,
                IdCard=dto.IdCard,
                Birthday=dto.Birthday,
                Sex=dto.Sex,
                Phone=dto.Phone,
                Cellphone=dto.Cellphone,
                Address=dto.Address,
                PostNo=dto.PostNo,
                Extend=dto.Extend,
                DomainAccount="",
                Id=0,
                SortSn=0,
                UserKey="",
                FileHash=dto.FileHash
            };

            return model;
        }

        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (string.IsNullOrEmpty(this.UserName))
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "用户名称不允许为空");
            }
            if (!string.IsNullOrEmpty(this.UserName) && this.UserName.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "用户名称长度不允许超过50");
            }

            if (string.IsNullOrEmpty(this.JobNumber))
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "工号不允许为空");
            }
            if (!string.IsNullOrEmpty(this.JobNumber) && this.JobNumber.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "工号长度不允许超过50");
            }

            if (string.IsNullOrEmpty(this.Account))
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "用户账号不允许为空");
            }
            if (!string.IsNullOrEmpty(this.Account) && this.Account.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "用户账号长度不允许超过50");
            }

            //if (string.IsNullOrEmpty(this.Password))
            //{
            //    result.AddError(StateCode.CODE_ARGUMENT_NULL, "用户密码不允许为空");
            //}
            //if (this.Password.Length > 200)
            //{
            //    result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "用户密码长度不允许超过200");
            //}

            //if (string.IsNullOrEmpty(this.PyCode))
            //{
            //    result.AddError(StateCode.CODE_ARGUMENT_NULL, "拼音码不允许为空");
            //}
            if (!string.IsNullOrEmpty(this.PyCode) && this.PyCode.Length > 10)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "拼音码长度不允许超过10");
            }

            return result;
        }

        #region Override

        public override int GetHashCode()
        {
            return UserName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapUserInDto)) return false;
            return ((WapUserInDto)obj).UserName == this.UserName;
        }

        #endregion
    }
}

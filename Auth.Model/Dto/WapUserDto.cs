using AutoMapper;
using SH3H.SDK.Share;
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
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/Auth")]
    public class WapUserDto
    {     
        /// <summary>
        /// 获取或设置用户唯一标识
        /// </summary>
        [DataMember(Name = "userKey")]
        public string UserKey { get; set; }

        /// <summary>
        /// 获取或设置用户编号
        /// </summary>        
        [DataMember(Name = "userId")]
        public int UserId { get; set; }

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
        /// 获取或设置用户域帐户
        /// </summary>
        [DataMember(Name = "domainAccount")]
        public string DomainAccount { get; set; }

        /// <summary>
        /// 获取或设置用户编码(拼音码)
        /// </summary>
        [DataMember(Name = "pyCode")]
        public string PyCode { get; set; }

        /// <summary>
        /// 获取或设置用户序号
        /// </summary>
        [DataMember(Name = "sortSn")]
        public int SortSn { get; set; }

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
        /// 获取或设置文件MD5哈希值
        /// </summary>
        [DataMember(Name="fileHash")]
        public string FileHash { get; set; }

        #region Override

        public override int GetHashCode()
        {
            return UserId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapUserDto)) return false;
            return ((WapUserDto)obj).UserKey == this.UserKey;
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", UserName, UserId);
        }

        #endregion

        static WapUserDto()
        { 
        
        }

        /// <summary>
        /// 将model转换为dto
        /// </summary>
        /// <param name="model">要转换的用户model对象.</param>
        /// <returns>返回转换后的用户dto对象.</returns>
        public static WapUserDto FromModel(WapUser model)
        {
            if (model == null)
            {
                return null;
            }

            WapUserDto dto = new WapUserDto() 
            { 
                UserKey=model.UserKey,
                UserId=model.Id,
                UserName=model.Name,
                JobNumber=model.JobNumber,
                Account=model.Account,
                DomainAccount=model.DomainAccount,
                PyCode=model.Code,
                SortSn=model.SortSn,
                Active=model.Active,
                Comment=model.Comment,
                Phone=model.Phone,
                Cellphone=model.Cellphone,
                Email=model.Email,
                IdCard=model.IdCard,
                Birthday=model.Birthday,
                Sex=model.Sex,
                Address=model.Address,
                PostNo=model.PostNo,
                Extend=model.Extend,
                FileHash=model.FileHash
            };

            return dto;
        }

        /// <summary>
        /// 将dto转换为model
        /// </summary>
        /// <param name="dto">要转换的用户dto对象</param>
        /// <returns>返回转换后的model对象</returns>
        public static WapUser ToModel(WapUserDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            WapUser model = new WapUser() 
            {
                UserKey=dto.UserKey,
                Id=dto.UserId,
                Name=dto.UserName,
                JobNumber=dto.JobNumber,
                Account=dto.Account,                
                DomainAccount=dto.DomainAccount,
                Code=dto.PyCode,
                SortSn=dto.SortSn,
                Active=dto.Active,
                Comment=dto.Comment,
                Phone=dto.Phone,
                Cellphone=dto.Cellphone,
                Email=dto.Email,
                IdCard =dto.IdCard,
                Birthday=dto.Birthday,
                Sex=dto.Sex,
                Address=dto.Address,
                PostNo=dto.PostNo,
                Extend=dto.Extend,
                Password="",
                FileHash=dto.FileHash
            };

            return model;
        }

        /// <summary>
        /// 返回Dto对象列表
        /// </summary>
        /// <param name="models">model对象列表</param>
        /// <returns>返回Dto对象列表.</returns>
        public static IEnumerable<WapUserDto> FromModel(IEnumerable<WapUser> models)
        {            
            if (models == null)
            {
                return null;
            }

            if (models.Count() <= 0)
            {
                return null;
            }

            List<WapUserDto> dtos = new List<WapUserDto>();

            foreach (WapUser model in models)
            {
                WapUserDto dto = WapUserDto.FromModel(model);
                dtos.Add(dto);
            }

            return dtos;
        }
    }
}

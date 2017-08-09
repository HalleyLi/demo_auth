
using SH3H.SDK.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
namespace SH3H.WAP.Auth.Model.Dto
{    /// <summary>
    /// 组织结构dto实体(全部)
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/Auth")]
    public class WapOrganizationDto
    {
        /// <summary>
        /// 组织对象主键
        /// </summary>
        [DataMember(Name = "organizationKey")]
        public string OrganizationKey { get; set; }
        /// <summary>
        /// 获取或设置父ID
        /// </summary>
        [DataMember(Name = "parentOrganizationKey")]
        public string ParentOrganizationKey { get; set; }

        /// <summary>
        /// 获取或设置站点编码
        /// </summary>
        [DataMember(Name = "organizationCode")]
        public string OrganizationCode { get; set; }

        /// <summary>
        /// 获取或设置站点名称
        /// </summary>
        [DataMember(Name = "organizationName")]
        public string OrganizationName { get; set; }

        /// <summary>
        /// 获取或设置类型
        /// </summary>
        [DataMember(Name = "type")]
        public int OrganizationType { get; set; }

        /// <summary>
        /// 获取或设置地址
        /// </summary>
        [DataMember(Name = "address")]
        public string OrganizationAddress { get; set; }

        /// <summary>
        /// 获取或设置责任人
        /// </summary>
        [DataMember(Name = "dutyMan")]
        public string OrganizationDutyMan { get; set; }

        /// <summary>
        /// 获取或设置联系电话
        /// </summary>
        [DataMember(Name = "tel")]
        public string OrganizationTel { get; set; }

        /// <summary>
        /// 获取或设置排序
        /// </summary>
        [DataMember(Name = "sortSn")]
        public int SortIndex { get; set; }

        /// <summary>
        /// 获取或设置状态
        /// </summary>
        [DataMember(Name = "active")]
        public bool Active { get; set; }

        /// <summary>
        /// 获取或设置租户ID
        /// </summary>
        [DataMember(Name = "tenantId")]
        public int TenantId { get; set; }

        /// <summary>
        /// 扩展
        /// </summary>
        [DataMember(Name = "extend")]
        public string Extend { get; set; }

        #region Override

        public override int GetHashCode()
        {
            return OrganizationKey.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapOrganization)) return false;
            return ((WapOrganization)obj).OrganizationKey == this.OrganizationKey;
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", OrganizationCode, OrganizationKey);
        }

        #endregion

        #region Mapper
        /// <summary>
        /// 注册映射规则
        /// </summary>
        static WapOrganizationDto()
        {
            //指定映射字段与自定义解析器进行值的转换bool和int
            AutoMapper.Mapper.CreateMap<WapOrganizationDto, WapOrganization>().ForMember(dest => dest.State, action => {
                action.MapFrom(src => src.Active);
                action.ResolveUsing<Int32Resolver>();
            });
            AutoMapper.Mapper.CreateMap<WapOrganization, WapOrganizationDto>().ForMember(dest => dest.Active, action => {
                action.MapFrom(src => src.State);
                action.ResolveUsing<BooleanResolver>();
            });
        }
        //bool转换为int
        internal class Int32Resolver : ValueResolver<bool, int>
        {
            protected override int ResolveCore(bool source)
            {
                if (source)
                    return 1;
                else
                    return 0;
            }
        }
        //int转换为bool
        internal class BooleanResolver : ValueResolver<int, bool>
        {
            protected override bool ResolveCore(int source)
            {
                if (source==1)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 将model转换为Dto
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public static WapOrganizationDto FromModel(WapOrganization organization)
        {
            if (organization == null)
            {
                return null;
            }

            WapOrganizationDto dto = new WapOrganizationDto() 
            { 
                OrganizationKey=organization.OrganizationKey,
                ParentOrganizationKey=organization.ParentOrganizationKey,
                OrganizationCode=organization.OrganizationCode,
                OrganizationName=organization.OrganizationName,
                OrganizationType=organization.OrganizationType,
                OrganizationAddress=organization.OrganizationAddress,
                OrganizationDutyMan=organization.OrganizationDutyMan,
                OrganizationTel=organization.OrganizationTel,
                SortIndex=organization.SortIndex,
                Active=Convert.ToBoolean(organization.State),
                TenantId=organization.TenantId,
                Extend=organization.Extend
            };
            return dto;
        }

        public static WapOrganization ToModel(WapOrganizationDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            WapOrganization model = new WapOrganization()
            {
                OrganizationKey = dto.OrganizationKey,
                ParentOrganizationKey = dto.ParentOrganizationKey,
                OrganizationCode = dto.OrganizationCode,
                OrganizationName = dto.OrganizationName,
                OrganizationType = dto.OrganizationType,
                OrganizationAddress = dto.OrganizationAddress,
                OrganizationDutyMan = dto.OrganizationDutyMan,
                OrganizationTel = dto.OrganizationTel,
                SortIndex = dto.SortIndex,
                State=Convert.ToInt32(dto.Active),
                TenantId = dto.TenantId,
                Extend = dto.Extend
            };
            return model;
        }

        #endregion
    }
}

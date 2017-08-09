using SH3H.SDK.Share;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SH3H.WAP.Auth.Model.Dto
{
    /// <summary>
    /// 更新功能点角色关联
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/Auth")]
    public class WapUpdateRoleFuncRelationDto
    {
        /// <summary>
        /// 删除列表
        /// </summary>
        [DataMember(Name = "delArr")]
        public IEnumerable<WapRoleFunctionDto> DelArr { get; set; }

        /// <summary>
        /// 新增列表
        /// </summary>
        [DataMember(Name = "addArr")]
        public IEnumerable<WapRoleFunctionDto> AddArr { get; set; }

        public WapUpdateRoleFuncRelationDto()
        {
            DelArr = new List<WapRoleFunctionDto>();
            AddArr = new List<WapRoleFunctionDto>();
        }


        /// <summary>
        /// 验证模型
        /// </summary>
        /// <returns>True: 验证通过; False:验证失败</returns>
        public ValidateResult Validate()
        {
            ValidateResult result = new ValidateResult();

            if (AddArr != null)
            {
                foreach (WapRoleFunctionDto item in AddArr)
                {
                    ValidateResult child = item.Validate();
                    if (!child.IsValid)
                    {
                        foreach (var error in child.Errors)
                        {
                            result.AddError(error.Code, error.Message);
                        }
                    }
                }
            }

            if (DelArr != null)
            {
                foreach (WapRoleFunctionDto item in DelArr)
                {
                    ValidateResult child = item.Validate();
                    if (!child.IsValid)
                    {
                        foreach (var error in child.Errors)
                        {
                            result.AddError(error.Code, error.Message);
                        }
                    }
                }
            }
            return result;
        }
    }
}

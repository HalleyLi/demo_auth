using SH3H.SDK.Share;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SH3H.WAP.Auth.Model.Dto
{
    /// <summary>
    ///  更新功能点功能组组关联
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/Auth")]
    public class WapUpdateFuncGroupRelationDto
    {
        /// <summary>
        /// 删除列表
        /// </summary>
        [DataMember(Name = "delArr")]
        public IEnumerable<WapFuncGroupRelativeDto> DelArr { get; set; }

        /// <summary>
        /// 新增列表
        /// </summary>
        [DataMember(Name = "addArr")]
        public IEnumerable<WapFuncGroupRelativeDto> AddArr { get; set; }


        public WapUpdateFuncGroupRelationDto()
        {
            DelArr = new List<WapFuncGroupRelativeDto>();
            AddArr = new List<WapFuncGroupRelativeDto>();
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
                foreach (WapFuncGroupRelativeDto item in AddArr)
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
                foreach (WapFuncGroupRelativeDto item in DelArr)
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

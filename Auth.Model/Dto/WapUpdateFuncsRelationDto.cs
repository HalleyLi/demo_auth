using SH3H.SDK.Share;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SH3H.WAP.Auth.Model.Dto
{
    /// <summary>
    ///  更新功能点功能组组关联
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/Auth")]
    public class WapUpdateFuncsRelationDto
    {
        /// <summary>
        /// 删除列表
        /// </summary>
        [DataMember(Name = "delFuncs")]
        public IEnumerable<string> DelFuncs { get; set; }

        /// <summary>
        /// 新增列表
        /// </summary>
        [DataMember(Name = "addFuncs")]
        public IEnumerable<string> AddFuncs { get; set; }


        public WapUpdateFuncsRelationDto()
        {
            DelFuncs = new List<string>();
            AddFuncs = new List<string>();
        }
    }
}

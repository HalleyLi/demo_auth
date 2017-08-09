using AutoMapper;
using SH3H.SDK.Share;
using System;
using System.Runtime.Serialization;

namespace SH3H.WAP.Auth.Model.Dto
{
    /// <summary>
    /// 功能点于对应功能点组信息
    /// </summary>

    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/Auth")]
    public class WapFuncGroupRelativeDto : WapFunctionDto
    {
        /// <summary>
        /// 是否关联
        /// </summary>
        [DataMember(Name = "isRelative")]
        public bool IsRelative { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapFuncGroupRelativeDto)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapFuncGroupRelativeDto>(this, obj as WapFuncGroupRelativeDto);
        }
    }
}

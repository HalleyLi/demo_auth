using SH3H.SDK.Share;
using System;
using System.Runtime.Serialization;

namespace SH3H.WAP.Auth.Model.Dto
{
    /// <summary>
    /// 功能点与角色的关联
    /// </summary>

    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/Auth")]
    public class WapFuncRelativeDto : WapFunctionDto
    {
        /// <summary>
        /// 关联Key
        /// </summary>
        [DataMember(Name = "relationKey")]
        public string RelationKey { get; set; }

        /// <summary>
        /// 是否关联
        /// </summary>
        [DataMember(Name = "isRelative")]
        public bool IsRelative { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapFuncRelativeDto)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapFuncRelativeDto>(this, obj as WapFuncRelativeDto);
        }
    }
}

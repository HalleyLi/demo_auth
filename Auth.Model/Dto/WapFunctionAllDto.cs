using SH3H.SDK.Share;
using System;
using System.Runtime.Serialization;

namespace SH3H.WAP.Auth.Model.Dto
{
    /// <summary>
    /// 功能点实体(全部)
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/Auth")]
    public class WapFunctionAllDto : WapFunctionDto
    {
        /// <summary>
        /// 功能点名称
        /// </summary>
        [DataMember(Name = "funcGroupName")]
        public string FuncGroupName { get; set; }


        /// <summary>
        /// 应用程序名称
        /// </summary>
        [DataMember(Name = "appName")]
        public string AppName { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapFunctionAllDto)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapFunctionAllDto>(this, obj as WapFunctionAllDto);
        }
    }
}

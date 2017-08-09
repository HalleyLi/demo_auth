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
    /// 角色功能点关联
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/Auth")]
    public class WapRoleFunctionDto
    {
        /// <summary>
        /// 角色key
        /// </summary>
        [DataMember(Name = "roleKey")]
        public string RoleKey { get; set; }

        /// <summary>
        /// 功能点key
        /// </summary>
        [DataMember(Name = "funcKey")]
        public string FuncKey { get; set; }


        /// <summary>
        /// 关系key
        /// </summary>
        [DataMember(Name = "relationKey")]
        public string RelationKey { get; set; }

        /// <summary>
        /// 角色层级，绝对层级，相对于根节点的层级
        /// </summary>
        [DataMember(Name = "roleLevel")]
        public int RoleLevel { get; set; }

        /// <summary>
        /// 角色路径，绝对路径，相对于根节点的路径
        /// </summary>
        [DataMember(Name = "rolePath")]
        public string RolePath { get; set; }


        /// <summary>
        /// 验证模型
        /// </summary>
        /// <returns>True: 验证通过; False:验证失败</returns>
        public ValidateResult Validate()
        {
            ValidateResult result = new ValidateResult();
            Guid guid;

            //对应功能点必填且必须是GUID
            if (string.IsNullOrEmpty(this.FuncKey) || !Guid.TryParse(this.FuncKey, out guid))
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "对应功能点必填且必须是GUID");
            }

            //对应角色必填且必须是GUID
            if (string.IsNullOrEmpty(this.RoleKey) || !Guid.TryParse(this.RoleKey, out guid))
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "对应角色必填且必须是GUID");
            }

            //关联主键不是一个GUID
            if (!string.IsNullOrEmpty(this.RelationKey) && !Guid.TryParse(this.RelationKey, out guid))
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "关联主键不是一个GUID");
            }

            return result;
        }

        #region 模转
        /// <summary>
        /// 返回DTO对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static WapRoleFunctionDto FromModel(WapRoleFunction model)
        {
            //WapRoleFunctionDto dto = Mapper.Instance.Map<WapRoleFunction, WapRoleFunctionDto>(model);

            WapRoleFunctionDto result = new WapRoleFunctionDto()
            {
                FuncKey = model.FuncKey,
                RelationKey = model.RelationKey,
                RoleKey = model.RelationKey,
                RoleLevel = model.RoleLevel,
                RolePath = model.RolePath
            };

            return result;
        }


        /// <summary>
        /// 以当前对象状态生成一个Model对象
        /// </summary>
        /// <returns></returns>
        public WapRoleFunction ToModel()
        {
            //WapRoleFunction model = Mapper.Instance.Map<WapRoleFunctionDto, WapRoleFunction>(this);

            WapRoleFunction result = new WapRoleFunction()
            {
                FuncKey = this.FuncKey,
                RelationKey = this.RelationKey,
                RoleKey = this.RelationKey,
                RoleLevel = this.RoleLevel,
                RolePath = this.RolePath
            };

            return result;
        }
        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapRoleFunctionDto)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapRoleFunctionDto>(this, obj as WapRoleFunctionDto);
        }
    }
}

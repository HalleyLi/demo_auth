using AutoMapper;
using SH3H.SDK.Share;
using SH3H.WAP.Share;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace SH3H.WAP.Auth.Model.Dto
{
    /// <summary>
    /// 功能点组实体
    /// </summary>

    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/Auth")]
    public class WapFunctionGroupDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DataMember(Name = "funcGroupName")]
        public string FuncGroupName { get; set; }

        /// <summary>
        /// 拼音Code
        /// </summary>
        [DataMember(Name = "pyCode")]
        public string FuncGroupPycode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DataMember(Name = "sortSn")]
        public decimal FuncGroupSortsn { get; set; }

        /// <summary>
        /// 所属应用
        /// </summary>
        [DataMember(Name = "appKey")]
        public string FuncAppKey { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        [DataMember(Name = "funcAppName")]
        public string FuncAppName { get; set; }

        /// <summary>
        /// 上级功能组
        /// </summary>
        [DataMember(Name = "parentFuncGroupKey")]
        public string ParentFuncGroupKey { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        [DataMember(Name = "active")]
        public bool? FuncGroupActive { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember(Name = "comment")]
        public string FuncGroupComment { get; set; }

        /// <summary>
        /// 扩展
        /// </summary>
        [DataMember(Name = "extend")]
        public string Extend { get; set; }

        /// <summary>
        /// 工能组主键
        /// </summary>
        [DataMember(Name = "funcGroupKey")]
        public string FuncGroupKey { get; set; }

        /// <summary>
        /// 组层级 绝对层级
        /// </summary>
        [DataMember(Name = "funcGroupLevel")]
        public int FuncGroupLevel { get; set; }

        /// <summary>
        /// 组路径 绝对路径
        /// </summary>
        [DataMember(Name = "funcGroupPath")]
        public string FuncGroupPath { get; set; }

        /// <summary>
        /// 获取权限Code
        /// </summary>
        /// <returns></returns>
        public string GetAuditString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("名称:{0}, ", this.FuncGroupName);
            sb.AppendFormat("应用程序:{0}, ", this.FuncAppKey);
            sb.AppendFormat("上级:{0}, ", this.ParentFuncGroupKey);
            sb.AppendFormat("备注:{0}, ", this.FuncGroupComment);

            string audit = sb.ToString();
            return audit;
        }

        /// <summary>
        /// 获取功能点对应的权限Code
        /// </summary>
        /// <param name="oldObj"></param>
        /// <returns></returns>
        public string GetAuditString(WapFunctionGroupDto oldObj)
        {
            if (oldObj == null)
            {
                return GetAuditString();
            }

            StringBuilder sb = new StringBuilder();

            if (string.Compare(this.FuncGroupName, oldObj.FuncGroupName) != 0)
            {
                sb.AppendFormat("名称:{0} -> {1}, ", oldObj.FuncGroupName, this.FuncGroupName);
            }

            if (string.Compare(this.FuncAppKey, oldObj.FuncAppKey) != 0)
            {
                sb.AppendFormat("应用程序:{0} -> {1}, ", oldObj.FuncAppKey, this.FuncAppKey);
            }

            if (string.Compare(this.ParentFuncGroupKey, oldObj.ParentFuncGroupKey) != 0)
            {
                sb.AppendFormat("上级:{0} -> {1}, ", oldObj.ParentFuncGroupKey, this.ParentFuncGroupKey);
            }

            if (string.Compare(this.FuncGroupComment, oldObj.FuncGroupComment) != 0)
            {
                sb.AppendFormat("备注:{0} -> {1}, ", oldObj.FuncGroupComment, this.FuncGroupComment);
            }

            string audit = sb.ToString();
            return audit;
        }


        /// <summary>
        /// 验证模型
        /// </summary>
        /// <returns>True: 验证通过; False:验证失败</returns>
        public ValidateResult Validate()
        {
            ValidateResult result = new ValidateResult();

            //名字不能为空 长度不超过50
            if (string.IsNullOrEmpty(this.FuncGroupName) || this.FuncGroupName.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "名字不能为空 长度不超过50字符");
            }

            //拼音不为空时长度不超过10
            if (!string.IsNullOrEmpty(this.FuncGroupPycode)&&this.FuncGroupPycode.Length > 10)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "拼音长度不能超过10字符");
            }

            Guid func;
            //所属应用必须是一个guid
            if (!string.IsNullOrEmpty(this.FuncAppKey) && !Guid.TryParse(this.FuncAppKey, out func))
            {
                result.AddError(StateCode.CODE_ARGUMENT_TYPE_ERROR, "所属应用必须是一个guid");
            }

            //备注不能超过500个字符
            if (!string.IsNullOrEmpty(this.FuncGroupComment) && this.FuncGroupComment.Length > 500)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "备注长度不能超过500字符");
            }
            return result;
        }

        #region 模转
        /// <summary>
        /// 返回DTO对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static WapFunctionGroupDto FromModel(WapFunctionGroup model)
        {
            //WapFunctionGroupDto dto = Mapper.Instance.Map<WapFunctionGroup, WapFunctionGroupDto>(model);
            WapFunctionGroupDto result = new WapFunctionGroupDto()
            {
                FuncAppKey = model.FuncAppKey,
                FuncGroupActive = model.FuncGroupActive,
                FuncGroupComment = model.FuncGroupComment,
                FuncGroupKey = model.FuncGroupKey.ToString(),
                FuncGroupName = model.FuncGroupName,
                FuncGroupPycode = model.FuncGroupPycode,
                FuncGroupSortsn = model.FuncGroupSortsn,
                ParentFuncGroupKey = model.ParentFuncGroupKey.ToString(),
                Extend = model.Extend
            };

            return result;
        }


        /// <summary>
        /// 以当前对象状态生成一个Model对象
        /// </summary>
        /// <returns></returns>
        public WapFunctionGroup ToModel()
        {
            //WapFunctionGroup model = Mapper.Instance.Map<WapFunctionGroupDto, WapFunctionGroup>(this);
            WapFunctionGroup result = new WapFunctionGroup()
            {
                FuncAppKey = this.FuncAppKey,
                FuncGroupActive = this.FuncGroupActive,
                FuncGroupComment = this.FuncGroupComment,
                FuncGroupName = this.FuncGroupName,
                FuncGroupPycode = this.FuncGroupPycode,
                FuncGroupSortsn = this.FuncGroupSortsn,
                Extend = this.Extend
            };

            Guid groupkey;
            Guid parentgroupkey;
            Guid.TryParse(this.FuncGroupKey, out groupkey);
            Guid.TryParse(this.ParentFuncGroupKey, out parentgroupkey);

            result.FuncGroupKey = groupkey;
            result.ParentFuncGroupKey = parentgroupkey;

            return result;
        }
        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapFunctionGroupDto)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapFunctionGroupDto>(this, obj as WapFunctionGroupDto);
        }
    }
}

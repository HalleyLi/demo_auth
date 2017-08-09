using AutoMapper;
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
    /// 功能点
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/Auth")]
    public class WapFunctionDto
    {
        /// <summary>
        /// 获取或设置功能点关键字
        /// </summary>
        [DataMember(Name = "funcKey")]
        public string Key { get; set; }

        /// <summary>
        /// 获取或设置功能点代码
        /// </summary>
        [DataMember(Name = "funcCode")]
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置功能点名称
        /// </summary>
        [DataMember(Name = "funcName")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置当前功能点所在分组
        /// </summary>
        //[DataMember]
        //public string GroupName { get; set; }

        /// <summary>
        /// 获取或设置功能点拼音码
        /// </summary>
        [DataMember(Name = "pyCode")]
        public string Pycode { get; set; }

        /// <summary>
        /// 获取或设置功能点排序
        /// </summary>
        [DataMember(Name = "sortSn")]
        public decimal Sortsn { get; set; }

        /// <summary>
        /// 功能点组是否激活状态
        /// </summary>
        [DataMember(Name = "active")]
        public bool? Active { get; set; }

        /// <summary>
        /// 功能点备注信息
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// 临时KEY
        /// </summary>
        [DataMember(Name = "funcTemplateKey")]
        public string TemplateKey { get; set; }

        /// <summary>
        /// 额外信息
        /// </summary>
        [DataMember(Name = "extend")]
        public string Extend { get; set; }

        /// <summary>
        /// 所属应用主键
        /// </summary>
        [DataMember(Name = "appKey")]
        public string AppKey { get; set; }


        /// <summary>
        /// 功能点组Key
        /// </summary>
        [DataMember(Name = "funcGroupKey")]
        public string FuncGroupKey { get; set; }

        /// <summary>
        /// 复制功能点对象
        /// </summary>
        /// <returns>返回新的功能点对象</returns>
        public WapFunction Clone()
        {
            WapFunction func = new WapFunction();
            func.Key = this.Key;
            func.Code = this.Code;
            func.Name = this.Name;
            //func.GroupName = this.GroupName;
            func.Pycode = this.Pycode;
            func.Extend = this.Extend;
            return func;
        }

        #region Override

        public override string ToString()
        {
            return string.Format("{0}({1})", Name, Code);
        }

        #endregion

        /// <summary>
        /// 验证模型
        /// </summary>
        /// <returns>True: 验证通过; False:验证失败</returns>
        public ValidateResult Validate()
        {
            ValidateResult result = new ValidateResult();

            //名字不能为空 长度不能超过50
            if (string.IsNullOrEmpty(this.Name) || this.Name.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "名字不能为空 长度不能超过50字符");
            }

            //Code不能为空 长度不能超过50
            if (string.IsNullOrEmpty(this.Code) || this.Code.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "Code不能为空 长度不能超过50字符");
            }

            //拼音不为空时 长度不能超过10
            if (!string.IsNullOrEmpty(this.Pycode) && this.Pycode.Length > 10)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "拼音长度不能超过10字符");
            }

            //备注不能超过500个字符
            if (!string.IsNullOrEmpty(this.Comment) && this.Comment.Length > 500)
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
        public static WapFunctionDto FromModel(WapFunction model)
        {
            //WapFunctionDto dto = Mapper.Instance.Map<WapFunction, WapFunctionDto>(model);
            WapFunctionDto result = new WapFunctionDto()
            {
                Active = model.Active,
                AppKey = model.AppKey,
                Code = model.Code,
                Comment = model.Comment,
                Extend = model.Extend,
                FuncGroupKey = model.FuncGroupKey,
                Key = model.Key,
                Name = model.Name,
                Pycode = model.Pycode,
                Sortsn = model.Sortsn,
                TemplateKey = model.TemplateKey
            };


            return result;
        }


        /// <summary>
        /// 以当前对象状态生成一个Model对象
        /// </summary>
        /// <returns></returns>
        public WapFunction ToModel()
        {
            //WapFunction model = Mapper.Instance.Map<WapFunctionDto, WapFunction>(this);
            //return model;

            WapFunction result = new WapFunction()
            {
                Active = this.Active,
                AppKey = this.AppKey,
                Code = this.Code,
                Comment = this.Comment,
                Extend = this.Extend,
                FuncGroupKey = this.FuncGroupKey,
                Key = this.Key,
                Name = this.Name,
                Pycode = this.Pycode,
                Sortsn = this.Sortsn,
                TemplateKey = this.TemplateKey
            };


            return result;
        }
        #endregion


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapFunctionDto)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapFunctionDto>(this, obj as WapFunctionDto);
        }
    }
}

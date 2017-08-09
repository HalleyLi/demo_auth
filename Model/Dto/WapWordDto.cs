using SH3H.SDK.Share;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Model.Dto
{
    /// <summary>
    /// 词语对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/config")]
    public class WapWordDto
    {
        /// <summary>
        /// 获取或设置子节点
        /// </summary>
        [DataMember(Name="nodes")]
        public List<WapWordDto> Nodes { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [DataMember(Name = "wordId")]
        public int WordId { get; set; }

        /// <summary>
        /// 父编号
        /// </summary>
        [DataMember(Name = "parentId")]
        public int ParentId { get; set; }


        /// <summary>
        /// 编码
        /// </summary>
        [DataMember(Name = "wordCode")]
        public string WordCode { get; set; }

        /// <summary>
        /// 词语文本
        /// </summary>
        [DataMember(Name = "wordText")]
        public string WordText { get; set; }

        /// <summary>
        /// 词语值
        /// </summary>
        [DataMember(Name = "wordValue")]
        public string WordValue { get; set; }

        /// <summary>
        /// 所属应用标识
        /// </summary>
        [DataMember(Name = "app")]
        public string App { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        [DataMember(Name = "sort")]
        public int WordSortIndex { get; set; }

        /// <summary>
        /// 获取或设置词语状态
        /// </summary>
        [DataMember(Name = "active")]
        public bool WordState { get; set; }

        /// <summary>
        /// 获取或设置租户编号
        /// </summary>
        [DataMember(Name = "tenentId")]
        public int TenentId { get; set; }

        /// <summary>
        /// 获取或设置词语备注
        /// </summary>
        [DataMember(Name = "remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 词语分组字段
        /// </summary>
        [DataMember(Name = "groupKey")]
        public string WordGroupKey { get; set; }

        /// <summary>
        /// 词语拼音码
        /// </summary>
        [DataMember(Name = "pyCode")]
        public string WordPYCode { get; set; }

        /// <summary>
        /// 是否外部可见
        /// </summary>
        [DataMember(Name = "isExternalVisible")]
        public bool IsExternalVisible { get; set; }


        #region 模转
        /// <summary>
        /// 返回DTO对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static WapWordDto FromModel(WapWord model)
        {
            WapWordDto result = new WapWordDto()
            {
                App = model.App,
                IsExternalVisible = model.IsExternalVisible,
                ParentId = model.ParentId,
                TenentId = model.TenentId,
                Remark = model.Remark,
                WordCode = model.WordCode,
                WordGroupKey = model.WordGroupKey,
                WordId = model.WordId,
                WordPYCode = model.WordPYCode,
                WordSortIndex = model.WordSortIndex,
                WordState = model.WordState == 1,
                WordText = model.WordText,
                WordValue = model.WordValue
            };

            return result;
        }


        /// <summary>
        /// 以当前对象状态生成一个Model对象
        /// </summary>
        /// <returns></returns>
        public WapWord ToModel()
        {
            WapWord result = new WapWord()
            {
                App = this.App,
                IsExternalVisible = this.IsExternalVisible,
                ParentId = this.ParentId,
                TenentId = this.TenentId,
                Remark = this.Remark,
                WordCode = this.WordCode,
                WordGroupKey = this.WordGroupKey,
                WordId = this.WordId,
                WordPYCode = this.WordPYCode,
                WordSortIndex = this.WordSortIndex,
                WordState = this.WordState ? 1 : 0,
                WordText = this.WordText,
                WordValue = this.WordValue
            };

            return result;
        }
        #endregion

        /// <summary>
        /// 验证模型
        /// </summary>
        /// <returns>True: 验证通过; False:验证失败</returns>
        public ValidateResult Validate()
        {
            ValidateResult result = new ValidateResult();

            //词语组不能为空 长度不能超过50
            if (string.IsNullOrEmpty(this.WordGroupKey) || this.WordGroupKey.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "词语组不能为空 长度不能超过50");
            }

            //编码不能为空 长度不能超过50
            if (string.IsNullOrEmpty(this.WordCode) || this.WordCode.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "编码不能为空 长度不能超过50");
            }

            //词语文本不能为空 长度不能超过50
            if (string.IsNullOrEmpty(this.WordText) || this.WordText.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "词语文本不能为空 长度不能超过50");
            }

            //词语值不能为空 长度不能超过50
            if (!string.IsNullOrEmpty(this.WordValue) && this.WordValue.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "词语值长度不能超过50");
            }

            //词语拼音码不能为空 长度不能超过50
            if (!string.IsNullOrEmpty(this.WordPYCode) && this.WordPYCode.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "词语拼音码长度不能超过50");
            }

            //所属应用不能为空 长度不能超过50
            if (string.IsNullOrEmpty(this.App) || this.App.Length > 20)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "所属应用不能为空 长度不能超过20");
            }


            //备注不能超过100个字符
            if (!string.IsNullOrEmpty(this.Remark) && this.Remark.Length > 500)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "配置默认值不能超过500个字符");
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapWordDto)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapWordDto>(this, obj as WapWordDto);
        }
    }
}

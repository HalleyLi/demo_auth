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
    /// 标签Dto对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/config")]
    public class WapTagDto
    {

        /// <summary>
        /// 标签id
        /// </summary>
        [DataMember(Name = "id")]
        public int TagId { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        [DataMember(Name = "name")]
        public string TagName { get; set; }

        /// <summary>
        /// 标签组code
        /// </summary>
        [DataMember(Name = "tagGroupCode")]
        public string TagGroupCode { get; set; }

        /// <summary>
        /// 应用标识
        /// </summary>
        [DataMember(Name = "appIdentity")]
        public string AppIdentity { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        /// <summary>
        /// 引用次数
        /// </summary>
        [DataMember(Name = "referenceCount")]
        public int ReferenceCount { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
       [DataMember(Name = "color")]
        public string Color { get; set; }

        /// <summary>
        /// 标签描述
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember(Name = "createTime")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [DataMember(Name = "updateTime")]
        public DateTime? UpdateTime { get; set; }


        #region 模块装换

        /// <summary>
        /// model转DTO对象
        /// </summary>
        /// <param name="model">model对象</param>
        /// <returns>dto对象</returns>
        public static WapTagDto FromModel(WapTag model)
        {
            if (model == null)
                return null;
            WapTagDto result = new WapTagDto()
            {           
                TagId = model.TagId,
                TagName = model.TagName,
                TagGroupCode = model.TagGroupCode,
                AppIdentity = model.AppIdentity,
                UserId = model.UserId,
                ReferenceCount = model.ReferenceCount,
                Color = model.Color,
                Comment = model.Comment,
                CreateTime = model.CreateTime,
                UpdateTime = model.UpdateTime
            };
            return result;
        }


        /// <summary>
        /// dto转model
        /// </summary>
        /// <returns>model对象</returns>
        public WapTag ToModel()
        {
            WapTag result = new WapTag()
            {
                TagId = this.TagId,
                TagName = this.TagName,
                TagGroupCode = this.TagGroupCode,
                AppIdentity = this.AppIdentity,
                UserId = this.UserId,
                ReferenceCount = this.ReferenceCount,
                Color = this.Color,
                Comment = this.Comment,
                CreateTime = this.CreateTime,
                UpdateTime = this.UpdateTime
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

            if (string.IsNullOrEmpty(this.TagName) || this.TagName.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "标签名不能为空 长度不能超过50");
            }
            if(!string.IsNullOrEmpty(this.TagName)  &&  this.TagName.IndexOf(" ")>=0)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LIMIT_ERROR, "标签名不能有空格");
            }
            if (string.IsNullOrEmpty(this.TagGroupCode) || this.TagGroupCode.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "标签组code不能为空 长度不能超过50");
            }
            if (string.IsNullOrEmpty(this.AppIdentity))
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "应用标识不能为空");
            }
            if (string.IsNullOrEmpty(this.Color))
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "颜色不能为空");
            }
            if(this.UserId<0)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LIMIT_ERROR, "用户ID不能小于零！");
            }

            return result;
        }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapTagDto)) return false;
            return ((WapTagDto)obj).TagId == this.TagId;
        }


    }
}

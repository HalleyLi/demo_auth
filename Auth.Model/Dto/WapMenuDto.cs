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
    /// 菜单业务模型
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/Auth")]
    public class WapMenuDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WapMenuDto()
        {
            this.Menus = new List<WapMenuDto>();
        }
        /// <summary>
        /// 获取或设置当前菜单的子菜单列表
        /// </summary>
        [DataMember(Name = "menus")]
        public List<WapMenuDto> Menus { get; set; }

        /// <summary>
        /// 获取或设置菜单主键
        /// </summary>
        [DataMember(Name = "menuKey")]
        public string MenuKey { get; set; }

        /// <summary>
        /// 获取或设置菜单名称
        /// </summary>
        [DataMember(Name = "menuName")]
        public string MenuName { get; set; }

        /// <summary>
        /// 获取或设置当前菜单的父菜单主键
        /// </summary>
        [DataMember(Name = "parentMenuKey")]
        public string ParentMenuKey { get; set; }

        /// <summary>
        /// 获取或设置当前菜单排列序号
        /// </summary>
        [DataMember(Name = "sortSn")]
        public int SortSn { get; set; }

        /// <summary>
        /// 获取或设置当前菜单是否为活动菜单
        /// </summary>
        [DataMember(Name = "isActive")]
        public bool IsActive { get; set; }

        /// <summary>
        /// 获取或设置当前菜单备注
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// 获取或设置Url
        /// </summary>
        [DataMember(Name = "operation")]
        public string Operation { get; set; }

        /// <summary>
        /// 获取或设置当前菜单对应的组件编号
        /// </summary>
        [DataMember(Name = "componentId")]
        public string ComponentId { get; set; }

        /// <summary>
        /// 获取或设置当前菜单应用的CSS样式名称
        /// </summary>
        [DataMember(Name = "css")]
        public string Css { get; set; }

        /// <summary>
        /// 获取或设置应用Key
        /// </summary>
        [DataMember(Name = "appKey")]
        public string AppKey { get; set; }

        /// <summary>
        /// 获取或设置当前菜单是否显示
        /// </summary>
        [DataMember(Name = "isShow")]
        public bool IsShow { get; set; }


        /// <summary>
        /// 获取或设置参数
        /// </summary>
        [DataMember(Name = "parameter")]
        public string Parameter { get; set; }

        /// <summary>
        /// 获取或设置扩展信息
        /// </summary>
        [DataMember(Name = "extend")]
        public string Extend { get; set; }

        /// <summary>
        /// 获取功能点Code
        /// </summary>
        [DataMember(Name = "funcKey")]
        public string FuncKey { get; set; }

        /// <summary>
        /// 是否是权限功能
        /// </summary>
        [DataMember(Name = "isFuncActive")]
        public bool IsFuncActive { get; set; }

        /// <summary>
        /// 获取或设置功能点Key
        /// </summary>
        [DataMember(Name = "funcCode")]
        public string FuncCode { get; set; }

        /// <summary>
        /// 获取或设置应用Identity
        /// </summary>
        [DataMember(Name = "appIdentity")]
        public string AppIdentitfy { get; set; }

        #region Override

        public override string ToString()
        {
            return MenuName;
        }

        #endregion

        /// <summary>
        /// 验证模型
        /// </summary>
        /// <returns>True: 验证通过; False:验证失败</returns>
        public ValidateResult Validate()
        {
            ValidateResult result = new ValidateResult();
            Guid guid;

            //名称必填且长度不能超过50
            if (string.IsNullOrEmpty(this.MenuName) || this.MenuName.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "名称必填且长度不能超过50");
            }

            //父级菜单必填 且必须是GUID形式
            if (string.IsNullOrEmpty(this.ParentMenuKey) || !Guid.TryParse(ParentMenuKey, out guid))
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "父级菜单必填 且必须是GUID形式");
            }

            //备注必填且长度不能超过500
            if (!string.IsNullOrEmpty(this.Comment) && this.Comment.Length > 500)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "备注必填且长度不能超过500");
            }

            //功能定必填 且必须是guid形式
            if (string.IsNullOrEmpty(this.FuncKey) || !Guid.TryParse(FuncKey, out guid))
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "功能点必填");
            }

            //网址必填且长度不能超过1000
            if (!string.IsNullOrEmpty(this.Operation) && this.Operation.Length > 1000)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "网址必填且长度不能超过1000");
            }

            //组件长度不能超过1000
            if (!string.IsNullOrEmpty(this.ComponentId) && this.ComponentId.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "组件长度不能超过1000");
            }

            //样式长度不能超过50
            if (!string.IsNullOrEmpty(this.Css) && this.Css.Length > 50)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "样式长度不能超过50");
            }


            //参数长度不能超过200
            if (!string.IsNullOrEmpty(this.ParentMenuKey) && this.ParentMenuKey.Length > 200)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "参数长度不能超过200");
            }


            return result;
        }


        #region 模转
        /// <summary>
        /// 返回DTO对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static WapMenuDto FromModel(WapMenu model)
        {
            //WapMenuDto dto = Mapper.Instance.Map<WapMenu, WapMenuDto>(model);


            WapMenuDto result = new WapMenuDto()
            {
                AppKey = model.AppKey,
                FuncCode = model.FunCode,
                ComponentId = model.ComponentId,
                Css = model.Css,
                Comment = model.Comment,
                Extend = model.Extend,
                FuncKey = model.FunKey,
                IsActive = model.IsActive,
                IsShow = model.IsShow,
                MenuKey = model.MenuKey,
                MenuName = model.Name,
                Operation = model.Url,
                Parameter = model.Parameter,
                ParentMenuKey = model.ParentKey,
                SortSn = model.Sortsn,
                AppIdentitfy = model.AppIdentitfy,
                IsFuncActive = model.IsFuncActive,
            };

            if (model.Menus != null && model.Menus.Count > 0)
            {
                result.Menus = model.Menus.Select(p => WapMenuDto.FromModel(p)).ToList();
            }

            return result;

        }


        /// <summary>
        /// 以当前对象状态生成一个Model对象
        /// </summary>
        /// <returns></returns>
        public WapMenu ToModel()
        {
            //WapMenu model = Mapper.Instance.Map<WapMenuDto, WapMenu>(this);


            WapMenu result = new WapMenu()
            {
                AppKey = this.AppKey,
                FunCode = this.FuncKey,
                ComponentId = this.ComponentId,
                Css = this.Css,
                Comment = this.Comment,
                Extend = this.Extend,
                FunActive = this.IsFuncActive,
                IsActive = this.IsActive,
                IsShow = this.IsShow,
                MenuKey = this.MenuKey,
                Name = this.MenuName,
                Url = this.Operation,
                Parameter = this.Parameter,
                ParentKey = this.ParentMenuKey,
                Sortsn = this.SortSn,
                AppIdentitfy = this.AppIdentitfy,
                FunKey = this.FuncKey,
                IsFuncActive = this.IsFuncActive
            };

            if (this.Menus != null && this.Menus.Count > 0)
            {
                result.Menus = this.Menus.Select(p => p.ToModel()).ToList();
            }

            return result;
        }
        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapMenuDto)) return false;

            return SH3H.WAP.Share.Utils.EqualsObj<WapMenuDto>(this, obj as WapMenuDto);
        }

    }
}

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
    /// 定义添加应用程序对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = SH3H.SDK.Share.Consts.NAMESPACE + "/Model/Auth")]
    public class WapAppAddDto
    {
        /// <summary>
        /// 获取或设置应用程序标识
        /// </summary>
        [DataMember(Name = "appIdentity")]
        public string AppIdentity { get; set; }

        /// <summary>
        /// 获取或设置应用程序名称
        /// </summary>
        [DataMember(Name = "appName")]
        public string AppName { get; set; }

        /// <summary>
        /// 获取或设置应用拼音码
        /// </summary>
        [DataMember(Name = "pyCode")]
        public string PyCode { get; set; }

        /// <summary>
        /// 获取或设置应用程序是否激活
        /// </summary>
        [DataMember(Name = "active")]
        public bool? Active { get; set; }

        /// <summary>
        /// 获取或设置应用类型
        /// </summary>
        [DataMember(Name = "type")]
        public int Type { get; set; }

        /// <summary>
        /// 获取或设置应用程序描述
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// 获取或设置应用图标
        /// </summary>
        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        /// <summary>
        /// 获取或设置应用首页地址
        /// </summary>
        [DataMember(Name = "defaultIndex")]
        public string DefaultIndex { get; set; }

        /// <summary>
        /// 获取或设置应用程序扩展信息
        /// </summary>
        [DataMember(Name = "extend")]
        public string Extend { get; set; }

        /// <summary>
        /// 返回model对象.
        /// </summary>
        /// <param name="dto">dto对象</param>
        /// <returns>返回model对象.</returns>
        public static WapApp ToModel(WapAppAddDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            WapApp model = new WapApp()
            {
                AppIdentity = dto.AppIdentity,
                AppName = dto.AppName,
                PyCode = dto.PyCode,
                Active = dto.Active,
                Comment = dto.Comment,
                Type = dto.Type,
                Extend = dto.Extend,
                Icon = dto.Icon,
                DefaultIndex = dto.DefaultIndex
            };

            return model;
        }

        public static WapAppDto FromDtoToDto(WapAppAddDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            WapAppDto appDto = new WapAppDto()
            {

            };

            return appDto;
        }

        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (string.IsNullOrWhiteSpace(this.AppIdentity))
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "应用程序标识不允许为空！");
            }
            if (!string.IsNullOrWhiteSpace(this.AppIdentity) && this.AppIdentity.Length > 100)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "应用程序标识长度不允许超过100！");
            }

            if (string.IsNullOrWhiteSpace(this.AppName))
            {
                result.AddError(StateCode.CODE_ARGUMENT_NULL, "应用程序名称不允许为空！");
            }
            if (!string.IsNullOrWhiteSpace(this.AppName) && this.AppName.Length > 100)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "应用程序名称长度不允许超过100！");
            }
          
            if (!string.IsNullOrEmpty(this.PyCode) && this.PyCode.Length > 10)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "应用拼音码长度不允许超过10！");
            }
            if (!string.IsNullOrEmpty(this.Comment) && this.Comment.Length > 500)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "应用程序描述长度不允许超过500！");
            }
            if ((this.Type != null) && this.Type <= 0)
            {
                result.AddError(StateCode.CODE_ARGUMENT_LENGTH, "应用程序类型不允许为空！");
            }
            return result;
        }

        #region Override

        public override int GetHashCode()
        {
            return AppIdentity.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapAppAddDto)) return false;
            return ((WapAppAddDto)obj).AppIdentity == this.AppIdentity;
        }

        public override string ToString()
        {
            return string.Format("[{0}]{1}",
                AppIdentity, AppName);

        }

        #endregion
    }
}

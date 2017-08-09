using AutoMapper;
using SH3H.SDK.Share;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Model.Dto
{
    /// <summary>
    /// 定义应用程序对象
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model/Auth")]
    public class WapAppDto
    {
        /// <summary>
        /// 获取或设置应用程序Key
        /// </summary>
        [DataMember(Name = "appKey")]
        public string AppKey { get; set; }

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
        /// 获取或设置应用排序
        /// </summary>
        [DataMember(Name = "sortSn")]
        public int SortSn { get; set; }

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
        /// 获取或设置图标地址
        /// </summary>
        [DataMember(Name = "iconUrl")]
        public string IconUrl { get; set; }

        static WapAppDto()
        {
            Mapper.CreateMap<WapApp, WapAppDto>();
            Mapper.CreateMap<WapAppDto, WapApp>();
        }

        /// <summary>
        /// 返回Dto对象
        /// </summary>
        /// <param name="model">model对象</param>
        /// <returns>返回Dto对象.</returns>
        public static WapAppDto FromModel(WapApp model)
        {
            WapAppDto dto = new WapAppDto();
            dto.Active = model.Active;
            dto.AppIdentity = model.AppIdentity;
            dto.AppKey = model.AppKey;
            dto.AppName = model.AppName;
            dto.Comment = model.Comment;
            dto.DefaultIndex = model.DefaultIndex;
            dto.Extend = model.Extend;
            dto.Icon = model.Icon;
            dto.PyCode = model.PyCode;
            dto.SortSn = model.SortSn;
            dto.Type = model.Type;
            if (model.Icon != null)
            {
                dto.IconUrl = ConfigurationManager.AppSettings["FileRequestRootUrl"] + "/image/" + model.Icon;
            }
            return dto;
        }

        /// <summary>
        /// 返回model对象.
        /// </summary>
        /// <param name="dto">dto对象</param>
        /// <returns>返回model对象.</returns>
        public static WapApp ToModel(WapAppDto dto)
        {
            WapApp model = new WapApp();
            model.Active = dto.Active;
            model.AppIdentity = dto.AppIdentity;
            model.AppKey = dto.AppKey;
            model.AppName = dto.AppName;
            model.Comment = dto.Comment;
            model.DefaultIndex = dto.DefaultIndex;
            model.Extend = dto.Extend;
            model.Icon = dto.Icon;
            model.PyCode = dto.PyCode;
            model.SortSn = dto.SortSn;
            model.Type = dto.Type;
            return model;
        }

        /// <summary>
        /// 返回Dto对象列表
        /// </summary>
        /// <param name="models">model对象列表</param>
        /// <returns>返回Dto对象列表.</returns>
        public static IEnumerable<WapAppDto> FromModel(IEnumerable<WapApp> models)
        {
            if (models == null)
            {
                return null;
            }

            if (models.Count() <= 0)
            {
                return null;
            }
            List<WapAppDto> dtos = new List<WapAppDto>();

            foreach (WapApp model in models)
            {
                WapAppDto dto = new WapAppDto();
                dto.Active = model.Active;
                dto.AppIdentity = model.AppIdentity;
                dto.AppKey = model.AppKey;
                dto.AppName = model.AppName;
                dto.Comment = model.Comment;
                dto.DefaultIndex = model.DefaultIndex;
                dto.Extend = model.Extend;
                dto.Icon = model.Icon;
                dto.PyCode = model.PyCode;
                dto.SortSn = model.SortSn;
                dto.Type = model.Type;
                if (model.Icon != null)
                {
                    dto.IconUrl = ConfigurationManager.AppSettings["FileRequestRootUrl"] + "/image/" + model.Icon;
                }
                dtos.Add(dto);
            }

            return dtos;
        }

        #region Override

        public override int GetHashCode()
        {
            return AppIdentity.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WapAppDto)) return false;
            return ((WapAppDto)obj).AppIdentity == this.AppIdentity;
        }

        public override string ToString()
        {
            return string.Format("[{0}]{1}",
                AppIdentity, AppName);

        }

        #endregion
    }
}

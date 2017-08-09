using AutoMapper;
using SH3H.SDK.Share;
using System;
using System.Collections.Generic;
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
    public class WapAppStateDto
    {
        public WapAppStateDto()
        {

        }

        /// <summary>
        /// 激活状态
        /// </summary>
        [DataMember(Name = "active")]
        public bool Active { get; set; }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static WapAppStateDto()
        {
            Mapper.CreateMap<WapAppStateDto, WapAppState>();
            Mapper.CreateMap<WapAppState, WapAppStateDto>();
        }

        /// <summary>
        /// 返回Dto对象
        /// </summary>
        /// <param name="model">model对象</param>
        /// <returns>返回Dto对象.</returns>
        public static WapAppStateDto FromModel(WapAppState model)
        {
            WapAppStateDto dto = Mapper.Map<WapAppState, WapAppStateDto>(model);

            return dto;
        }

        /// <summary>
        /// 返回model对象.
        /// </summary>
        /// <param name="dto">dto对象</param>
        /// <returns>返回model对象.</returns>
        public static WapAppState ToModel(WapAppStateDto dto)
        {
            WapAppState model = Mapper.Map<WapAppStateDto, WapAppState>(dto);

            return model;
        }
    }
}

using SH3H.SDK.Share;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http;

namespace SH3H.SDK.WebApi.Core.Models
{
    /// <summary>
    /// 定义WebApi集合类型响应对象
    /// </summary>
    /// <typeparam name="TEntity">返回实体类型</typeparam>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model")]
    public class WapCollection<TEntity>
        : WapResponse<IEnumerable<TEntity>> 
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entities">返回对象列表</param>
        public WapCollection(IEnumerable<TEntity> entities)
            : base(entities)
        {

        }
    }
}
using SH3H.SDK.WebApi.Controllers;
using SH3H.SDK.WebApi.Core;
using SH3H.SDK.WebApi.Core.Models;
using SH3H.WAP.Contracts;
using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SH3H.WAP.Controllers
{
    /// <summary>
    /// 定义Tag控制器
    /// </summary>
    [Resource("wapTagRes")]
    [RoutePrefix(SH3H.WAP.Share.Consts.URL_PREFIX_WAP + "/common")]
    public class WapTagController :
         BaseController<IWapTagService>
    {

        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="tag">标签对象</param>
        /// <returns>标签对象</returns>
        [HttpPost]
        [Route("tags")]
        [ActionName("createTag")]
        public WapResponse<WapTagDto> AddTag(WapTagDto tag)
        {
            var result = Service.AddTag(tag);
            return new WapResponse<WapTagDto>(result);
        }

        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <param name="tag">标签对象</param>
        /// <returns>标签对象</returns>
        [HttpPut]
        [Route("tags/{id}")]
        [ActionName("updateTag")]
        public WapResponse<WapTagDto> UpdateTag([FromUri]int id, [FromBody]WapTagDto tag)
        {
            var result = Service.UpdateTag(id, tag);
            return new WapResponse<WapTagDto>(result);
        }

        /// <summary>
        /// 根据应用获取标签列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>标签列表</returns>
        [HttpGet]
        [Route("tags")]
        [ActionName("getTagsByAppCode")]
        public WapResponse<IEnumerable<WapTagDto>> SelectTagsByAppCode(string appIdentity)
        {
            var result = Service.SelectTagsByAppCode(appIdentity);
            return new WapResponse<IEnumerable<WapTagDto>>(result);
        }

        /// <summary>
        /// 根据应用获取所有的标签组Code
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>标签组Code列表</returns>
        [HttpGet]
        [Route("tag/groups")]
        [ActionName("getTagGroupsByAppCode")]
        public WapResponse<IEnumerable<string>> SelectTagGroupsByAppCode(string appIdentity)
        {
            var result = Service.SelectTagGroupsByAppCode(appIdentity);
            return new WapResponse<IEnumerable<string>>(result);
        }

        /// <summary>
        /// 通过id获取指定标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <returns>标签对象</returns>
        [HttpGet]
        [Route("tags/{id}")]
        [ActionName("getTagById")]
        public WapResponse<WapTagDto> GetTagById(int id)
        {
            var result = Service.GetTagById(id);
            return new WapResponse<WapTagDto>(result);
        }

        /// <summary>
        /// 指定应用和标签组code获取标签列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <param name="tagGroupCode">标签组code</param>
        /// <returns>标签列表</returns>
        [HttpGet]
        [Route("tags")]
        [ActionName("getTagsByGroupCode")]
        public WapResponse<IEnumerable<WapTagDto>> SelectTagsByTagGroupCode(string appIdentity, string tagGroupCode)
        {
            var result = Service.SelectTagsByTagGroupCode(appIdentity,tagGroupCode);
            return new WapResponse<IEnumerable<WapTagDto>>(result);
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <returns>是否成功</returns>
        [HttpDelete]
        [Route("tags/{id}")]
        [ActionName("deleteTag")]
        public WapBoolean DeleteTag([FromUri]int id)
        {
            var result = Service.DeleteTag(id);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 修改标签引用次数
        /// </summary>
        /// <param name="id">标签id</param>
        /// <param name="referenceCount">标签引用次数</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [Route("tag/referenceCount/{id}/{referenceCount}")]
        [ActionName("updateReferenceCount")]
        public WapBoolean UpdateReferenceCount(int id, int referenceCount)
        {
            var result = Service.UpdateReferenceCount(id, referenceCount);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 点击标签引用次数加1
        /// </summary>
        /// <param name="id">标签ID</param>
        /// <returns></returns>
        [HttpPut]
        [Route("tag/referenceCount/{id}")]
        [ActionName("referenceAdd")]
        public WapInt32 ReferenceAdd(int id)
        {
            var result = Service.ReferenceAdd(id);
            return new WapInt32(result);
        }

    }
}

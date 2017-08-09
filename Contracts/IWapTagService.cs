using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Contracts
{
    /// <summary>
    /// 标签Service接口层
    /// </summary>
    public interface IWapTagService
    {


        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="tag">标签对象</param>
        /// <returns>标签对象</returns>
        WapTagDto AddTag(WapTagDto tag);

        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <param name="tag">标签对象</param>
        /// <returns>标签对象</returns>
        WapTagDto UpdateTag(int id, WapTagDto tag);

        /// <summary>
        /// 根据应用获取标签列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>标签列表</returns>
        IEnumerable<WapTagDto> SelectTagsByAppCode(string appIdentity);

        /// <summary>
        /// 根据应用获取所有的标签组Code
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>标签组Code列表</returns>
        IEnumerable<string> SelectTagGroupsByAppCode(string appIdentity);

        /// <summary>
        /// 通过id获取指定标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <returns>标签对象</returns>
        WapTagDto GetTagById(int id);

        /// <summary>
        /// 指定应用和标签组code获取标签列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <param name="tagGroupCode">标签组code</param>
        /// <returns>标签列表</returns>
        IEnumerable<WapTagDto> SelectTagsByTagGroupCode(string appIdentity, string tagGroupCode);

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <returns>是否成功</returns>
        bool DeleteTag(int id);

        /// <summary>
        /// 修改标签引用次数
        /// </summary>
        /// <param name="id">标签id</param>
        /// <param name="referenceCount">标签引用次数</param>
        /// <returns>是否成功</returns>
        bool UpdateReferenceCount(int id, int referenceCount);

        /// <summary>
        /// 点击标签引用次数加1
        /// </summary>
        /// <param name="id">标签ID</param>
        /// <returns></returns>
        int ReferenceAdd(int id);

    }
}

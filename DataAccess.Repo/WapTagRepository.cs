using SH3H.SDK.DataAccess.Repo;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.DataAccess.Repo
{
    /// <summary>
    /// 标签Repository层
    /// </summary>
    public class WapTagRepository: Repository<IWapTagStorage> , IWapTagRepository
    {

        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="tag">标签对象</param>
        /// <returns>标签对象</returns>
        public WapTag AddTag(WapTag tag)
        {
            return Storage.AddTag(tag);
        }

        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <param name="tag">标签对象</param>
        /// <returns>标签对象</returns>
        public WapTag UpdateTag(int id, WapTag tag)
        {
            return Storage.UpdateTag(id, tag);
        }

        /// <summary>
        /// 根据应用获取标签列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>标签列表</returns>
        public IEnumerable<WapTag> SelectTagsByAppCode(string appIdentity)
        {
            return Storage.SelectTagsByAppCode(appIdentity);
        }

        /// <summary>
        /// 根据应用获取所有的标签组Code
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>标签组Code列表</returns>
        public IEnumerable<string> SelectTagGroupsByAppCode(string appIdentity)
        {
            return Storage.SelectTagGroupsByAppCode(appIdentity);
        }

        /// <summary>
        /// 通过id获取指定标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <returns>标签对象</returns>
        public WapTag GetTagById(int id)
        {
            return Storage.GetTagById(id);
        }

        /// <summary>
        /// 指定应用和标签组code获取标签列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <param name="tagGroupCode">标签组code</param>
        /// <returns>标签列表</returns>
        public IEnumerable<WapTag> SelectTagsByTagGroupCode(string appIdentity, string tagGroupCode)
        {
            return Storage.SelectTagsByTagGroupCode(appIdentity,tagGroupCode);
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <returns>是否成功</returns>
        public bool DeleteTag(int id)
        {
            return Storage.DeleteTag(id);
        }

        /// <summary>
        /// 修改标签引用次数
        /// </summary>
        /// <param name="id">标签id</param>
        /// <param name="referenceCount">标签引用次数</param>
        /// <returns>是否成功</returns>
        public bool UpdateReferenceCount(int id, int referenceCount)
        {
            return Storage.UpdateReferenceCount(id, referenceCount);
        }

        /// <summary>
        /// 点击标签引用次数加1
        /// </summary>
        /// <param name="id">标签ID</param>
        /// <returns></returns>
        public int ReferenceAdd(int id)
        {
            return Storage.ReferenceAdd(id);
        }

    }
}

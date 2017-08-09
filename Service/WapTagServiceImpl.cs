using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Contracts;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Model;
using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Service
{
    /// <summary>
    /// 标签Service层
    /// </summary>
    public class WapTagServiceImpl: IWapTagService
    {       
        private IWapTagRepository _repoTag;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repo"></param>
        public WapTagServiceImpl(IWapTagRepository repo)
        {
            _repoTag = repo;
        }

        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="tag">标签对象</param>
        /// <returns>标签对象</returns>
        public WapTagDto AddTag(WapTagDto tag)
        {
            if (tag == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "标签对象不允许为空");
            }
            var validateResult = tag.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            var model = tag.ToModel();
            if (model == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "标签对象不允许为空");
            }
            try
            {
                var ret = this._repoTag.AddTag(model);
                return WapTagDto.FromModel(ret);
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <param name="tag">标签对象</param>
        /// <returns>标签对象</returns>
        public WapTagDto UpdateTag(int id, WapTagDto tag)
        {
            if (id<=0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_LIMIT_ERROR, "标签id必须大于0");
            }
            if (tag == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "标签对象不允许为空");
            }
            var validateResult = tag.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            var model = tag.ToModel();
            if (model == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "标签对象不允许为空");
            }
            try
            {
                var ret = this._repoTag.UpdateTag(id, model);
                return WapTagDto.FromModel(ret);
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 根据应用获取标签列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>标签列表</returns>
        public IEnumerable<WapTagDto> SelectTagsByAppCode(string appIdentity)
        {
            if (string.IsNullOrEmpty(appIdentity))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "应用标识不允许为空");
            }
            try
            {
                var retlist = this._repoTag.SelectTagsByAppCode(appIdentity);
                if (retlist == null)
                    return null;
                IEnumerable<WapTagDto> tempTagDtoList = retlist.Select(b => WapTagDto.FromModel(b)).ToList();
                return tempTagDtoList;
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 根据应用获取所有的标签组Code
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>标签组Code列表</returns>
        public IEnumerable<string> SelectTagGroupsByAppCode(string appIdentity)
        {
            if (string.IsNullOrEmpty(appIdentity))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "应用标识不允许为空");
            }
            try
            {
                return this._repoTag.SelectTagGroupsByAppCode(appIdentity);
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 通过id获取指定标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <returns>标签对象</returns>
        public WapTagDto GetTagById(int id)
        {
            if (id <= 0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_LIMIT_ERROR, "标签id必须大于0");
            }
            try
            {
                var ret = this._repoTag.GetTagById(id);
                return WapTagDto.FromModel(ret);
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 指定应用和标签组code获取标签列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <param name="tagGroupCode">标签组code</param>
        /// <returns>标签列表</returns>
        public IEnumerable<WapTagDto> SelectTagsByTagGroupCode(string appIdentity, string tagGroupCode)
        {
            if (string.IsNullOrEmpty(appIdentity))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "应用标识不允许为空");
            }
            if (string.IsNullOrEmpty(tagGroupCode))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "标签组Code不允许为空");
            }
            try
            {
                var retlist = this._repoTag.SelectTagsByTagGroupCode(appIdentity,tagGroupCode);
                if (retlist == null)
                    return null;
                IEnumerable<WapTagDto> tempTagDtoList = retlist.Select(b => WapTagDto.FromModel(b)).ToList();
                return tempTagDtoList;
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <returns>是否成功</returns>
        public bool DeleteTag(int id)
        {
            if (id <= 0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_LIMIT_ERROR, "标签id必须大于0");
            }
            try
            {
                var ret = this._repoTag.DeleteTag(id);
                return ret;
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 修改标签引用次数
        /// </summary>
        /// <param name="id">标签id</param>
        /// <param name="referenceCount">标签引用次数</param>
        /// <returns>是否成功</returns>
        public bool UpdateReferenceCount(int id, int referenceCount)
        {
            if (id <= 0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_LIMIT_ERROR, "标签id必须大于0");
            }
            try
            {
                var ret = this._repoTag.UpdateReferenceCount(id,referenceCount);
                return ret;
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// 点击标签引用次数加1
        /// </summary>
        /// <param name="id">标签ID</param>
        /// <returns></returns>
        public int ReferenceAdd(int id)
        {
            if (id <= 0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_LIMIT_ERROR, "标签id必须大于0");
            }
            try
            {
                var ret = this._repoTag.ReferenceAdd(id);
                return ret;
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, ex.Message);
            }
        }


    }
}

using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Contracts;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Service
{
    /// <summary>
    /// 量程Service层
    /// </summary>
    public class WapMeterRangeServiceImpl:IWapMeterRangeService
    {
      private IWapMeterRangeRepository _repoMeterRange;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repo"></param>
      public WapMeterRangeServiceImpl(IWapMeterRangeRepository repo)
        {
            _repoMeterRange = repo;
        }

        /// <summary>
        /// 新增量程
        /// </summary>
        /// <param name="range">量程对象</param>
        /// <returns>量程对象</returns>
        public WapMeterRangeDto AddRange(WapMeterRangeDto range)
        {
            if (range == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "量程对象不允许为空");
            }
            var validateResult = range.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            var model = range.ToModel();
            if (model == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "量程对象不允许为空");
            }
            try
            {
                var ret = this._repoMeterRange.AddRange(model);
                return WapMeterRangeDto.FromModel(ret);
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
        /// 修改量程
        /// </summary>
        /// <param name="id">量程id</param>
        /// <param name="range">量程对象</param>
        /// <returns>量程对象</returns>
       public WapMeterRangeDto UpdateRange(int id, WapMeterRangeDto range)
        {
            if (id <= 0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_LIMIT_ERROR, "量程id必须大于0");
            }
            if (range == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "量程对象不允许为空");
            }
            var validateResult = range.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            var model = range.ToModel();
            if (model == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "量程对象不允许为空");
            }
            try
            {
                var ret = this._repoMeterRange.UpdateRange(id, model);
                return WapMeterRangeDto.FromModel(ret);
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
        /// 获取所有的量程列表
        /// </summary>
        /// <returns>所有的量程列表</returns>
        public IEnumerable<WapMeterRangeDto> SelectAllRanges()
       {
           try
           {
               var retlist = this._repoMeterRange.SelectAllRanges();
               if (retlist == null)
                   return null;
               IEnumerable<WapMeterRangeDto> tempDtoList = retlist.Select(b => WapMeterRangeDto.FromModel(b)).ToList();
               return tempDtoList;
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
        /// 通过id获取指定量程
        /// </summary>
        /// <param name="id">量程id</param>
        /// <returns>量程对象</returns>
        public WapMeterRangeDto GetRangeById(int id)
        {
            if (id <= 0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_LIMIT_ERROR, "量程id必须大于0");
            }
            try
            {
                var ret = this._repoMeterRange.GetRangeById(id);
                return WapMeterRangeDto.FromModel(ret);
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
        /// 删除量程
        /// </summary>
        /// <param name="id">量程id</param>
        /// <returns>是否成功</returns>
        public bool DeleteRange(int id)
        {
            if (id <= 0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_LIMIT_ERROR, "量程id必须大于0");
            }
            try
            {
                var ret = this._repoMeterRange.DeleteRange(id);
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

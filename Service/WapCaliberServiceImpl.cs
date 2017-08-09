using SH3H.WAP.Contracts;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Share;

namespace SH3H.WAP.Service
{
    /// <summary>
    /// 口径服务操作
    /// </summary>
    public class WapCaliberServiceImpl : IWapCaliberService
    {
        private IWapCaliberRepository _repo;

        public WapCaliberServiceImpl(IWapCaliberRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 添加口径
        /// </summary>
        /// <param name="caliber">口径对象</param>
        /// <returns>口径对象</returns>
        public WapCaliberDto AddCaliber(WapCaliberDto caliber)
        {
            if (caliber == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "口径对象不允许为空");
            }
            var validateResult = caliber.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            var result = _repo.Insert(caliber.ToModel());
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "添加水表型号失败");
            }
            return WapCaliberDto.FromModel(result);
        }

        /// <summary>
        /// 删除口径
        /// </summary>
        /// <param name="id">口径编号</param>
        /// <returns>是否删除成功</returns>
        public bool RemoveCaliber(int id)
        {
            
            return _repo.Delete(id);
        }

        /// <summary>
        /// 通过口径编号修改口径
        /// </summary>
        /// <param name="id">口径编号</param>
        /// <param name="caliber">口径对象</param>
        /// <returns>返回时否修改成功</returns>
        public WapCaliberDto ModifyCaliberById(int id, WapCaliberDto caliber)
        {
            if (caliber == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "口径对象不允许为空");
            }
            var validateResult = caliber.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            var result= _repo.Update(id, caliber.ToModel());
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, " 通过口径编号修改口径失败");
            }
            return WapCaliberDto.FromModel(result);
        }

        /// <summary>
        /// 通过口径编号查询口径
        /// </summary>
        /// <param name="id">口径编号</param>
        /// <returns>口径对象</returns>
        public WapCaliberDto GetCaliberById(int id)
        {
            
            var result = _repo.Select(id);
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "通过口径编号查询口径失败");
            }
            return WapCaliberDto.FromModel(result);
        }

        /// <summary>
        /// 根据口径编码查找
        /// </summary>
        /// <returns>所有口径集合</returns>
        public IEnumerable<WapCaliberDto> GetAllCalibers()
        {

            return _repo.SelectAll().Select(p => WapCaliberDto.FromModel(p)).ToList();
        }

        #region 参数检查
        /// <summary>
        /// 检查入参是否是GUID类型字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="fieldName">参数中文名</param>
        /// <returns></returns>
        public static void CheckGuidStr(string str, string fieldName)
        {
            if (!Utils.IsGuid(str))
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_INVALID_ARGUMENTS, "参数 " + fieldName + " 不是合法的GUID类型字符串");
            }
        }

        /// <summary>
        /// 检查入参是否是空字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="fieldName">参数中文名</param>
        /// <returns></returns>
        public static void CheckNullStr(string str, string fieldName)
        {

            if (string.IsNullOrEmpty(str))
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_INVALID_ARGUMENTS, "参数 " + fieldName + " 不能为空");

            }
        }

        /// <summary>
        /// 检查入参是否是空
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static void CheckNull<T>(T obj, string fieldName)
        {
            if (obj == null)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_INVALID_ARGUMENTS, "参数 " + fieldName + " 不能为空");

            }
        }

        #endregion

    }
}

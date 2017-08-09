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
    /// 水表型号服务操作
    /// </summary>
    public class WapModelServiceImpl : IWapModelService
    {
        private IWapModelRepository _repo;

        public WapModelServiceImpl(IWapModelRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 添加水表型号
        /// </summary>
        /// <param name="model">水表型号对象</param>
        /// <returns>水表型号对象</returns>
        public WapModelDto AddModel(WapModelDto model)
        {
            if (model == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "水表型号不允许为空");
            }
            var validateResult = model.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            var result = _repo.Insert(model.ToModel());
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "添加水表型号失败");
            }

            return WapModelDto.FromModel(result);
        }

        /// <summary>
        /// 删除水表型号
        /// </summary>
        /// <param name="id">水表型号编号</param>
        /// <returns>是否删除成功</returns>
        public bool RemoveModel(int id)
        {

            return _repo.Delete(id);
        }

        /// <summary>
        /// 通过水表型号编号修改水表型号
        /// </summary>
        /// <param name="id">水表型号编号</param>
        /// <param name="model">水表型号对象</param>
        /// <returns>返回时否修改成功</returns>
        public bool ModifyModelById(int id, WapModelDto model)
        {
            if (model == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "水表型号不允许为空");
            }
            var validateResult = model.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            return _repo.Update(id, model.ToModel());
        }

        /// <summary>
        /// 通过水表型号编号查询水表型号
        /// </summary>
        /// <param name="id">水表型号编号</param>
        /// <returns>水表型号对象</returns>
        public WapModelDto GetModelById(int id)
        {

            var result = _repo.Select(id);
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "通过水表型号编号查询水表型号失败");
            }

            return WapModelDto.FromModel(result);
        }

        /// <summary>
        /// 根据水表型号编码查找
        /// </summary>
        /// <returns>所有水表型号集合</returns>
        public IEnumerable<WapModelDto> GetAllModels()
        {

            return _repo.SelectAll().Select(p => WapModelDto.FromModel(p)).ToList();
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

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
    /// 系统配置表的操作
    /// </summary>
    public class WapConfigurationServiceImpl : IWapConfigurationService
    {
        private IWapConfigurationRepository _repo;

        public WapConfigurationServiceImpl(IWapConfigurationRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 添加系统配置
        /// </summary>
        /// <param name="configuration">对象</param>
        /// <returns>口径对象</returns>
        public WapConfigurationDto AddConfiguration(WapConfigurationDto configuration)
        {
            if (configuration == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "系统配置不允许为空");
            }
            var validateResult = configuration.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            var result = _repo.Insert(configuration.ToModel());
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "添加水表型号失败");
            }

            return WapConfigurationDto.FromModel(result);
        }

        /// <summary>
        /// 删除系统配置
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>是否删除成功</returns>
        public bool RemoveConfiguration(int id)
        {

            return _repo.Delete(id);
        }

        /// <summary>
        /// 修改系统配置
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="configuration">系统配置对象</param>
        /// <returns>返回时否修改成功</returns>
        public bool ModifyConfiguration(int id, WapConfigurationDto configuration)
        {
            if (configuration == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "系统配置不允许为空");
            }
            var validateResult = configuration.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            return _repo.Update(id, configuration.ToModel());
        }

        /// <summary>
        /// 通过编号查询系统配置
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>系统配置对象</returns>
        public WapConfigurationDto GetConfigurationById(int id)
        {


            var result = _repo.SelectById(id);
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "通过编号查询系统配置失败");
            }

            return WapConfigurationDto.FromModel(result);
        }

        /// <summary>
        /// 通过配置编码和分组获取系统配置列表
        /// </summary>
        /// <param name="appCode">app编码</param>
        /// <param name="group">配置分组</param>
        /// <returns>系统配置对象</returns>
        public IEnumerable<WapConfigurationDto> GetConfigurationByAppAndGroup(string appCode, string group)
        {

            return _repo.SelectByAppAndGroup(appCode, group).Select(p => WapConfigurationDto.FromModel(p)); ;
        }

        /// <summary>
        /// 获取指定应用下的配置
        /// </summary>
        /// <param name="appCode">app编码</param>
        /// <returns>系统配置对象</returns>
        public IEnumerable<WapConfigurationDto> GetConfigsByAppCode(string appCode)
        {
            if (string.IsNullOrEmpty(appCode))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "应用Code不允许为空");
            }
            try
            {
                return _repo.GetConfigsByAppCode(appCode).Select(p => WapConfigurationDto.FromModel(p));
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
        /// 通过配置编码获取启用的分组
        /// </summary>
        /// <param name="appCode">应用编码</param>
        /// <returns>系统配置对象</returns>
        public IEnumerable<WapConfigurationDto> SelectGroupsByApp(string appCode)
        {
            if (string.IsNullOrEmpty(appCode))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "应用Code不允许为空");
            }
            try
            {
                return _repo.SelectGroupsByApp(appCode).Select(p => WapConfigurationDto.FromModel(p)); 
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
        /// 根据配置code更新配置组code
        /// </summary>
        /// <param name="configCode">配置标识</param>
        /// <param name="group">配置组标识</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateConfigParent(string configCode, string group)
        {
            if (string.IsNullOrEmpty(configCode))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "配置Code不允许为空");
            }
            if (string.IsNullOrEmpty(group))
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "配置组Code不允许为空");
            }
            try
            {
                return _repo.UpdateConfigParent(configCode, group);
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
        /// 修改配置状态
        /// </summary>
        /// <param name="id">配置ID</param>
        /// <param name="active">激活状态</param>
        /// <returns>是否修改成功</returns>
        public bool UpdateConfigState(int id, bool active)
        {
            try
            {
                return _repo.UpdateConfigState(id, active);
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
        /// 获取所有系统配置
        /// </summary>
        /// <returns>系统配置对象列表</returns>
        public IEnumerable<WapConfigurationDto> GetAllConfiguration()
        {

            return _repo.SelectAll().Select(p => WapConfigurationDto.FromModel(p));
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

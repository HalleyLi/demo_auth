using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Contracts;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Service
{
    /// <summary>
    /// 定义用户配置参数服务实现
    /// </summary>
    public class WapUserSettingSchemeServiceImpl : IWapUserSettingSchemeService
    {
        private IWapUserSettingSchemeRepository _repo;

        public WapUserSettingSchemeServiceImpl(IWapUserSettingSchemeRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 添加用户配置参数
        /// </summary>
        /// <param name="userSettingSchemeDto">用户配置参数对象</param>
        /// <returns>新增后的用户配置参数对象</returns>
        public Model.WapUserSettingSchemeDto Add(Model.WapUserSettingSchemeDto userSettingSchemeDto)
        {
            if (userSettingSchemeDto == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置参数对象不允许为空");
            }
            var validateResult = userSettingSchemeDto.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            var result = _repo.Add(userSettingSchemeDto.ToModel());
            if (result != null)
                return WapUserSettingSchemeDto.FromModel(result);
            return null;


        }

        /// <summary>
        /// 删除用户配置参数
        /// </summary>
        /// <param name="schemeId">用户配置参数编号</param>
        /// <returns>是否删除成功，true成功，false失败</returns>
        public bool Remove(int schemeId)
        {
            return _repo.Remove(schemeId);

        }

        /// <summary>
        /// 根据参数编号修改用户配置参数
        /// </summary>
        /// <param name="userSettingSchemeDto">用户配置参数对象</param>
        /// <returns>更新成功，返回更新后的对象;更新失败，返回空</returns>
        public Model.WapUserSettingSchemeDto Modify(Model.WapUserSettingSchemeDto userSettingSchemeDto)
        {
            if (userSettingSchemeDto == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置参数对象不允许为空");
            }
            var validateResult = userSettingSchemeDto.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            var result = _repo.Modify(userSettingSchemeDto.ToModel());
            if (result != null)
                return WapUserSettingSchemeDto.FromModel(result);
            return null;
        }

        /// <summary>
        /// 根据参数编号获取用户配置参数
        /// </summary>
        /// <param name="schemeId">参数编号</param>
        /// <returns>用户配置参数对象</returns>
        public Model.WapUserSettingSchemeDto Get(int schemeId)
        {
            var result = _repo.Get(schemeId);
            if (result != null)
                return WapUserSettingSchemeDto.FromModel(result);
            return null;
        }
    }
}

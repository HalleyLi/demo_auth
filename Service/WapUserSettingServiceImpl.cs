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
    /// 定义用户设置服务实现
    /// </summary>
    public class WapUserSettingServiceImpl : IWapUserSettingService
    {
        private IWapUserSettingRepository _repo;

        public WapUserSettingServiceImpl(IWapUserSettingRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 添加用户配置
        /// </summary>
        /// <returns>新增后的用户配置对象</returns>
        public WapUserSettingDto Add(WapUserSettingDto userSetting)
        {
            if (userSetting == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置对象不允许为空");
            }
            else if (userSetting != null)
            {
                if (userSetting.CreatorId == 0)
                {
                    throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置创建者不允许为空");
                }
                var validateResult = userSetting.Validate();
                if (!validateResult.IsValid)
                {
                    throw validateResult.BuildException();
                }
            }
            var result = _repo.Add(userSetting.ToModel());
            if (result != null)
            {
                //返回创建时间和修改时间
                var entity = GetByUserSettingId(result.UserSettingId);
                if (entity.Count() > 0)
                {
                    return entity.FirstOrDefault();
                }
                return null;
            }

            return null;
        }

        /// <summary>
        /// 添加用户配置组别
        /// </summary>
        /// <returns>新增后的用户配置组别对象</returns>
        public WapUserSettingDto AddGroup(AddUserSettingGroupDto addGroup)
        {
            if (addGroup == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置组别对象不允许为空");
            }
            else
            {
                var validateResult = addGroup.Validate();
                if (!validateResult.IsValid)
                {
                    throw validateResult.BuildException();
                }
            }
            var result = _repo.AddGroup(addGroup.ToModel());
            if (result != null)
            {
                //返回创建时间和修改时间
                var entity = GetByUserSettingId(result.UserSettingId);
                if (entity.Count() > 0)
                {
                    return entity.FirstOrDefault();
                }
                return null;
            }
            return null;
        }

        /// <summary>
        /// 删除用户配置
        /// </summary>
        /// <param name="userSettingId">用户配置编号</param>
        /// <returns>是否删除成功</returns>
        public bool Remove(int userSettingId)
        {
            if (userSettingId == 0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置编号不能为空");
            }
            return _repo.Remove(userSettingId);
        }

        /// 根据用户配置编号修改用户配置
        /// </summary>
        /// <param name="userSetting">用户配置对象</param>
        /// <returns>修改后的用户配置对象</returns>
        public WapUserSettingDto Modify(WapUserSettingDto userSetting)
        {
            if (userSetting == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置对象不允许为空");
            }
            else if (userSetting.UserSettingId == 0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置编号不能为空");
            }
            else if (userSetting.UserSettingCode == null || userSetting.UserSettingCode == string.Empty)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置编码不能为空");
            }
            else if (userSetting.UserSettingValue == null || userSetting.UserSettingValue == string.Empty)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置值不能为空");
            }
            else if (userSetting.ModifierId == 0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置修改者不能为空");
            }
            var result = _repo.Modify(userSetting.ToModel());
            if (result != null)
            {
                //返回修改后的新对象
                var entity = GetByUserSettingId(result.UserSettingId);
                if (entity.Count() > 0)
                {
                    return entity.FirstOrDefault();
                }
                return null;
            }
            return null;
        }

        /// <summary>
        /// 根据用户配置编号修改用户配置组别
        /// </summary>
        /// <param name="userSetting">用户配置对象</param>
        /// <returns>修改后的用户配置对象组别</returns>
        public WapUserSettingDto ModifyGroup(ModifyUserSettingGroupDto modifyGroup)
        {
            if (modifyGroup == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置组别对象不允许为空");
            }
            else
            {
                var validateResult = modifyGroup.Validate();
                if (!validateResult.IsValid)
                {
                    throw validateResult.BuildException();
                }
            }
            var result = _repo.ModifyGroup(modifyGroup.ToModel());
            if (result != null)
            {
                //返回修改时间
                var entity = GetByUserSettingId(result.UserSettingId);
                if (entity.Count() > 0)
                {
                    return entity.FirstOrDefault();
                }
                return null;
            }
            return null;
        }

        /// <summary>
        /// 根据用户编号和配置编码修改用户配置
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="code">用户配置编码</param>
        /// <returns>更新后的用户配置对象</returns>
        public WapUserSettingDto ModifyByUserIdAndCode(int userId, string code, WapUserSettingDto userSetting)
        {
            if (userId == 0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置编号不允许为空");
            }
            if (code == null || code == string.Empty)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置编码不允许为空");
            }
            if (userSetting == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置对象不允许为空");
            }
            else if (userSetting.UserSettingValue == null || userSetting.UserSettingValue == string.Empty)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置值不允许为空");
            }
            else if (userSetting.ModifierId == 0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置修改者不能为空");
            }
            var result = _repo.ModifyByUserIdAndCode(userId, code, userSetting.ToModel());
            //返回修改后的新对象
            var entity = GetByUserSettingId(result.UserSettingId);
            if (entity.Count() > 0)
            {
                return entity.FirstOrDefault();
            }
            return null;
        }

        /// <summary>
        ///获取所有用户配置
        /// </summary>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSettingDto> GetAll()
        {
            var result = _repo.GetAll();
            if (result.Count() > 0)
            {
                return result.Select(r => WapUserSettingDto.FromModel(r)).ToList();
            }
            return null;
        }

        /// <summary>
        /// 根据配置编号获取用户配置参数
        /// </summary>
        /// <param name="userSettingId">用户配置对象编号</param>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSettingDto> GetByUserSettingId(int userSettingId)
        {
            if (userSettingId == 0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户配置编号不能为空");
            }
            var result = _repo.GetByUserSettingId(userSettingId);
            if (result.Count() > 0)
            {
                return result.Select(r => WapUserSettingDto.FromModel(r)).ToList();
            }
            return null;

        }

        /// <summary>
        /// 根据配置编号获取用户配置参数
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSettingDto> GetByUserId(int userId)
        {
            if (userId == 0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户编号不能为空");
            }
            var result = _repo.GetByUserId(userId);
            if (result.Count() > 0)
            {
                return result.Select(r => WapUserSettingDto.FromModel(r)).ToList();
            }
            return null;
        }

        /// <summary>
        /// 根据用户编号和配置编码查询用户配置
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="code">配置编码</param>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSettingDto> GetByUserIdAndCode(int userId, string code)
        {
            if (userId == 0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户编号不能为空");
            }
            if (code == null || code == String.Empty)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "配置编码不能为空");
            }
            var result = _repo.GetByUserIdAndCode(userId, code);
            if (result.Count() > 0)
            {
                return result.Select(r => WapUserSettingDto.FromModel(r)).ToList();
            }
            return null;
        }

        /// <summary>
        /// 根据用户编号和应用标识查询用户配置
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSettingDto> GetByUserIdAndAppIdentity(int userId, string appIdentity)
        {
            if (userId == 0)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "用户编号不能为空");
            }
            if (appIdentity == null || appIdentity == String.Empty)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "应用标识不能为空");
            }
            var result = _repo.GetByUserIdAndAppIdentity(userId, appIdentity);
            if (result.Count() > 0)
            {
                return result.Select(r => WapUserSettingDto.FromModel(r)).ToList();
            }
            return null;
        }

        /// <summary>
        /// 根据应用标识查询用户配置
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>用户配置对象列表</returns>
        public IEnumerable<WapUserSettingDto> GetByAppIdentity(string appIdentity)
        {
            if (appIdentity == null || appIdentity == String.Empty)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "应用标识不能为空");
            }
            var result = _repo.GetByAppIdentity(appIdentity);
            if (result.Count() > 0)
            {
                return result.Select(r => WapUserSettingDto.FromModel(r)).ToList();
            }
            return null;
        }
    }
}

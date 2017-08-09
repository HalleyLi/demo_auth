using SH3H.WAP.Auth.Contracts;
using SH3H.WAP.Auth.Model;
using SH3H.SDK.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SH3H.WAP.Auth.DataAccess.Repo;
using System.Threading;
using Newtonsoft.Json;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using SH3H.WAP.Auth.Model.Dto;
using SH3H.SDK.Definition.Exceptions;
using SH3H.SDK.Infrastructure.Logging;
namespace SH3H.WAP.Auth.Service
{
    /// <summary>
    /// 组织机构服务实例
    /// </summary>
    public class WapOrganizationServiceImpl : BaseService, IWapOrganizationService
    {
        // Repository类型的字段声明
        private IWapOrganizationRepository _organizationRepository = null;
        // Repository类型的字段声明
        private IAuthSeqRepository _authSeqRepository = null;
        // Repository类型的字段声明
        private IWapAuthAuditRepository _wapAuthAudit = null;
        /// <summary>
        /// 组织结构服务实例构造函数
        /// </summary>
        /// <param name="repository">组织对象仓库接口</param>
        /// <param name="seq">权限认证仓库接口</param>
        /// <param name="audit">数据审计认证仓库接口</param>
        public WapOrganizationServiceImpl(IWapOrganizationRepository repository, IAuthSeqRepository seq, IWapAuthAuditRepository audit)
        {
            _organizationRepository = repository;
            _authSeqRepository = seq;
            _wapAuthAudit = audit;
        }

        private static string SeqKey_Organization = "organization";
        private static string SeqKey_OrganizationUser = "organizationUser";

        /// <summary>
        /// 添加组织对象
        /// </summary>
        /// <param name="organization">组织对象</param>
        /// <returns>组织对象</returns>
        public WapOrganizationDto AddOrganization(WapOrganizationDto organization)
        {
            try
            {
                if (organization.ParentOrganizationKey == null)
                    organization.ParentOrganizationKey = Guid.NewGuid().ToString().ToUpper();

                var seq = _authSeqRepository.CreateSequence(SeqKey_Organization);
                if (seq == null)
                {
                    return null;
                }
                organization.OrganizationKey = seq.IdentityKey;
                WapOrganization model = WapOrganizationDto.ToModel(organization);

                model = _organizationRepository.AddOrganization(model);
                if (model != null)
                {
                    AddAudit(model.OrganizationKey, "AddOrganization", model);
                }
                return WapOrganizationDto.FromModel(model);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "添加组织对象执行异常");
            }
        }

        /// <summary>
        /// 删除组织对象
        /// </summary>
        /// <param name="organizationKey">组织对象编号</param>
        /// <returns>是否删除成功</returns>
        public bool RemoveOrganization(string organizationKey)
        {
            try
            {
                AddAudit(organizationKey, "RemoveOrganization", organizationKey);

                return _organizationRepository.RemoveOrganization(organizationKey);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "执行异常");
            }
        }

        /// <summary>
        /// 通过组织对象编号修改组织对象
        /// </summary>
        /// <param name="organization">组织对象</param>
        /// <returns>成功返回组织对象，否则返回null</returns>
        public WapOrganizationDto ModifyOrganizationById(WapOrganizationDto organization)
        {
            try
            {
                WapOrganization model = WapOrganizationDto.ToModel(organization);
                model = _organizationRepository.ModifyOrganizationById(model);
                if (model != null)
                {
                    AddAudit(model.OrganizationKey, "ModifyOrganizationById", model);
                }
                return WapOrganizationDto.FromModel(model);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "执行异常");
            }
        }
        /// <summary>
        /// 更新组织结构状态
        /// </summary>
        /// <param name="organizationKey">组织对象编号</param>
        /// <param name="isActive">是否激活</param>
        /// <returns>是否激活成功</returns>
        public bool UpdateOrganizationState(string organizationKey, WapStateDto isActive)
        {
            try
            {
                if (isActive == null)
                {
                    throw new WapException(Share.StateCode.CODE_ARGUMENT_NULL, "状态不能为空");
                }


                if (isActive.Active)
                {
                    var result = this.ActiveOrganization(organizationKey);
                    return (bool)result;
                }
                else
                {
                    var result = this.DeactiveOrganization(organizationKey);
                    return (bool)result;
                }
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "执行异常");
            }
        }
        /// <summary>
        /// 禁用组织对象（state=0）
        /// </summary>
        /// <param name="organizationKey">组织对象编号</param>
        /// <returns>是否成功</returns>
        public bool DeactiveOrganization(string organizationKey)
        {

            try
            {
                AddAudit(organizationKey, "DeactiveOrganization", organizationKey);

                return _organizationRepository.DeactiveOrganization(organizationKey);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "执行异常");
            }
        }
        /// <summary>
        /// 启用组织对象（state=1）
        /// </summary>
        /// <param name="organizationKey">组织对象编号</param>
        /// <returns>是否成功</returns>
        public bool ActiveOrganization(string organizationKey)
        {

            try
            {
                AddAudit(organizationKey, "ActiveOrganization", organizationKey);

                return _organizationRepository.ActiveOrganization(organizationKey);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "启用组织对象执行异常");
            }
        }
        /// <summary>
        /// 通过组织对象编号查询站点
        /// </summary>
        /// <param name="organizationKey">组织对象编号</param>
        /// <returns>组织对象</returns>
        public WapOrganizationDto GetOrganizationByOrganizationKey(string organizationKey)
        {
            try
            {
                WapOrganization model = _organizationRepository.GetOrganizationByOrganizationKey(organizationKey);
                return WapOrganizationDto.FromModel(model);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "通过组织对象编号查询站点执行异常");
            }
        }
        /// <summary>
        /// 根据组织对象编号批量更新词语排序值
        /// </summary>
        /// <param name="sortIndexes">组织对象排序值数组</param>
        /// <returns>是否所有更新都成功</returns>
        public bool ModifyOrgIndexByOrganizationKey(Dictionary<string, int> sortIndexes)
        {

            try
            {
                AddAudit(Guid.NewGuid(), "ModifyOrgIndexByOrganizationKey", sortIndexes);

                return _organizationRepository.ModifyOrgIndexByOrganizationKey(sortIndexes);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "根据组织对象编号批量更新词语排序值执行异常");
            }
        }
        /// <summary>
        /// 根据组织对象编号更新站点排序值
        /// </summary>
        /// <param name="organizationKey">组织对象编号</param>
        /// <param name="sortIndex">组织对象排序值</param>
        ///<param name="trans"></param>
        /// <returns>更新是否成功</returns>
        public bool ModifyOrgIndexByOrganizationKey(string organizationKey, int sortIndex, System.Data.Common.DbTransaction trans = null)
        {

            try
            {
                AddAudit(organizationKey, "ModifyOrgIndexByOrganizationKey", organizationKey + "|" + sortIndex);

                return _organizationRepository.ModifyOrgIndexByOrganizationKey(organizationKey, sortIndex);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "根据组织对象编号更新站点排序值执行异常");
            }
        }
        /// <summary>
        /// 获取所有组织对象
        /// </summary>
        /// <returns>所有组织对象列表</returns>
        public IEnumerable<WapOrganizationDto> GetAllOrganizations()
        {
            try
            {
                List<WapOrganizationDto> dtos = new List<WapOrganizationDto>();
                IEnumerable<WapOrganization> organizations = _organizationRepository.GetAllOrganizations();
                if (organizations != null && organizations.Count() > 0)
                {
                    foreach (var item in organizations)
                    {
                        dtos.Add(WapOrganizationDto.FromModel(item));
                    }
                }
                return dtos;
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取所有组织对象执行异常");
            }
        }

        #region UserOrganziation
        /// <summary>
        /// 创建或更新用户与组织的关联
        /// </summary>
        /// <param name="userKey">用户key</param>
        /// <param name="relationObject">关联对象</param>
        /// <returns>是否操作成功</returns>
        public bool CreateOrDeleteOrganizationUserRelation(string userKey, Model.Dto.UpdateUserOrganizationRelationObject relationObject)
        {
            try
            {
                UpdateUserOrganizationRelationModel relationModel = new UpdateUserOrganizationRelationModel();//处理object对象，返回model对象，方便数据库操作
                if (relationObject != null && relationObject.addOrg != null && relationObject.addOrg.Count() > 0)
                {
                    List<UserOrganizationRelation> relationList = new List<UserOrganizationRelation>();
                    foreach (var item in relationObject.addOrg)
                    {
                        UserOrganizationRelation relation = new UserOrganizationRelation();
                        var seq = _authSeqRepository.CreateSequence(SeqKey_OrganizationUser);
                        if (seq == null)
                        {
                            return false;
                        }
                        relation.UserKey = userKey;
                        relation.RelationKey = seq.IdentityKey;
                        relation.OrganKey = item;
                        relationList.Add(relation);
                    }
                    relationModel.AddArr = relationList;
                }
                if (relationObject != null && relationObject.delOrg != null && relationObject.delOrg.Count() > 0)
                {
                    List<UserOrganizationRelation> relationList = new List<UserOrganizationRelation>();
                    foreach (var item in relationObject.delOrg)
                    {
                        UserOrganizationRelation relation = new UserOrganizationRelation();
                        relation.UserKey = userKey;
                        relation.OrganKey = item;
                        relationList.Add(relation);
                    }
                    relationModel.DeletedArr = relationList;
                }

                AddAudit(Guid.NewGuid(), "CreateOrDeleteOrganizationUserRelation", relationObject);

                return _organizationRepository.UpdateOrganizationUserRelation(relationModel);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "创建或更新用户与组织的关联执行异常");
            }
        }
        /// <summary>
        /// 获取指定用户的组织
        /// </summary>
        /// <param name="userKey">用户编号</param>
        /// <returns>组织对象集合</returns>
        public IEnumerable<WapOrganizationDto> GetOrganizationbyUser(string userKey)
        {
            try
            {
                List<WapOrganizationDto> dtos = new List<WapOrganizationDto>();
                IEnumerable<WapOrganization> organizations = _organizationRepository.GetOrganizationbyUser(userKey);
                if (organizations != null && organizations.Count() > 0)
                {
                    foreach (var item in organizations)
                    {
                        dtos.Add(WapOrganizationDto.FromModel(item));
                    }
                }
                return dtos;
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取指定用户的组织执行异常");
            }
        }






        /// <summary>
        ///   更新组织和用户的关联
        /// </summary>
        /// <param name="relationModel"></param>
        /// <returns></returns>

        public bool UpdateOrganizationUserRelation(Model.Dto.UpdateUserOrganizationRelationModel relationModel)
        {

            try
            {
                if (relationModel != null && relationModel.AddArr != null && relationModel.AddArr.Count() > 0)
                {
                    foreach (var item in relationModel.AddArr)
                    {
                        var seq = _authSeqRepository.CreateSequence(SeqKey_OrganizationUser);
                        if (seq == null)
                        {
                            return false;
                        }
                        item.RelationKey = seq.IdentityKey;

                    }
                }

                AddAudit(Guid.NewGuid(), "UpdateOrganizationUserRelation", relationModel);

                return _organizationRepository.UpdateOrganizationUserRelation(relationModel);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "更新组织和用户的关联执行异常");
            }
        }

        /// <summary>
        /// 获取组织和用户的关联
        /// </summary>
        /// <param name="relationKey"></param>
        /// <returns></returns>
        public Model.Dto.UserOrganizationRelation GetOrganizationUserRelation(string relationKey)
        {
            try
            {
                return _organizationRepository.GetOrganizationUserRelation(relationKey);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取组织和用户的关联执行异常");
            }
        }

        /// <summary>
        /// 获取所有组织和用户的关联
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Model.Dto.UserOrganizationRelation> GetAllOrganizationUserRelation()
        {
            try
            {
                return _organizationRepository.GetAllOrganizationUserRelation();
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "获取所有组织和用户的关联执行异常");
            }
        }

        /// <summary>
        /// 凭组织获取所有组织和用户的关联
        /// </summary>
        /// <param name="organKey"></param>
        /// <returns></returns>
        public IEnumerable<Model.Dto.UserOrganizationRelation> GetAllOrganizationUserRelationByOrganKey(string organKey)
        {
            try
            {
                return _organizationRepository.GetAllOrganizationUserRelationByOrganKey(organKey);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "凭组织获取所有组织和用户的关联执行异常");
            }
        }

        /// <summary>
        /// 凭组用户取所有组织和用户的关联
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns></returns>
        public IEnumerable<Model.Dto.UserOrganizationRelation> GetAllOrganizationUserRelationByUserKey(string userKey)
        {
            try
            {
                return _organizationRepository.GetAllOrganizationUserRelationByUserKey(userKey);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "凭组用户取所有组织和用户的关联执行异常");
            }
        }

        /// <summary>
        /// 凭组织获取用户
        /// </summary>
        /// <param name="organkey"></param>
        /// <returns></returns>
        public IEnumerable<WapUser> GetUserByOrganKey(string organkey)
        {
            try
            {
                return _organizationRepository.GetUserByOrganKey(organkey);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "凭组织获取用户执行异常");
            }
        }

        /// <summary>
        /// 凭组织列表获取用户
        /// </summary>
        /// <param name="organkeys"></param>
        /// <returns></returns>
        public IEnumerable<WapUser> GetUserByOrganKeys(IEnumerable<string> organkeys)
        {
            try
            {
                return _organizationRepository.GetUserByOrganKeys(organkeys);
            }
            catch (WapException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);

                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_EXCEPTION, "凭组织列表获取用户执行异常");
            }
        }

        #endregion

        #region 审计
        private void AddAudit(string key, string actionName, string content)
        {
            //记录数据审计日志
            ThreadPool.QueueUserWorkItem(p =>
            {
                try
                {
                    var audit = new WapAuthAudit();
                    audit.TrackingGuid = key;
                    audit.OperateFunc = actionName;
                    audit.OperateContent = content;
                    _wapAuthAudit.AddAudit(audit);
                }
                catch (Exception)
                {
                    //不记录审计插入错误
                }
            });
        }

        private void AddAudit(string key, string actionName, object content)
        {
            string strContent = JsonConvert.SerializeObject(content);
            if (strContent.Length > 4000)
            {
                strContent = strContent.Substring(0, 4000);
            }

            AddAudit(key, actionName, strContent);
        }

        private void AddAudit(Guid key, string actionName, object content)
        {
            string strContent = JsonConvert.SerializeObject(content);
            if (strContent.Length > 4000)
            {
                strContent = strContent.Substring(0, 4000);
            }
            AddAudit(key.ToString(), actionName, strContent);
        }

        private void AddAudit(Guid key, string actionName, string content)
        {
            string strContent = JsonConvert.SerializeObject(content);
            if (strContent.Length > 4000)
            {
                strContent = strContent.Substring(0, 4000);
            }
            AddAudit(key.ToString(), actionName, strContent);
        }


        #endregion

    }
}

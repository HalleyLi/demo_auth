using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Contracts
{
    /// <summary>
    /// 功能点接口
    /// </summary>
    public interface IWapFunctionService
    {
        #region  Function
        /// <summary>
        /// 向某个功能点组添加一个功能点
        /// </summary>
        /// <param name="functionGroupRelative"></param>
        /// <returns></returns>
        WapFuncGroupRelativeDto Add(WapFuncGroupRelativeDto functionGroupRelative);

        /// <summary>
        /// 获取所有功能点
        /// </summary>
        /// <returns>返回所有功能点列表</returns>
        IEnumerable<WapFunctionAllDto> GetAllFunction();

        /// <summary>
        /// 获取所有功能点
        /// </summary>
        /// <param name="appidentity"></param> 
        /// <returns>返回所有功能点列表</returns>
        IEnumerable<WapFunctionAllDto> GetAllFunctionByAppIdentity(string appidentity);

        /// <summary>
        /// 获取所有功能点
        /// </summary>
        /// <param name="appkey"></param> 
        /// <returns>返回所有功能点列表</returns>
        IEnumerable<WapFunctionAllDto> GetAllFunctionByAppKey(string appkey);

        /// <summary>
        /// 凭角色获取功能点集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<WapFunctionAllDto> GetAllFunctionByRoles(IEnumerable<string> rolekeys);

        ///<summary> 
        ///获取功能点 
        ///</summary> 
        /// <param name="functionkey"></param> 
        ///<return></return> 
        WapFunctionDto GetFunction(string functionkey);

        /// <summary>
        ///  获取功能点集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<WapFunctionDto> GetAllFunctions();

        /// <summary>
        ///新增功能点
        /// </summary>
        /// <param name="function">功能点</param>
        /// <returns></returns> 
        WapFunctionDto AddFunction(WapFunctionDto function);

        /// <summary>
        /// 删除功能点
        /// </summary>
        /// <param name="funckey">功能点</param>
        /// <returns></returns> 
        bool DeleteFunction(string funckey);

        /// <summary>
        /// 更新功能点
        /// </summary>
        /// <param name="function">Function</param>
        /// <returns></returns> 
        WapFunctionDto UpdateFunction(WapFunctionDto function);

        /// <summary>
        /// 激活功能点
        /// </summary>
        /// <param name="functionkey"></param>
        /// <returns></returns>
        bool ActiveFunction(string functionkey);

        /// <summary>
        /// 禁用功能点
        /// </summary>
        /// <param name="functionkey"></param>
        /// <returns></returns>
        bool DeactiveFunction(string functionkey);

        ///<summary> 
        ///获取功能点 
        ///</summary> 
        /// <param name="code"></param> 
        ///<return></return> 
        WapFunctionDto GetFunctionByCode(string code);

        /// <summary>
        ///修改功能点状态
        /// </summary>
        /// <param name="funcKey"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        bool UpdateFunctionState(string funcKey, WapStateDto active);

        /// <summary>
        /// 功能点模糊查询
        /// </summary>
        /// <param name="searchText">搜索关键字</param>
        /// <returns>功能点列表</returns>
        IEnumerable<WapFunctionDto> FuzzySearch(string searchText);

        /// <summary>
        /// 通过appKey和funcCode获取功能点
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <param name="funcCode">功能点Code</param>
        /// <returns>存在返回true\不存在返回false</returns>
        bool IsHaveFuncByFuncCodeAndAppKey(string appKey, string funcCode);

        #endregion

        #region RoleFunction

        /// <summary>
        ///获取功能点信息
        /// </summary>
        /// <param name="relationKey"></param>
        /// <returns></returns>
        WapRoleFunctionDto GetRoleFunctionRelationByKey(string relationKey);

        /// <summary>
        /// 获取角色和功能点的关联
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        IEnumerable<WapRoleFunctionDto> GetAllRoleFunctionRelationByAppKey(string appKey);

        /// <summary>
        /// 获取角色和功能点的关联
        /// </summary>
        /// <param name="appIdentity"></param>
        /// <returns></returns>
        IEnumerable<WapRoleFunctionDto> GetAllRoleFunctionRelationByAppIdentity(string appIdentity);

        /// <summary>
        /// 根据角色获取功能点组信息
        /// </summary>
        /// <param name="roleKey">角色标识</param>
        /// <returns>功能点组列表</returns>
        IEnumerable<WapFuncRelativeDto> GetRoleFunction(string roleKey);


        /// <summary>
        /// 根据角色和应用获取功能点信息
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="appIdentity"></param>
        /// <returns></returns>
        IEnumerable<WapFuncRelativeDto> GetRoleFunctionRelationByRoleKeyAndAppIdentity(string roleKey, string appIdentity);

        /// <summary>
        /// 根据角色和应用获取功能点信息
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="appKey"></param>
        /// <returns></returns>
        IEnumerable<WapFuncRelativeDto> GetRoleFunctionRelationByRoleKeyAndAppKey(string roleKey, string appKey);

        /// <summary>
        /// 更新角色功能点关联关系
        /// </summary>
        /// <param name="relationModel">需要关联和需要解绑的功能点角色关系</param>
        /// <returns>是否更新成功</returns>
        WapUpdateRoleFuncRelationDto UpdateRoleFunctionRelation(WapUpdateRoleFuncRelationDto relationModel);

        /// <summary>
        /// 更新角色功能点关联关系
        /// </summary>
        /// <param name="roleKey">角色主键</param>
        /// <param name="relationModel">需要关联的功能点</param>
        /// <returns>是否更新成功</returns>
        bool UpdateRoleFunctionRelation(string roleKey, WapUpdateFuncsRelationDto relationModel);

        #endregion

        #region FunctionGroupRelation

        /// <summary>
        /// 根据角色获取功能点组信息
        /// </summary>
        /// <param name="roleKey">角色标识</param>
        /// <returns>功能点组列表</returns>
        IEnumerable<WapFuncGroupRelativeDto> GetFunctionGroupRelation(string funcGroupKey);

        /// <summary>
        /// 更新角色功能点关联关系
        /// </summary>
        /// <param name="relationModel">需要关联和需要解绑的功能点角色关系</param>
        /// <returns>是否更新成功</returns>
        WapUpdateFuncGroupRelationDto UpdateFunctionGroupRelation(WapUpdateFuncGroupRelationDto relationModel);

        #endregion

        #region  FunctionGroup
        /// <summary>
        /// 获取所有功能点组(原)
        /// </summary>
        /// <returns>功能点组列表</returns>
        IEnumerable<WapFunctionGroupDto> GetAllFunctionGroup();

        /// <summary>
        /// 获取所有功能点组
        /// </summary>
        /// <returns>功能点组列表</returns>
        IEnumerable<WapFunctionGroupDto> GetAllFunctionGroups();

        /// <summary>
        /// 获取所有功能点组
        /// </summary>
        /// <param name="appidentity"></param> 
        /// <returns>功能点组列表</returns>
        IEnumerable<WapFunctionGroupDto> GetAllFunctionGroupsByAppIdentity(string appidentity);

        /// <summary>
        /// 获取所有功能点组
        /// </summary>
        /// <param name="appkey"></param> 
        /// <returns>功能点组列表</returns>
        IEnumerable<WapFunctionGroupDto> GetAllFunctionGroupsByAppKey(string appkey);

        ///<summary> 
        ///获取功能点组 
        ///</summary> 
        /// <param name="functiongroupkey"></param> 
        ///<return></return> 
        WapFunctionGroupDto GetFunctionGroup(string functiongroupkey);


        /// <summary>
        ///新增功能点组
        /// </summary>
        /// <param name="functiongroup">功能点</param>
        /// <returns></returns> 
        WapFunctionGroupDto AddFunctionGroup(WapFunctionGroupDto functiongroup);

        /// <summary>
        /// 删除功能点组
        /// </summary>
        /// <param name="functiongroupkey">功能点组</param>
        /// <returns></returns> 
        bool DeleteFunctionGroup(string functiongroupkey);

        /// <summary>
        /// 更新功能点组
        /// </summary>
        /// <param name="functiongroup">FunctionGroup</param>
        /// <returns></returns> 
        WapFunctionGroupDto UpdateFunctionGroup(WapFunctionGroupDto functiongroup);


        /// <summary>
        /// 激活功能点
        /// </summary>
        /// <param name="fungroupkey"></param>
        /// <returns></returns>
        bool ActiveFunctionGroup(string fungroupkey);

        /// <summary>
        /// 禁用功能点
        /// </summary>
        /// <param name="fungroupkey"></param>
        /// <returns></returns>
        bool DeactiveFunctionGroup(string fungroupkey);

        /// <summary>
        ///修改功能组状态
        /// </summary>
        /// <param name="funcGroupKey"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        bool UpdateFuncGroupState(string funcGroupKey, WapStateDto active);

        /// <summary>
        /// 通过appKey和groupName获取功能点组(该功能点组是否存在)
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <param name="groupName">功能点组名称</param>
        /// <returns>存在返回true\不存在返回false</returns>
        bool IsHaveFuncGroupByNameAndAppKey(string appKey, string groupName);

        #endregion

    }
}

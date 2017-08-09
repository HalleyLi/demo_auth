using SH3H.SDK.WebApi.Controllers;
using SH3H.SDK.WebApi.Core;
using SH3H.SDK.WebApi.Core.Models;
using SH3H.WAP.Auth.Contracts;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WapConsts = SH3H.WAP.Share.Consts;


namespace SH3H.WAP.Auth.Controllers
{
    /// <summary>
    /// 定义WAP权限认证功能服务控制器
    /// </summary>
    [Resource("wapFunctionRes")]
    [RoutePrefix(WapConsts.URL_PREFIX_WAP)]
    public class WapFunctionController :
        BaseController<IWapFunctionService>
    {

        #region FunctionGroup


        /// <summary>
        /// 新增功能点组
        /// </summary>
        /// <param name="model">功能组</param>
        /// <returns>是否成功</returns>
        [HttpPost]
        [Route("function/groups")]
        [ActionName("addFuncGroup")]
        public WapResponse<WapFunctionGroupDto> AddFunctionGroup(WapFunctionGroupDto model)
        {

            if (model == null)
            {
                return new WapResponse<WapFunctionGroupDto>(null);
            }
            var result = Service.AddFunctionGroup(model);
            return new WapResponse<WapFunctionGroupDto>(result);
        }

        /// <summary>
        /// 修改功能点组
        /// </summary>
        /// <param name="funcGroupKey">功能组编号</param>
        /// <param name="model">输入模型</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [Route("function/groups/{funcGroupKey}")]
        [ActionName("updateFuncGroup")]
        public WapResponse<WapFunctionGroupDto> UpdateFunctionGroup(string funcGroupKey, WapFunctionGroupDto model)
        {
            if (model == null)
            {
                return new WapResponse<WapFunctionGroupDto>(null);
            }
            if (!string.IsNullOrEmpty(funcGroupKey))
            {
                model.FuncGroupKey = funcGroupKey;
            }

            var result = Service.UpdateFunctionGroup(model);
            return new WapResponse<WapFunctionGroupDto>(result);
        }

        /// <summary>
        /// 修改功能点组状态
        /// </summary>
        /// <param name="funcGroupKey"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("function/groups/{funcGroupKey}/state")]
        [ActionName("updateFuncGroupState")]
        public WapBoolean UpdateFuncGroupState(string funcGroupKey, [FromBody]WapStateDto active)
        {
            bool result = Service.UpdateFuncGroupState(funcGroupKey, active);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 获取所有功能点组
        /// </summary>
        /// <returns>功能点组列表</returns>
        [HttpGet]
        [Route("function/groups")]
        [ActionName("getAllFuncGroups")]
        public WapCollection<WapFunctionGroupDto> GetAllFunctionGroups()
        {
            var result = Service.GetAllFunctionGroups();
            return new WapCollection<WapFunctionGroupDto>(result);
        }

        /// <summary>
        /// 获取指定功能点组
        /// </summary>
        /// <param name="funcGroupKey">功能组编号</param>
        /// <returns>功能点组</returns>
        [HttpGet]
        [Route("function/groups/{funcGroupKey}")]
        [ActionName("getFuncGroupByKey")]
        public WapFunctionGroupDto GetFunctionGroup(string funcGroupKey)
        {
            return Service.GetFunctionGroup(funcGroupKey);
        }

        /// <summary>
        /// 凭应用获取功能点组列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>功能点组列表</returns>
        [HttpGet]
        [Route("function/groups")]
        [ActionName("getAllFunctionGroupsByAppIdentity")]
        public WapCollection<WapFunctionGroupDto> GetAllFunctionGroupsByAppIdentity(string appIdentity)
        {
            var result = Service.GetAllFunctionGroupsByAppIdentity(appIdentity);
            return new WapCollection<WapFunctionGroupDto>(result);
        }


        /// <summary>
        /// 凭功能组获取下属功能点
        /// </summary>
        /// <param name="funcGroupKey">功能组编号</param>
        /// <returns>功能点与功能点组关联列表</returns>
        [HttpGet]
        [Route("function/groups/{funcGroupKey}/functions")]
        [ActionName("getFunctionGroupRelation")]
        public WapCollection<WapFuncGroupRelativeDto> GetFunctionGroupRelation(string funcGroupKey)
        {
            var result = Service.GetFunctionGroupRelation(funcGroupKey);
            return new WapCollection<WapFuncGroupRelativeDto>(result);
        }
        
        /// <summary>
        /// 通过appKey和groupName获取功能点组(该功能点组是否存在)
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <param name="groupName">功能点组名称</param>
        /// <returns>存在返回true\不存在返回false</returns>
        [HttpGet]
        [Route("functions")]
        [ActionName("isHaveFuncGroupByNameAndAppKey")]
        public WapBoolean IsHaveFuncGroupByNameAndAppKey(string appKey, string groupName)
        {
            bool result = Service.IsHaveFuncGroupByNameAndAppKey(appKey, groupName);
            return new WapBoolean(result);
        }

        #endregion

        #region Function

        /// <summary>
        /// 新增功能点
        /// </summary>
        /// <param name="model">新增模型</param>
        /// <returns>功能点</returns>
        [HttpPost]
        [Route("functions")]
        [ActionName("createFunction")]
        private WapResponse<WapFunctionDto> AddFunction(WapFunctionDto model)
        {
            if (model == null)
            {
                return null;
            }

            var result = Service.AddFunction(model);
            return new WapResponse<WapFunctionDto>(result);
        }

        /// <summary>
        /// 修改功能点
        /// </summary>
        /// <param name="funcKey">功能点编号</param>
        /// <param name="model">更新模型</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [Route("functions/{funcKey}")]
        [ActionName("updateFunction")]
        public WapResponse<WapFunctionDto> UpdateFunction(string funcKey, WapFunctionDto model)
        {

            if (model == null)
            {
                return new WapResponse<WapFunctionDto>(null);
            }
            if (!string.IsNullOrEmpty(funcKey))
            {
                model.Key = funcKey;
            }

            var result = Service.UpdateFunction(model);
            return new WapResponse<WapFunctionDto>(result);
        }

        /// <summary>
        /// 修改功能点状态
        /// </summary>
        /// <param name="funcKey">功能点编号</param>
        /// <param name="active">是否激活</param>
        /// <returns>是否成功</returns>
        [HttpPatch]
        [Route("functions/{funcKey}/state")]
        [ActionName("updateFunctionState")]
        public WapBoolean UpdateFunctionState(string funcKey, [FromBody]WapStateDto active)
        {
            bool result = Service.UpdateFunctionState(funcKey, active);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 获取所有功能点
        /// </summary>
        /// <returns>功能点列表</returns>
        [HttpGet]
        [Route("functions")]
        [ActionName("getAllFunctions")]
        public WapCollection<WapFunctionDto> GetAllFunctions()
        {
            var result = Service.GetAllFunctions();
            return new WapCollection<WapFunctionDto>(result);
        }

        /// <summary>
        /// 获取指定功能点
        /// </summary>
        /// <param name="funcKey">功能点编号</param>
        /// <returns>功能点</returns>
        [HttpGet]
        [Route("functions/{funcKey}")]
        [ActionName("getFunctionByKey")]
        public WapResponse<WapFunctionDto> GetFunction(string funcKey)
        {
            var result = Service.GetFunction(funcKey);
            return new WapResponse<WapFunctionDto>(result);
        }


        /// <summary>
        /// 获取指定代码的功能点
        /// </summary>
        /// <param name="funcCode">功能点代码</param>
        /// <returns>功能点列表</returns>
        [HttpGet]
        [Route("functions")]
        [ActionName("getFunctionByCode")]
        public WapResponse<WapFunctionDto> GetFunctionByCode(string funcCode)
        {
            var result = Service.GetFunctionByCode(funcCode);
            return new WapResponse<WapFunctionDto>(result);
        }

        /// <summary>
        ///  凭应用标识获取功能点列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>功能点列表</returns>
        [HttpGet]
        [Route("functions")]
        [ActionName("getAllFunctionsByAppIdentity")]
        public WapCollection<WapFunctionDto> GetAllFunctionsByAppIdentity(string appIdentity)
        {
            var result = Service.GetAllFunctionByAppIdentity(appIdentity);
            return new WapCollection<WapFunctionDto>(result);
        }

        /// <summary>
        /// 功能点模糊查询
        /// </summary>
        /// <param name="searchText">搜索关键字</param>
        /// <returns>功能点列表</returns>
        [HttpGet]
        [Route("functions/search")]
        [ActionName("fuzzyQuery")]
        public WapResponse<IEnumerable<WapFunctionDto>> FuzzyQuery(string searchText)
        {
            var result = Service.FuzzySearch(searchText);
            return new WapResponse<IEnumerable<WapFunctionDto>>(result);
        }

        /// <summary>
        /// 通过appKey和funcCode获取功能点(该功能点是否存在)
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <param name="funcCode">功能点Code</param>
        /// <returns>存在返回true\不存在返回false</returns>
        [HttpGet]
        [Route("functions")]
        [ActionName("isHaveFuncByFuncCodeAndAppKey")]
        public WapBoolean IsHaveFuncByFuncCodeAndAppKey(string appKey, string funcCode)
        {
            bool result = Service.IsHaveFuncByFuncCodeAndAppKey(appKey, funcCode);
            return new WapBoolean(result);
        }


        #endregion

        #region WapRoleFunctionDto


        /// <summary>
        /// 获取指定角色的所有功能点
        /// </summary>
        /// <param name="roleKey">角色编号</param>
        /// <returns>功能点与角色关联列表</returns>
        [HttpGet]
        [Route("roles/{roleKey}/functions")]
        [ActionName("getFunctionsByRole")]
        public WapCollection<WapFuncRelativeDto> GetRoleFunction(string roleKey)
        {
            var result = Service.GetRoleFunction(roleKey);
            return new WapCollection<WapFuncRelativeDto>(result);
        }

        /// <summary>
        ///  获取指定角色在指定应用下的所有功能点
        /// </summary>
        /// <param name="roleKey">角色编号</param>
        /// <param name="appKey">应用主键</param>
        /// <returns>功能点与角色关联列表</returns>
        [HttpGet]
        [Route("functions")]
        [ActionName("getFunctionsByRoleAndApp")]
        public WapCollection<WapFuncRelativeDto> GetRoleFunctionyRoleKeyAndAppKey(string roleKey, string appKey)
        {
            IEnumerable<WapFuncRelativeDto> result = Service.GetRoleFunctionRelationByRoleKeyAndAppKey(roleKey, appKey);
            return new WapCollection<WapFuncRelativeDto>(result);
        }

        /// <summary>
        ///创建或更新指定角色与功能点关联
        /// </summary>
        /// <param name="roleKey">角色主键</param>
        /// <param name="relationModel">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPost]
        [Route("roles/{roleKey}/functions/relation")]
        [ActionName("createRoleFuncRelation")]
        public WapBoolean UpdateRoleFunctionRelation(string roleKey, WapUpdateFuncsRelationDto relationModel)
        {
            bool result = Service.UpdateRoleFunctionRelation(roleKey, relationModel);
            return new WapBoolean(result);
        }

        #endregion

        #region FunctionGroupRelation

        /// <summary>
        /// 向某个功能点组添加一个功能点
        /// </summary>
        /// <param name="functionGroupRelative"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("functions")]
        [ActionName("createFunction")]
        public WapResponse<WapFuncGroupRelativeDto> Add(WapFuncGroupRelativeDto functionGroupRelative)
        {
            var result = Service.Add(functionGroupRelative);
            return new WapResponse<WapFuncGroupRelativeDto>(result);
        }

        #endregion



        #region 1.6 封闭接口

        #region Function



        ///// <summary>
        ///// 凭应用主键获取功能点列表
        ///// </summary>
        ///// <param name="appkey">应用编号</param>
        ///// <returns>功能点列表</returns>
        //[HttpGet]
        //[Route("queryAllByAppKey/{appkey}")]
        //[ActionName("getAllFunctionsByAppKey")]
        //private WapCollection<WapFunctionDto> GetAllFunctionsByAppKey(string appkey)
        //{
        //    var result = Service.GetAllFunctionByAppKey(appkey);
        //    return new WapCollection<WapFunctionDto>(result);
        //}

        ///// <summary>
        ///// 凭角色获取功能点列表
        ///// </summary>
        ///// <param name="strroles">角色字符串 逗号分隔</param>
        ///// <returns>功能点列表</returns>
        //[HttpGet]
        //[Route("queryAllByRoles")]
        //[ActionName("getAllFunctionByRoles")]
        //private WapCollection<WapFunctionAllDto> GetAllFunctionByRoles(string strroles)
        //{
        //    string[] rolekeys = strroles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        //    var result = Service.GetAllFunctionByRoles(rolekeys);
        //    return new WapCollection<WapFunctionAllDto>(result);
        //}

        ///// <summary>
        ///// 激活功能点
        ///// </summary>
        ///// <param name="funckey">功能点编号</param>
        ///// <returns>是否成功</returns>
        //[HttpPut]
        //[Route("{funckey}/active")]
        //[ActionName("activeFunction")]
        //private WapBoolean ActiveFunction(string funckey)
        //{
        //    bool result = Service.ActiveFunction(funckey);
        //    return new WapBoolean(result);
        //}

        ///// <summary>
        ///// 禁用功能点
        ///// </summary>
        ///// <param name="funckey">功能点编号</param>
        ///// <returns>是否成功</returns>
        //[HttpPut]
        //[Route("{funckey}/deactive")]
        //[ActionName("deactiveFunction")]
        //private WapBoolean DeactiveFunction(string funckey)
        //{
        //    bool result = Service.DeactiveFunction(funckey);
        //    return new WapBoolean(result);
        //}



        ///// <summary>
        ///// 删除功能点
        ///// </summary>
        ///// <param name="funckey">功能点编号</param>
        ///// <returns>是否成功</returns>
        //[HttpDelete]
        //[Route("{funckey}")]
        //[ActionName("deleteFunction")]
        //private WapBoolean DeleteFunction(string funckey)
        //{
        //    bool result = Service.DeleteFunction(funckey);
        //    return new WapBoolean(result);
        //}

        ///// <summary>
        ///// 获取所有功能点
        ///// </summary>
        ///// <returns>功能点列表</returns>
        //[HttpGet]
        //[Route("functions")]
        //[ActionName("getAllFunctions")]
        //private WapCollection<WapFunctionAllDto> GetAllFunction()
        //{
        //    var result = Service.GetAllFunction();
        //    return new WapCollection<WapFunctionAllDto>(result);
        //}

        #endregion

        #region WapRoleFunctionDto
        ///// <summary>
        ///// 凭应用获取角色权限关联 
        ///// </summary>
        ///// <param name="appKey">应用编号</param>
        ///// <returns>功能点与角色关联列表</returns>
        //[HttpGet]
        //[Route("queryRelationByApp")]
        //[ActionName("getAllRoleFunctionRelationByAppKey")]
        //private WapCollection<WapRoleFunctionDto> GetAllRoleFunctionRelationByAppKey(string appKey)
        //{
        //    var result = Service.GetAllRoleFunctionRelationByAppKey(appKey);
        //    return new WapCollection<WapRoleFunctionDto>(result);
        //}

        ///// <summary>
        ///// 凭应用获取角色权限关联 
        ///// </summary>
        ///// <param name="appIdentity">应用标识</param>
        ///// <returns>功能点与角色关联列表</returns>
        //[HttpGet]
        //[Route("queryRelationByAppIdentity")]
        //[ActionName("getAllRoleFunctionRelationByAppIdentity")]
        //private WapCollection<WapRoleFunctionDto> GetAllRoleFunctionRelationByAppIdentity(string appIdentity)
        //{
        //    var result = Service.GetAllRoleFunctionRelationByAppIdentity(appIdentity);
        //    return new WapCollection<WapRoleFunctionDto>(result);
        //}

        ///// <summary>
        ///// 获取角色权限关联 
        ///// </summary>
        ///// <param name="relationKey">关联编号</param>
        ///// <returns>功能点与角色关联</returns>
        //[HttpGet]
        //[Route("queryRelation")]
        //[ActionName("getRoleFunctionRelationByKey")]
        //private WapResponse<WapRoleFunctionDto> GetRoleFunctionRelationByKey(string relationKey)
        //{
        //    var result = Service.GetRoleFunctionRelationByKey(relationKey);
        //    return new WapResponse<WapRoleFunctionDto>(result);
        //}


        ///// <summary>
        /////  凭角色和应用获取功能点
        ///// </summary>
        ///// <param name="roleKey">角色编号</param>
        ///// <param name="appIdentity">应用标识</param>
        ///// <returns>功能点与角色关联列表</returns>
        //[HttpGet]
        //[Route("roles/{roleKey}/apps/{appIdentity}/functions")]
        //[ActionName("getFunctionsByRoleAndApp2")]
        //private WapCollection<WapFuncRelativeDto> GetRoleFunctionyRoleKeyAndAppIdentity(string roleKey, string appIdentity)
        //{
        //    var result = Service.GetRoleFunctionRelationByRoleKeyAndAppIdentity(roleKey, appIdentity);
        //    return new WapCollection<WapFuncRelativeDto>(result);
        //}


        ///// <summary>
        /////更新角色和功能点的关联
        ///// </summary>
        ///// <param name="relationModel">更新对象</param>
        ///// <returns>是否成功</returns>
        //[HttpPut]
        //[Route("updateRoleFunctionRelation")]
        //[ActionName("updateRoleFunctionRelation")]
        //private WapResponse<WapUpdateRoleFuncRelationDto> UpdateRoleFunctionRelation(WapUpdateRoleFuncRelationDto relationModel)
        //{

        //    if (relationModel == null)
        //    {
        //        return new WapResponse<WapUpdateRoleFuncRelationDto>(null);
        //    }
        //    var result = Service.UpdateRoleFunctionRelation(relationModel);
        //    return new WapResponse<WapUpdateRoleFuncRelationDto>(result);
        //}


        #endregion

        #region FunctionGroupRelation
        ///// <summary>
        ///// 凭功能组获取下属功能点
        ///// </summary>
        ///// <param name="funcGroupKey">功能组编号</param>
        ///// <returns>功能点与功能点组关联列表</returns>
        //[HttpGet]
        //[Route("function/groups/{funcGroupKey}/functions")]
        //[ActionName("getFunctionGroupRelation")]
        //private WapCollection<WapFuncGroupRelativeDto> GetFunctionGroupRelation(string funcGroupKey)
        //{
        //    var result = Service.GetFunctionGroupRelation(funcGroupKey);
        //    return new WapCollection<WapFuncGroupRelativeDto>(result);
        //}

        ///// <summary>
        ///// 更新功能组和功能点的关联
        ///// </summary>
        ///// <param name="relationModel">更新对象</param>
        ///// <returns>是否成功</returns>
        //[HttpPut]
        //[Route("function/groups/relation")]
        //[ActionName("updateFunctionGroupRelation")]
        //private WapResponse<WapUpdateFuncGroupRelationDto> UpdateFunctionGroupRelation(WapUpdateFuncGroupRelationDto relationModel)
        //{

        //    if (relationModel == null)
        //    {
        //        return new WapResponse<WapUpdateFuncGroupRelationDto>(null);
        //    }
        //    var result = Service.UpdateFunctionGroupRelation(relationModel);
        //    return new WapResponse<WapUpdateFuncGroupRelationDto>(null);
        //}

        #endregion

        #region FunctionGroup

        ///// <summary>
        ///// 获取功能点组列表(原)
        ///// </summary>
        ///// <returns>功能点组列表</returns>
        //[HttpGet]
        //[Route("function/groups/queryAll")]
        //[ActionName("getAllFunctionGroup")]
        //private WapCollection<WapFunctionGroupDto> GetAllFunctionGroup()
        //{
        //    var result = Service.GetAllFunctionGroup();
        //    return new WapCollection<WapFunctionGroupDto>(result);
        //}



        ///// <summary>
        ///// 凭应用获取功能点组列表
        ///// </summary>
        ///// <param name="appkey">应用编号</param>
        ///// <returns>功能点组列表</returns>
        //[HttpGet]
        //[Route("function/groups/queryByAppKey/{appkey}")]
        //[ActionName("getAllFunctionGroupsByAppKey")]
        //private WapCollection<WapFunctionGroupDto> GetAllFunctionGroupsByAppKey(string appkey)
        //{
        //    var result = Service.GetAllFunctionGroupsByAppKey(appkey);
        //    return new WapCollection<WapFunctionGroupDto>(result);
        //}


        ///// <summary>
        ///// 启用功能点组
        ///// </summary>
        ///// <param name="funcGroupKey">功能组编号</param>
        ///// <returns>是否成功</returns>
        //[HttpPost]
        //[Route("function/groups/{funcGroupKey}/active")]
        //[ActionName("activeFunctionGroup")]
        //private WapBoolean ActiveFunctionGroup(string funcGroupKey)
        //{
        //    bool result = Service.ActiveFunctionGroup(funcGroupKey);
        //    return new WapBoolean(result);
        //}


        ///// <summary>
        /////  禁用功能点组
        ///// </summary>
        ///// <param name="funcGroupKey">功能组编号</param>
        ///// <returns>是否成功</returns>
        //[HttpPut]
        //[Route("function/groups/{funcGroupKey}/deactive")]
        //[ActionName("deactiveFunctionGroup")]
        //private WapBoolean DeactiveFunctionGroup(string funcGroupKey)
        //{
        //    bool result = Service.DeactiveFunctionGroup(funcGroupKey);
        //    return new WapBoolean(result);
        //}


        ///// <summary>
        ///// 删除功能点组
        ///// </summary>
        ///// <param name="funcGroupKey">功能组编号</param>
        ///// <returns>是否成功</returns>
        //[HttpDelete]
        //[Route("function/groups/{funcGroupKey}")]
        //[ActionName("deleteFunctionGroup")]
        //private WapBoolean DeleteFunctionGroup(string funcGroupKey)
        //{
        //    bool result = Service.DeleteFunctionGroup(funcGroupKey);
        //    return new WapBoolean(result);
        //}

        #endregion

        #endregion
    }
}

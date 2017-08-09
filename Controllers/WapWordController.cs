using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Share;
using SH3H.SDK.WebApi.Controllers;
using SH3H.SDK.WebApi.Core;
using SH3H.SDK.WebApi.Core.Models;
using SH3H.WAP.Contracts;
using SH3H.WAP.Model;
using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SH3H.WAP.Controllers
{
    /// <summary>
    /// 定义词语对象控制器
    /// </summary>
    [Resource("wapWordRes")]
    [RoutePrefix(SH3H.WAP.Share.Consts.URL_PREFIX_WAP + "/common")]
    public class WapWordController :
        BaseController<IWapWordService>
    {
        /// <summary>
        /// 获取指定词语组的词语
        /// </summary>
        /// <param name="group">分组key</param>
        /// <returns>词语集合</returns>
        [HttpGet]
        [Route("groups/{group}/words")]
        [ActionName("getWordsByGroup")]
        public WapResponse<IEnumerable<WapWordDto>> GetWordsByGroupKey(string group)
        {
            return new WapResponse<IEnumerable<WapWordDto>>(Service.GetWordsByGroupKey(group));
        }

        /// <summary>
        /// 新增词语
        /// </summary>
        /// <param name="word">词语对象</param>
        /// <returns>词语对象</returns>
        [HttpPost]
        [Route("words")]
        [ActionName("createWord")]
        public WapResponse<WapWordDto> AddWord([FromBody]WapWordDto word)
        {
            var result = Service.AddWord(word);
            return new WapResponse<WapWordDto>(result);
        }

        /// <summary>
        /// 删除词语
        /// </summary>
        /// <param name="wordId">词语编号</param>
        /// <returns>是否删除成功</returns>
        [HttpDelete]
        [Route("words/{wordId}")]
        [ActionName("deleteWord")]
        public WapBoolean RemoveWord(int wordId)
        {
            var result = Service.RemoveWord(wordId);
            return new WapBoolean(result);
        }

        /// <summary>
        ///修改词语
        /// </summary>
        /// <param name="wordId">词语编号</param>
        /// <param name="word">词语对象</param>
        /// <returns>返回修改后的词语信息</returns>
        [HttpPut]
        [Route("words/{wordId}")]
        [ActionName("updateWord")]
        public WapResponse<WapWordDto> ModifyWordById(int wordId, [FromBody]WapWordDto word)
        {
            var result = Service.ModifyWordById(wordId, word);
            return new WapResponse<WapWordDto>(result);
        }

        /// <summary>
        /// 更新词语父子关系
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <param name="parentId">词语父编号</param>
        /// <returns>是否更新成功</returns>
        [HttpPut]
        [Route("words/{parentId}/child/{id}")]
        [ActionName("updateWordRelation")]
        public WapBoolean ModifyParentId(int parentId, int id)
        {
            bool result = Service.ModifyParentId(id, parentId);
            return new WapBoolean(result);
        }

        /// <summary>
        ///  修改词语状态
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <param name="active">词语操作</param>
        /// <returns>是否更新成功</returns>
        [HttpPatch]
        [Route("words/{id}/state")]
        [ActionName("updateState")]
        public WapBoolean ModifyState(int id, [FromBody]WapCommonStateDto active)
        {
            var result = Service.ModifyStateById(id, active);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 更新词语顺序
        /// </summary>
        /// <param name="sorts">词语排序值数组</param>
        /// <returns></returns>
        [HttpPut]
        [Route("words/sort")]
        [ActionName("sortWords")]
        public WapBoolean ModifySortIndex([FromBody]Dictionary<int, int> sorts)
        {
            var result = Service.ModifySortIndexById(sorts);
            return new WapBoolean(result);
        }

        /// <summary>
        /// 获取指定词语#
        /// </summary>
        /// <param name="wordId">词语编号</param>
        /// <returns>词语对象</returns>
        [HttpGet]
        [Route("words/{wordId}")]
        [ActionName("getWordById")]
        public WapWordDto GetWord(int wordId)
        {
            return Service.GetWordById(wordId);
        }

        /// <summary>
        /// 获取指定编码的词语
        /// </summary>
        /// <param name="code">词语编码</param>
        /// <returns>词语对象</returns>
        [HttpGet]
        [Route("words")]
        [ActionName("getWordByWordCode")]
        public WapResponse<IEnumerable<WapWordDto>> GetWordByWordCode(string code)
        {
            return new WapResponse<IEnumerable<WapWordDto>>(Service.GetWordByWordCode(code));
        }

        /// <summary>
        /// 查询所有词语
        /// </summary>
        /// <returns>词语对象</returns>
        [HttpGet]
        [Route("words")]
        [ActionName("getAllWords")]
        public WapResponse<IEnumerable<WapWordDto>> GetAllWords()
        {
            return new WapResponse<IEnumerable<WapWordDto>>(Service.GetAllWords());
        }

        /// <summary>
        /// 获取指定应用下的词语
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>词语对象</returns>
        [HttpGet]
        [Route("apps/{appIdentity}/words")]
        [ActionName("getWordsByApp")]
        public WapResponse<IEnumerable<WapWordDto>> GetAllWordsByApp(string appIdentity)
        {
            return new WapResponse<IEnumerable<WapWordDto>>(Service.GetAllWordsByApp(appIdentity));
        }


        /// <summary>
        /// 获取指定应用指定词语组的词语
        /// </summary>
        /// <param name="app">应用标识</param>
        /// <param name="group">词语组ID</param>
        /// <returns>词语对象</returns>
        [HttpGet]
        [Route("words")]
        [ActionName("getWordsByAppAndGroup")]
        public WapResponse<IEnumerable<WapWordDto>> GetAllWordsByAppAndGroup(string app, string group)
        {
            return new WapResponse<IEnumerable<WapWordDto>>(Service.GetAllWordsByAppAndGroup(app, group));
        }

        /// <summary>
        /// 通过词语父级编号查询词语
        /// </summary>
        /// <param name="parentCode">词语编号</param>
        /// <returns>词语对象</returns>
        [HttpGet]
        [Route("words")]
        [ActionName("getByParentCode")]
        public IEnumerable<WapWordDto> GetByPcode(string parentCode)
        {
            return Service.GetByPcode(parentCode);
        }


        #region 过去接口
        /// <summary>
        /// 通过多个词语编码查询
        /// </summary>
        /// <param name="wordCodes">词语编码</param>
        /// <returns>词语对象</returns>
        [HttpPost]
        [Route("code")]
        [ActionName("getWordsByWordCodes")]
        private IEnumerable<WapWordDto> GetWordsByWordCodes([FromBody]IEnumerable<string> wordCodes)
        {
            return Service.GetWordsByWordCodes(wordCodes);
        }

        /// <summary>
        /// 查询所有词语结点
        /// </summary>
        /// <returns>词语结点</returns>
        [HttpGet]
        [Route("all/node")]
        [ActionName("getAllWordNodes")]
        private IEnumerable<WapWordNodeDto> GetAllWordNodes()
        {
            return Service.GetAllWordNodes();
        }

        /// <summary>
        /// 查询多个词语结点
        /// </summary>
        /// <param name="wordCodes">词语编码</param>
        /// <returns>词语结点</returns>
        [HttpPost]
        [Route("code/node")]
        [ActionName("getWordNodesByCodes")]
        private IEnumerable<WapWordNodeDto> GetWordNodesByCodes([FromBody]IEnumerable<string> wordCodes)
        {
            return Service.GetWordNodesByCodes(wordCodes);
        }

        /// <summary>
        /// 查询单个词语结点
        /// </summary>
        /// <param name="wordCode">词语编码</param>
        /// <returns>词语结点</returns>
        [HttpGet]
        [Route("code/{wordCode}/node")]
        [ActionName("getWordNodeByCode")]
        private IEnumerable<WapWordNodeDto> GetWordNodeByCode(string wordCode)
        {
            return Service.GetWordNodeByCode(wordCode);
        }

        /// <summary>
        /// 通过词语父级编号查询词语
        /// </summary>
        /// <param name="pid">词语编号</param>
        /// <returns>词语对象</returns>
        [HttpGet]
        [Route("getByPid/{pid}")]
        [ActionName("getByPid")]
        private IEnumerable<WapWordDto> GetByPid(int pid)
        {
            return Service.GetByPid(pid);
        }


        /// <summary>
        /// 通过词语父级编号查询词语
        /// </summary>
        /// <param name="code">词语父级编号</param>
        /// <param name="value">词语编号</param>
        /// <returns>词语对象</returns>
        [HttpGet]
        [Route("getByCodeAndValue/{code}/{value}")]
        [ActionName("getByCodeAndValue")]
        private WapWordDto GetByCodeAndValue(string code, string value)
        {
            return Service.GetByCodeAndValue(code, value);
        }
        #endregion


    }
}

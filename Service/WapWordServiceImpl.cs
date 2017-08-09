using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Contracts;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Model.Dto;
using SH3H.WAP.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SH3H.WAP.Service
{
    /// <summary>
    /// 词汇服务操作
    /// </summary>
    public class WapWordServiceImpl : IWapWordService
    {
        private IWapWordRepository _repo;

        public WapWordServiceImpl(IWapWordRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 根据WORD_GROUP_KEY字段获取词语集合
        /// </summary>
        /// <param name="key">分组key</param>
        /// <returns>词语集合</returns>
        public IEnumerable<WapWordDto> GetWordsByGroupKey(string key)
        {

            return _repo.GetWordsByGroupKey(key).Select(p => WapWordDto.FromModel(p)).ToList();
        }
        /// <summary>
        /// 添加词语
        /// </summary>
        /// <param name="word">词语对象</param>
        /// <returns>词语对象</returns>
        public WapWordDto AddWord(WapWordDto word)
        {
            if (word == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "词语不允许为空");
            }
            var validateResult = word.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            
            var result = _repo.Insert(word.ToModel());
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "添加词语失败");
            }

            return WapWordDto.FromModel(result);
        }

        /// <summary>
        /// 删除词语
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <returns>是否删除成功</returns>
        public bool RemoveWord(int id)
        {

            return _repo.Delete(id);
        }

        /// <summary>
        /// 通过词语编号修改词语
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <param name="word">词语对象</param>
        /// <returns>返回修改后的词语信息</returns>
        public WapWordDto ModifyWordById(int id, WapWordDto word)
        {
            if (word == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "词语不允许为空");
            }
            var validateResult = word.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            var model = _repo.Update(id, word.ToModel());
            if(model != null)
                return WapWordDto.FromModel(model);
            return null;
        }

        /// <summary>
        /// 通过词语编号查询词语
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <returns>词语对象</returns>
        public WapWordDto GetWordById(int id)
        {

            var result = _repo.Select(id);
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "通过词语编号查询词语失败");
            }

            return WapWordDto.FromModel(result);
        }

        /// <summary>
        /// 根据词语编码查找
        /// </summary>
        /// <param name="wordCodes">词语编码集合</param>
        /// <returns>词语集合</returns>
        public IEnumerable<WapWordDto> GetWordsByWordCodes(IEnumerable<string> wordCodes)
        {

            var result = _repo.SelectByWordCodes(wordCodes);
            return result.Select(p => WapWordDto.FromModel(p)).ToList(); ;
        }

        /// <summary>
        /// 根据词语编码查找
        /// </summary>
        /// <param name="wordCode">词语编码集合</param>
        /// <returns>单个词语集合</returns>
        public IEnumerable<WapWordDto> GetWordByWordCode(string wordCode)
        {
            IEnumerable<string> code = new List<string> { wordCode };

            var result = _repo.SelectByWordCodes(code);
            return result.Select(p => WapWordDto.FromModel(p)).ToList(); ;
        }

        /// <summary>
        /// 根据词语编码查找
        /// </summary>
        /// <returns>所有词语集合</returns>
        public IEnumerable<WapWordDto> GetAllWords()
        {
            IEnumerable<string> wordCode = new List<string> { };

            var result = _repo.SelectByWordCodes(wordCode);
            return result.Select(p => WapWordDto.FromModel(p)).ToList(); ;
        }

        /// <summary>
        /// 查询所有词语结点
        /// </summary>
        /// <returns>词语结点对象</returns>
        public IEnumerable<WapWordNodeDto> GetAllWordNodes()
        {
            IEnumerable<WapWordDto> words = GetAllWords();
            return WapWordNodeDto.Create(words).Where(a => a.ParentId == 0).ToList();
        }

        /// <summary>
        /// 查询多个词语结点
        /// </summary>
        /// <param name="wordCodes">词语编码</param>
        /// <returns>词语结点对象</returns>
        public IEnumerable<WapWordNodeDto> GetWordNodesByCodes(IEnumerable<string> wordCodes)
        {
            IEnumerable<WapWordDto> words = GetWordsByWordCodes(wordCodes);
            return WapWordNodeDto.Create(words);
        }

        /// <summary>
        /// 查询单个词语结点
        /// </summary>
        /// <param name="wordCode"></param>
        /// <returns></returns>
        public IEnumerable<WapWordNodeDto> GetWordNodeByCode(string wordCode)
        {
            IEnumerable<WapWordDto> words = GetAllWords();
            var parent = words.Where(a => a.WordCode == wordCode).FirstOrDefault();
            if (parent == null)
            {
                return new List<WapWordNodeDto>();
            }
            var chlidren = words.Where(a => a.ParentId == parent.WordId).ToList();
            chlidren.Add(parent);
            return WapWordNodeDto.Create(chlidren);
        }

        /// <summary>
        /// 根据词语编号改词语父编号
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <param name="parentId">词语父编号</param>
        /// <returns>是否更新成功</returns>
        public bool ModifyParentId(int id, int parentId)
        {

            return _repo.UpdateParentId(id, parentId);
        }

        /// <summary>
        /// 更新词语状态
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <param name="state">词语操作</param>
        /// <returns>是否更新成功</returns>
        public bool ModifyStateById(int id, WapCommonStateDto state)
        {
            CheckNull<WapCommonStateDto>(state, "状态");

            return _repo.UpdateStateById(id, state.Active ? 1 : 0);
        }

        /// <summary>
        /// 根据词语编号更新词语排序值
        /// </summary>
        /// <param name="sortIndexes">词语排序值数组</param>
        /// <returns></returns>
        public bool ModifySortIndexById(Dictionary<int, int> sortIndexes)
        {

            return _repo.UpdateSortIndexById(sortIndexes);
        }



        /// <summary>
        /// 通过词语父级编号查询词语
        /// </summary>
        /// <param name="pid">词语编号</param>
        /// <returns>词语对象</returns>
        public IEnumerable<WapWordDto> GetByPid(int pid)
        {

            return _repo.SelectByPerentId(pid).Select(p => WapWordDto.FromModel(p));
        }

        /// <summary>
        /// 通过词语父级编号查询词语
        /// </summary>
        /// <param name="pcode">词语编号</param>
        /// <returns>词语对象</returns>
        public IEnumerable<WapWordDto> GetByPcode(string pcode)
        {

            return _repo.SelectByPerentCode(pcode).Select(p => WapWordDto.FromModel(p)).ToList().ToList();
        }

        /// <summary>
        /// 通过词语父级编号查询词语
        /// </summary>
        /// <param name="code">词语父级编号</param>
        /// <param name="value">词语编号</param>
        /// <returns>词语对象</returns>
        public WapWordDto GetByCodeAndValue(string code, string value)
        {

            var result = _repo.SelectByCodeAndValue(code, value);
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "通过词语父级编号查询词语失败");
            }

            return WapWordDto.FromModel(result);
        }


        /// <summary>
        /// 获取指定应用下的词语
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>词语对象</returns>
        public IEnumerable<WapWordDto> GetAllWordsByApp(string appIdentity)
        {
            return _repo.GetAllWordsByApp(appIdentity).Select(p => WapWordDto.FromModel(p)).ToList();
        }


        /// <summary>
        /// 获取指定应用指定词语组的词语
        /// </summary>
        /// <param name="app">应用标识</param>
        /// <param name="group">词语组ID</param>
        /// <returns>词语对象</returns>
        public IEnumerable<WapWordDto> GetAllWordsByAppAndGroup(string app, string group)
        {
            return _repo.GetAllWordsByAppAndGroup(app, group).Select(p => WapWordDto.FromModel(p)).ToList();
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

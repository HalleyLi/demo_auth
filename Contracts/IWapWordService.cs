using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Contracts
{
    /// <summary>
    /// 词汇
    /// </summary>
    public interface IWapWordService
    {
        /// <summary>
        /// 根据WORD_GROUP_KEY字段获取词语集合
        /// </summary>
        /// <param name="key">分组key</param>
        /// <returns>词语集合</returns>
        IEnumerable<WapWordDto> GetWordsByGroupKey(string key);

        /// <summary>
        /// 添加词语
        /// </summary>
        /// <param name="word">词语对象</param>
        WapWordDto AddWord(WapWordDto word);

        /// <summary>
        /// 删除词语
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <returns>是否删除成功</returns>
        bool RemoveWord(int id);

        /// <summary>
        /// 通过词语编号修改词语
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <param name="word">词语对象</param>
        /// <returns>返回修改后的词语信息</returns>
        WapWordDto ModifyWordById(int id, WapWordDto word);

        /// <summary>
        /// 通过词语编号查询词语
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <returns>词语对象</returns>
        WapWordDto GetWordById(int id);

        /// <summary>
        /// 根据词语编码查找
        /// </summary>
        /// <param name="wordCodes">词语编码集合</param>
        /// <returns>词语集合</returns>
        IEnumerable<WapWordDto> GetWordsByWordCodes(IEnumerable<string> wordCodes);

        /// <summary>
        /// 根据词语编码查找
        /// </summary>
        /// <param name="wordCode">词语编码</param>
        /// <returns>单个词语集合</returns>
        IEnumerable<WapWordDto> GetWordByWordCode(string wordCode);

        /// <summary>
        /// 根据词语编码查找
        /// </summary>
        /// <returns>所有词语集合</returns>
        IEnumerable<WapWordDto> GetAllWords();

        /// <summary>
        /// 查询所有词语结点
        /// </summary>
        /// <returns>词语结点对象</returns>
        IEnumerable<WapWordNodeDto> GetAllWordNodes();

        /// <summary>
        /// 查询多个词语结点
        /// </summary>
        /// <param name="wordCodes">词语编码</param>
        /// <returns>词语结点对象</returns>
        IEnumerable<WapWordNodeDto> GetWordNodesByCodes(IEnumerable<string> wordCodes);

        /// <summary>
        /// 查询单个词语结点
        /// </summary>
        /// <param name="wordCode"></param>
        /// <returns></returns>
        IEnumerable<WapWordNodeDto> GetWordNodeByCode(string wordCode);

        /// <summary>
        /// 根据词语编号改词语父编号
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <param name="parentId">词语父编号</param>
        /// <returns>是否更新成功</returns>
        bool ModifyParentId(int id, int parentId);

        /// <summary>
        /// 更新词语状态
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <param name="state">词语操作</param>
        /// <returns>是否更新成功</returns>
        bool ModifyStateById(int id, WapCommonStateDto state);

        /// <summary>
        /// 根据词语编号更新词语排序值
        /// </summary>
        /// <param name="sortIndexes">词语排序值数组</param>
        /// <returns></returns>
        bool ModifySortIndexById(Dictionary<int, int> sortIndexes);

        /// <summary>
        /// 通过词语父级编号查询词语
        /// </summary>
        /// <param name="pid">词语编号</param>
        /// <returns>词语对象</returns>
        IEnumerable<WapWordDto> GetByPid(int pid);

        /// <summary>
        /// 通过词语父级编号查询词语
        /// </summary>
        /// <param name="pcode">词语编号</param>
        /// <returns>词语对象</returns>
        IEnumerable<WapWordDto> GetByPcode(string pcode);

        /// <summary>
        /// 通过词语父级编号查询词语
        /// </summary>
        /// <param name="code">词语父级编号</param>
        /// <param name="value">词语编号</param>
        /// <returns>词语对象</returns>
        WapWordDto GetByCodeAndValue(string code, string value);


        /// <summary>
        /// 获取指定应用下的词语
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>词语对象</returns>
        IEnumerable<WapWordDto> GetAllWordsByApp(string appIdentity);


        /// <summary>
        /// 获取指定应用指定词语组的词语
        /// </summary>
        /// <param name="app">应用标识</param>
        /// <param name="group">词语组ID</param>
        /// <returns>词语对象</returns>
        IEnumerable<WapWordDto> GetAllWordsByAppAndGroup(string app, string group);
    }
}

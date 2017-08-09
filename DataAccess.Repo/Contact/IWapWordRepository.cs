using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SH3H.WAP.Model;
using SH3H.SDK.DataAccess.Core;

using System.Data.Common;

namespace SH3H.WAP.DataAccess.Repo.Contact
{
    /// <summary>
    /// 定义词语操作接口
    /// </summary>
    public interface IWapWordRepository
    {

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        WapWord Select(int key);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        WapWord Insert(WapWord entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        WapWord Update(int key, WapWord entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        bool Delete(int key);

        /// <summary>
        /// 根据WORD_GROUP_KEY字段获取词语集合
        /// </summary>
        /// <param name="key">分组key</param>
        /// <returns>词语集合</returns>
        IEnumerable<WapWord> GetWordsByGroupKey(string key);

        /// <summary>
        /// 根据词语编码查找
        /// </summary>
        /// <param name="wordCodes">词语编码集合</param>
        /// <returns>词语集合</returns>
        IEnumerable<WapWord> SelectByWordCodes(IEnumerable<string> wordCodes);

        /// <summary>
        /// 根据词语编号改词语父编号
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <param name="parentId">词语父编号</param>
        bool UpdateParentId(int id, int parentId);

        /// <summary>
        /// 更新词语状态
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <param name="state">词语操作</param>
        /// <returns>是否更新成功</returns>
        bool UpdateStateById(int id, int state);

        /// <summary>
        /// 通过词语父级编号查询词语
        /// </summary>
        /// <param name="pid">词语父级编号</param>
        /// <returns>词语对象</returns>
        IEnumerable<WapWord> SelectByPerentId(int pid);

       /// <summary>
       /// 通过词语父级查询词语
       /// </summary>
       /// <param name="pcode">词语父级编号</param>
       /// <returns>词语对象</returns>
        IEnumerable<WapWord> SelectByPerentCode(string pcode);

        /// <summary>
        /// 通过词语父级编号查询词语
        /// </summary>
        /// <param name="code">词语父级编号</param>
        /// <param name="value">词语编号</param>
        /// <returns>词语对象</returns>
        WapWord SelectByCodeAndValue(string code, string value);

        /// <summary>
        /// 根据词语编号更新词语排序值
        /// </summary>
        /// <param name="sortIndexes">词语编号和排序值字典</param>
        /// <returns>是否更新成功</returns>
        bool UpdateSortIndexById(Dictionary<int, int> sortIndexes);



        /// <summary>
        /// 获取指定应用下的词语
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>词语对象</returns>
        IEnumerable<WapWord> GetAllWordsByApp(string appIdentity);

        /// <summary>
        /// 获取指定应用指定词语组的词语
        /// </summary>
        /// <param name="app">应用标识</param>
        /// <param name="group">词语组ID</param>
        /// <returns>词语对象</returns>
        IEnumerable<WapWord> GetAllWordsByAppAndGroup(string app, string group);
    }
}

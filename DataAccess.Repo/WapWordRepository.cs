using SH3H.SDK.DataAccess.Repo;
using SH3H.WAP.DataAccess;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SH3H.WAP.DataAccess.Repo
{
    /// <summary>
    /// Word数据仓库
    /// </summary>
   public class WapWordRepository
       : Repository<IWapWordStorage>
        , IWapWordRepository
    {
        /// <summary>
        /// 根据WORD_GROUP_KEY字段获取词语集合
        /// </summary>
        /// <param name="key">分组key</param>
        /// <returns>词语集合</returns>
       public IEnumerable<WapWord> GetWordsByGroupKey(string key)
       {
           return Storage.GetWordsByGroupKey(key);
       }
       /// <summary>
       /// 添加词语操作
       /// </summary>
       /// <param name="word">词语对象</param>
       /// <returns>词语对象</returns>
      public WapWord Insert(WapWord word) 
       {
           return Storage.Insert(word);
       }

       /// <summary>
       /// 删除词语
       /// </summary>
       /// <param name="id">词语编号</param>
       /// <returns>是否删除成功</returns>
      public bool Delete(int id)
       {
           return Storage.Delete(id);
       }

       /// <summary>
       /// 修改词语
       /// </summary>
       /// <param name="id">词语编号</param>
       /// <param name="word">词语对象</param>
       /// <returns>返回修改后的词语信息</returns>
       public WapWord Update(int id, WapWord word)
       {
           return Storage.Update(id,word);
       }

       /// <summary>
       /// 查询词语对象
       /// </summary>
       /// <param name="id">词语编号</param>
       /// <returns>词语对象</returns>
       public WapWord Select(int id)
       {
           return Storage.Select(id);
       }

       /// <summary>
        /// 根据词语编码查找
        /// </summary>
       /// <param name="wordCodes">词语编码集合</param>
        /// <returns>词语集合</returns>
       public IEnumerable<WapWord> SelectByWordCodes(IEnumerable<string> wordCodes)
       {
           return Storage.SelectByWordCodes(wordCodes);
       }

        /// <summary>
        /// 根据词语编号改词语父编号
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <param name="parentId">词语父编号</param>
        /// <returns>是否更新成功</returns>
       public bool UpdateParentId(int id, int parentId)
       {
           return Storage.UpdateParentId(id,parentId);
       }

        /// <summary>
        /// 根据词语父编号更新词语
        /// </summary>
        /// <param name="id">词语编号</param>
        /// <param name="state">词语操作</param>
        /// <returns>是否更新成功</returns>
       public bool UpdateStateById(int id, int state)
       {
           return Storage.UpdateStateById(id,state);
       }

        /// <summary>
        /// 根据词语编号更新词语排序值
        /// </summary>
        /// <param name="sortIndexes">词语排序值数组</param>
        /// <returns>是否更新成功</returns>
       public bool UpdateSortIndexById(Dictionary<int, int> sortIndexes)
       {
           return Storage.UpdateSortIndexById(sortIndexes);
       }

       
        /// <summary>
        /// 通过词语父级编号查询词语
        /// </summary>
        /// <param name="pid">词语父级编号</param>
        /// <returns>词语对象</returns>
       public IEnumerable<WapWord> SelectByPerentId(int pid)
       {
           return Storage.SelectByPerentId(pid);
       }

       /// <summary>
       /// 通过词语父级查询词语
       /// </summary>
       /// <param name="pcode">词语父级编号</param>
       /// <returns>词语对象</returns>
       public IEnumerable<WapWord> SelectByPerentCode(string pcode)
       {
           return Storage.SelectByPerentCode(pcode);
       }

       /// <summary>
       /// 通过词语父级编号查询词语
       /// </summary>
       /// <param name="code">词语父级编号</param>
       /// <param name="value">词语编号</param>
       /// <returns>词语对象</returns>
       public WapWord SelectByCodeAndValue(string code, string value)
       {
           return Storage.SelectByCodeAndValue(code, value);
       }


       /// <summary>
       /// 获取指定应用下的词语
       /// </summary>
       /// <param name="appIdentity">应用标识</param>
       /// <returns>词语对象</returns>

       public IEnumerable<WapWord> GetAllWordsByApp(string appIdentity)
       {
           return Storage.GetAllWordsByApp(appIdentity);
       }

       /// <summary>
       /// 获取指定应用指定词语组的词语
       /// </summary>
       /// <param name="app">应用标识</param>
       /// <param name="group">词语组ID</param>
       /// <returns>词语对象</returns>
       public IEnumerable<WapWord> GetAllWordsByAppAndGroup(string app, string group)
       {
           return Storage.GetAllWordsByAppAndGroup(app, group);
       }
    }
}

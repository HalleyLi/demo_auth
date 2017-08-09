using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SH3H.SDK.DataAccess.Core;

using SH3H.WAP.Model;
using System.Data.Common;

namespace SH3H.WAP.DataAccess.Repo.Contact
{
    /// <summary>
    /// 定义口径操作接口
    /// </summary>
    public interface IWapCaliberRepository
    {
        /// <summary>
        /// 获取所有口径
        /// </summary>
        /// <returns>口径对象列表</returns>
       IEnumerable<WapCaliber> SelectAll();


       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="key"></param>
       /// <returns></returns>
       WapCaliber Select(int key);

       /// <summary>
       /// 新增
       /// </summary>
       /// <param name="entity"></param>
       /// <param name="trans"></param>
       /// <returns></returns>
       WapCaliber Insert(WapCaliber entity);

       /// <summary>
       /// 修改
       /// </summary>
       /// <param name="key"></param>
       /// <param name="entity"></param>
       /// <param name="trans"></param>
       /// <returns></returns>
       WapCaliber Update(int key, WapCaliber entity);

       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="key"></param>
       /// <param name="trans"></param>
       /// <returns></returns>
       bool Delete(int key);

    }
}

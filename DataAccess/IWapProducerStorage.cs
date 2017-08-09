using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SH3H.WAP.Model;
using SH3H.SDK.DataAccess.Core;
using SH3H.SDK.DataAccess.Db.Core;
using System.Data.Common;

namespace SH3H.WAP.DataAccess
{
    /// <summary>
    /// 定义生产商操作接口
    /// </summary>
   public interface IWapProducerStorage
    {

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        WapProducer Select(int key);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        WapProducer Insert(WapProducer entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        bool Update(int key, WapProducer entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        bool Delete(int key);

        /// <summary>
        /// 获取所有生产商
        /// </summary>
        /// <returns>生产商对象列表</returns>
       IEnumerable<WapProducer> SelectAll();

    }
}

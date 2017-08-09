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
    /// 定义水表型号操作接口
    /// </summary>
    public interface IWapModelRepository
    {

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        WapModel Select(int key);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        WapModel Insert(WapModel entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        bool Update(int key, WapModel entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        bool Delete(int key);

        /// <summary>
        /// 获取所有水表型号
        /// </summary>
        /// <returns>水表型号对象列表</returns>
       IEnumerable<WapModel> SelectAll();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SH3H.SDK.DataAccess.Repo;
using SH3H.WAP.DataAccess;
using SH3H.WAP.Model;
using SH3H.WAP.DataAccess.Repo.Contact;

namespace SH3H.WAP.DataAccess.Repo
{
    /// <summary>
    /// Model数据仓库
    /// </summary>
   public class WapModelRepository
       : Repository<IWapModelStorage>
        , IWapModelRepository
    {
       /// <summary>
       /// 添加水表型号操作
       /// </summary>
       /// <param name="model">水表型号对象</param>
       /// <returns>水表型号对象</returns>
       public WapModel Insert(WapModel model) 
       {
           return Storage.Insert(model);
       }

       /// <summary>
       /// 删除水表型号
       /// </summary>
       /// <param name="id">水表型号编号</param>
       /// <returns>是否删除成功</returns>
       public bool Delete(int id)
       {
           return Storage.Delete(id);
       }

       /// <summary>
       /// 修改水表型号
       /// </summary>
       /// <param name="id">水表型号编号</param>
       /// <param name="model">水表型号对象</param>
       /// <returns>是否修改成功</returns>
       public bool Update(int id, WapModel model)
       {
           return Storage.Update(id, model);
       }

       /// <summary>
       /// 查询水表型号对象
       /// </summary>
       /// <param name="id">水表型号编号</param>
       /// <returns>水表型号对象</returns>
       public WapModel Select(int id)
       {
           return Storage.Select(id);
       }

       /// <summary>
        /// 查询所有水表型号
        /// </summary>
        /// <returns>水表型号列表</returns>
       public IEnumerable<WapModel> SelectAll()
       {
           return Storage.SelectAll();
       }

    }
}

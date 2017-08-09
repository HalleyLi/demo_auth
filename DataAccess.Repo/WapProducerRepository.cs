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
    /// Producer数据仓库
    /// </summary>
   public class WapProducerRepository
       : Repository<IWapProducerStorage>
        , IWapProducerRepository
    {
       /// <summary>
       /// 添加水表厂商操作
       /// </summary>
       /// <param name="producer">水表厂商对象</param>
       /// <returns>水表厂商对象</returns>
       public WapProducer Insert(WapProducer producer) 
       {
           return Storage.Insert(producer);
       }

       /// <summary>
       /// 删除水表厂商
       /// </summary>
       /// <param name="id">水表厂商编号</param>
       /// <returns>是否删除成功</returns>
       public bool Delete(int id)
       {
           return Storage.Delete(id);
       }

       /// <summary>
       /// 修改水表厂商
       /// </summary>
       /// <param name="id">水表厂商编号</param>
       /// <param name="producer">水表厂商对象</param>
       /// <returns>是否修改成功</returns>
       public bool Update(int id, WapProducer producer)
       {
           return Storage.Update(id, producer);
       }

       /// <summary>
       /// 查询水表厂商对象
       /// </summary>
       /// <param name="id">水表厂商编号</param>
       /// <returns>水表厂商对象</returns>
       public WapProducer Select(int id)
       {
           return Storage.Select(id);
       }

       /// <summary>
        /// 查询所有水表厂商
        /// </summary>
        /// <returns>水表厂商列表</returns>
       public IEnumerable<WapProducer> SelectAll()
       {
           return Storage.SelectAll();
       }

    }
}

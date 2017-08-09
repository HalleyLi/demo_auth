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
    /// Caliber数据仓库
    /// </summary>
    public class WapCaliberRepository
        : Repository<IWapCaliberStorage>
         , IWapCaliberRepository
    {
       /// <summary>
       /// 添加口径操作
       /// </summary>
       /// <param name="caliber">口径对象</param>
       /// <returns>口径对象</returns>
        public WapCaliber Insert(WapCaliber caliber) 
       {
           return Storage.Insert(caliber);
       }

       /// <summary>
       /// 删除口径
       /// </summary>
       /// <param name="id">口径编号</param>
       /// <returns>是否删除成功</returns>
        public bool Delete(int id)
       {
           return Storage.Delete(id);
       }

       /// <summary>
       /// 修改口径
       /// </summary>
       /// <param name="id">口径编号</param>
       /// <param name="caliber">口径对象</param>
       /// <returns>是否修改成功</returns>
        public WapCaliber Update(int id, WapCaliber caliber)
       {
           return Storage.Update(id, caliber);
       }

       /// <summary>
       /// 查询口径对象
       /// </summary>
       /// <param name="id">口径编号</param>
       /// <returns>口径对象</returns>
        public WapCaliber Select(int id)
       {
           return Storage.Select(id);
       }

       /// <summary>
        /// 查询所有口径
        /// </summary>
        /// <returns>口径列表</returns>
        public IEnumerable<WapCaliber> SelectAll()
       {
           return Storage.SelectAll();
       }

    }
}

using SH3H.SDK.DataAccess.Repo;
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
    /// 量程Repository
    /// </summary>
    public class WapMeterRangeRepository : Repository<IWapMeterRangeStorage>, IWapMeterRangeRepository
    {

        /// <summary>
        /// 新增量程
        /// </summary>
        /// <param name="range">量程对象</param>
        /// <returns>量程对象</returns>
        public WapMeterRange AddRange(WapMeterRange range)
        {
            return Storage.AddRange(range);
        }

        /// <summary>
        /// 修改量程
        /// </summary>
        /// <param name="id">量程id</param>
        /// <param name="range">量程对象</param>
        /// <returns>量程对象</returns>
        public WapMeterRange UpdateRange(int id, WapMeterRange range)
        {
            return Storage.UpdateRange(id, range);
        }

        /// <summary>
        /// 获取所有的量程列表
        /// </summary>
        /// <returns>所有的量程列表</returns>
        public IEnumerable<WapMeterRange> SelectAllRanges()
        {
            return Storage.SelectAllRanges();
        }

        /// <summary>
        /// 通过id获取指定量程
        /// </summary>
        /// <param name="id">量程id</param>
        /// <returns>量程对象</returns>
        public WapMeterRange GetRangeById(int id)
        {
            return Storage.GetRangeById(id);
        }

        /// <summary>
        /// 删除量程
        /// </summary>
        /// <param name="id">量程id</param>
        /// <returns>是否成功</returns>
        public bool DeleteRange(int id)
        {
            return Storage.DeleteRange(id);
        }


    }
}

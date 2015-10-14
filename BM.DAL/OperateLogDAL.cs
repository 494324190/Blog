using System;
using BM.Models;

namespace BM.DAL
{
    /// <summary>
    /// 操作
    /// </summary>
    public class OperateLogDAL:BaseRepository<tb_OperateLog>
    {
        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="ol">操作实体</param>
        public void Save(Models.tb_OperateLog ol)
        {
            try
            {
                using (ce = new BlogEntities())
                {
                    ce.Set<tb_OperateLog>().Add(ol);
                    ce.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

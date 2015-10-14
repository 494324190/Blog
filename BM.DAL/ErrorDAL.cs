using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BM.Models;

namespace BM.DAL
{
    /// <summary>
    /// 错误日志
    /// </summary>
    public class ErrorDAL:BaseRepository<tb_ErrorLog>
    {
        /// <summary>
        /// 保存错误
        /// </summary>
        /// <param name="er">错误实体</param>
        public void Save(Models.tb_ErrorLog er)
        {
            try
            {
                using (ce = new BlogEntities())
                {
                    ce.Set<tb_ErrorLog>().Add(er);
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

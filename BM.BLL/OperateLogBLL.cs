using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BM.DAL;

namespace BM.BLL
{
    /// <summary>
    /// 操作，不进行注入
    /// </summary>
    public class OperateLogBLL
    {
        private OperateLogDAL olDal = new OperateLogDAL();
        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="ol"></param>
        public void Save(Models.tb_OperateLog ol)
        {
            olDal.Save(ol);
        }
    }
}

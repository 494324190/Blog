using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BM.Models;
using BM.DAL;

namespace BM.BLL
{
    /// <summary>
    /// 不进行注入
    /// </summary>
    public class ErrorLogBLL
    {
        private ErrorDAL errorDal = new ErrorDAL();
        public void Save(tb_ErrorLog er)
        {
            errorDal.Save(er);
        }
    }
}

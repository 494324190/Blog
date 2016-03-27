using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BM.Models;
using BM.DAL;

namespace BM.BLL
{
    public class view_HotReadBLL
    {
        view_HotReadDAL view_HotReadDal = new view_HotReadDAL();
        /// <summary>
        /// 查询热门点击列表
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageTatol">总页数</param>
        /// <param name="nextPage">下一页（默认值1）</param>
        /// <param name="pageRowCount">每页行数（默认值10）</param>
        public List<view_HotRead> PageByWhere(Func<view_HotRead, bool> where, Func<view_HotRead, object> order, out int pageTatol, int nextPage = 1, int pageRowCount = 10)
        {
            return view_HotReadDal.PageByWhere(where, order, out pageTatol, nextPage, pageRowCount);
        }
    }
}

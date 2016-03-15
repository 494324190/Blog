using BM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BM.DAL;

namespace BM.BLL
{
    public class ArticleBLL
    {
        ArticleDAL articleDal = new ArticleDAL();
        /// <summary>
        /// 根据条件分页
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageTatol">总页数</param>
        /// <param name="nextPage">下一页（默认值1）</param>
        /// <param name="pageRowCount">每页行数（默认值10）</param>
        /// <returns>list</returns>
        public List<tb_Article> pageByWhere(Func<tb_Article, bool> where, Func<tb_Article, object> order, out int pageTatol, int nextPage = 1, int pageRowCount = 10)
        {
            try
            {
                if (where != null)
                {
                    return articleDal.pageByWhere(where, nextPage, pageRowCount, order, out pageTatol);
                }
                else
                {
                    pageTatol = 0;
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

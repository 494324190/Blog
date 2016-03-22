using BM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BM.DAL
{
    public class ArticleDAL : BaseRepository<view_Article>
    {
        /// <summary>
        /// 根据条件分页
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="nextPage">下一页</param>
        /// <param name="pageRowCount">每页行数</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageTatol">总页数</param>
        /// <returns>list</returns>
        public List<view_Article> pageByWhere(Func<view_Article, bool> where, int nextPage, int pageRowCount, Func<view_Article, object> order, out int pageTatol)
        {
            try
            {
                IQueryable<view_Article> modelList = Getlist(where, order).AsQueryable<view_Article>();
                if (modelList.Count() % pageRowCount != 0)
                {
                    pageTatol = (modelList.Count() / pageRowCount) + 1;
                }
                else
                {
                    pageTatol = modelList.Count() / pageRowCount;
                }
                return modelList.Skip((nextPage - 1) * pageRowCount).Take(pageRowCount).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

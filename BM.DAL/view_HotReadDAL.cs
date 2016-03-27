using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BM.Models;

namespace BM.DAL
{
    public class view_HotReadDAL:BaseRepository<view_HotRead>
    {
        /// <summary>
        /// 查询热门点击列表
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageTatol">总页数</param>
        /// <param name="nextPage">下一页（默认值1）</param>
        /// <param name="pageRowCount">每页行数（默认值10）</param>
        public List<view_HotRead> PageByWhere(Func<view_HotRead, bool> where, Func<view_HotRead, object> order, out int pageTatol, int nextPage, int pageRowCount)
        {
            try
            {
                IQueryable<view_HotRead> modelList = Getlist(where, order).AsQueryable<view_HotRead>();
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

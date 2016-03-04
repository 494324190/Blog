using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// 扩展HtmlHelper
    /// </summary>
    public static class HtmlHelper_Extend
    {

        /// <summary>
        /// 分页控件
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="pageCount">总页码</param>
        /// <param name="action">请求Action</param>
        /// <returns></returns>
        public static HtmlString HtmlHelper_Page(this HtmlHelper helper, int pageCount, string action)
        {
            string html = "<ul style='list-style:none;'><li style='float:left;margin-left:10px;'><a href='" + action + "1'><span>首页</span></a></li>";

            for (int i = 0; i < pageCount; i++)
            {
                html = html + "<li style='float:left;margin-left:10px;'>" +
                    "<a href='"+action+""+ (i + 1) + "'><span>"+(i+1)+"</span></a>"+
                    "</li>";
            }
            html = html + "<li  style='float:left;margin-left:10px;'><a href='" + action + "" + pageCount  + "'><span>末页</span></a></li></ul>";
            return new HtmlString(html);
        }

    }
}
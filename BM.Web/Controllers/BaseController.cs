using BM.IBLL;
using BM.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BM.BLL;
using BM.Web.Models;

namespace BM.Web.Controllers
{
    public class BaseController:Controller
    {
        ArticleBLL articleBll = new ArticleBLL();
        public BaseController()
        {
            int pageTatol = 0;
            List<view_Article> recommendList = articleBll.pageByWhere(p => p.Id != "", p => p.LikeNum, out pageTatol, 1, 9);
            ViewData["recommend"] = recommendList;
        }
    }
}
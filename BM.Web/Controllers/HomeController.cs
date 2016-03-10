using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BM.Common.StringOperate;
using BM.IBLL;
using BM.Models;
using Microsoft.Practices.Unity;

namespace BM.Web.Controllers
{
    public class HomeController : BaseController
    {
        [Dependency]
        public ICommonBLL<tb_Article> ArticleBll { get; set; }
        [Dependency]
        public ICommonBLL<tb_ArticleClassification> ArticleClassificationBll { get; set; }
        public ActionResult Index()
        {
            int pageTatol = 0;
            List<tb_Article> articleList = ArticleBll.pageByWhere(p => p.Id != "", p => p.Date, out pageTatol, 1, 10);
            return View(articleList);
        }

        public int Like(string id)
        {
            tb_Article articleModel = new tb_Article();
            articleModel=ArticleBll.getModel(p=>p.Id==id);
            articleModel.LikeNum++;
            return ArticleBll.Edit(articleModel)?1:0;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
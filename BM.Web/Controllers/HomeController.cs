using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return View();
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
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public string ArticleList()
        {
            try
            {
                List<tb_Article> articleList = ArticleBll.pageByWhere(p=>p.Id!="", p => p.Date, 0, 20);
                string html = "";
                for (int i = 0; i < articleList.Count; i++)
                {
                    html = html + "<div class='form-group'>" +
                            "<h2 class='col-md-12'>" + articleList[i].Title +
                            "</h2>" +
                            "</div>" +
                            "<div class='form-group'>" +
                            "<label class='col-md-12 control-label label_tip' >" + articleList[i].Date + "</label>" +
                            "</div>" +
                            "<div class='form-group' > " +
                            "<label class='col-md-12 control-label label_content' > " + (articleList[i].Content.Count() > 100 ? articleList[i].Content.Substring(0, 100) : articleList[i].Content) +
                            "</label>" +
                            "</div>";
                }
                return html;
            }
            catch (Exception e)
            {
                return "加载失败！错误信息："+e.Message;
            }
        }
    }
}
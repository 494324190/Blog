using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BM.BLL;
using BM.IBLL;
using BM.Models;
using Microsoft.Practices.Unity;

namespace BM.Web.Controllers
{
    /// <summary>
    /// 文章
    /// </summary>
    public class ArticleController : BaseController
    {
        [Dependency]
        public ICommonBLL<tb_Article> ArticleBll { get; set; }
        [Dependency]
        public ICommonBLL<tb_ArticleClassification> ArticleClassificationBll { get; set; }
        /// <summary>
        /// 文章列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public string ArticleList(int page)
        {
            try
            {
                List<tb_Article> articleList = ArticleBll.pageByWhere(p => p.Id != "",p=>p.Date, page, 10);
                string html = "";
                for (int i = 0; i < articleList.Count; i++)
                {
                    html = html + "<div class='form-group'>" +
                            "<label class='col-md-12 control-label label_title'>" + articleList[i].Title +
                            "</label>" +
                            "</div>" +
                            "<div class='form-group'>" +
                            "<label class='col-md-12 control-label label_tip' >"+ articleList[i].Date+ "</label>" +
                            "</div>" +
                            "<div class='form-group' > " +
                            "<label class='col-md-12 control-label label_content' > " + (articleList[i].Content.Count()>100? articleList[i].Content.Substring(0,100): articleList[i].Content) +
                            "</label>" +
                            "</div>";
                }
                return html;
            }
            catch (Exception e)
            {
                return "加载失败！";
            }
        }
        /// <summary>
        /// 保存文章页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Save()
        {
            Func<tb_ArticleClassification, bool> whereFunc = p => p.Id != "";
            List<tb_ArticleClassification> articleClassificationList = ArticleClassificationBll.getList(whereFunc);
            ViewData["Classification"] = new SelectList(articleClassificationList, "Id", "Name"); ;
            return View();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public int Save(tb_Article article)
        {
            try
            {
                if (article != null)
                {
                    article.Id = Guid.NewGuid().ToString();
                    article.Date = DateTime.Now;
                    if (ArticleBll.Save(article))
                    {
                        return 1;
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public int SaveAryicle(string aryicleName)
        {
            try
            {
                BM.Models.tb_ArticleClassification articleClassificationModel = new tb_ArticleClassification();
                articleClassificationModel.Id = Guid.NewGuid().ToString();
                articleClassificationModel.Name = aryicleName;
                return ArticleClassificationBll.Save(articleClassificationModel) ? 1 : 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ArticleClass()
        {
            try
            {
                List<tb_ArticleClassification> articleClassificationsList =
                    ArticleClassificationBll.getList(p => p.Id != "");
                string html = "";
                for (int i = 0; i < articleClassificationsList.Count; i++)
                {
                    html = html + "<ul class='ul_menu'>"+
                            "<li>"+
                            "<a href='javascript:void(0)'>"+articleClassificationsList[i].Name+"</a>"+
                            "</li>"+
                            "</ul>";
                }
                return html;
            }
            catch (Exception e)
            {
                return "加载出错！";
            }
        }
    }
}
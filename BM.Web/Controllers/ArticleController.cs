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
    }
}
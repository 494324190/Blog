using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BM.BLL;
using BM.Common.StringOperate;
using BM.IBLL;
using BM.Models;
using Microsoft.Practices.Unity;
using BM.Web.Filter;

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
        public ActionResult Index(string articleClassificationId, int? page)
        {
            List<tb_Article> articleList = new List<tb_Article>();
            if (articleClassificationId!="aid" && page!=null)
            {
                Func<tb_Article, bool> where = p => p.Id == articleClassificationId;
                articleList = ArticleBll.pageByWhere(where, p => p.Date, int.Parse(page.ToString()), 10);
            }
            else
            {
                page = 1;
                articleList = ArticleBll.pageByWhere(p => p.Id != "", p => p.Date, int.Parse(page.ToString()), 10);
            }
            int pageCount = articleList.Count() % 10 == 0 ? articleList.Count() / 10 : (articleList.Count() / 10) + 1;
            ViewData["pageCount"] = pageCount;
            return View(articleList);
        }
        /// <summary>
        /// 保存文章页面
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
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

        public ActionResult Detailed(string id)
        {
            tb_Article article = new tb_Article();
            article = ArticleBll.getModel(p => p.Id == id);
            return View(article);
        }
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase upload)
        {
            var fileName = System.IO.Path.GetFileName(upload.FileName);
            var filePhysicalPath = Server.MapPath("~/upload/" + fileName);//我把它保存在网站根目录的 upload 文件夹

            upload.SaveAs(filePhysicalPath);

            var url = "/upload/" + fileName;
            var ckEditorFuncNum = System.Web.HttpContext.Current.Request["CKEditorFuncNum"];

            //上传成功后，我们还需要通过以下的一个脚本把图片返回到第一个tab选项
            return Content("<script>window.parent.CKEDITOR.tools.callFunction(" + ckEditorFuncNum + ", \"" + url + "\");</script>");
        }
    }
}
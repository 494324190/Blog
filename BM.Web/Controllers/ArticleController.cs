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
using BM.Common.Web;
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
        public ICommonBLL<view_Article> vArticleBll { get; set; }

        ArticleBLL articleBll = new ArticleBLL();
        [Dependency]
        public ICommonBLL<tb_ArticleClassification> ArticleClassificationBll { get; set; }
        [Dependency]
        public ICommonBLL<tb_Comment> commentBll { get; set; }
        /// <summary>
        /// 文章列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? page = 1, string articleClassificationId = "aid")
        {
            List<view_Article> articleList = new List<view_Article>();
            int pageTatol = 0;
            if (articleClassificationId != "aid" && articleClassificationId != null)
            {
                Func<view_Article, bool> where = p => p.ClassificationId == articleClassificationId;
                if (page != null)
                {

                    articleList = vArticleBll.pageByWhere(where, p => p.Date, out pageTatol, int.Parse(page.ToString()),10);
                }
                else
                {
                    page = 1;
                    articleList = vArticleBll.pageByWhere(where, p => p.Date, out pageTatol, int.Parse(page.ToString()), 10);
                }
            }
            else
            {
                if (page == null)
                {
                    page = 1;
                }
                articleList = vArticleBll.pageByWhere(p => p.Id != "", p => p.Date, out pageTatol, int.Parse(page.ToString()), 10);
            }
            ViewData["pageCount"] = pageTatol;
            return View(articleList);
        }
        /// <summary>
        /// 保存文章页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Save()
        {
            if (Session["LoginState"] != null)
            {
                Func<tb_ArticleClassification, bool> whereFunc = p => p.Id != "";
                List<tb_ArticleClassification> articleClassificationList = ArticleClassificationBll.getList(whereFunc);
                ViewData["Classification"] = new SelectList(articleClassificationList, "Id", "Name");
            }
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
        /// <summary>
        /// 详情页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detailed(string id)
        {
            tb_Article articleModel = new tb_Article();
            articleModel = ArticleBll.getModel(p => p.Id == id);
            articleModel.ReadNum++;
            ArticleBll.Edit(articleModel);
            view_Article article = new view_Article();
            article = vArticleBll.getModel(p => p.Id == id);
            return View(article);
        }
        /// <summary>
        /// 保存评论
        /// </summary>
        /// <returns></returns>
        public int AddComment(string _Content)
        {
            tb_Comment commentModel = new tb_Comment();
            commentModel.ArticleId = Request.UrlReferrer.Segments[3];
            commentModel.C_Content = _Content;
            commentModel.DateTime = DateTime.Now;
            commentModel.IP = Ip.getIp();
            commentModel.Id = Guid.NewGuid().ToString();
            return commentBll.Save(commentModel)?1:0;
        }
        /// <summary>
        /// 评论列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Comment(string id)
        {
            List<tb_Comment> list = commentBll.getList(p=>p.ArticleId==id);
            return View(list);

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
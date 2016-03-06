using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BM.Common.FileOperate;
using BM.Common.StringOperate;
using Microsoft.Practices.Unity;
using BM.IBLL;
using BM.Models;
using BM.Common.Web;

namespace BM.Web.Controllers
{
    public class AccountController : BaseController
    {
        [Dependency]
        public ICommonBLL<tb_Email> emailBll { get; set; }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="code_input">随机登录码</param>
        /// <returns></returns>
        [HttpPost]
        public int Login(string code_input)
        {
            if (String.IsNullOrEmpty(code_input) || Encryption.MD5(code_input) != Xml.Read(Server.MapPath("../Config.xml"), "Login", 2, "password")[0])
            {
                return 0;
            }
            else
            {
                Session["LoginState"] = 1;
                return 1;
            }
        }

        [HttpPost]
        public void LoginOff()
        {
            Session.Clear();
        }

        [HttpPost]
        public int Subscription(string email)
        {
            tb_Email emailModel = new tb_Email();
            try
            {
                emailModel.Id = Guid.NewGuid().ToString();
                emailModel.Email = email;
                if (emailBll.getModel(p => p.Email == email) != null)
                {
                    return 1;
                }
                if (emailBll.Save(emailModel))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
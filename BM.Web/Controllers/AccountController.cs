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

namespace BM.Web.Controllers
{
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public int  Login(string LoginName,string Password)
        {
            if (String.IsNullOrEmpty(LoginName) && String.IsNullOrEmpty(Password))
            {
                return 0;
            }
            else
            {
                string path = Server.MapPath("../Config.xml");
                if (Xml.Read(path, "Login", 2, "user")[0] == LoginName &&
                    Xml.Read(path, "Login", 2, "password")[0] == Encryption.EncryptDES(Password, "zg753321"))
                {
                    Session["LoginState"] = Encryption.EncryptDES("8491hgj", "87W5Kj^nU2@$6"); 
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        [HttpPost]
        public void LoginOff()
        {
            Session.Clear();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BM.Common.StringOperate;

namespace BM.Web.Filter
{
    public class LoginFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
            {
                string loginState = filterContext.HttpContext.Session["LoginState"]==null?"" : filterContext.HttpContext.Session["LoginState"].ToString();
                if (loginState != Encryption.EncryptDES("8491hgj", "87W5Kj^nU2@$6"))
                {
                    filterContext.HttpContext.Response.Redirect("/Account/Login");
                }
            }
        }
    }
}
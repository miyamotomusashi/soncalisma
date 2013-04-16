using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace deneysan.Areas.Admin.Filters
{
    public class AuthenticateUser:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Redirect("/yonetim/login");
                HttpContext.Current.Response.End();
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
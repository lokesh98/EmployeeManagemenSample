using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EmployeeMgmt.UI.Models
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //filterContext.Result = new HttpUnauthorizedResult(); // Try this but i'm not sure
            filterContext.Result = new RedirectResult("~/Home/Unauthorized");
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (this.AuthorizeCore(filterContext.HttpContext))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                this.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
    public class SessionTimeout : ActionFilterAttribute
    {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                HttpContext context = HttpContext.Current;
                if (context.Session != null)
                {
                    if (context.Session.IsNewSession)
                    {

                        if ((context.Session["userId"] == null))
                        {
                            HttpContext.Current.GetOwinContext().Authentication.SignOut();
                            string redirectTo = "~/Account/Login";
                            if (!string.IsNullOrEmpty(context.Request.RawUrl))
                            {
                                redirectTo = string.Format("~/Account/Login?ReturnUrl={0}", HttpUtility.UrlEncode(context.Request.RawUrl));
                                filterContext.Result = new RedirectResult(redirectTo);
                                return;
                            }

                        }
                    }
                }

                base.OnActionExecuting(filterContext);
            }
        

    }
}
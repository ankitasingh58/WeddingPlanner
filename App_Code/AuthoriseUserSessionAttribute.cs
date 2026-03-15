using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Wedding_Planner.App_Code
{
    public class AuthoriseUserSessionAttribute:AuthorizeAttribute
    {
        // to check the user is valid or not
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool status;
            if (httpContext.Session["uid"] == null)
                status = false;
            else
                status = true;
            return status;
        }
        // to handle unauthorised user
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            /* filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { 
                 { "action", "Login" },
                 { "controller", "General" }
             });
            */
            filterContext.Result = new RedirectResult("/General/Login");

        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ForDemo
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,Inherited =true,AllowMultiple =true)]
    public class MyAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Login", action = "Login" }));
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Session["UserName"] != null;
        }
    }
    public class NoStoreAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.AppendCacheExtension("no-store, must-revalidate");
        }
    }

}
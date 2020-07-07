using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Repository.Repositories.AuthRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.Filters
{
    public class ResetPassFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //if (!context.HttpContext.Request.Cookies.TryGetValue("resetpassword", out string token))
            //{
            //    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "signin", controller = "account" }));
            //}

            if (!context.HttpContext.Request.Cookies.TryGetValue("forgettoken", out string token))
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}

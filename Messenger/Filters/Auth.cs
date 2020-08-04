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
    public class Auth : ActionFilterAttribute
    {
        private readonly IAuthRepository _authRepository;

        public Auth(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Cookies.TryGetValue("token", out string token))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "signin", controller = "account" }));
            }

            var user = _authRepository.CheckByToken(token);

            if (user == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "signin", controller = "account" }));
            }

            if (user != null && user.Status == false)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "accessdenied", controller = "account" }));
            }

            var controller = context.Controller as Controller;

            if (controller != null)
            {
                controller.ViewBag.User = user;
                if (user != null)
                {
                    controller.ViewBag.User = user;
                }
            }

            context.RouteData.Values["Account"] = user;
        }
    }
}

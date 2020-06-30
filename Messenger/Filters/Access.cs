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
    public class Access : ActionFilterAttribute
    {
        private readonly IAuthRepository _authRepository;

        public Access(IAuthRepository authRepository)
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


            if (user.IsEmailVerified == false)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "unverified", controller = "account" }));
            }

            context.RouteData.Values["User"] = user;
        }
    }
}

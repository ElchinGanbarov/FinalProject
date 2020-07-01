using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messenger.Filters;
using Messenger.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories.AuthRepositories;

namespace Messenger.Controllers
{
    [TypeFilter(typeof(Auth))]
    public class PagesController : Controller
    {
        private Repository.Models.Account _user => RouteData.Values["User"] as Repository.Models.Account;
        private readonly IAuthRepository _authRepository;
        public PagesController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        [TypeFilter(typeof(Access))]
        public IActionResult Chat1()
        {
            GeneralViewModel model = new GeneralViewModel
            {
                Account=_authRepository.CheckByToken(_user.Token)
            };
            return View(model);
        }
        public IActionResult Chat2()
        {
            return View();
        }
    }
}
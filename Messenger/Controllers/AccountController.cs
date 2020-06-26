using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Messenger.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories.AuthRepositories;

namespace Messenger.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
        public AccountController(IMapper mapper,IAuthRepository authRepository)
        {
            _mapper = mapper;
            _authRepository = authRepository;
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(RegisterViewModel model)
        {
            bool checkUser = _authRepository.CheckEmail(model.Email);

            if (checkUser)
            {
                ModelState.AddModelError("register.Email", "Bu E-mail artiq movcuddur");
            }
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<RegisterViewModel, Account>(model);
                user.Token = Guid.NewGuid().ToString();
                user.Status = true;
                _authRepository.Register(user);

                Response.Cookies.Delete("token");

                Response.Cookies.Append("token", user.Token, new Microsoft.AspNetCore.Http.CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddYears(1)
                });

                return RedirectToAction("chat1", "pages");
            }

            return View(model);
        }
        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
    }
}
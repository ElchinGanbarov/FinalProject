using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
using AutoMapper;
using Messenger.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories.AuthRepositories;
=======
using Microsoft.AspNetCore.Mvc;
>>>>>>> 23c55aa3d140936fc774e0f33feaa1f602313d8f

namespace Messenger.Controllers
{
    public class AccountController : Controller
    {
<<<<<<< HEAD
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
        public AccountController(IMapper mapper,IAuthRepository authRepository)
        {
            _mapper = mapper;
            _authRepository = authRepository;
        }
=======
>>>>>>> 23c55aa3d140936fc774e0f33feaa1f602313d8f
        public IActionResult SignUp()
        {
            return View();
        }
<<<<<<< HEAD
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
=======
>>>>>>> 23c55aa3d140936fc774e0f33feaa1f602313d8f
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
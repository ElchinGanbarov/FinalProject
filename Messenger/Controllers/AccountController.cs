using System;
using System.Net;
using System.Net.Mail;
using AutoMapper;
using Messenger.Filters;
using Messenger.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories.AuthRepositories;
using Repository.Services;

namespace Messenger.Controllers
{
    public class AccountController : Controller
    {
        private Repository.Models.Account  _user => RouteData.Values["User"] as Repository.Models.Account;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
        private readonly ISendEmail _emailService;

        public AccountController(IMapper mapper,
                                 IAuthRepository authRepository,
                                 ISendEmail sendEmail)
        {
            _mapper = mapper;
            _authRepository = authRepository;
            _emailService = sendEmail;
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
            bool number = _authRepository.CheckPhone(model.Phone);
            
            if (checkUser)
            {
                ModelState.AddModelError("Email", "Bu E-mail artiq movcuddur");
            }
            if (number)
            {
                ModelState.AddModelError("Phone", "Bu Nömrə artıq mövcuddur");
            }
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<RegisterViewModel, Account>(model);
                user.Token = Guid.NewGuid().ToString();
                user.Status = true;
                user.IsEmailVerified = false;

                //email verification code
                user.EmailActivationCode = Guid.NewGuid().ToString();

                _authRepository.Register(user);

                //send verification link email
                string userFullname = user.Name + " " + user.Surname;

                string link = HttpContext.Request.Scheme + "://" + Request.Host + "/account/verifyemail/" + user.EmailActivationCode;

                _emailService.VerificationEmail(user.Email, link, user.EmailActivationCode, userFullname);

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _authRepository.Login(model.Email, model.Password);
                if (user != null)
                {

                    user.Token = Guid.NewGuid().ToString();
                    _authRepository.UpdateToken(user.Id, user.Token);
                    Response.Cookies.Delete("token");
                    Response.Cookies.Append("token", user.Token, new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = model.RememberMe ? DateTime.Now.AddYears(1) : DateTime.Now.AddDays(1),
                        HttpOnly = true
                    });

                    if (user.IsEmailVerified == false)
                    {
                        return RedirectToAction("unverified", "account");
                    }

                    return RedirectToAction("chat1", "pages");
                }

                ModelState.AddModelError("Password", "E-poct veya Sifre yanlisdir...");

            }
            return View(model);
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        public IActionResult Logout()
        {
            Request.Cookies.TryGetValue("token", out string token);
            var user = _authRepository.CheckByToken(token);
            if (user.Token!= null)
            {
                _authRepository.UpdateToken(user.Id, null);
            }
            Response.Cookies.Delete("token");
            //return PartialView("chat1", "pages");
            return RedirectToAction("signin","account");
        }

        //Email Verification Link Click View
        [TypeFilter(typeof(Auth))]
        [HttpGet]
        public IActionResult VerifyEmail()
        {
            string Url = Request.Path.Value;
            if (Url.Length < 22)
            {
                return NotFound();
            }
            string linkId = Url.Substring(21);


            if (_user.IsEmailVerified && _user.EmailActivationCode == "verified")
            {
                ViewBag.VerifiedAccount = true;
                return View();
            }

            ViewBag.VerifiedAccount = false;

            ViewBag.IsVerified = false;

            if (_user.EmailActivationCode == null || _user.EmailActivationCode == "expired")
            {
                ViewBag.Message = "Account Verification Link Has Expired !";
                ViewBag.IsVerified = false;

                return View();
            }

            if (_user.EmailActivationCode.ToString() == linkId)
            {
                ViewBag.Message = "Account Successfully Verified";
                ViewBag.IsVerified = true;

                _authRepository.VerifyUserEmail(_user.Id);

                return View();
            }

            ViewBag.Message = "Account Verification Link Has Expired !";
            ViewBag.IsVerified = false;

            return View();
        }

        [TypeFilter(typeof(Auth))]
        public IActionResult UnVerified()
        {
            if (_user.IsEmailVerified)
            {
                return NotFound();
            }

            return View();
        }
    }
}
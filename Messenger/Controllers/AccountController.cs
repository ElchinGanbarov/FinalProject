using System;
using System.Net;
using System.Net.Mail;
using AutoMapper;
using Messenger.Filters;
using Messenger.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories.AuthRepositories;

namespace Messenger.Controllers
{
    public class AccountController : Controller
    {
        private Repository.Models.Account  _user => RouteData.Values["User"] as Repository.Models.Account;
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

                SendVerificationLinkEmail(user.Email, user.EmailActivationCode, userFullname);

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
            return RedirectToAction("SignIn","Account");
        }

        //Send Verification Link Email
        [NonAction]
        public void SendVerificationLinkEmail(string email, string activationCode, string userFullname)
        {
            bool isRetry = false;
            if (string.IsNullOrEmpty(activationCode) || activationCode == "verified")
            {
                activationCode = Guid.NewGuid().ToString();
                isRetry = true;
            }

            string link = HttpContext.Request.Scheme + "://" + Request.Host + "/account/verifyemail/" + activationCode;

            var fromEmail = new MailAddress("parvinkhp@code.edu.az", "Messenger App");
            var fromEmailPassword = "Pervin_1997";
            var toEmail = new MailAddress(email);
            var appeal = "Dear, " + userFullname +"! ";
            var subject = ""; //in testing proccess!
            if (isRetry)
            {
                subject = "Messenger Account Verify Link";
            }
            else
            {
                subject = "Messenger Account Successfully Created";
            }

            var messageBody = " <center><img style='width: 80; padding: 10px 0px' src='http://frontendmatters.org/quicky/assets/media/logo.svg' /></center> </br> " +
                "<div style=' background-color: #665dfe; padding: 20px 0px;'> " +
                "<h2 style='padding: 10px 30px; font-size: 29px; color: #fff;'>" + appeal +
                "Thank you for creating your new Messanger App account! Please, Click the below button to Verify Your Account </h2>" +
                "<center><a style='display: inline-block; background-color: #28a745; font-weight: bold; color: #fff; padding: 10px; " +
                "text-align: center; text-decoration: none; border: 1px solid transparent; font-size: 22px; border-radius: 5px; line-height: 1.5;' " +
                "href=" + link + ">Verify Account</a></center>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = messageBody,
                IsBodyHtml = true
            };
            smtp.Send(message);

        }

        //Email Verification Link Click View
        [TypeFilter(typeof(Auth))]
        [HttpGet]
        public IActionResult VerifyEmail(int? id)
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
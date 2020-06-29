using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using Messenger.Models;
using Microsoft.AspNetCore.Http.Extensions;
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
                user.EmailActivationCode = Guid.NewGuid();

                _authRepository.Register(user);

                //send verification link email
                SendVerificationLinkEmail(user.Email, user.EmailActivationCode);

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
        public void SendVerificationLinkEmail(string email, Guid activationCode)
        {
            string link = HttpContext.Request.Scheme + "://" + Request.Host + "/account/verifyemail/" + activationCode;

            var fromEmail = new MailAddress("parvinkhp@code.edu.az", "Messanger Application");
            var fromEmailPassword = "Pervin_1997";
            var toEmail = new MailAddress(email);

            var subject = "Your Account Successfully Created";
            var messageBody = "<br/></br></br>Thank you for creating your new Messanger App account! " +
                "Please, Click the below link to Verify Your Account. " +
                "<a href='" + link + "'>" + link + "</a>";

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
        [HttpGet]
        public IActionResult VerifyEmail(Guid guId)
        {
            if (_authRepository.VerifyEmail(guId)) return View();

            return View();
        }
    }
}
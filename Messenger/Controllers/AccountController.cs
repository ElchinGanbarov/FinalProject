using System;
using System.Collections.Generic;
using AutoMapper;
using CryptoHelper;
using Messenger.Filters;
using Messenger.Models.Account;
using Messenger.Models.AccountDetail;
using Messenger.Models.General;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories.AccountRepository;
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
        private readonly IAccountDetailRepository _accountDetailRepository;
        private readonly IFriendsRepository _friendsRepository;


        public AccountController(IMapper mapper,
                                 IAuthRepository authRepository,
                                 ISendEmail sendEmail,
                                 IAccountDetailRepository accountDetailRepository,
                                 IFriendsRepository friendsRepository)
        {
            _mapper = mapper;
            _authRepository = authRepository;
            _emailService = sendEmail;
            _accountDetailRepository = accountDetailRepository;
            _friendsRepository = friendsRepository;
        }

        //SignUp; SignIn; Logout Start
        #region signUp,singIn,Logout

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
                user.Fullname = model.Name + " " + model.Surname;
                user.Token = Guid.NewGuid().ToString();
                user.Status = true;
                user.IsEmailVerified = false;

                //email verification code
                user.EmailActivationCode = Guid.NewGuid().ToString();
           
                _authRepository.Register(user);

                AccountSocialLink accountSocialLink = new AccountSocialLink
                {
                    AccountId = user.Id,
                    Status = true,
                    AddedBy = "System",
                    AddedDate = DateTime.Now
                };
                _authRepository.AddedSocial(accountSocialLink);

                //creating account's privacy Database
                AccountPrivacy accountPrivacy = new AccountPrivacy
                {
                    AccountId = user.Id,
                    Status = true,
                    AddedDate = DateTime.Now,
                    AddedBy = "System",
                    Phone = true,
                    Email = true,
                    LastLogin = true,
                    LastSeen = true,
                    Address = true,
                    Birthday = true,
                    ProfileImg = true,
                    SocialLink = true,
                    StatusText = true,
                    Website = true
                };
                _authRepository.CreatePrivacy(accountPrivacy);

                AccountSecurity accountSecurity = new AccountSecurity
                {
                    AccountId=user.Id,
                    TwoFactoryAuth = false,
                    LoginAlerts = false
                };
                _authRepository.CreateSecurity(accountSecurity);

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
        #endregion
        //SignUp; SignIn; Logout End


        //Reset and Update Password Start
        #region Reset & Update Password
        public IActionResult ResetPassword()
        {
            return View();
        }

        [TypeFilter(typeof(ResetPassFilter))]
        public IActionResult ResetPassConfirm()
        {
            return View();
        }

        [TypeFilter(typeof(ResetPassFilter))]
        [HttpPost]
        public IActionResult ResetPassConfirm(ResetPasswordConfirmViewModel model)
        {
            if (ModelState.IsValid)
            {
                string forgettoken = Request.Cookies["forgettoken"];
                if (string.IsNullOrEmpty(forgettoken)) return BadRequest();
                Account account = _authRepository.GetByForgetToken(forgettoken);
                if (account != null)
                {
                    _authRepository.UpdatePassword(account.Id, model.Password);
                }
                Response.Cookies.Delete("forgettoken");
                return RedirectToAction("signin", "account");
            }

            return View();
        }

        [TypeFilter(typeof(Auth))]
        [HttpPost]
        public IActionResult UpdatePassword(UpdatePasswordViewModel UpdatePasswordViewModel)
        {
            var account = _authRepository.CheckByToken(_user.Token);
         
            if (ModelState.IsValid)
            {
                if (!Crypto.VerifyHashedPassword(account.Password, UpdatePasswordViewModel.CurrentPassword))
                {
                    return Ok(new
                    {
                        message = "Password is Not Valid!",
                        status=false
                    });
                }
                if(_authRepository.UpdatePassword(_user.Id, UpdatePasswordViewModel.Password))
                {
                    return Ok(new
                    {
                        message = "Password Updated Successfully!",
                        status = true
                    });
                }
                
            }


            return View("Views/Pages/Chat1.cshtml", new GeneralViewModel
            {
                UpdatePasswordViewModel = UpdatePasswordViewModel,
                AccountDetailViewModel = _mapper.Map<Account, AccountDetailViewModel>(_authRepository.CheckByToken(_user.Token))

            });

        }
        #endregion
        //Reset and Update Password End

        //VerifyEmail, UnVerified, CheckEmailAddress, CheckForgetCode Start
        #region Email 
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
                return RedirectToAction("chat1", "pages");
            }

            return View();
        }

        public bool CheckEmailAddress(string userEmail) //checkemailaddress
        {

            if (string.IsNullOrEmpty(userEmail)) return false;

            if (_authRepository.CheckEmail(userEmail))
            {
                Account account = _authRepository.GetByEmail(userEmail);
                if (account == null) return false;
                _emailService.ResetPassword(account);
                return true;
            };

            return false;
           
        }
        public IActionResult CheckForgetCode(string userEmail, string inputResetPass)
        {
            if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(inputResetPass))
            {
                return Ok(new { status = false });
            }

            Account account = _authRepository.GetByEmail(userEmail);
            if (account == null || string.IsNullOrEmpty(account.ForgetToken)) return Ok(new { status = false });

            if (_authRepository.CheckPasswordResetCode(account.Id, inputResetPass))
            {
                return Ok(new { status = true, forgetToken = account.ForgetToken });
            }

            //return NotFound();
            return Ok(new { status = false});
        }
        #endregion
        //VerifyEmail, UnVerified, CheckEmailAddress, CheckForgetCode Start

        [HttpPost]
        public IActionResult GetAccountDatas(int currentAccountId, int searchedAccountId)
        {
            if (currentAccountId != searchedAccountId)
            {
                if (_friendsRepository.IsFriends(currentAccountId, searchedAccountId)) //friends true
                {
                    return Ok(_accountDetailRepository.GetDatasFriend(searchedAccountId));
                }
                else //not friends
                {
                    return Ok(_accountDetailRepository.GetDatasPublic(currentAccountId, searchedAccountId));
                }
            }
            else //view own profile
            {
                return Ok(_accountDetailRepository.GetDatasOwn(searchedAccountId));
            }
        }

    }
}
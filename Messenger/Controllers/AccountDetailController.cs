using AutoMapper;
using Messenger.Filters;
using Messenger.Models;
using Messenger.Models.Account;
using Messenger.Models.AccountDetail;
using Messenger.Models.AccountPrivacySecurity;
using Messenger.Models.General;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories.AccountRepository;
using Repository.Repositories.AuthRepositories;

namespace Messenger.Controllers
{
    [TypeFilter(typeof(Auth))]
    public class AccountDetailController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IAccountDetailRepository _accountDetailRepository;
        private Repository.Models.Account _user => RouteData.Values["User"] as Repository.Models.Account;

        public AccountDetailController(IAuthRepository authRepository,
                                       IMapper mapper,
                                       IAccountDetailRepository accountDetailRepository)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _accountDetailRepository = accountDetailRepository;
        }
        [HttpPost]
        public IActionResult Update(AccountDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<AccountDetailViewModel, Account>(model);
                
                var updateuser = _authRepository.CheckByToken(_user.Token);
              
                    if (updateuser.Name != user.Name ||
                        updateuser.Surname != user.Surname ||
                        updateuser.Address != user.Address ||
                        updateuser.Birthday != user.Birthday ||
                        updateuser.Email != user.Email ||
                        updateuser.Website != user.Website ||
                        updateuser.Phone != user.Phone)
                    {

                        _authRepository.UpdateAccount(updateuser, user);
                        return Ok(user);
                    }
            }
            return View("Views/Pages/Chat1.cshtml", new GeneralViewModel
            {
                AccountDetailViewModel = model
            });
        }

        [HttpPost]
        public IActionResult UpdateSocialLink(AccountSocialLinkViewModel model)
        {
            var updateuser = _authRepository.CheckByToken(_user.Token);
            
                if (ModelState.IsValid)
                {
                 var socialLink = _mapper.Map<AccountSocialLinkViewModel, AccountSocialLink>(model);
                socialLink.AccountId = _user.Id;
                    var updatesociallink = _accountDetailRepository.GetByAccountId(_user.Id);

                
                    if (updatesociallink.Facebook != socialLink.Facebook ||
                     updatesociallink.Twitter != socialLink.Twitter||
                     updatesociallink.Instagram != socialLink.Instagram ||
                     updatesociallink.Linkedin != socialLink.Linkedin)
                    {
                        _accountDetailRepository.UpdateSocialLink(updatesociallink, socialLink);
                        return Ok(socialLink);
                    }
                
             



                }
         

            return View("Views/Pages/Chat1.cshtml", new GeneralViewModel
            {
                AccountSocialLinkViewModel = model,
                AccountDetailViewModel = _mapper.Map<Account, AccountDetailViewModel>(updateuser)
            });
        }

        //Privacy & Security Start
        #region Privacy & Security

        [TypeFilter(typeof(Auth))]
        [HttpPost]
        public IActionResult UpdatePrivacy(AccountPrivacyViewModel model)
        {
            if (ModelState.IsValid)
            {
                Account account = _authRepository.CheckByToken(_user.Token);
                if (account != null)
                {
                    _accountDetailRepository.UpdatePrivacy(account.Id,
                                                           model.ProfileImg,
                                                           model.StatusText,
                                                           model.LastSeen,
                                                           model.Phone,
                                                           model.Birthday,
                                                           model.Address,
                                                           model.SocialLink,
                                                           model.AcceptAllMessages);
                    return Ok(new { status = true, message = "Account Privacy Successfully Updated!" });
                }
            }

            return Ok(new { status = false, message = "Unexpected Error Ocoured! Update account privacy Failed!" });
        }

        [TypeFilter(typeof(Auth))]
        [HttpPost]
        public IActionResult UpdateSecurity(AccountSecurityViewModel model)
        {
            if (ModelState.IsValid)
            {
                Account account = _authRepository.CheckByToken(_user.Token);
                if (account != null)
                {
                    _accountDetailRepository.UpdateSecurity(account.Id, model.TwoFactoryAuth, model.LoginAlerts);
                    return Ok(new { status = true, message = "Account Security Successfully Updated!" });
                }
            }

            return Ok(new { status = false, message = "Unexpected Error Ocoured! Update account security Failed!" });
        }
        #endregion
        //Privacy & Security End
    }
}
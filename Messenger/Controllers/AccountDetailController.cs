using AutoMapper;
using Messenger.Filters;
using Messenger.Models;
using Messenger.Models.Account;
using Messenger.Models.AccountDetail;
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
                if (updateuser.Name.Trim() != user.Name.Trim() || updateuser.Surname.Trim() != user.Surname.Trim() ||
                    updateuser.Address.Trim() != user.Address.Trim() || updateuser.Birthday != user.Birthday ||
                    updateuser.Email.Trim() != user.Email.Trim() || updateuser.Website.Trim() != user.Website.Trim())
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

                    if (updatesociallink.Facebook != socialLink.Facebook.Trim() ||
                        updatesociallink.Twitter != socialLink.Twitter.Trim() ||
                        updatesociallink.Instagram != socialLink.Instagram.Trim() ||
                        updatesociallink.Linkedin != socialLink.Linkedin.Trim())
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
    }
}
using AutoMapper;
using Messenger.Filters;
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
    public class PagesController : Controller
    {
        private Repository.Models.Account _user => RouteData.Values["User"] as Repository.Models.Account;
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IAccountDetailRepository _accountDetailRepository;
        public PagesController(IAuthRepository authRepository,
                               IMapper mapper,
                               IAccountDetailRepository accountDetailRepository)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _accountDetailRepository = accountDetailRepository;
        }
        [TypeFilter(typeof(Access))]
        public IActionResult Chat1()
        {
            
            GeneralViewModel model = new GeneralViewModel
            {
                AccountDetailViewModel = _mapper.Map<Account, AccountDetailViewModel>(_authRepository.CheckByToken(_user.Token)),
                AccountSocialLinkViewModel =_mapper.Map<AccountSocialLink,AccountSocialLinkViewModel>(_accountDetailRepository.GetAccountSocialLink(_user.Id)),
                AccountPrivacyViewModel =_mapper.Map<AccountPrivacy, AccountPrivacyViewModel>(_accountDetailRepository.GetAccountPrivacy(_user.Id))
            };
            return View(model);
        }
        public IActionResult Chat2()
        {
            return View();
        }
    }
}
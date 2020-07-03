using AutoMapper;
using Messenger.Filters;
using Messenger.Models;
using Messenger.Models.Account;
using Messenger.Models.AccountDetail;
using Messenger.Models.General;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories.AuthRepositories;

namespace Messenger.Controllers
{
    [TypeFilter(typeof(Auth))]
    public class AccountDetailController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private Repository.Models.Account _user => RouteData.Values["User"] as Repository.Models.Account;

        public AccountDetailController(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult Update(AccountDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<AccountDetailViewModel, Account>(model);
                var updateuser = _authRepository.CheckByToken(_user.Token);
                if (updateuser.Name != user.Name.Trim() || updateuser.Surname !=user.Surname.Trim() ||
                    updateuser.Address != user.Address.Trim() || updateuser.Birthday!= user.Birthday ||
                    updateuser.Email != user.Email.Trim() || updateuser.Website != user.Website.Trim())
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


        public IActionResult UpdateSocialLink(AccountSocialLinkViewModel model)
        {
            return View();
        }
    }
}
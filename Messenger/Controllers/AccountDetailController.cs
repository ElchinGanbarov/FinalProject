using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Messenger.Filters;
using Messenger.Models;
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
                _authRepository.UpdateAccount(updateuser, user);
                return Ok(user);
            }
            return View("Views/Pages/Chat1.cshtml", new GeneralViewModel
            {
               AccountDetailViewModel = model
            });
        }
    }
}
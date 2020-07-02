﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Messenger.Filters;
using Messenger.Models;
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
            var user = _mapper.Map<Account, AccountDetailViewModel>(_authRepository.CheckByToken(_user.Token));
            GeneralViewModel model = new GeneralViewModel
            {
                AccountDetailViewModel = user,
                AccountSocialLink = _accountDetailRepository.GetAccountSocialLink(_user.Id)
            };
            return View(model);
        }
        public IActionResult Chat2()
        {
            return View();
        }
    }
}
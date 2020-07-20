﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Messenger.Filters;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories.AccountRepository;
using Repository.Repositories.AuthRepositories;

namespace Messenger.Controllers
{
    [TypeFilter(typeof(Auth))]
    public class FriendsController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IFriendsRepository _friendsRepository;
        private Repository.Models.Account _user => RouteData.Values["User"] as Repository.Models.Account;


        public FriendsController(IAuthRepository authRepository,
                                       IMapper mapper,
                                       IAccountDetailRepository accountDetailRepository,
                                       IFriendsRepository friendsRepository)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _friendsRepository = friendsRepository;
        }

        [HttpPost]
        public IActionResult AllFriends(int userId)
        {
            ICollection<Account> friends = _friendsRepository.GetAllFriends(userId);

            return Ok(friends);
        }

        [HttpPost]
        public IActionResult FriendInfos(int friendId)
        {
            Account account = _friendsRepository.GetFriendById(friendId);
            if (account != null)
            {
                return Ok(account);
            };
            
            return Ok(StatusCode(404));
        }

        [HttpPost]
        public IActionResult FriendSocialLinks(int friendId)
        {
            AccountSocialLink friendsocials = _friendsRepository.GetFriendSocialLinks(friendId);
            if (friendsocials != null)
            {
                return Ok(friendsocials);
            };

            return Ok(StatusCode(404));
        }

        //testing

        public IActionResult testing()
        {
            Account account = _friendsRepository.GetFriendById(9025);
            return Ok(account);
        }
    }
}
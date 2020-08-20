using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Messenger.Filters;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories.AccountRepository;
using Repository.Repositories.AuthRepositories;
using Repository.Repositories.SearchRepository;

namespace Messenger.Controllers
{
    [TypeFilter(typeof(Auth))]
    public class ChatController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAccountDetailRepository _accountDetailRepository;
        private readonly IMapper _mapper;
        private readonly IFriendsRepository _friendsRepository;
        private readonly ISearchRepository _searchRepository;
        private Repository.Models.Account _user => RouteData.Values["User"] as Repository.Models.Account;


        public ChatController(IAuthRepository authRepository,
                                       IMapper mapper,
                                       IAccountDetailRepository accountDetailRepository,
                                       IFriendsRepository friendsRepository,
                                       ISearchRepository searchRepository)
        {
            _authRepository = authRepository;
            _accountDetailRepository = accountDetailRepository;
            _mapper = mapper;
            _friendsRepository = friendsRepository;
            _searchRepository = searchRepository;
        }

        [HttpPost]
        public IActionResult GetAllChatHubs(int userId)
        {


            return View();
        }
    }
}

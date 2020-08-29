using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Messenger.Filters;
using Messenger.Models.Chat;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.Models;
using Repository.Repositories.AccountRepository;
using Repository.Repositories.AuthRepositories;
using Repository.Repositories.ChatRepository;
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
        private readonly IChatRepository _chatRepository;
        private Repository.Models.Account _user => RouteData.Values["User"] as Repository.Models.Account;


        public ChatController(IAuthRepository authRepository,
                                       IMapper mapper,
                                       IAccountDetailRepository accountDetailRepository,
                                       IFriendsRepository friendsRepository,
                                       ISearchRepository searchRepository,
                                       IChatRepository chatRepository)
        {
            _authRepository = authRepository;
            _accountDetailRepository = accountDetailRepository;
            _mapper = mapper;
            _friendsRepository = friendsRepository;
            _searchRepository = searchRepository;
            _chatRepository = chatRepository;
        }

        [HttpPost]
        public IActionResult GetAllChatHubs(int userId)
        {
            var hubs = _chatRepository.GetAccountHubsAll(userId);

            string json = JsonConvert.SerializeObject(hubs);

            return Ok(json);

        }

        [HttpPost]
        public IActionResult GetHubMessagesAll(int userId, int hubId)
        {

            //var messages = _chatRepository.GetHubMessagesAll(1);

            //var hubMessages = _mapper.Map<ICollection<Message>, ICollection<MessageViewModel>>(messages);

            //return Ok(new { status = true, messages = hubMessages });

            var checkHubAccess = _chatRepository.CheckHubAccess(userId, hubId);

            userId = _user.Id;

            if (checkHubAccess)
            {
                var hubMessages = _chatRepository.GetHubMessagesAll(hubId);

                string json = JsonConvert.SerializeObject(hubMessages);

                return Ok(json);
            }
            else
            {
                return Ok(new { status = false });
            }
        }

        public IActionResult Test()
        {

            var hubs = _chatRepository.GetAccountHubsAll(1);

            string json = JsonConvert.SerializeObject(hubs);

            return Ok(json);


            //var checkHubAccess = _chatRepository.CheckHubAccess(1, 1);

            //if (checkHubAccess)
            //{
            //    var hubMessages = _chatRepository.GetHubMessagesAll(1);

            //    string json = JsonConvert.SerializeObject(hubMessages);

            //    return Ok(json);
            //}
            //else
            //{
            //    return Ok(new { status = false });
            //}
        }
    }
}

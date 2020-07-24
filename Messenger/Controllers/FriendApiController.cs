using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories.AccountRepository;

namespace Messenger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendApiController : ControllerBase
    {
        private readonly IFriendsRepository _friendsRepository;
        private Repository.Models.Account _user => RouteData.Values["User"] as Repository.Models.Account;


        public FriendApiController(IFriendsRepository friendsRepository)
        {
            _friendsRepository = friendsRepository;
        }

        [Produces("application/json")]
        [HttpGet("searchaccount")]
        public async Task<IActionResult> SearchAccount()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var accouns = _friendsRepository.SearchByName(term);
                return Ok(accouns);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
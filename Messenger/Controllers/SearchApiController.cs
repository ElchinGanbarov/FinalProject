using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories.AccountRepository;
using Repository.Repositories.SearchRepository;

namespace Messenger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchApiController : ControllerBase
    {
        private readonly ISearchRepository _searchRepository;

        public SearchApiController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        [Produces("application/json")]
        [HttpGet("searchaccount")]
        public async Task<IActionResult> SearchAccount(int currentUserId)
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var results = _searchRepository.SearchAccounts(currentUserId, term);
                return Ok(results);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
using Repository.Data;
using Repository.Models;
using Repository.Repositories.AccountRepository;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repositories.SearchRepository
{
    public interface ISearchRepository
    {
        ICollection<SearchAccount> SearchAccounts(int currentUserId, string term);
        ICollection<SearchAccount> SearchFriendsAccounts(int currentUserId, string term);
    }

    public class SearchRepository : ISearchRepository
    {
        private readonly MessengerDbContext _context;
        private readonly IFriendsRepository _friendsRepository;
        private readonly IAccountDetailRepository _accountDetailRepository;

        public SearchRepository(MessengerDbContext context,
                                IFriendsRepository friendsRepository,
                                IAccountDetailRepository detailRepository)
        {
            _context = context;
            _friendsRepository = friendsRepository;
            _accountDetailRepository = detailRepository;
        }

        public ICollection<SearchAccount> SearchAccounts(int currentUserId, string term)
        {
            //final search results
            List<SearchAccount> results = new List<SearchAccount>();

            ICollection<int> accountIdList = _context.Accounts.Where(a => a.Fullname.Contains(term))
                                                             .OrderBy(a => a.Name)
                                                             .Select(a => a.Id)
                                                             .ToList();

            foreach (var itemId in accountIdList)
            {

                if (currentUserId != itemId) //don't show current user 2x
                {
                    if (_friendsRepository.IsFriends(currentUserId, itemId)) //friends
                    {
                        SearchAccount searchItem = _accountDetailRepository.GetDatasFriend(itemId);
                        if (searchItem != null)
                        {
                            results.Add(searchItem);
                        }
                    }
                    else //not friends
                    {
                        SearchAccount searchItem = _accountDetailRepository.GetDatasPublic(currentUserId, itemId);
                        if (searchItem != null)
                        {
                            results.Add(searchItem);
                        }
                    }
                }
                else //search own profile
                {
                    SearchAccount searchItem = _accountDetailRepository.GetDatasOwn(itemId);
                    if (searchItem != null)
                    {
                        results.Add(searchItem);
                    }
                } 
            }

            return results;
        }
        public ICollection<SearchAccount> SearchFriendsAccounts(int currentUserId, string term)
        {
            //final search results
            List<SearchAccount> results = new List<SearchAccount>();

            ICollection<Account> friendslist = _friendsRepository.GetAllFriends(currentUserId);

            foreach (var item in friendslist)
            {
                if (item.Fullname.ToLower().Contains(term.ToLower()))
                {

                    if (currentUserId != item.Id) //don't show current user 2x
                    {
                 
                        SearchAccount searchItem = _accountDetailRepository.GetDatasFriend(item.Id);
                        if (searchItem != null)
                        {
                            results.Add(searchItem);
                        }

                    }   
                }

            }

            return results;
        }
    }
}

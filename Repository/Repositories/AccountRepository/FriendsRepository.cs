using Repository.Data;
using Repository.Models;
using Repository.Services;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Repository.Repositories.AccountRepository
{
    public interface IFriendsRepository
    {
        int GetAllFriendsCount(int userId);
        bool IsFriends(int currentAccountId, int searchedAccountId);
        Account GetFriendById(int friendId);
        AccountSocialLink GetFriendSocialLinks(int friendId);
        ICollection<Account> GetAllFriends(int userId);
        ICollection<SearchAccount> SearchByName(string term);
    }
    public class FriendsRepository : IFriendsRepository
    {
        private readonly MessengerDbContext _context;
        public FriendsRepository(MessengerDbContext context)
        {
            _context = context;
        }

        public ICollection<Account> GetAllFriends(int userId)
        {
            //userId is current log in user

            List<Friend> friendList = _context.Friends.Where(a => a.FromUserId == userId)
                                                      .Where(a => a.IsConfirmed)
                                                      .ToList();

            friendList.AddRange(_context.Friends.Where(a => a.ToUserId == userId)
                                                .Where(a => a.IsConfirmed)
                                                .ToList());

            List<Account> friendAccounts = new List<Account>();

            foreach (var item in friendList)
            {
                var fromAccount = _context.Accounts.Find(item.FromUserId);
                if (fromAccount != null && fromAccount.Id != userId)
                {
                    if (friendAccounts.Find(a => a.Id == fromAccount.Id) == null) //for dont show dublicate datas
                    {
                        friendAccounts.Add(fromAccount);
                    };
                }
                var toAccount = _context.Accounts.Find(item.ToUserId); 
                if (toAccount != null && toAccount.Id != userId)
                {
                    if (friendAccounts.Find(a => a.Id == toAccount.Id) == null) //for dont show dublicate datas
                    {
                        friendAccounts.Add(toAccount);
                    }
                }
            }

            return friendAccounts;
        }

        public bool IsFriends(int currentAccountId, int searchedAccountId)
        {
            Friend friendship = _context.Friends.FirstOrDefault(f => (f.FromUserId == currentAccountId && f.ToUserId == searchedAccountId)
                                                                 || f.FromUserId == searchedAccountId && f.ToUserId == currentAccountId);

            if (friendship != null)
            {
                return true;
            }

            return false;
        }

        public int GetAllFriendsCount(int userId)
        {
            return _context.Friends.Where(a => a.FromUserId == userId).Count(f => f.IsConfirmed) + 
                _context.Friends.Where(a => a.ToUserId == userId).Count(f => f.IsConfirmed);
        }

        public Account GetFriendById(int friendId)
        {
            Account account = _context.Accounts.Find(friendId);
            if (account == null) return null;

            return account;
        }

        public AccountSocialLink GetFriendSocialLinks(int friendId)
        {
            AccountSocialLink socialLinks = _context.AccountSocialLinks.FirstOrDefault(f => f.AccountId == friendId);
            
            return socialLinks;
        }

        public ICollection<SearchAccount> SearchByName(string term)
        {
            List<SearchAccount> results = new List<SearchAccount>();

            ICollection<Account> accounts = _context.Accounts.Where(a => a.Name.Contains(term) || a.Surname.Contains(term))
                                                             //.Select(a => a.Name + " " + a.Surname)
                                                             .ToList();

            foreach (var item in accounts)
            {
                SearchAccount searchItem = new SearchAccount();
                searchItem.Label = item.Name + " " + item.Surname;
                searchItem.Id = item.Id;
                searchItem.Img = item.ProfileImg;

                results.Add(searchItem);
            }

            return results;
        }
    }
}
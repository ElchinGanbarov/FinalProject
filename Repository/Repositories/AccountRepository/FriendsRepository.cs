using Repository.Data;
using Repository.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.AccountRepository
{
    public interface IFriendsRepository
    {
        int GetAllFriendsCount(int userId);
        Account GetFriendById(int friendId);
        AccountSocialLink GetFriendSocialLinks(int friendId);
        ICollection<Account> GetAllFriends(int userId);
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
    }
}
using Repository.Data;
using Repository.Models;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Repository.Repositories.AccountRepository
{
    public interface IFriendsRepository
    {
        int GetAllFriendsCount(int userId);
        Friend GetFriendship(int currentUserId, int accountId);
        FriendshipStatus GetFriendshipStatus(int currentUserId, int accountId);
        bool IsFriends(int currentAccountId, int searchedAccountId);
        void RemoveFriend(int currentUserId, int friendId);
        void NewFriendRequest(int fromUserId, int toUserId);
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

            if (friendship != null && friendship.StatusCode == FriendshipStatus.Accepted && friendship.Status)
            {
                return true;
            }

            return false;
        }

        public void RemoveFriend(int currentUserId, int friendId)
        {
            Friend friendship = _context.Friends.FirstOrDefault(f => (f.FromUserId == currentUserId && f.ToUserId == friendId)
                                                                 || f.FromUserId == friendId && f.ToUserId == currentUserId);
            if (friendship != null && friendship.StatusCode == FriendshipStatus.Accepted)
            {
                _context.Friends.Remove(friendship);
                _context.SaveChanges();
            }
        }

        public FriendshipStatus GetFriendshipStatus(int currentUserId, int accountId)
        {
            Friend friendship = _context.Friends.FirstOrDefault(f => (f.FromUserId == currentUserId && f.ToUserId == accountId)
                                                                 || f.FromUserId == accountId && f.ToUserId == currentUserId);
            if (friendship != null && friendship.Status)
            {
                return friendship.StatusCode;
            }

            return FriendshipStatus.NotFriend; //not friend
        }

        public Friend GetFriendship(int currentUserId, int accountId)
        {
            Friend friendship = _context.Friends.FirstOrDefault(f => (f.FromUserId == currentUserId && f.ToUserId == accountId)
                                                                 || f.FromUserId == accountId && f.ToUserId == currentUserId);
            if (friendship != null && friendship.Status)
            {
                return friendship;
            }

            return friendship; //not friend
        }


        public void NewFriendRequest(int fromUserId, int toUserId)
        {
            var fromUser = _context.Accounts.FirstOrDefault(a1 => a1.Id == fromUserId);
            var ToUser = _context.Accounts.FirstOrDefault(a2 => a2.Id == toUserId);

            if (fromUser != null && ToUser != null)
            {
                Friend friendRequest = new Friend
                {
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    AddedBy = fromUser.Fullname,
                    ModifiedBy = "System",
                    Status = true,
                    FromUserId = fromUserId,
                    ToUserId = toUserId,
                    IsConfirmed = false,
                    StatusCode = 0 //pending
                };

                _context.Friends.Add(friendRequest);
                _context.SaveChanges();
            }
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
                //searchItem.Label = item.Name + " " + item.Surname;
                searchItem.Id = item.Id;
                searchItem.Img = item.ProfileImg;

                results.Add(searchItem);
            }

            return results;
        }


    }
}
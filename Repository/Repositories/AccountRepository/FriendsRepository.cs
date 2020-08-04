using Repository.Data;
using Repository.Models;
using Repository.Repositories.SignalRepository;
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
        void AcceptFriendRequest(int senderId, int receiverId);
        Account GetFriendById(int friendId);
        AccountSocialLink GetFriendSocialLinks(int friendId);
        ICollection<Account> GetAllFriends(int userId);
        ICollection<SearchAccount> SearchByName(string term);
    }
    public class FriendsRepository : IFriendsRepository
    {
        private readonly MessengerDbContext _context;
        private readonly INotificationRepository _notificationRepository;

        public FriendsRepository(MessengerDbContext context,
                                 INotificationRepository notificationRepository)
        {
            _context = context;
            _notificationRepository = notificationRepository;
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

            if (friendship != null && friendship.StatusCode == FriendshipStatus.Accepted 
                                   && friendship.Status 
                                   && friendship.IsConfirmed)
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
                //creating new friend data (status pending)
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

                //creating new notification toUser
                Notification newNotification = new Notification
                {
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    AddedBy = fromUser.Fullname,
                    ModifiedBy = "System",
                    Status = true,
                    SenderId = fromUserId.ToString(), //int to string Static problem!
                    ReceiverId = toUserId,
                    Type = NotificationType.NewFriendRequest,
                    Text = fromUser.Fullname + " send you a friend request",
                    IsRead = false
                };

                _notificationRepository.Create(newNotification, toUserId);
            }
        }

        public void AcceptFriendRequest(int senderId, int receiverId)
        {
            var sender = _context.Accounts.FirstOrDefault(s => s.Id == senderId);
            var receiver = _context.Accounts.FirstOrDefault(r => r.Id == receiverId);
            Friend friendship = _context.Friends.FirstOrDefault(f => (f.FromUserId == senderId && f.ToUserId == receiverId)
                                                     || f.FromUserId == receiverId && f.ToUserId == senderId);

            if (sender != null && receiver != null
                && friendship != null
                && (friendship.StatusCode != FriendshipStatus.Accepted || friendship.StatusCode != FriendshipStatus.Error))
            {
                //accepting friend request
                friendship.ModifiedDate = DateTime.Now;
                friendship.ModifiedBy = receiver.Fullname;
                friendship.StatusCode = FriendshipStatus.Accepted;
                friendship.IsConfirmed = true;
                _context.SaveChanges();

                //creating notification for friend request sender account
                Notification acceptedNotification = new Notification
                {
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    AddedBy = "System",
                    ModifiedBy = "System",
                    Status = true,
                    SenderId = receiverId.ToString(), //int to string Static problem!
                    ReceiverId = senderId,
                    Type = NotificationType.FriendRequestAccepted,
                    Text = receiver.Fullname + " accepted your friend request",
                    IsRead = false
                };

                _context.Notifications.Add(acceptedNotification);
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
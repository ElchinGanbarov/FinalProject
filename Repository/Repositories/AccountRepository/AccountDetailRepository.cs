using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models;
using Repository.Services;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Repository.Repositories.AccountRepository
{
    public interface IAccountDetailRepository
    {
        SearchAccount GetDatasPublic(int currentAccountId, int accountId);
        SearchAccount GetDatasFriend(int friendId);
        SearchAccount GetDatasOwn(int accountId);
        AccountSocialLink GetAccountSocialLink(int id); //private friends
        AccountSocialLink GetPublicSocialLinks(int id); //public
        AccountPrivacy GetAccountPrivacy(int id);
        AccountSecurity GetAccountSecurity(int id);
        AccountSocialLink GetByAccountId(int id);
        void UpdateSocialLink(AccountSocialLink updatesociallink, AccountSocialLink socialLink);
        void UpdatePrivacy(int accountId, bool profileImg, bool statusText, bool lastSeen, bool phone, bool birthday, bool address, bool socials, bool acceptAllMessages);
        void UpdateSecurity(int accountId, bool tfa, bool loginAlerts);
        void ProfileImgUpload(int userId, string publicId);
        void DeletePhoto(string profileImg);
    }
    public class AccountDetailRepository : IAccountDetailRepository
    {
        private readonly MessengerDbContext _context;
        private readonly IFriendsRepository _friendsRepository;
        public AccountDetailRepository(MessengerDbContext context,
                                       IFriendsRepository friendsRepository)
        {
            _context = context;
            _friendsRepository = friendsRepository;
        }

        public SearchAccount GetDatasFriend(int friendId)
        {
            Account friend = _context.Accounts.Find(friendId);

            if (friend != null)
            {
                SearchAccount searchItem = new SearchAccount(); //result model

                searchItem.Id = friend.Id;
                searchItem.Friendship = FriendshipStatus.Accepted;
                searchItem.Label = friend.Fullname;
                searchItem.Img = friend.ProfileImg;
                searchItem.Email = friend.Email;
                searchItem.Phone = friend.Phone;
                searchItem.Birthday = friend.Birthday;
                searchItem.Address = friend.Address;
                searchItem.Website = friend.Website;
                searchItem.StatusText = friend.StatusText;

                AccountSocialLink accountSocials = _context.AccountSocialLinks.FirstOrDefault(a => a.AccountId == friendId);

                searchItem.Facebook = accountSocials.Facebook;
                searchItem.Twitter = accountSocials.Twitter;
                searchItem.Instagram = accountSocials.Instagram;
                searchItem.Linkedin = accountSocials.Linkedin;

                return searchItem;
            }

            return null;
        }

        public SearchAccount GetDatasPublic(int currentAccountId, int accountId)
        {
            Account account = _context.Accounts.Find(accountId);
            AccountPrivacy accountPrivacy = _context.AccountPrivacies.FirstOrDefault(p => p.AccountId == account.Id);
            
            if (accountPrivacy != null)
            {
                SearchAccount searchItem = new SearchAccount();
                //id
                searchItem.Id = account.Id;
                //fullname
                searchItem.Label = account.Fullname;
                //friendship

                //FriendshipStatus friendshipStatus = _friendsRepository.GetFriendshipStatus(currentAccountId, accountId);
                Friend friendship = _friendsRepository.GetFriendship(currentAccountId, accountId);

                if (friendship != null)
                {
                    if (friendship.StatusCode != FriendshipStatus.Error)
                    {
                        searchItem.Friendship = friendship.StatusCode;
                    }
                    if (friendship.FromUserId == currentAccountId)
                    {
                        searchItem.IsFriendRequestSender = true;
                    }
                }
                else
                {
                    //static for if error ocoured
                    searchItem.Friendship = FriendshipStatus.NotFriend;
                }

                //email
                searchItem.Email = account.Email; // static public
                //address
                if (accountPrivacy.Address == false)
                {
                    searchItem.Address = null;
                }
                else
                {
                    searchItem.Address = account.Address;
                }
                //website
                if (accountPrivacy.Website == false)
                {
                    searchItem.Website = null;
                }
                else
                {
                    searchItem.Website = account.Website;
                }
                //birthday
                if (accountPrivacy.Birthday == false)
                {
                    searchItem.Birthday = null;
                }
                else
                {
                    searchItem.Birthday = account.Birthday;
                }
                //phone
                if (accountPrivacy.Phone == false)
                {
                    searchItem.Phone = null;
                }
                else
                {
                    searchItem.Phone = account.Phone;
                }
                //profile img
                if (accountPrivacy.ProfileImg == false)
                {
                    searchItem.Img = null;
                }
                else
                {
                    searchItem.Img = account.ProfileImg;
                }
                //status text
                if (accountPrivacy.StatusText == false)
                {
                    searchItem.StatusText = null;
                }
                else
                {
                    searchItem.StatusText = account.StatusText;
                }
                //social links
                if (accountPrivacy.SocialLink == false)
                {
                    searchItem.Facebook = null;
                    searchItem.Twitter = null;
                    searchItem.Instagram = null;
                    searchItem.Linkedin = null;
                }
                else
                {
                    AccountSocialLink accountSocials = _context.AccountSocialLinks.FirstOrDefault(a => a.AccountId == accountId);

                    searchItem.Facebook = accountSocials.Facebook;
                    searchItem.Twitter = accountSocials.Twitter;
                    searchItem.Instagram = accountSocials.Instagram;
                    searchItem.Linkedin = accountSocials.Linkedin;
                }
                //if (accountPrivacy.AcceptAllMessages == false) //PROBLEM!!!
                //{
                //    
                //}

                return searchItem;
            }
            return null;
        }
        public SearchAccount GetDatasOwn(int accountId)
        {
            Account account = _context.Accounts.Find(accountId);

            if (account != null)
            {
                SearchAccount searchItem = new SearchAccount(); //result model

                searchItem.Id = account.Id;
                searchItem.Friendship = FriendshipStatus.OwnProfile;
                searchItem.Label = account.Fullname;
                searchItem.Img = account.ProfileImg;
                searchItem.Email = account.Email;
                searchItem.Phone = account.Phone;
                searchItem.Birthday = account.Birthday;
                searchItem.Address = account.Address;
                searchItem.Website = account.Website;
                searchItem.StatusText = account.StatusText;

                AccountSocialLink accountSocials = _context.AccountSocialLinks.FirstOrDefault(a => a.AccountId == account.Id);

                searchItem.Facebook = accountSocials.Facebook;
                searchItem.Twitter = accountSocials.Twitter;
                searchItem.Instagram = accountSocials.Instagram;
                searchItem.Linkedin = accountSocials.Linkedin;

                return searchItem;
            }

            return null;
        }

        public AccountSocialLink GetAccountSocialLink(int id)
        {
            return _context.AccountSocialLinks.Include(a => a.Account).FirstOrDefault(a => a.AccountId == id);
        }

        public AccountSocialLink GetPublicSocialLinks(int id)
        {
            Account account = _context.Accounts.Find(id);
            AccountPrivacy accountPrivacy = _context.AccountPrivacies.FirstOrDefault(p => p.AccountId == account.Id);
            AccountSocialLink accountSocial = _context.AccountSocialLinks.FirstOrDefault(s => s.AccountId == account.Id);
            if (accountPrivacy.SocialLink)
            {
                return accountSocial;
            }
            else
            {
                accountSocial.Facebook = null;
                accountSocial.Twitter = null;
                accountSocial.Instagram = null;
                accountSocial.Linkedin = null;

                return accountSocial;
            }
        }
        public AccountPrivacy GetAccountPrivacy(int id)
        {
            return _context.AccountPrivacies.Include(ap => ap.Account).FirstOrDefault(ap => ap.AccountId == id);
        }
        public AccountSecurity GetAccountSecurity(int id)
        {
            return _context.AccountSecurities.Include(s => s.Account).FirstOrDefault(s => s.AccountId == id);
        }
        public AccountSocialLink GetByAccountId(int id)
        {
            return _context.AccountSocialLinks.Include(a => a.Account).FirstOrDefault(a => a.AccountId == id);
        }
        public void UpdateSocialLink(AccountSocialLink updatesociallink, AccountSocialLink socialLink)
        {
            updatesociallink.Facebook = socialLink.Facebook;
            updatesociallink.Instagram = socialLink.Instagram;
            updatesociallink.Linkedin = socialLink.Linkedin;
            updatesociallink.Twitter = socialLink.Twitter;
            _context.SaveChanges();

        }
        public void UpdatePrivacy(int accountId, 
                                  bool profileImg, 
                                  bool statusText,
                                  bool lastSeen,
                                  bool phone, 
                                  bool birthday, 
                                  bool address, 
                                  bool socials,
                                  bool acceptAllMessages)
        {
            AccountPrivacy accountPrivacy = _context.AccountPrivacies.FirstOrDefault(p => p.AccountId == accountId);
            if (accountPrivacy != null)
            {
                accountPrivacy.ProfileImg = profileImg;
                accountPrivacy.StatusText = statusText;
                accountPrivacy.Phone = phone;
                accountPrivacy.LastSeen = lastSeen;
                accountPrivacy.Birthday = birthday;
                accountPrivacy.Address = address;
                accountPrivacy.SocialLink = socials;
                accountPrivacy.AcceptAllMessages = acceptAllMessages;

                _context.SaveChanges();
            }
        }

        public void UpdateSecurity(int accountId, bool tfa, bool loginAlerts)
        {
            AccountSecurity accountSecurity = _context.AccountSecurities.FirstOrDefault(p => p.AccountId == accountId);
            if (accountSecurity != null)
            {
                accountSecurity.TwoFactoryAuth = tfa;
                accountSecurity.LoginAlerts = loginAlerts;

                _context.SaveChanges();
            }
        }

        public void ProfileImgUpload(int userId, string publicId)
        {
            Account account = _context.Accounts.Find(userId);
            if (account != null)
            {
                account.ProfileImg = publicId;
                _context.SaveChanges();
            }
        }

        public void DeletePhoto(string profileImg)
        {
            var account = _context.Accounts.FirstOrDefault(p => p.ProfileImg == profileImg);
            account.ProfileImg = null;
            _context.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Repository.Repositories.AccountRepository
{
    public interface IAccountDetailRepository
    {
        Account GetPublicDatas(int accountId);
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
        public AccountDetailRepository(MessengerDbContext context)
        {
            _context = context;
        }

        public Account GetPublicDatas(int accountId)
        {
            Account account = _context.Accounts.Find(accountId);
            AccountPrivacy accountPrivacy = _context.AccountPrivacies.FirstOrDefault(p => p.AccountId == account.Id);
            
            if (accountPrivacy != null)
            {
                if (accountPrivacy.Address == false)
                {
                    account.Address = null;
                }
                if (accountPrivacy.Birthday == false)
                {
                    account.Birthday = null;
                }
                if (accountPrivacy.Phone == false)
                {
                    account.Phone = null;
                }
                if (accountPrivacy.ProfileImg == false)
                {
                    account.ProfileImg = null;
                }
                if (accountPrivacy.StatusText == false)
                {
                    account.StatusText = null;
                }
                //if (accountPrivacy.SocialLink == false)
                //{
                //    account.AccountSocialLinks.Clear();
                //}
                //if (accountPrivacy.AcceptAllMessages == false) //PROBLEM!!!
                //{
                //    account.AccountSocialLinks.Clear();
                //}

                Account resultAccount = new Account
                {
                    Id = account.Id,
                    Status = account.Status,
                    AddedDate = account.AddedDate,
                    ModifiedDate = account.ModifiedDate,
                    AddedBy = account.AddedBy,
                    ModifiedBy = account.ModifiedBy,
                    Name = account.Name,
                    Surname = account.Surname,
                    IsEmailVerified = account.IsEmailVerified,
                    Email = account.Email, //email is static public
                    Website = account.Website, //email is static public
                    Address = account.Address, //privacy
                    Birthday = account.Birthday, //privacy
                    Phone = account.Phone, //privacy
                    ProfileImg = account.ProfileImg, //privacy
                    StatusText = account.StatusText, //privacy
                    AccountSocialLinks = account.AccountSocialLinks, //privacy
                };

                return resultAccount;
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

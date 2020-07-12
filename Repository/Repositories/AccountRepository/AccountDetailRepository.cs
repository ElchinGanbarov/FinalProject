using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models;
using System.Linq;

namespace Repository.Repositories.AccountRepository
{
    public interface IAccountDetailRepository
    {
        AccountSocialLink GetAccountSocialLink(int id);
        AccountPrivacy GetAccountPrivacy(int id);
        AccountSocialLink GetByAccountId(int id);
        void UpdateSocialLink(AccountSocialLink updatesociallink, AccountSocialLink socialLink);
        void UpdatePrivacy(int accountId, bool profileImg, bool statusText, bool lastSeen, bool phone, bool birthday, bool address, bool socials, bool acceptAllMessages);
    }
    public class AccountDetailRepository : IAccountDetailRepository
    {
        private readonly MessengerDbContext _context;
        public AccountDetailRepository(MessengerDbContext context)
        {
            _context = context;
        }
        public AccountSocialLink GetAccountSocialLink(int id)
        {
            return _context.AccountSocialLinks.Include(a => a.Account).FirstOrDefault(a => a.AccountId == id);
        }
        public AccountPrivacy GetAccountPrivacy(int id)
        {
            return _context.AccountPrivacies.Include(ap => ap.Account).FirstOrDefault(ap => ap.AccountId == id);
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

    }
}

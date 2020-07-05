using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repositories.AccountRepository
{
    public interface IAccountDetailRepository
    {
        AccountSocialLink GetAccountSocialLink(int id);
        AccountSocialLink GetByAccountId(int id);
        void UpdateSocialLink(AccountSocialLink updatesociallink, AccountSocialLink socialLink);
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
    }
}

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
    }
}

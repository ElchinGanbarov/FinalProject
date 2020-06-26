using CryptoHelper;
using Repository.Data;
using Repository.Models;
using System;
using System.Linq;

namespace Repository.Repositories.AuthRepositories
{
    public interface IAuthRepository
    {
        Account Register(Account Account);
        Account Login(string email, string password);
        Account CheckByToken(string token);
        void UpdateToken(int id, string token);
        bool CheckEmail(string email);
    }
    public class AuthRepository : IAuthRepository
    {
        private readonly MessengerDbContext _context;
        public AuthRepository(MessengerDbContext context)
        {
            _context = context;
        }
        public Account CheckByToken(string token)
        {
            return _context.Accounts.FirstOrDefault(u => u.Token == token);
        }

        public bool CheckEmail(string email)
        {
            return _context.Accounts.Any(u => u.Email == email);
        }

        public Account Login(string email, string password)
        {
            Account Account = _context.Accounts.FirstOrDefault(u => u.Email == email);
            if (Account != null && Crypto.VerifyHashedPassword(Account.Password, password))
            {
                return Account;
            }
            return null;
        }
        public Account Register(Account Account)
        {
            Account.Password = Crypto.HashPassword(Account.Password);
            Account.AddedDate = DateTime.Now;
            Account.ModifiedDate = DateTime.Now;
            Account.AddedBy = "System";
            Account.ModifiedBy = "System";

            _context.Accounts.Add(Account);
            _context.SaveChanges();
            return Account;
        }
        public void UpdateToken(int id, string token)
        {
            Account Account = _context.Accounts.Find(id);
            Account.Token = token;
            _context.SaveChanges();
        }

    }
}

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
        Account GetByEmail(string email);
        Account GetByForgetToken(string forgetToken);
        void UpdateToken(int id, string token);
        bool CheckEmail(string email);
        bool CheckPasswordResetCode(int accountId, string passwordResetCode);
        bool CheckPhone(string phone);
        //remove AccountEmailVerficationCode
        void VerifyUserEmail(int accountId);
        void UpdateAccount(Account _user, Account user);
        bool UpdatePassword(int accountId, string password);
        void AddedSocial(AccountSocialLink accountSocialLink);
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

        //get user by email addres
        public Account GetByEmail(string email)
        {
            return _context.Accounts.FirstOrDefault(a => a.Email == email);
        }

        //get user by forget token
        public Account GetByForgetToken(string forgetToken)
        {
            return _context.Accounts.FirstOrDefault(a => a.ForgetToken == forgetToken);
        }

        public bool CheckEmail(string email)
        {
            return _context.Accounts.Any(u => u.Email == email);
        }

        //check account forget token
        public bool CheckPasswordResetCode(int accountId, string passwordResetCode)
        {
            if (string.IsNullOrEmpty(passwordResetCode)) return false;
            Account account = _context.Accounts.Find(accountId);
            if (account == null) return false;
            if (account.ResetPasswordCode == passwordResetCode)
            {
                return true;
            }
            return false;
        }

        public bool CheckPhone(string phone)
        {
            return _context.Accounts.Any(u => u.Phone == phone);
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

        public void UpdateAccount(Account _user, Account user)
        {
            _user.Name = user.Name;
            _user.Surname = user.Surname;
            _user.Email = user.Email;
            _user.Phone = user.Phone;
            _user.Website = user.Website;
            _user.Address = user.Address;
            _user.Birthday = user.Birthday;

            _context.SaveChanges();
        }

        public void UpdateToken(int id, string token)
        {
            Account Account = _context.Accounts.Find(id);
            Account.Token = token;
            _context.SaveChanges();
        }

        public void VerifyUserEmail(int accountId)
        {
            var account = _context.Accounts.Find(accountId);
            account.EmailActivationCode = "verified";
            account.IsEmailVerified = true;
            _context.SaveChanges();
        }

        //update account password
        public bool UpdatePassword(int accountId, string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            Account account = _context.Accounts.Find(accountId);
            if (account != null)
            {
                account.Password = Crypto.HashPassword(password);
                account.ModifiedDate = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void AddedSocial(AccountSocialLink accountSocialLink)
        {
             _context.AccountSocialLinks.Add(accountSocialLink);
            _context.SaveChanges();

        }
    }
}

using Repository.Data;
using Repository.Models;
using Repository.Repositories.AccountRepository;
using Repository.Repositories.AuthRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repositories.SignalRepository
{
    public interface ISignalrRepository
    {
        List<Account> GetUsersToChat();
    }

    public class SignalrRepository : ISignalrRepository
    {
        private readonly MessengerDbContext _context;
        private readonly IAuthRepository _authRepository;
        private readonly IFriendsRepository _friendsRepository;

        public SignalrRepository(MessengerDbContext context,
                                 IAuthRepository authRepository,
                                 IFriendsRepository friendsRepository)
        {
            _context = context;
            _authRepository = authRepository;
            _friendsRepository = friendsRepository;
        }

        public List<Account> GetUsersToChat()
        {
            var accounts = _context.Accounts.Where(a => a.IsOnline == false).ToList();

            return accounts;
        }
    }
}

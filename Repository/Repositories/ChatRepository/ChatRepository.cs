using Repository.Data;
using Repository.Models;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repositories.ChatRepository
{
    public interface IChatRepository
    {
        ICollection<AccountHubs> GetAllHubs(int userId);
    }

    public class ChatRepository : IChatRepository
    {
        private readonly MessengerDbContext _context;
        public ChatRepository(MessengerDbContext context)
        {
            _context = context;
        }

        public ICollection<AccountHubs> GetAllHubs(int userId)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.EntityFrameworkCore;
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
        ICollection<Hub> GetAccountHubsAll(int userId);

        bool CheckHubAccess(int userId, int hubId);
        ICollection<Message> GetHubMessagesAll(int hubId);
        ICollection<Message> GetAccountMessagesAll(int userId);


        AccountHubs TestCount(int userId);
    }

    public class ChatRepository : IChatRepository
    {
        private readonly MessengerDbContext _context;
        public ChatRepository(MessengerDbContext context)
        {
            _context = context;
        }

        public bool CheckHubAccess(int userId, int hubId)
        {
            var hub = _context.AccountHubs.FirstOrDefault(h => h.AccountId == userId && h.HubId == hubId);

            if (hub != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ICollection<Hub> GetAccountHubsAll(int userId)
        {
            var accountHubs = _context.AccountHubs.Include(h => h.Hub).Where(a => a.AccountId == userId).ToList();

            ICollection<Hub> resultHubs = new List<Hub>();

            foreach (var item in accountHubs)
            {
                var hubItem = _context.Hubs.Find(item.HubId);
                resultHubs.Add(hubItem);
            }

            return resultHubs;

        }

        public AccountHubs TestCount(int userId)
        {
            return _context.AccountHubs.FirstOrDefault(a => a.AccountId == 1);
        }

        public ICollection<Message> GetAccountMessagesAll(int userId)
        {
            return _context.Messages.Where(m => m.AccountId == userId).ToList();
        }

        public ICollection<Message> GetHubMessagesAll(int hubId)
        {
            return _context.Messages.Where(m => m.HubId == hubId).ToList();
        }
    }
}

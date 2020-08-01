using Microsoft.AspNetCore.SignalR;
using Repository.Data;
using Repository.Models;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repositories.SignalRepository
{
    public interface INotificationRepository
    {
        void Create(Notification notification, int accountId);
        List<Notification> GetUserNotifications(int userId);
    }
    public class NotificationRepository : INotificationRepository
    {
        private readonly MessengerDbContext _context;
        private readonly IHubContext<SignalServer> _hubContext;

        public NotificationRepository(MessengerDbContext context,
                                      IHubContext<SignalServer> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        //Create Notification
        public void Create(Notification notification, int accountId)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();

            //signalr
            _hubContext.Clients.All.SendAsync("displayNewFriendRequestNotification", "");
        }

        //Get Account's All Notifications
        public List<Notification> GetUserNotifications(int userId)
        {
            return _context.Notifications.Where(n => n.AccountId == userId && !n.IsRead && n.Status)
                                         .ToList();
        }
    }
}

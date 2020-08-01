using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class Notification : BaseEntity
    {
        public int AccountId { get; set; }
        public NotificationType Type { get; set; }
        public string Text { get; set; }
        public bool IsRead { get; set; }

        public Account Account { get; set; }
    }

    public enum NotificationType
    {
        NewFriendRequest = 1,
        FriendRequestAccepted = 2,
        FriendRequestRejected = 3
    }
}

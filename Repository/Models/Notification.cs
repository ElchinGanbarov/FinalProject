using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Models
{
    public class Notification : BaseEntity
    {
        public string SenderId { get; set; }

        [ForeignKey("Account")]
        public int ReceiverId { get; set; }
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

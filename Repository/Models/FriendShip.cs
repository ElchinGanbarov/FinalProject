using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class FriendShip:BaseEntity
    {
        public int AccountId { get; set; }
        public int AccountFriendId { get; set; }
        public Account Account { get; set; }
        public AccountFriend AccountFriend { get; set; }

    }
}

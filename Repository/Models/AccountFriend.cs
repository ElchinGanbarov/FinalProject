using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class AccountFriend : BaseEntity
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public ICollection<FriendShip> Friendships { get; set; }
    }
}

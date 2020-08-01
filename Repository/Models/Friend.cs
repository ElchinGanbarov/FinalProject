using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    public class Friend : BaseEntity
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public FriendshipStatus StatusCode { get; set; }
        public bool IsConfirmed { get; set; }
    }

    public enum FriendshipStatus
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2,
        NotFriend = 3,
        OwnProfile = 4,
        Error = 5 //system error
    }
}

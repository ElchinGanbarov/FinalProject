
namespace Repository.Models
{
    public class AccountPrivacy : BaseEntity
    {
        public int AccountId { get; set; }
        public bool AcceptAllMessages { get; set; }
        public bool Birthday { get; set; }
        public bool Website { get; set; }
        public bool Phone { get; set; }
        public bool Email { get; set; }
        public bool Address { get; set; }
        public bool ProfileImg { get; set; }
        public bool LastLogin { get; set; }
        public bool LastSeen { get; set; }
        public bool StatusText { get; set; }
        public bool SocialLink { get; set; }
        public Account Account { get; set; }
    }
}

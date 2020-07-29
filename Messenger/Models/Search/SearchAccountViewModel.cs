using Messenger.Models.AccountDetail;

namespace Messenger.Models.Search
{
    public class SearchAccountViewModel
    {
        public AccountDetailViewModel AccountDetail { get; set; }
        public AccountSocialLinkViewModel AccountSocials { get; set; }
        public bool IsFriends { get; set; }
    }
}

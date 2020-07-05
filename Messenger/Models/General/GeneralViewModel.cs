using Messenger.Models.AccountDetail;
using Repository.Models;


namespace Messenger.Models.General
{
    public class GeneralViewModel
    {
        public AccountDetailViewModel AccountDetailViewModel { get; set; }
        public AccountSocialLink AccountSocialLink { get; set; }
        public AccountSocialLinkViewModel AccountSocialLinkViewModel { get; set; }

    }
}

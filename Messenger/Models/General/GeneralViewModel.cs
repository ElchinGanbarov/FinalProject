using Messenger.Models.Account;
using Messenger.Models.AccountDetail;

namespace Messenger.Models.General
{
    public class GeneralViewModel
    {
        public UpdatePasswordViewModel UpdatePasswordViewModel { get; set; }
        public AccountDetailViewModel AccountDetailViewModel { get; set; }
        public AccountSocialLinkViewModel AccountSocialLinkViewModel { get; set; }
    }
}

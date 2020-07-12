using Messenger.Models.Account;
using Messenger.Models.AccountDetail;
using Messenger.Models.AccountPrivacySecurity;

namespace Messenger.Models.General
{
    public class GeneralViewModel
    {
        public UpdatePasswordViewModel UpdatePasswordViewModel { get; set; }
        public AccountDetailViewModel AccountDetailViewModel { get; set; }
        public AccountSocialLinkViewModel AccountSocialLinkViewModel { get; set; }
        public AccountPrivacyViewModel AccountPrivacyViewModel { get; set; }
    }
}

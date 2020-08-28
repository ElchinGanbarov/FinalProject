using Messenger.Models.Account;
using Messenger.Models.AccountDetail;
using Messenger.Models.AccountPrivacySecurity;
using Messenger.Models.Friend;
using Messenger.Models.Email;
using Repository.Models;
using System.Collections.Generic;
using Messenger.Models.Chat;

namespace Messenger.Models.General
{
    public class GeneralViewModel
    {
        public UpdatePasswordViewModel UpdatePasswordViewModel { get; set; }
        public AccountDetailViewModel AccountDetailViewModel { get; set; }
        public AccountSocialLinkViewModel AccountSocialLinkViewModel { get; set; }
        public AccountPrivacyViewModel AccountPrivacyViewModel { get; set; }
        public AccountSecurityViewModel AccountSecurityViewModel { get; set; }
        public FriendViewModel FriendViewModel { get; set; }
        public ICollection<AccountViewModel> AccountViewModels { get; set; }
        public InvitationEmailViewModel InvitationEmailViewModel { get; set; }
        public ICollection<AccountHubsViewModel> AccountHubsViewModel { get; set; }
        public ICollection<MessageViewModel> MessageViewModel { get; set; }

    }
}

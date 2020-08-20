using AutoMapper;
using Messenger.Models.Account;
using Messenger.Models.AccountDetail;
using Messenger.Models.AccountPrivacySecurity;
using Messenger.Models.Chat;
using Repository.Models;

namespace Messenger.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, Account>();
            CreateMap<Account, RegisterViewModel>();
            CreateMap<Account, AccountViewModel>();


            CreateMap<Account, AccountDetailViewModel>();
            CreateMap<AccountDetailViewModel, Account>();

            CreateMap<AccountSocialLink, AccountSocialLinkViewModel>();
            CreateMap<AccountSocialLinkViewModel, AccountSocialLink>();

            CreateMap<AccountPrivacy, AccountPrivacyViewModel>();
            CreateMap<AccountPrivacyViewModel, AccountPrivacy>();

            CreateMap<AccountSecurity, AccountSecurityViewModel>();
            CreateMap<AccountSecurityViewModel, AccountSecurity>();

            CreateMap<AccountHubs, AccountHubsViewModel>();
            CreateMap<AccountHubsViewModel, AccountHubs>();

        }
    }
}

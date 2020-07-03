using AutoMapper;
using Messenger.Models.Account;
using Messenger.Models.AccountDetail;
using Repository.Models;

namespace Messenger.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, Account>();
            CreateMap<Account, RegisterViewModel>();

            CreateMap<Account, AccountDetailViewModel>();
            CreateMap<AccountDetailViewModel, Account>();

            CreateMap<AccountSocialLink, AccountSocialLinkViewModel>();
            CreateMap<AccountSocialLinkViewModel, AccountSocialLink>();

        }
    }
}

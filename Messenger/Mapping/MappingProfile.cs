using AutoMapper;
using Messenger.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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



        }
    }
}

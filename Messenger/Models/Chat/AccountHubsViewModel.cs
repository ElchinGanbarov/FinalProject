using Messenger.Models.AccountDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.Models.Chat
{
    public class AccountHubsViewModel
    {
        public int AccountId { get; set; }
        public int HubId { get; set; }

        public AccountViewModel Account { get; set; }
        public HubViewModel Hub { get; set; }
    }
}

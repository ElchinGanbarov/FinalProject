using Messenger.Models.AccountDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.Models.Chat
{
    public class HubViewModel
    {
        public ICollection<MessageViewModel> Messages { get; set; }

        public ICollection<AccountViewModel> Accounts { get; set; }
    }
}

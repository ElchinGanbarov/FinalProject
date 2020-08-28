using Messenger.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.Models.AccountDetail
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string ProfileImg { get; set; }

        public ICollection<AccountHubsViewModel> AccountHubs { get; set; }
    }
}

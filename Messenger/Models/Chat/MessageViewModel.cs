using Messenger.Models.AccountDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.Models.Chat
{
    public class MessageViewModel
    {
        public int HubId { get; set; }
        public int AccountId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(300)]
        public string Text { get; set; }

        public HubViewModel Hub { get; set; }
        public AccountViewModel Account { get; set; }

        //public ICollection<AccountFavMessages> AccountFavMessages { get; set; }
    }
}

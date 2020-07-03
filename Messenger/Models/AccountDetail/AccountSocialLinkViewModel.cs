using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.Models.AccountDetail
{
    public class AccountSocialLinkViewModel
    {
        public int AccountId { get; set; }
        [MaxLength(100)]
        public string Facebook { get; set; }

        [MaxLength(100)]
        public string Twitter { get; set; }

        [MaxLength(100)]
        public string Instagram { get; set; }

        [MaxLength(100)]
        public string Linkedin { get; set; }
    }
}

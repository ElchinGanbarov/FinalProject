using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.Models.AccountDetail
{
    public class AccountSocialLinkViewModel
    {
        [MaxLength(100)]
        [Required]
        public string Facebook { get; set; }

        [MaxLength(100)]
        [Required]
        public string Twitter { get; set; }

        [MaxLength(100)]
        [Required]
        public string Instagram { get; set; }

        [MaxLength(100)]
        [Required]
        public string Linkedin { get; set; }
    }
}

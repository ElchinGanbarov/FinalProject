using System;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class AccountSocialLink : BaseEntity
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

        public Account Account { get; set; }
    }
}

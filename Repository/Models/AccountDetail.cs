using System;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class AccountDetail : BaseEntity
    {
        public int AccountId { get; set; }
        public DateTime Birthday { get; set; }

        [MaxLength(100)]
        public string Website { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string ProfileImg { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LastSeen { get; set; }

        [MaxLength(50)]
        public string StatusText { get; set; }

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

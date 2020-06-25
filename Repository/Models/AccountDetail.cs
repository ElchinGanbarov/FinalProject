using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class AccountDetail : BaseEntity
    {
        public int AccountId { get; set; }
        public DateTime Birthday { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string ProfileImg { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LastSeen { get; set; }
        public string StatusText { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Linkedin { get; set; }

        public Account Account { get; set; }
    }
}

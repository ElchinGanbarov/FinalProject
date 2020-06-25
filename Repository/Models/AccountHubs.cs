using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class AccountHubs : BaseEntity
    {
        public int AccountId { get; set; }
        public int HubId { get; set; }

        public Account Account { get; set; }
        public Hub Hub { get; set; }
    }
}

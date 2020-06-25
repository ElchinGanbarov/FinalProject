using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class AccountSecurity : BaseEntity
    {
        public int AccountId { get; set; }
        public bool TwoFactoryAuth { get; set; }
        public bool LoginAlerts { get; set; }

        public Account Account { get; set; }
    }
}

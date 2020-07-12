using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.Models.AccountPrivacySecurity
{
    public class AccountSecurityViewModel
    {
        public bool TwoFactoryAuth { get; set; }
        public bool LoginAlerts { get; set; }
    }
}

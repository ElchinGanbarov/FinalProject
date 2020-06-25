using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class Account : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string ForgetToken { get; set; }
        public bool IsOnline { get; set; }
        public int AccountDetailId { get; set; }
        public int AccountPrivacyId { get; set; }
        public int AccountSecurityId { get; set; }

        public AccountDetail Detail { get; set; }
        public AccountPrivacy Privacy { get; set; }
        public AccountSecurity Security { get; set; }

        public ICollection<AccountHubs> AccountHubs { get; set; }
    }
}

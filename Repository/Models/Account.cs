using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class Account : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(15)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string Token { get; set; }

        [Required]
        [MaxLength(100)]
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

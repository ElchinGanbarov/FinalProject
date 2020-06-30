using System;
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
        public DateTime? Birthday { get; set; }
        [MaxLength(100)]
        public string ProfileImg { get; set; }
        [MaxLength(100)]
        public string Token { get; set; }
        [MaxLength(100)]
        public string ForgetToken { get; set; }
        public bool IsEmailVerified { get; set; }
        [MaxLength(100)]
        public string EmailActivationCode { get; set; }
        [MaxLength(50)]
        public string Address { get; set; }
        [MaxLength(100)]
        public string Website { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastSeen { get; set; }

        [MaxLength(50)]
        public string StatusText { get; set; }

        public bool IsOnline { get; set; }

        public ICollection<AccountSocialLink> AccountSocialLinks { get; set; }
        public ICollection<AccountPrivacy> Privacy { get; set; }
        public ICollection<AccountSecurity> Security { get; set; }

        public ICollection<AccountHubs> AccountHubs { get; set; }
        public ICollection<AccountFavMessages> AccountFavMessages { get; set; }
        public ICollection<GroupMembers> GroupMembers { get; set; }
    }
}

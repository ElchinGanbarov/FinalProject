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

        [MaxLength(100)]
        public string Token { get; set; }

        [MaxLength(100)]
        public string ForgetToken { get; set; }

        public bool IsEmailVerified { get; set; }
        public Guid EmailActivationCode { get; set; }

        public bool IsOnline { get; set; }

        public ICollection<AccountDetail> Detail { get; set; }
        public ICollection<AccountPrivacy> Privacy { get; set; }
        public ICollection<AccountSecurity> Security { get; set; }

        public ICollection<AccountHubs> AccountHubs { get; set; }
        public ICollection<AccountFavMessages> AccountFavMessages { get; set; }
        public ICollection<GroupMembers> GroupMembers { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class Message : BaseEntity
    {
        public int HubId { get; set; }
        public int AccountId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(300)]
        public string Text { get; set; }

        public Hub Hub { get; set; }
        public Account Account { get; set; }
        public ICollection<AccountFavMessages> AccountFavMessages { get; set; }
    }
}

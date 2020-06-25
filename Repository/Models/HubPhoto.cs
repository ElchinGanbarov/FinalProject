using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Models
{
    public class HubPhoto : BaseEntity
    {
        public int HubId { get; set; }
        public int AccountId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Image { get; set; }

        public Hub Hub { get; set; }
        public Account Account { get; set; }
    }
}

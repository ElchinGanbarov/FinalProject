using System;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        [MaxLength(100)]
        public string CreatedBy { get; set; }

        [MaxLength(100)]
        public string ModifiedBy { get; set; }
    }
}

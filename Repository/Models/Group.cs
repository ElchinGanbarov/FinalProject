using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class Group : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Photo { get; set; }
        public int HubId { get; set; }

        public Hub Hub { get; set; }
    }
}

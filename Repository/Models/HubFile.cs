using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class HubFile : BaseEntity
    {
        public int HubId { get; set; }
        public int AccountId { get; set; }

        [Required]
        [MaxLength(100)]
        public string File { get; set; }

        [Required]
        [MaxLength(50)]
        public string FileType { get; set; }

        public Hub Hub { get; set; }
        public Account Account { get; set; }
    }
}

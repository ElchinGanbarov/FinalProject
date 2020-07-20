using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    public class Friend : BaseEntity
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public StatusCode StatusCode { get; set; }
        public bool IsConfirmed { get; set; }
    }

    public enum StatusCode
    {
        Pending = 0,
        Accepted = 1,
        Declined = 2
        //Blocked = 3
    }
}

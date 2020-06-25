
namespace Repository.Models
{
    public class AccountFavMessages : BaseEntity
    {
        public int AccountId { get; set; }
        public int MessageId { get; set; }

        public Account Account { get; set; }
        public Message Message { get; set; }
    }
}

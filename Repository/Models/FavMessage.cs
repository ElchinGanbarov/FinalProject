using System.Collections.Generic;

namespace Repository.Models
{
    public class FavMessage : BaseEntity
    {
        public ICollection<Message> Messages { get; set; }

        public ICollection<Account> Account { get; set; }
    }
}

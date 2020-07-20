using System.Collections.Generic;

namespace Repository.Models
{
    public class Hub : BaseEntity
    {
        public ICollection<Message> Messages { get; set; }

        public ICollection<AccountHubs> AccountHubs { get; set; }
    }
}

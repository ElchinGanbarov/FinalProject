using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class FavMessage : BaseEntity
    {
        public ICollection<Message> Messages { get; set; }

        public ICollection<Account> Account { get; set; }
    }
}

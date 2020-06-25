using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class Hub : BaseEntity
    {
        public ICollection<Message> Messages { get; set; }

        public ICollection<AccountHubs> AccountHubs { get; set; }
    }
}

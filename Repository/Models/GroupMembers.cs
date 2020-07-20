using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class GroupMembers : BaseEntity
    {
        public int AccountId { get; set; }
        public int GroupAccountId { get; set; }

        public Account Account { get; set; }
        public GroupAccount GroupAccount { get; set; }
    }
}

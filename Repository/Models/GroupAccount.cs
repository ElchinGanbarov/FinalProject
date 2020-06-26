using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class GroupAccount : BaseEntity
    {
        public int GroupId { get; set; }
        public ICollection<GroupMembers> GroupMembers { get; set; }

        public Group Group { get; set; }
    }
}

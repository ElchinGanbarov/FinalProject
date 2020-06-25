using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class Message : BaseEntity
    {
        public int HubId { get; set; }
        public int AccountId { get; set; }
        public string Text { get; set; }

        public Hub Hub { get; set; }
        public Account Account { get; set; }
    }
}

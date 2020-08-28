using Newtonsoft.Json;
using System.Collections.Generic;

namespace Repository.Models
{
    public class Hub : BaseEntity
    {
        public ICollection<Message> Messages { get; set; }

        
        public ICollection<AccountHubs> AccountHubs { get; set; }

        public HubType HubType { get; set; }
    }

    public enum HubType
    {
        DirectHub = 1,
        GroupHub = 2
    }
}
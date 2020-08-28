
using Newtonsoft.Json;

namespace Repository.Models
{
    public class AccountHubs : BaseEntity
    {
        public int AccountId { get; set; }
        public int HubId { get; set; }

        [JsonIgnore]
        public Account Account { get; set; }

        [JsonIgnore]
        public Hub Hub { get; set; }
    }
}

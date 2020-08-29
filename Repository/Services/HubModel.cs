using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Services
{
    public class HubModel
    {
        public int UserId{ get; set; }
        public string UseFullname{ get; set; }
        public string UserImg{ get; set; }
        public int HubId{ get; set; }

    }
}

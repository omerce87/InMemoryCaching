using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisApp.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDesc { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

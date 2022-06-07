using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace The_Bank_of_Cardinal.Models
{
    public class UserAccount
    {
        [Key]
        public string UserName { get; set; }
        public string RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Bank_of_Cardinal.Models
{
    public class UserRoleModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}

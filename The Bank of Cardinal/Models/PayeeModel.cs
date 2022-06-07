using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace The_Bank_of_Cardinal.Models
{
    public class PayeeModel
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Payee { get; set; }
        public string PhoneNumber { get; set; }

        public string Email { get; set; }


    }
}

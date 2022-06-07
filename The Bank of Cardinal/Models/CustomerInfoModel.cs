using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Bank_of_Cardinal.Models
{
    public class CustomerInfoModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public double AccountBalance { get; set; }  

    }
}

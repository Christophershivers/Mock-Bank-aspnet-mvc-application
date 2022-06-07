using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Bank_of_Cardinal.Models
{
    public class BillPayModel
    {
        public string Company { get; set; }
        public string AccountNumber { get; set; }
        public double Amount { get; set; }
    }
}

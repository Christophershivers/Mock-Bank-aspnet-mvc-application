using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using The_Bank_of_Cardinal.Areas.Identity.Data;

namespace The_Bank_of_Cardinal.Models
{
    public class DepositModel
    {
        [Key]
        public string Id { get; set; }
        public double AccountBalance { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }


    }
}

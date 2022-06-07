using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Bank_of_Cardinal.Models
{
    public class CustomerInfoConnection : DbContext
    {
        public CustomerInfoConnection(DbContextOptions<CustomerInfoConnection> options) : base (options)
        {

        }
        public DbSet<CustomerInfoModel> AspNetUsers { get; set; }
    }
}

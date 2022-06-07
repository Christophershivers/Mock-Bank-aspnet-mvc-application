using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Bank_of_Cardinal.Models
{
    public class DepositConnection : DbContext
    {
        public DepositConnection(DbContextOptions<DepositConnection> options) : base(options)

        {

        }

        public DbSet<DepositModel> AspNetUsers { get; set; }

    }
}

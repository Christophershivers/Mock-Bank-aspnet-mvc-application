using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Bank_of_Cardinal.Models
{
    public class TransactionConnection : DbContext
    {
        public TransactionConnection(DbContextOptions<TransactionConnection> options) : base(options)
        {

        }
        public DbSet<TransactionModel> TransactionHistory { get; set; }
    }
}

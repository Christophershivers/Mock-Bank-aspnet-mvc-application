using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Bank_of_Cardinal.Models
{
    public class UserAccountConnection : DbContext
    {
        public UserAccountConnection(DbContextOptions<UserAccountConnection> options) : base (options)
        {

        }

        public DbSet<UserAccount> AccountInfo { get; set; }
    }
}

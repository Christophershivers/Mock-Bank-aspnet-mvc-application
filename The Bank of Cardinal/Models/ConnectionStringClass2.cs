using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Bank_of_Cardinal.Models
{
    public class ConnectionStringClass2 : DbContext
    {
        public ConnectionStringClass2(DbContextOptions<ConnectionStringClass2> options) : base(options)

        {

        }

        public DbSet<UserInfo> AspNetUsers { get; set; }

    }
}


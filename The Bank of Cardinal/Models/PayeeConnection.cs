﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Bank_of_Cardinal.Models
{
    public class PayeeConnection : DbContext
    {
        public PayeeConnection(DbContextOptions<PayeeConnection> options) : base(options)

        {

        }

        public DbSet<PayeeModel> Payee { get; set; }

    }
}

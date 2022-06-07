
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace The_Bank_of_Cardinal.Models
{
    public class TransactionModel
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }
        public string Amount { get; set; }
        public DateTime Date { get; set; }

    }
}

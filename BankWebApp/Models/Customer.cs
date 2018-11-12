using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebApp.Models
{
    public class Customer
    {
        public int CustomerNumber { get; set; } 
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }
    }
}

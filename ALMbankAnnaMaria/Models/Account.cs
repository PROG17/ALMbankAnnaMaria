using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALMbankAnnaMaria.Models
{
    public class Account
    {
        public Account(int id, decimal balance, Customer accountHolder)
        {
            Id = id;
            Balance = balance;
            AccountHolder = accountHolder;
        }

        public int Id { get; set; }
        public decimal Balance { get; set; }
        public Customer AccountHolder { get; set; }
    }
}

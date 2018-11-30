using ALMbankAnnaMaria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ALMbankAnnaMaria.ViewModels
{
    public class BankAppViewModel
    {
        public BankAppViewModel()
        {
            Message = "";
        }

        public int AccountNumber { get; set; }        
        public decimal Ammount { get; set; }
        public string Message { get; set; }
        public decimal Balance { get; set; }        
    }
}

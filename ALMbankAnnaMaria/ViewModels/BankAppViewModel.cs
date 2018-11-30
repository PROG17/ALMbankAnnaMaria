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
        [Required]
        [RegularExpression("([0-9]+)")]
        public int AccountNumber { get; set; }
        [Required]
        public decimal Ammount { get; set; }

        public string Message { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALMbankAnnaMaria.ViewModels
{
    public class TransferViewModel
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Sum { get; set; }
        public string Message { get; set; } = string.Empty;
        public decimal ToAccountBalance { get; set; }
        public decimal FromAccountBalance { get; set; }
    }
}

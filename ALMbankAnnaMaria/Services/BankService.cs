using ALMbankAnnaMaria.Models;
using ALMbankAnnaMaria.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALMbankAnnaMaria.Services
{
    public class BankService
    {
        private readonly IBankRepository _bankRepository;

        public BankService(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _bankRepository.GetAllCustomers();
        }
        private Account GetAccount(int bankKontoNummer)
        {
            return GetAllCustomers().SelectMany(r => r.Accounts).FirstOrDefault(a => a.Id == bankKontoNummer);
        }
    }
}

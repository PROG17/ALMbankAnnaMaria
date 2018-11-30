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
        public bool Deposit(int bankKontoNummer, decimal belopp)
        {
            var account = GetAccount(bankKontoNummer);
            if (belopp == 0) return false;
            if (account != null)
            {
                account.Balance += belopp;               
                return true;
            }
            return false;            
        }

        public bool Withdrawal(int bankKontoNummer, decimal belopp)
        {
            var account = GetAccount(bankKontoNummer);
            if (account == null) return false;
            if (belopp == 0) return false;
            if (belopp > account.Balance) return false;
            
            account.Balance -= belopp;
            return true;
        }

        public Account GetAccount(int bankKontoNummer)
        {
            return GetAllCustomers().SelectMany(r => r.Accounts).FirstOrDefault(a => a.Id == bankKontoNummer);
        }
    }
}
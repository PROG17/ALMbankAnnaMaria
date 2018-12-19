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

        public bool Transfer(int fromAccountId, int toAccountId, decimal sum, out string message)
        {
            if(sum <= 0)
            {
                message = "Summan måste vara högre än 0.";
                return false;
            }

            var fromAccount = GetAccount(fromAccountId);
            if(fromAccount == null)
            {
                message = "Från-kontonumret finns inte, kontrollera att siffrorna stämmer.";
                return false;
            }

            var toAccount = GetAccount(toAccountId);
            if (toAccount == null)
            {
                message = "Till-kontonumret finns inte, kontrollera att siffrorna stämmer.";
                return false;
            }

            var isWithdrawn = Withdrawal(fromAccount.Id, sum);
            if (!isWithdrawn)
            {
                message = $"Det fanns inte tillräckligt med pengar på konto med kontonummer {fromAccountId}";
                return false;
            }

            var isDeposit = Deposit(toAccountId, sum);

            if (!isDeposit)
            {
                message = "Någonting gick fel när pengar skulle överföras. Prova igen senare.";
            }

            message = $"{sum.ToString("C")} har överförts från konto {fromAccountId} till konto {toAccountId}";
            return true;
        }

        public Account GetAccount(int bankKontoNummer)
        {
            return GetAllCustomers().SelectMany(r => r.Accounts).FirstOrDefault(a => a.Id == bankKontoNummer);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALMbankAnnaMaria.Models;

namespace ALMbankAnnaMaria.Repos
{
    public interface IBankRepository
    {
        IEnumerable<Account> GetAllAccounts();
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomer(int id);

        decimal Deposit();
        decimal Withdrawal();
    }
}

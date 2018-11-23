using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALMbankAnnaMaria.Models;

namespace ALMbankAnnaMaria.Repos
{
    public class BankRepository : IBankRepository
    {
        private static List<Customer> listOfCustomers;
        public Customer GetCustomer(int id)
        {
            return GetCustomers().FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return GetCustomers();
        }
        public IEnumerable<Account> GetAllAccounts()
        {
            return GetCustomers().SelectMany(c => c.Accounts);
        }


        private List<Customer> GetCustomers()
        {
            if (listOfCustomers == null)
            {
                var cust1 = new Customer(1, "Anna-Maria");
                var cust2 = new Customer(2, "Bechuchi");
                var cust3 = new Customer(3, "Blom");

                var cust1Account1 = new Account(1, 1000, cust1);
                var cust1Account2 = new Account(2, 2000, cust1);
                var cust1Account3 = new Account(3, 3000, cust1);

                var cust2Account1 = new Account(4, 10200, cust2);
                var cust2Account2 = new Account(5, 20200, cust2);
                var cust2Account3 = new Account(6, 30200, cust2);

                var cust3Account1 = new Account(7, 100300, cust3);
                var cust3Account2 = new Account(8, 200300, cust3);
                var cust3Account3 = new Account(9, 300300, cust3);

                cust1.Accounts.Add(cust1Account1);
                cust1.Accounts.Add(cust1Account2);
                cust1.Accounts.Add(cust1Account3);

                cust2.Accounts.Add(cust2Account1);
                cust2.Accounts.Add(cust2Account2);
                cust2.Accounts.Add(cust2Account3);

                cust3.Accounts.Add(cust3Account1);
                cust3.Accounts.Add(cust3Account2);
                cust3.Accounts.Add(cust3Account3);

                listOfCustomers = new List<Customer>()
                {
                    cust1,
                    cust2,
                    cust3
                };
            }
            return listOfCustomers;
        }
    }
}

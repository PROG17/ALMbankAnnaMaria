using ALMbankAnnaMaria.Models;
using ALMbankAnnaMaria.Repos;
using ALMbankAnnaMaria.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ALMbankAnnaMaria.Tests
{
    [TestClass]
    public class BankServiceTests
    {
        private BankService _bankService;

        [TestMethod]
        public void AfterDepositCorrectNewSaldoShouldBeSet()
        {
            _bankService = new BankService(new BankRepository());
            Customer customer = _bankService.GetAllCustomers().First();
            Account accountToTest = customer.Accounts.First();
            var balanceBefore = accountToTest.Balance;
            _bankService.Deposit(accountToTest.Id, 100);
        }

        [TestMethod]
        public void AfterWithdrawalCorrectNewSaldoShouldBeSet()
        {
            _bankService = new BankService(new BankRepository());
            Customer customer = _bankService.GetAllCustomers().First();
            Account accountToTest = customer.Accounts.First();
            var balanceBefore = accountToTest.Balance;
            _bankService.Withdrawal(accountToTest.Id, 100);

            Assert.AreEqual(balanceBefore - 100, accountToTest.Balance);
        }


        [TestMethod]
        public void CantWithdrawMoreThanBalance()
        {
            _bankService = new BankService(new BankRepository());
            Customer customer = _bankService.GetAllCustomers().First();
            Account accountToTest = customer.Accounts.First();
            var balanceBefore = accountToTest.Balance;
            _bankService.Withdrawal(accountToTest.Id, balanceBefore + 1);

            Assert.AreEqual(balanceBefore, accountToTest.Balance);
        }
    }
}

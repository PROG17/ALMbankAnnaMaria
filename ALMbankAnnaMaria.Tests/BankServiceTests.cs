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

        [TestMethod]
        public void Transfer_Between_Accounts_Are_Successful()
        {
            // Arrange
            _bankService = new BankService(new BankRepository());
            var fromCustomer = _bankService.GetAllCustomers().First();
            var toCustomer = _bankService.GetAllCustomers().Last();
            var fromAccount = fromCustomer.Accounts.First();
            var toAccount = toCustomer.Accounts.First();
            decimal sum = 500;
            var expectedBalanceOnFromAccount = fromAccount.Balance - sum;
            var expectedBalanceOnToAccont = toAccount.Balance + sum;

            // Act
            var isSuccess = _bankService.Transfer(fromAccount.Id, toAccount.Id, sum, out string message);

            // Assert
            Assert.AreEqual(expectedBalanceOnFromAccount, fromAccount.Balance);
            Assert.AreEqual(expectedBalanceOnToAccont, toAccount.Balance);
        }

        [TestMethod]
        public void Transfer_Cannot_Be_Made_When_Insufficient_Funds()
        {
            // Arrange
            _bankService = new BankService(new BankRepository());
            var fromCustomer = _bankService.GetAllCustomers().First();
            var toCustomer = _bankService.GetAllCustomers().Last();
            var fromAccount = fromCustomer.Accounts.First();
            var toAccount = toCustomer.Accounts.First();
            decimal sum = fromAccount.Balance + 1;
            var expectedBalanceOnFromAccount = fromAccount.Balance;
            var expectedBalanceOnToAccont = toAccount.Balance;

            // Act
            var isSuccess = _bankService.Transfer(fromAccount.Id, toAccount.Id, sum, out string message);

            // Assert
            Assert.IsFalse(isSuccess);
            Assert.AreEqual(expectedBalanceOnFromAccount, fromAccount.Balance);
            Assert.AreEqual(expectedBalanceOnToAccont, toAccount.Balance);
        }
    }
}

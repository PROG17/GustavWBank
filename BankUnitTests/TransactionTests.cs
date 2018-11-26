using System.Collections.Generic;
using System.Linq;
using BankWebApp.DAL;
using BankWebApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankUnitTests
{
    [TestClass]
    public class TransactionTests
    {
        public BankRepository _bankRepo;

        public TransactionTests()
        {
            _bankRepo = BankRepository.Instance();
        }

        [TestMethod]
        public void OverDrawAccount()
        {
            var newCustomer = new Customer() {CustomerNumber = 0, Name = "Test Kalle"};
            var newAccount = new Account() {AccountNumber = 0, Balance = 100};

            newCustomer.Accounts = new List<Account>(){newAccount};

            _bankRepo._customersWithAccounts.Add(newCustomer);

            var result =_bankRepo.Withdraw(0, 1000);

            //Test that the result we get back is correct
            Assert.AreEqual("Uttaget är större än saldot på kontot", result);

            //Test that no funds have been withdrawn from the account
            Assert.AreEqual(_bankRepo._customersWithAccounts.SingleOrDefault(x=>x.CustomerNumber==0).Accounts.SingleOrDefault(c=>c.AccountNumber == 0).Balance, 100);

            _bankRepo._customersWithAccounts.Remove(newCustomer);
        }

        [TestMethod]
        public void WithdrawAmount()
        {
            var newCustomer = new Customer() { CustomerNumber = 0, Name = "Test Kalle" };
            var newAccount = new Account() { AccountNumber = 0, Balance = 100 };

            newCustomer.Accounts = new List<Account>() { newAccount };

            _bankRepo._customersWithAccounts.Add(newCustomer);

            var result = _bankRepo.Withdraw(0, 50);
            //Test that the result we get back is correct
            Assert.AreEqual("Lyckades ta ut 50kr. Nytt saldo: 50kr", result);

            //Test that funds have been withdrawn from the account
            Assert.AreEqual(_bankRepo._customersWithAccounts.SingleOrDefault(x => x.CustomerNumber == 0).Accounts.SingleOrDefault(c => c.AccountNumber == 0).Balance, 50);
            _bankRepo._customersWithAccounts.Remove(newCustomer);
        }

        [TestMethod]
        public void DepositAmount()
        {
            var newCustomer = new Customer() { CustomerNumber = 0, Name = "Test Kalle" };
            var newAccount = new Account() { AccountNumber = 0, Balance = 100 };

            newCustomer.Accounts = new List<Account>() { newAccount };

            _bankRepo._customersWithAccounts.Add(newCustomer);

            var result = _bankRepo.Deposit(0, 50);
           
            //Test that the result we get back is correct
            Assert.AreEqual("Lyckades med insättningen. Nytt saldo: 150kr", result);

            //Test that funds have been deposited to the account
            Assert.AreEqual(_bankRepo._customersWithAccounts.SingleOrDefault(x => x.CustomerNumber == 0).Accounts.SingleOrDefault(c => c.AccountNumber == 0).Balance, 150);
            _bankRepo._customersWithAccounts.Remove(newCustomer);
        }
        [TestMethod]
        public void VerifyAmount()
        {
            var bankRepo = BankRepository.Instance();

            int sum = 1;
            var accounts = bankRepo.GetAllCustomersAndAccounts();
            var expectedBalanceFrom = accounts[0].Accounts[0].Balance - sum;
            var expecedBalanceTo = accounts[1].Accounts[0].Balance + sum;

            bankRepo.Transfer(accounts[0].Accounts[0].AccountNumber, accounts[1].Accounts[0].AccountNumber, sum);
            Assert.AreEqual(expectedBalanceFrom, accounts[0].Accounts[0].Balance);
            Assert.AreEqual(expecedBalanceTo, accounts[1].Accounts[0].Balance);
        }
    }
}

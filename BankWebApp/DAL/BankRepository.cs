using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankWebApp.Models;

namespace BankWebApp.DAL
{
    public class BankRepository
    {
        private List<Customer> _customersWithAccounts;

        public BankRepository()
        {
            _customersWithAccounts = CreateFakeCustomersAndAccounts();
        }
        public List<Customer> GetAllCustomersAndAccounts()
        {
            return _customersWithAccounts;
        }

        public string Transfer(int withdraw_account, int deposit_account, decimal amount)
        {
            
            var withdrawAccount = _customersWithAccounts.SelectMany(sublist => sublist.Accounts)
                .SingleOrDefault(account => account.AccountNumber == withdraw_account);
            var depositAccount = _customersWithAccounts.SelectMany(sublist => sublist.Accounts)
                .SingleOrDefault(account => account.AccountNumber == deposit_account);
            if (withdrawAccount == null)
                return "Kontot du vill göra uttag från finns inte";
            
            if (depositAccount == null)
                return "Kontot du vill göra insättning till finns inte";

            if (amount > withdrawAccount.Balance)
                return "Kontot du vill överföra från har inte nog med pengar på kontot för att utföra överföringen";
           
            withdrawAccount.Balance -= amount;
            depositAccount.Balance += amount;

            return "Lyckades med överföringen";
        }

        private List<Customer> CreateFakeCustomersAndAccounts()
        {
            var customer1 = new Customer() {CustomerNumber = 1, Name = "Kalle Anka"};
            var customer2 = new Customer() {CustomerNumber = 2, Name = "Musse Pigg"};
            var customer3 = new Customer() {CustomerNumber = 3, Name = "Optimus Prime"};

            var account1 = new Account() {AccountNumber = 1, Balance = 10000};
            var account2 = new Account() { AccountNumber = 2, Balance = 20000 };
            var account3 = new Account() { AccountNumber = 3, Balance = 30000 };
            var account4 = new Account() { AccountNumber = 4, Balance = 40000 };
            var account5 = new Account() { AccountNumber = 5, Balance = 50000 };


            customer1.Accounts = new List<Account>() {account1, account2};
            customer2.Accounts = new List<Account>() { account3, account4 };
            customer3.Accounts = new List<Account>() { account5 };
            ;

            return new List<Customer>(){customer1,customer2,customer3};

        }
    }
}

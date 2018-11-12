using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankWebApp.Models;

namespace BankWebApp.DAL
{
    public class BankRepository
    {
        public List<Customer> _customersWithAccounts;
        private static BankRepository _instance;

        private BankRepository()
        {
            _customersWithAccounts = CreateFakeCustomersAndAccounts();
        }

        public static BankRepository Instance()
        {
            if(_instance == null)
                _instance = new BankRepository();
            return _instance;
        }
        public List<Customer> GetAllCustomersAndAccounts()
        {
            return _customersWithAccounts;
        }

        public string Deposit(int depositAccountNumber, decimal amount)
        {
            var depositAccount = GetAccount(depositAccountNumber);

            if (depositAccount == null)
                return "Kontot finns inte";

            if (amount < 0)
                return "Kan ej sätta in en negativ summa";

            depositAccount.Balance += amount;
            return $"Lyckades med insättningen. Nytt saldo: {depositAccount.Balance}kr";
        }

        public string Withdraw(int withdrawAccountNumber, decimal amount)
        {
            var withdrawAccount = GetAccount(withdrawAccountNumber);
            if (withdrawAccount == null)
                return "Kontot finns inte";
            if (withdrawAccount.Balance < amount)
                return "Uttaget är större än saldot på kontot";
            withdrawAccount.Balance -= amount;
            return $"Lyckades ta ut {amount}kr. Nytt saldo: {withdrawAccount.Balance}kr";
        }

        public string Transfer(int withdrawAccountNumber, int depositAccountNumber, decimal amount)
        {
            var withdrawAccount = GetAccount(withdrawAccountNumber);
            var depositAccount = GetAccount(depositAccountNumber);
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

        private Account GetAccount(int accountNubmer)
        {
            return _customersWithAccounts.SelectMany(sublist => sublist.Accounts)
                .SingleOrDefault(account => account.AccountNumber == accountNubmer);
        }

        private List<Customer> CreateFakeCustomersAndAccounts()
        {
            var customer1 = new Customer() { CustomerNumber = 1, Name = "Kalle Anka" };
            var customer2 = new Customer() { CustomerNumber = 2, Name = "Musse Pigg" };
            var customer3 = new Customer() { CustomerNumber = 3, Name = "Optimus Prime" };

            var account1 = new Account() { AccountNumber = 1, Balance = 10000 };
            var account2 = new Account() { AccountNumber = 2, Balance = 20000 };
            var account3 = new Account() { AccountNumber = 3, Balance = 30000 };
            var account4 = new Account() { AccountNumber = 4, Balance = 40000 };
            var account5 = new Account() { AccountNumber = 5, Balance = 50000 };


            customer1.Accounts = new List<Account>() { account1, account2 };
            customer2.Accounts = new List<Account>() { account3, account4 };
            customer3.Accounts = new List<Account>() { account5 };
            ;

            return new List<Customer>() { customer1, customer2, customer3 };

        }
    }
}

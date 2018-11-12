using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankWebApp.DAL;
using Microsoft.AspNetCore.Mvc;

namespace BankWebApp.Controllers
{
    public class BankController : Controller
    {
        public BankRepository _repository;

        public BankController()
        {
            _repository = BankRepository.Instance();
        }

        public IActionResult ListOfAccounts()
        {
            var listOfAccounts = _repository.GetAllCustomersAndAccounts();
            //TEST
            _repository.Transfer(1, 2, 3000);
            return View(listOfAccounts);
        }

        public IActionResult DepositWithdraw(string message)
        {
            ViewBag.message = message;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deposit(int accountNumber, decimal sum)
        {
            var result =_repository.Deposit(accountNumber, sum);
            return RedirectToAction("DepositWithdraw", new {message = result});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Withdraw(int accountNumber, decimal sum)
        {
            var result = _repository.Withdraw(accountNumber, sum);
            return RedirectToAction("DepositWithdraw", new {message = result});
        }

    }
}
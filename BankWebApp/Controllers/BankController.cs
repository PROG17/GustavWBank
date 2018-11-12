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
            _repository = new BankRepository();
        }

        public IActionResult ListOfAccounts()
        {
            var listOfAccounts = _repository.GetAllCustomersAndAccounts();
            return View(listOfAccounts);
        }
    }
}
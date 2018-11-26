using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankWebApp.DAL;
using Microsoft.AspNetCore.Mvc;

namespace BankWebApp.Controllers
{
    public class TransferController : Controller
    {
        public BankRepository _repository;

        public TransferController()
        {
            _repository = BankRepository.Instance();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Transfer(int fromAccountNumber, int toAccountNumber, int sum)
        {
            string message = "";
            if (sum == 0)
            {
                message = "Summa är noll eller i fel format";
            }
            else if (fromAccountNumber == 0 || toAccountNumber == 0)
            {
                message = "Minst ett av kontonummerna är i fel format";
            }
            else
            {
                message = _repository.Transfer(fromAccountNumber, toAccountNumber, sum);
            }

            ViewBag.message = message;
            return View("TransferPage");
        }

        public IActionResult TransferPage()
        {
            return View();
        }
    }
}
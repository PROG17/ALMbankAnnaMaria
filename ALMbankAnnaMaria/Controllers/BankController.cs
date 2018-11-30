using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALMbankAnnaMaria.Models;
using ALMbankAnnaMaria.Services;
using ALMbankAnnaMaria.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ALMbankAnnaMaria.Controllers
{
    public class BankController : Controller
    {
        readonly BankService bankService;

        public BankController(BankService bankService)
        {
            this.bankService = bankService;
        }
        public IActionResult Index()
        {
            var model = new ViewModels.BankAppViewModel();
            return View(model);
        }

        public IActionResult Transaction()
        {
            var model = new ViewModels.BankAppViewModel();
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Deposit(ViewModels.BankAppViewModel model)
        {
            if (ModelState.IsValid)
            {                
                if (bankService.Deposit(model.AccountNumber, model.Ammount)== true)
                {
                    Account currentAccount = bankService.GetAccount(model.AccountNumber);
                    model.Message = "Pengarna insatta";
                    return View(nameof(Index), new ViewModels.BankAppViewModel
                    {
                        AccountNumber = model.AccountNumber,
                        Ammount = model.Ammount,
                        Balance = currentAccount.Balance,
                        Message = model.Message
                    });
                }
                else
                {
                    model.Message = "Något gick fel. Kontrollera kontonummer och saldo.";
                }
            }            
            return View("Index", model);
        }
        
        [HttpPost]
        public IActionResult Withdrawal(ViewModels.BankAppViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (bankService.Withdrawal(model.AccountNumber, model.Ammount) == false)
                {                    
                    model.Message = "Något gick fel. Kontrollera kontonummer och saldo.";                   
                }
                else
                {
                    Account currentAccount = bankService.GetAccount(model.AccountNumber);
                    model.Message = "Pengarna uttagna";
                    return View(nameof(Index), new ViewModels.BankAppViewModel
                    {
                        AccountNumber = model.AccountNumber,
                        Ammount = model.Ammount,
                        Balance = currentAccount.Balance,
                        Message = model.Message
                    });
                }                    
            }
            return View("Index", model);
        }
    }
}
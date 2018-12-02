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

        [HttpGet]
        public IActionResult Transfer()
        {
            return View(new TransferViewModel());
        }

        [HttpPost]
        public IActionResult Transfer(TransferViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = bankService.Transfer(vm.FromAccountId, vm.ToAccountId, vm.Sum, out string message);

                if (isSuccess)
                {
                    vm.Message = message;
                    vm.ToAccountBalance = bankService.GetAccount(vm.ToAccountId).Balance;
                    vm.FromAccountBalance = bankService.GetAccount(vm.FromAccountId).Balance;
                    return View(vm);
                }
                else
                {
                    vm.Message = message;
                    vm.ToAccountBalance = 0;
                    vm.FromAccountBalance = 0;
                }
            }
            return View(vm);
        }
    }
}

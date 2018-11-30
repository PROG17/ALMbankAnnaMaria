using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ALMbankAnnaMaria.Models;
using ALMbankAnnaMaria.Repos;
using ALMbankAnnaMaria.Services;

namespace ALMbankAnnaMaria.Controllers
{
    public class HomeController : Controller
    {
        private readonly BankService _bankService;

        public HomeController(BankService bankService)
        {
            _bankService = bankService;
        }

        public IActionResult Index()
        {
            return View(_bankService.GetAllCustomers());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
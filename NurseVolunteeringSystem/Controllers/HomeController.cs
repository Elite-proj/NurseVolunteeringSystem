using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NurseVolunteeringSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Controllers
{
    public class HomeController : Controller
    {
        private AppDBContext context { get; set; }

        public HomeController( AppDBContext ctx)
        {
            
            this.context = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HomePage()
        {
            ViewBag.TotalManagers = context.Users.Where(u => u.UserType == "O").Count();
            ViewBag.TotalNurses = context.Users.Where(u => u.UserType == "N").Count();
            ViewBag.TotalPatients = context.Users.Where(u => u.UserType == "P").Count();
            ViewBag.TotalContracts = context.CareContract.Where(c => c.DeleteStatus == "Active").Count();

            DateTime Maxdate = DateTime.Today;
            DateTime MinDate = DateTime.Today.AddDays(-3);

            var Contracts = context.CareContract.Where(c => c.ContractDate <= Maxdate && c.ContractDate >= MinDate && c.ContractStatus == "N" && c.DeleteStatus=="Active").Include(s=>s.Suburb).OrderBy(o => o.ContractDate);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class HomeController : Controller
    {
        private AppDBContext context { get; set; }

        public HomeController(AppDBContext ctx)
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
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            ViewBag.TotalPatients = context.Users.Where(u => u.UserType == "P").Count();
            double totalContracts= context.CareContract.Where(c => c.DeleteStatus == "Active").Count();
            ViewBag.TotalContracts = totalContracts;
            ViewBag.TotalNurses = context.Users.Where(u => u.UserType == "N").Count();

            double TotalAssignedContracts = context.CareContract.Where(c => c.ContractStatus == "A" && c.DeleteStatus == "Active").Count();

            double AssignedContractPercentage = (TotalAssignedContracts / totalContracts) * 100;

            ViewBag.AssignedPercentage = AssignedContractPercentage;

            DateTime Maxdate = DateTime.Today;
            DateTime MinDate = DateTime.Today.AddDays(-3);

            var Contracts = context.CareContract.Where(c => c.ContractDate <= Maxdate && c.ContractDate >= MinDate && c.ContractStatus == "N" && c.DeleteStatus=="Active").Include(s => s.Suburb).OrderBy(o => o.ContractDate);


            return View(Contracts);
        }

    }
}

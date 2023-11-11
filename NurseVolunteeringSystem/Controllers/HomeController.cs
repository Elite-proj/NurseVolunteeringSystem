using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var business = context.Business.Include(b => b.Suburb).ThenInclude(c => c.City);

            Business business1 = new Business();
            foreach ( var item in business)
            {
                

                business1.AddressLine1 = item.AddressLine1;
                business1.AddressLine2 = item.AddressLine2;
                business1.BusinessID = item.BusinessID;
                business1.ContactNo = item.ContactNo;
                business1.Email = item.Email;
                business1.NPONumber = item.NPONumber;
                business1.OperatingHours = item.OperatingHours;
                business1.OrganizationName = item.OrganizationName;
                business1.Suburb = item.Suburb;
            }

            return View(business1);
        }

        [HttpGet]
        public IActionResult UpdateBusinessInfo()
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            var Cities = context.City.OrderBy(o => o.CityName);
            

            ViewBag.Cities = new SelectList(Cities, "CityID", "CityName");

            var business = context.Business.First();

            return View(business);
        }

        [HttpPost]
        public IActionResult UpdateBusinessInfo(Business business)
        {
            if(ModelState.IsValid)
            {
                context.Business.Update(business);
                context.SaveChanges();

                return RedirectToAction("HomePage", "Home", new { area = "" });
            }
            else
            {
                var cities = context.City.Where(c => c.Status == "Active").OrderBy(o => o.CityName);

                ViewBag.Cities = new SelectList(cities, "CityID", "CityName");

                return View(business);
            }
        }

        [HttpGet]
        public IActionResult HomePage()
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            ViewBag.TotalManagers = context.Users.Where(u => u.UserType == "O").Count();
            ViewBag.TotalNurses = context.Users.Where(u => u.UserType == "N").Count();
            ViewBag.TotalPatients = context.Users.Where(u => u.UserType == "P").Count();
            ViewBag.TotalContracts = context.CareContract.Where(c => c.DeleteStatus == "Active").Count();

            DateTime Maxdate = DateTime.Today;
            DateTime MinDate = DateTime.Today.AddDays(-3);

            var Contracts = context.CareContract.Where(c => c.ContractDate <= Maxdate && c.ContractDate >= MinDate && c.ContractStatus == "N" && c.DeleteStatus=="Active").Include(s=>s.Suburb).OrderBy(o => o.ContractDate);

            return View(Contracts);
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NurseVolunteeringSystem.DataAccess;
using NurseVolunteeringSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Areas.Nurse.Controllers
{
    [Area("Nurse")]
    public class PreferredSuburbController : Controller
    {
        //public readonly IConfiguration _IConfiguration;
        //DataAccessLayer data;
        //DataTable dt;

        private AppDBContext context { get; set; }

        public PreferredSuburbController(IConfiguration config, AppDBContext ctx)
        {
            //this._IConfiguration = config;
            this.context = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult AddPreferredSuburb()
        {
            if (HttpContext.Session.GetInt32("NurseID") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            var Cities = context.City.OrderBy(o => o.CityName);

            ViewBag.Cities = new SelectList(Cities, "CityID", "CityName");

            return View();
        }

        [HttpPost]
        public IActionResult AddPreferredSuburb(PrefferedSuburb Prefferedsuburb)
        {
            if(ModelState.IsValid)
            {
                Prefferedsuburb.Status = "Active";
                Prefferedsuburb.NurseID = int.Parse(HttpContext.Session.GetInt32("NurseID").ToString());

                context.PrefferedSuburb.Add(Prefferedsuburb);
                context.SaveChanges();

                return RedirectToAction("ListPreferredSuburbs", "PreferredSuburb", new { area="Nurse"});
            }
            else
            {
                var Cities = context.City.OrderBy(o => o.CityName);

                ViewBag.Cities = new SelectList(Cities, "CityID", "CityName");

                return View(Prefferedsuburb);
            }
        }

        public JsonResult GetSuburbs(int id)
        {
            var results = context.Suburb.Where(s => s.CityID == id).OrderBy(o => o.SuburbName);

            var Suburbs = new SelectList(results, "SuburbID", "SuburbName").ToList();

            return Json(Suburbs);
        }

        [HttpGet]
        public IActionResult ListPreferredSuburbs()
        {
            if (HttpContext.Session.GetInt32("NurseID") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            int id = int.Parse(HttpContext.Session.GetInt32("NurseID").ToString());

            var suburbs= context.PrefferedSuburb.Where(p => p.NurseID == id && p.Status=="Active").Include(s => s.Suburb).Include(c => c.Suburb.City);

            return View(suburbs);

        }

        [HttpGet]
        public IActionResult UpdatePreferredSuburb(int id)
        {
            if (HttpContext.Session.GetInt32("NurseID") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            var Cities = context.City.OrderBy(o => o.CityName);

            ViewBag.Cities = new SelectList(Cities, "CityID", "CityName");

            var Suburb = context.PrefferedSuburb.Find(id);

            return View(Suburb);
        }

        [HttpPost]
        public IActionResult UpdatePreferredSuburb(PrefferedSuburb prefferedSuburb)
        {
            if(ModelState.IsValid)
            {
                context.PrefferedSuburb.Update(prefferedSuburb);

                context.SaveChanges();

                return RedirectToAction("ListPreferredSuburbs", "PreferredSuburb", new { area = "Nurse" });
            }
            else
            {
                var Cities = context.City.OrderBy(o => o.CityName);

                ViewBag.Cities = new SelectList(Cities, "CityID", "CityName");

                return View(prefferedSuburb);
            }
        }

        [HttpGet]
        public IActionResult ConfirmPreferredSuburb(int id)
        {
            if (HttpContext.Session.GetInt32("NurseID") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            var suburb = context.PrefferedSuburb.Where(p => p.PrefferedSuburbID == id).Include(s => s.Suburb);

            return View(suburb);
        }

        [HttpPost]
        public IActionResult DeletePreferredSuburb(int id)
        {
            var suburb = context.PrefferedSuburb.Find(id);

            
            suburb.Status = "In-Active";

            context.PrefferedSuburb.Update(suburb);
            context.SaveChanges();

            return RedirectToAction("ListPreferredSuburbs", "PreferredSuburb", new { area = "Nurse" });
        }
    }
}

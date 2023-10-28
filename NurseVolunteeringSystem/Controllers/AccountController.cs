using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NurseVolunteeringSystem.DataAccess;
using NurseVolunteeringSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Controllers
{
    public class AccountController : Controller
    {
        public readonly IConfiguration _IConfiguration;
        DataAccessLayer data;
        DataTable dt;

        private AppDBContext context { get; set; }

        public AccountController(IConfiguration config, AppDBContext ctx)
        {
            this._IConfiguration = config;
            this.context = ctx;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var Cities = context.City.OrderBy(o => o.CityName);
            var Genders = context.Gender.OrderBy(o => o.GenderName);

            ViewBag.Cities = new SelectList(Cities, "CityID", "CityName");
            ViewBag.Genders = new SelectList(Genders, "GenderID", "GenderName");


            return View();
        }

        public JsonResult GetSuburbs(int id)
        {
            var results = context.Suburb.Where(s => s.CityID == id).OrderBy(o => o.SuburbName);

            var Suburbs = new SelectList(results, "SuburbID", "SuburbName").ToList();

            return Json(Suburbs);
        }

        [HttpPost]
        public IActionResult Register(PatientViewModel patient)
        {
            if(ModelState.IsValid)
            {
                data = new DataAccessLayer(_IConfiguration);

                data.RegisterPatient(patient);

                return RedirectToAction("Login", "Account");
            }
            else
            {
                var Cities = context.City.OrderBy(o => o.CityName);
                var Genders = context.Gender.OrderBy(o => o.GenderName);

                ViewBag.Cities = new SelectList(Cities, "CityID", "CityName");
                ViewBag.Genders = new SelectList(Genders, "GenderID", "GenderName");

                return View(patient);
            }
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel user)
        {
            if(ModelState.IsValid)
            {
                data = new DataAccessLayer(_IConfiguration);

                dt = data.Login(user);

                if(dt.Rows.Count>0)
                {
                    if(dt.Rows[0]["UserType"].ToString()=="A")
                    {
                        return RedirectToAction("HomePage", "Home");
                    }
                    else if(dt.Rows[0]["UserType"].ToString() == "O")
                    {
                        return RedirectToAction("HomePage", "Home", new { area = "Manager" });
                    }
                    else if(dt.Rows[0]["UserType"].ToString() == "N")
                    {
                        HttpContext.Session.SetInt32("NurseID", Convert.ToInt32(dt.Rows[0]["UserID"].ToString()));

                       return RedirectToAction("HomePage", "Home", new { area = "Nurse" });

                    }
                    else if(dt.Rows[0]["UserType"].ToString() == "P")
                    {
                        HttpContext.Session.SetInt32("PatientID", Convert.ToInt32(dt.Rows[0]["UserID"].ToString()));
                        return RedirectToAction("HomePage", "Home", new { area = "Patient" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username/password");

                        return View(user);
                    }

                }
                ModelState.AddModelError("", "Invalid username/password");

                return View(user);
            }
            else
            {
                ModelState.AddModelError("", "Invalid username/password");

                return View(user);
                
            }
        }
    }
}

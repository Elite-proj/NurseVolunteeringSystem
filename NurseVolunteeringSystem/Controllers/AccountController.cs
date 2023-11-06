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
using NurseVolunteeringSystem.Password;

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
                    string names;
                    

                    if (dt.Rows[0]["UserType"].ToString()=="A")
                    {
                        names = dt.Rows[0]["Username"].ToString();

                        HttpContext.Session.SetString("Names", names);

                        return RedirectToAction("HomePage", "Home");
                    }
                    else if(dt.Rows[0]["UserType"].ToString() == "O")
                    {
                        names = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["Surname"].ToString();

                        HttpContext.Session.SetString("Names", names);
                        return RedirectToAction("HomePage", "Home", new { area = "Manager" });
                    }
                    else if(dt.Rows[0]["UserType"].ToString() == "N")
                    {
                        names = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["Surname"].ToString();

                        HttpContext.Session.SetString("Names", names);

                        HttpContext.Session.SetInt32("NurseID", Convert.ToInt32(dt.Rows[0]["UserID"].ToString()));

                       return RedirectToAction("HomePage", "Home", new { area = "Nurse" });

                    }
                    else if(dt.Rows[0]["UserType"].ToString() == "P")
                    {
                        names = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["Surname"].ToString();

                        HttpContext.Session.SetString("Names", names);

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

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login","Account", new { area = "" });
        }
    }
}

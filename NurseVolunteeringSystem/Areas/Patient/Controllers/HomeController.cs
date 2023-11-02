using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NurseVolunteeringSystem.DataAccess;
using NurseVolunteeringSystem.Models.ViewModels;
using NurseVolunteeringSystem.Password;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class HomeController : Controller
    {
        public readonly IConfiguration _IConfiguration;
        DataAccessLayer data;
        DataTable dt;

        private AppDBContext context { get; set; }

        public HomeController(IConfiguration config, AppDBContext ctx)
        {
            this._IConfiguration = config;
            this.context = ctx;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HomePage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            data = new DataAccessLayer(_IConfiguration);
            dt = new DataTable();
            int id = (int)HttpContext.Session.GetInt32("PatientID");
            dt = data.GetUserByID(id);

            UpdateUserPassword password = new UpdateUserPassword();

            password.ConfirmOldPassword = PasswordEncryption.ConvertToDecryption(dt.Rows[0]["Password"].ToString());
            password.UserID = int.Parse(dt.Rows[0]["UserID"].ToString());

            return View(password);
        }

        [HttpPost]
        public IActionResult ChangePassword(UpdateUserPassword userPassword)
        {
            if (ModelState.IsValid)
            {
                data = new DataAccessLayer(_IConfiguration);
                data.UpdateUserPassword(userPassword);

                return RedirectToAction("HomePage", "Home", new { area = "Patient" });
            }
            else
            {
                return View(userPassword);
            }
        }

        [HttpGet]
        public IActionResult UpdatePatientPersonalInfo()
        {
            data = new DataAccessLayer(_IConfiguration);
            int id = (int)HttpContext.Session.GetInt32("PatientID");

            var gender = context.Gender.OrderBy(o => o.GenderName);
            var cities = context.City.OrderBy(o => o.CityName);

            ViewBag.Genders = new SelectList(gender, "GenderID", "GenderName");
            ViewBag.Cities = new SelectList(cities, "CityID", "CityName");

            dt = data.GetPatientByID(id);

            UpdatePatientViewModel user = new UpdatePatientViewModel();

            user.FirstName = dt.Rows[0]["FirstName"].ToString();
            user.Surname = dt.Rows[0]["Surname"].ToString();
            user.Email = dt.Rows[0]["Email"].ToString();
            user.AddressLine1= dt.Rows[0]["AddressLine1"].ToString();
            user.AddressLine2 = dt.Rows[0]["AddressLine2"].ToString();
            user.DateOfBirth= DateTime.Parse(dt.Rows[0]["DateOfBirth"].ToString());
            user.IDNumber= dt.Rows[0]["IDNumber"].ToString();
            user.SuburbID=int.Parse(dt.Rows[0]["SuburbID"].ToString());
            user.EmergencyContactNumber= dt.Rows[0]["EmergencyContactNumber"].ToString();
            user.EmergencyContactPerson = dt.Rows[0]["EmergencyContactPerson"].ToString();
            user.GenderID = int.Parse(dt.Rows[0]["GenderID"].ToString());
            user.ContactNo = dt.Rows[0]["ContactNo"].ToString();
            user.PatientID = int.Parse(dt.Rows[0]["PatientID"].ToString());

            return View(user);
        }

        [HttpPost]
        public IActionResult UpdatePatientPersonalInfo(UpdatePatientViewModel update)
        {
            if(ModelState.IsValid)
            {
                data = new DataAccessLayer(_IConfiguration);
                data.UpdatePatientPersonalInfo(update);

                return RedirectToAction("HomePage", "Home", new { area = "Patient" });
            }
            else
            {
                var gender = context.Gender.OrderBy(o => o.GenderName);
                var cities = context.City.OrderBy(o => o.CityName);

                ViewBag.Genders = new SelectList(gender, "GenderID", "GenderName");
                ViewBag.Cities = new SelectList(cities, "CityID", "CityName");

                return View(update);
            }
        }

    }
}

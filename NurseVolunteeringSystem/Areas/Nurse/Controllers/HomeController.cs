using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NurseVolunteeringSystem.DataAccess;
using NurseVolunteeringSystem.Models.ViewModels;
using NurseVolunteeringSystem.Password;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Areas.Nurse.Controllers
{
    [Area("Nurse")]
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
            int id = (int)HttpContext.Session.GetInt32("NurseID");
            ViewBag.TotalContracts = context.CareContract.Where(c => c.NurseID == id && c.DeleteStatus == "Active").Count();
            ViewBag.TotalPrefferedSuburbs = context.PrefferedSuburb.Where(c => c.NurseID == id && c.Status == "Active").Count();
            ViewBag.TotalVisits = context.CareVisit.Include(c => c.CareContract).Where(n => n.VisitDate>=DateTime.Now && n.Status=="Active" && n.CareContract.NurseID == id && n.CareContract.DeleteStatus=="Active").Count();

            var CareVisits = context.CareVisit.Include(c => c.CareContract).ThenInclude(s=> s.Suburb).Where(n => n.VisitDate>=DateTime.Now && n.Status == "Active" && n.CareContract.NurseID == id && n.CareContract.DeleteStatus == "Active").OrderBy(o => o.VisitDate);


            return View(CareVisits);
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            data = new DataAccessLayer(_IConfiguration);
            dt = new DataTable();
            int id = (int)HttpContext.Session.GetInt32("NurseID");
            dt = data.GetNurseByID(id);

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

                return RedirectToAction("HomePage", "Home", new { area = "Nurse" });
            }
            else
            {
                return View(userPassword);
            }
        }

        [HttpGet]
        public IActionResult UpdateNursePersonalInfo()
        {
            data = new DataAccessLayer(_IConfiguration);
            int id = (int)HttpContext.Session.GetInt32("NurseID");

            var gender = context.Gender.OrderBy(o => o.GenderName);

            ViewBag.Genders = new SelectList(gender, "GenderID", "GenderName");

            dt = data.GetNurseByID(id);

            UpdateNurseViewModel user = new UpdateNurseViewModel();

            user.FirstName = dt.Rows[0]["FirstName"].ToString();
            user.Surname = dt.Rows[0]["Surname"].ToString();

            user.Email = dt.Rows[0]["Email"].ToString();
            user.GenderID = int.Parse(dt.Rows[0]["GenderID"].ToString());
            user.ContactNo = dt.Rows[0]["ContactNo"].ToString();
            user.IDNumber= dt.Rows[0]["IDNumber"].ToString();
            user.NurseID = int.Parse(dt.Rows[0]["UserID"].ToString());

            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateNursePersonalInfo(UpdateNurseViewModel update)
        {
            if(ModelState.IsValid)
            {
                data = new DataAccessLayer(_IConfiguration);
                data.UpdateNursePersonalInfo(update);

                return RedirectToAction("HomePage", "Home", new { area = "Nurse" });
            }
            else
            {
                var gender = context.Gender.OrderBy(o => o.GenderName);

                ViewBag.Genders = new SelectList(gender, "GenderID", "GenderName");

                return View(update);
            }
        }
    }
}

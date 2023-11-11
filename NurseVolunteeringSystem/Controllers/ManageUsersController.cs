using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NurseVolunteeringSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using NurseVolunteeringSystem.Models.ViewModels;
using NurseVolunteeringSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace NurseVolunteeringSystem.Controllers
{
    public class ManageUsersController : Controller
    {
        public readonly IConfiguration _IConfiguration;
        DataAccessLayer data;
        DataTable dt;

        private AppDBContext context { get; set; }

        public ManageUsersController(IConfiguration config, AppDBContext ctx)
        {
            this._IConfiguration = config;
            this.context = ctx;
        }

        #region user registrations

        [HttpGet]
        public IActionResult RegisterManager()
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            return View();
        }

        [HttpPost]
        public IActionResult RegisterManager(ManagerViewModel manager)
        {
            if(ModelState.IsValid)
            {
                data = new DataAccessLayer(_IConfiguration);

                data.RegisterManager(manager);

                return RedirectToAction("ListManagers", "ManageUsers");
            }
            else
            {
                return View(manager);
            }
        }

        [HttpGet]
        public IActionResult RegisterNurse()
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            var gender = context.Gender.OrderBy(o => o.GenderName);

            ViewBag.Genders = new SelectList(gender, "GenderID", "GenderName");

            return View();
        }
        

        [HttpPost]
        public IActionResult RegisterNurse(NurseViewModel nurse)
        {
            if(ModelState.IsValid)
            {
                data = new DataAccessLayer(_IConfiguration);

                data.RegisterNurse(nurse);

                return RedirectToAction("ListNurses", "ManageUsers");
            }
            else
            {
                var gender = context.Gender.OrderBy(o => o.GenderName);

                ViewBag.Genders = new SelectList(gender, "GenderID", "GenderName");

                return View(nurse);
            }
        }

        [HttpGet]
        public IActionResult RegisterPatient()
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            var gender = context.Gender.OrderBy(o => o.GenderName);

            var suburb = context.Suburb.OrderBy(s => s.SuburbName);

            ViewBag.Genders = new SelectList(gender, "GenderID", "GenderName");

            ViewBag.Suburbs = new SelectList(suburb, "SuburbID", "SuburbName");

            return View();
        }

        [HttpPost]
        public IActionResult RegisterPatient(PatientViewModel  patient)
        {
            if(ModelState.IsValid)
            {
                data = new DataAccessLayer(_IConfiguration);

                data.RegisterPatient(patient);

                return View();
            }
            else
            {
                var gender = context.Gender.OrderBy(o => o.GenderName);

                var suburb = context.Suburb.OrderBy(s => s.SuburbName);

                ViewBag.Genders = new SelectList(gender, "GenderID", "GenderName");

                ViewBag.Suburbs = new SelectList(suburb, "SuburbID", "SuburbName");

                return View(patient);
            }
        }
        #endregion

        #region list Users

        [HttpGet]
        public IActionResult ListManagers()
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            data = new DataAccessLayer(_IConfiguration);

            dt = data.ListManagers();

            List<User> users = new List<User>();

            for(int i=0;i<dt.Rows.Count;i++)
            {
                User user = new User();

                user.Username = dt.Rows[i]["Username"].ToString();
                user.Email = dt.Rows[i]["Email"].ToString();
                user.ContactNo = dt.Rows[i]["ContactNo"].ToString();
                user.UserType = dt.Rows[i]["UserType"].ToString();
                user.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());

                users.Add(user);
            }

            var managers = users;

          

            return View(managers);
        }

        [HttpGet]
        public IActionResult ListNurses()
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            data = new DataAccessLayer(_IConfiguration);

            dt = data.ListNurses();

            List<NurseViewModel> users = new List<NurseViewModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                NurseViewModel user = new NurseViewModel();

                user.Username = dt.Rows[i]["Username"].ToString();
                user.FirstName = dt.Rows[i]["FirstName"].ToString();
                user.Surname = dt.Rows[i]["Surname"].ToString();
               user.IDNumber = dt.Rows[i]["IDNumber"].ToString();
                user.Email = dt.Rows[i]["Email"].ToString();
                user.ContactNo = dt.Rows[i]["ContactNo"].ToString();
                user.UserType = dt.Rows[i]["UserType"].ToString();
                user.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
                user.GenderName = dt.Rows[i]["GenderName"].ToString();

                users.Add(user);
            }

            var nurses = users;

            return View(users);
        }

        #endregion

        #region Update users

        [HttpGet]
        public IActionResult UpdateManager(int id)
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            data = new DataAccessLayer(_IConfiguration);

            dt = data.GetManagerByID(id);

            UpdateManagerVM user = new UpdateManagerVM();

           
            user.Email = dt.Rows[0]["Email"].ToString();
            user.ContactNo = dt.Rows[0]["ContactNo"].ToString();
            
            user.UserID = int.Parse(dt.Rows[0]["UserID"].ToString());

            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateManager(UpdateManagerVM manager)
        {
            if (ModelState.IsValid)
            {
                data = new DataAccessLayer(_IConfiguration);
                data.UpdateManager(manager);

                return RedirectToAction("ListManagers", "ManageUsers");
            }
            else
            {
                return View(manager);
            }
        }

        [HttpGet]
        public IActionResult UpdateNurse(int id)
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            data = new DataAccessLayer(_IConfiguration);

            var gender = context.Gender.OrderBy(o => o.GenderName);

            ViewBag.Genders = new SelectList(gender, "GenderID", "GenderName");

            dt = data.GetNurseByID(id);

            UpdateNurseVM user = new UpdateNurseVM();

            
            user.FirstName = dt.Rows[0]["FirstName"].ToString();
            user.Surname = dt.Rows[0]["Surname"].ToString();
           
            user.Email = dt.Rows[0]["Email"].ToString();
            user.GenderID = int.Parse(dt.Rows[0]["GenderID"].ToString());
            user.ContactNo = dt.Rows[0]["ContactNo"].ToString();
            
            user.UserID = int.Parse(dt.Rows[0]["UserID"].ToString());
           

            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateNurse(UpdateNurseVM nurse)
        {
            if(ModelState.IsValid)
            {
                data = new DataAccessLayer(_IConfiguration);

                data.UpdateNurse(nurse);

                return RedirectToAction("ListNurses", "ManageUsers");
            }
            else
            {

                var gender = context.Gender.OrderBy(o => o.GenderName);

                ViewBag.Genders = new SelectList(gender, "GenderID", "GenderName");

                return View(nurse);
            }
        }

        [HttpGet]
        public IActionResult DeleteManager(int id)
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            data = new DataAccessLayer(_IConfiguration);

            dt = data.GetManagerByID(id);

            User user = new User();


            user.Email = dt.Rows[0]["Email"].ToString();
            user.ContactNo = dt.Rows[0]["ContactNo"].ToString();

            user.UserID = int.Parse(dt.Rows[0]["UserID"].ToString());

            return View(user);
        }

        [HttpPost]
        public IActionResult DeleteManager(User user)
        {
            data = new DataAccessLayer(_IConfiguration);

            data.DeleteUser(user);

            return RedirectToAction("ListManagers", "ManageUsers");
        }

        [HttpGet]
        public IActionResult DeleteNurse(int id)
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            data = new DataAccessLayer(_IConfiguration);

            dt= data.GetNurseByID(id);

            User user = new User();

            user.FirstName = dt.Rows[0]["FirstName"].ToString();
            user.Surname = dt.Rows[0]["Surname"].ToString();
            user.IDNumber = dt.Rows[0]["IDNumber"].ToString();
            user.Email = dt.Rows[0]["Email"].ToString();
            user.ContactNo = dt.Rows[0]["ContactNo"].ToString();

            user.UserID = int.Parse(dt.Rows[0]["UserID"].ToString());

            return View(user);
        }

        [HttpPost]
        public IActionResult DeleteNurse(User user)
        {
            data = new DataAccessLayer(_IConfiguration);

            data.DeleteUser(user);

            return RedirectToAction("ListNurses", "ManageUsers");
        }
        #endregion
    }
}

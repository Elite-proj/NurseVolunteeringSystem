﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NurseVolunteeringSystem.Areas.Manager.Models;
using NurseVolunteeringSystem.DataAccess;
using NurseVolunteeringSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Areas.Manager.Controllers
{

    [Area("Manager")]
    public class ContractController : Controller
    {
        public readonly IConfiguration _IConfiguration;
        DataAccessLayer data;
        DataTable dt;

        private AppDBContext context { get; set; }

        public ContractController(IConfiguration config, AppDBContext ctx)
        {
            this._IConfiguration = config;
            this.context = ctx;
        }

        [HttpGet]
        public IActionResult ListContracts()
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            var contracts = context.CareContract.Where(c=>c.ContractStatus=="N").Include(s => s.Suburb).OrderBy(o => o.CareContractID);

            return View(contracts);
        }

        [HttpGet]
        public IActionResult ContractsByDate()
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            return View();
        }

        [HttpPost]
        public IActionResult ContractsByDate(DateRangeVM date)
        {
            

            if (ModelState.IsValid)
            {
                data = new DataAccessLayer(_IConfiguration);
                dt = new DataTable();

                dt = data.SearchContractsByDates(date);

                List<CareContract> contracts = new List<CareContract>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CareContract contract = new CareContract();

                    contract.ContractDate = DateTime.Parse(dt.Rows[i]["ContractDate"].ToString());
                    contract.CareContractID = int.Parse(dt.Rows[i]["CareContractID"].ToString());
                    contract.AddressLine1 = dt.Rows[i]["AddressLine1"].ToString();
                    contract.AddressLine2 = dt.Rows[i]["AddressLine2"].ToString();
                    contract.Suburb.SuburbName = dt.Rows[i]["SuburbName"].ToString();
                    contract.SuburbID = int.Parse(dt.Rows[i]["SuburbID"].ToString());
                    contract.WoundDescription = dt.Rows[i]["WoundDescription"].ToString();
                    contract.ContractStatus = dt.Rows[i]["ContractStatus"].ToString();
                    contract.StartCareDate = DateTime.Parse(dt.Rows[i]["StartCareDate"].ToString());
                    contract.EndCareDate = DateTime.Parse(dt.Rows[i]["EndCareDate"].ToString());
                    contract.PatientID= int.Parse(dt.Rows[i]["CareContractID"].ToString());
                    

                    contracts.Add(contract);
                }

                return View("ContractSearchResults", contracts);

            }
            else
            {
                return View(date);
            }
        }

        [HttpGet]
        public IActionResult ListAssignedContracts()
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            var contracts = context.CareContract.Where(c => c.ContractStatus == "A").Include(s => s.Suburb).OrderBy(o => o.CareContractID);

            return View(contracts);
        }

        [HttpGet]
        public IActionResult ContractVisits(int id)
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            ViewBag.ContractID = id;
            return View();
        }

        [HttpPost]
        public IActionResult ContractVisits(DateRangeVM date)
        {
            if(ModelState.IsValid)
            {
                data = new DataAccessLayer(_IConfiguration);
                dt = new DataTable();

                dt = data.SearchVisitsByDates(date);

                //List<CareVisit> visits = new List<CareVisit>();

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    CareVisit visit = new CareVisit();

                //    visit.ApproximateArriveTime = DateTime.Parse(dt.Rows[i]["ApproximateArriveTime"].ToString());
                //    visit.CareContractID = int.Parse(dt.Rows[i]["CareContractID"].ToString());
                //    visit.CareVisitID = int.Parse(dt.Rows[i]["CareVisitID"].ToString());
                //    visit.DepartTime = DateTime.Parse(dt.Rows[i]["DepartTime"].ToString());
                //    visit.Notes = dt.Rows[i]["Notes"].ToString();
                //    visit.VisistArriveTime = DateTime.Parse(dt.Rows[i]["DepartTime"].ToString());
                //    visit.VisitDate = DateTime.Parse(dt.Rows[i]["DepartTime"].ToString());
                //    visit.WoundProgress = dt.Rows[i]["WoundProgress"].ToString();

                //    visits.Add(visit);
                //}

                var visits = context.CareVisit.Where(c => c.CareContractID == date.contractID && c.VisitDate>=date.MinDate && c.VisitDate<=date.MaxDate && c.Status == "Active").OrderBy(o => o.CareVisitID);

                return View("SearchResults", visits);
            }
            else
            {
                ViewBag.ContractID = date.contractID;

                return View(date);
            }
            
        }

        [HttpGet]
        public IActionResult AssignNurse(int id)
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            var contract = context.CareContract.Find(id);

            var nurses = context.PrefferedSuburb.Where(p => p.SuburbID == contract.SuburbID).Include(n => n.Nurse).ThenInclude(u => u.User);

            AssignNurseVM nurseVM = new AssignNurseVM();

            nurseVM.CareContractID = contract.CareContractID;

            ViewBag.Nurses = new SelectList((from s in nurses
                                             select new
                                             {
                                                 ID = s.Nurse.User.UserID,
                                                 FullName = s.Nurse.User.FirstName + " " + s.Nurse.User.Surname
                                             }),"ID","FullName",null);

            //ViewBag.Nurses = new SelectList(nurses select new , "UserID", "FirstName"+" "+"Surname");

            return View(nurseVM);
        }

        [HttpPost]
        public IActionResult AssignNurse(AssignNurseVM assign)
        {
            if(ModelState.IsValid)
            {
                var contract = context.CareContract.Find(assign.CareContractID);

                contract.NurseID = assign.NurseID;
                contract.ContractStatus = "A";

                context.CareContract.Update(contract);
                context.SaveChanges();

                return RedirectToAction("ListAssignedContracts", "Contract");
            }
            else
            {
                var contract = context.CareContract.Find(assign.CareContractID);

                var nurses = context.PrefferedSuburb.Where(p => p.SuburbID == contract.SuburbID).Include(n => n.Nurse).ThenInclude(u => u.User);

                ViewBag.Nurses = new SelectList(nurses, "UserID", "FirstName" + " " + "Surname");

                return View(assign);
            }
        }
    }
}

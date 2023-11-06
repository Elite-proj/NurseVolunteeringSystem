using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NurseVolunteeringSystem.Areas.Nurse.Models;
using NurseVolunteeringSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using Microsoft.Extensions.Configuration;
using NurseVolunteeringSystem.DataAccess;
using System.Data;
using System.IO;

namespace NurseVolunteeringSystem.Areas.Nurse.Controllers
{
    [Area("Nurse")]
    public class CareContractController : Controller
    {
        public readonly IConfiguration _IConfiguration;
        DataAccessLayer data;
        DataTable dt;

        private AppDBContext context { get; set; }

        public CareContractController(AppDBContext ctx, IConfiguration config)
        {
            this._IConfiguration = config;
            this.context = ctx;
        }

        [HttpGet]
        public IActionResult ListContracts()
        {

            int id= int.Parse(HttpContext.Session.GetInt32("NurseID").ToString());

            var preferred = context.PrefferedSuburb.Where(p => p.NurseID == id && p.Status=="Active").ToList();

            List<CareContract> contracts = new List<CareContract>();

            
            foreach(var pref in preferred)
            {
                var getContract = context.CareContract.Where(c => c.SuburbID == pref.SuburbID && c.ContractStatus=="N").Include(s => s.Suburb);

                foreach(var cont in getContract)
                {
                    CareContract contract = new CareContract();

                    contract.CareContractID = cont.CareContractID;
                    contract.AddressLine1 = cont.AddressLine1;
                    contract.AddressLine2 = cont.AddressLine2;
                    contract.Suburb = cont.Suburb;
                    contract.ContractDate = cont.ContractDate;
                    contract.WoundDescription = cont.WoundDescription;
                    contract.ContractStatus = cont.ContractStatus;
                    contract.DeleteStatus = cont.DeleteStatus;
                    contract.EndCareDate = cont.EndCareDate;
                    contract.StartCareDate = cont.StartCareDate;
                    contract.NurseID = cont.NurseID;
                    contract.PatientID = cont.PatientID;
                    

                    contracts.Add(contract);
                }
            }

            

            return View(contracts);
        }

        [HttpGet]
        public IActionResult ListClosedContracts()
        {
            int id = int.Parse(HttpContext.Session.GetInt32("NurseID").ToString());

            var contracts = context.CareContract.Where(c => c.NurseID == id && c.ContractStatus=="C").Include(s => s.Suburb).OrderBy(o => o.CareContractID);

            return View(contracts);
        }

        public IActionResult ListClosedCareVisits(int id)
        {
            var careVisits = context.CareVisit.Where(c => c.CareContractID == id && c.Status == "Active").OrderBy(o => o.CareVisitID);

            return View(careVisits);
        }


        public IActionResult Choose(int id)
        {
            var contract = context.CareContract.Find(id);

            contract.NurseID= int.Parse(HttpContext.Session.GetInt32("NurseID").ToString());

            contract.ContractStatus = "A";

            context.CareContract.Update(contract);

            context.SaveChanges();

            return RedirectToAction("ListAssignedContracts", "CareContract", new { area = "Nurse" });
        }

        [HttpGet]
        public IActionResult ListAssignedContracts()
        {
            int id = int.Parse(HttpContext.Session.GetInt32("NurseID").ToString());

            var contracts = context.CareContract.Where(c => c.NurseID == id).Include(s => s.Suburb).OrderBy(o => o.CareContractID);

            return View(contracts);
        }


        [HttpGet]
        public IActionResult ListPatientConditions(int id)
        {
            var patient = context.CareContract.Find(id);
            ViewBag.ContractID = id;

            var conditions = context.Patient_ChronicConditions.Where(p => p.PatientID == patient.PatientID && p.Status == "Active").Include(c => c.ChronicCondition).OrderBy(o => o.ChronicCondition.ConditionName);

            return View(conditions);
        }

        [HttpGet]
        public IActionResult DownloadPatientConditionsPDF(int id)
        {
            var document = new PdfDocument();
            data = new DataAccessLayer(_IConfiguration);
            dt = new DataTable();

            var patient = context.CareContract.Find(id);

            var conditions = context.Patient_ChronicConditions.Where(p => p.PatientID == patient.PatientID && p.Status == "Active").Include(c => c.ChronicCondition).OrderBy(o => o.ChronicCondition.ConditionName);
            dt = data.GetUserByID((int)patient.PatientID);

            int total = context.Patient_ChronicConditions.Where(p => p.PatientID == patient.PatientID && p.Status == "Active").Count();

            string htmlcontent = "<div style='width:100%; text-align:center'>";

            htmlcontent += "<h1>HELPING HANDS</h1>";
            htmlcontent += "<h2>PATIENT CONDITIONS REPORT</h2>";


            if (dt.Rows.Count > 0)
            {


                htmlcontent = "<div style='text-align:Left'>";

                htmlcontent += "<h1 style='text-align:center'>HELPING HANDS</h1>";
                htmlcontent += "<h2 style='text-align:center'>PATIENT CONDITIONS REPORT</h2>";

                htmlcontent += "<h3 style='text-align:right'>Report date: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "</h3>";
                htmlcontent += "<h3> PATIENT NAME: " + dt.Rows[0]["FirstName"].ToString() + "</h3>";
                htmlcontent += "<h3> PATIENT SURNAME: " + dt.Rows[0]["Surname"].ToString() + "</h3>";
                htmlcontent += "<h3> PATIENT ADDRESS: " + dt.Rows[0]["AddressLine1"].ToString() + "<br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp;" + dt.Rows[0]["AddressLine2"].ToString() + "<br/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp;" + dt.Rows[0]["SuburbName"].ToString() + "</h3>";
                htmlcontent += "<h3> PATIENT CONTACTS: " + dt.Rows[0]["ContactNo"].ToString() + "</h3>";
                htmlcontent += "<h3> PATIENT  EMAIL: " + dt.Rows[0]["Email"].ToString() + "</h3>";
                htmlcontent += "</div>";

            }

            htmlcontent += "<br/>";

            htmlcontent += "<table style='width:100%; border:1px solid #000; border-collapse:collapse'>";
            htmlcontent += "<thead style='font-weight:bold border:1px solid #000; border-collapse:collapse'>";
            htmlcontent += "<tr>";
           
            htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse; text-align:center'> CONDITION NAME </td>";
            htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse; text-align:center'> CONDITION DESCRIPTION </td>";
           

            htmlcontent += "</tr>";
            htmlcontent += "</thead>";


            htmlcontent += "<tbody>";
            

            
            
            foreach(var item in conditions)
            {
                htmlcontent += "<tr>";
                htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse'>"+item.ChronicCondition.ConditionName+"</td>";
                htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse'>"+item.ChronicCondition.Description+"</td>";


                htmlcontent += "</tr>";
            }
           
            htmlcontent += "<tr><td colspan='2' style='text-align: right; border:1px solid #000; border-collapse:collapse'> Total visits: " + total + "</td></tr>";

            htmlcontent += "</tbody>";

            htmlcontent += "</table>";



            htmlcontent += "</div>";


            PdfGenerator.AddPdfPages(document, htmlcontent, PageSize.A4);

            byte[] response = null;

            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }

            string FileName = dt.Rows[0]["FirstName"].ToString() + "_chronic_conditions.pdf";

            return File(response, "application/pdf", FileName);
        }

        [HttpGet]
        public IActionResult ListCareVisits(int id)
        {
            ViewBag.ContractID = id;

            var careVisits = context.CareVisit.Where(c => c.CareContractID == id && c.Status=="Active").OrderBy(o => o.CareVisitID);

            return View(careVisits);
        }

        [HttpGet]
        public IActionResult DownloadCareVisitPDF(int id)
        {
            var document = new PdfDocument();
            data = new DataAccessLayer(_IConfiguration);
            dt = new DataTable();

            var contract = context.CareContract.Find(id);
            var careVisits = context.CareVisit.Where(c => c.CareContractID == id && c.Status == "Active").OrderBy(o => o.CareVisitID);
            dt = data.GetUserByID((int)contract.PatientID);

            int total = context.CareVisit.Where(c => c.CareContractID == id && c.Status == "Active").Count();

            string htmlcontent = "<div style='width:100%; text-align:center'>";

            htmlcontent += "<h1>Helping Hands</h1>";
            htmlcontent += "<h2>Patient care visits report</h2>";
           

            if (dt.Rows.Count>0)
            {
                

                htmlcontent = "<div style='text-align:Left'>";

                htmlcontent += "<h1 style='text-align:center'>HELPING HANDS</h1>";
                htmlcontent += "<h2 style='text-align:center'>PATIENT CARE VISITS REPORT</h2>";

                htmlcontent += "<h3 style='text-align:right'>Report date: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "</h3>";
                htmlcontent += "<h3> PATIENT NAME: " + dt.Rows[0]["FirstName"].ToString() + "</h3>";
                htmlcontent += "<h3> PATIENT SURNAME: " + dt.Rows[0]["Surname"].ToString() + "</h3>";
                htmlcontent += "<h3> PATIENT ADDRESS: " + dt.Rows[0]["AddressLine1"].ToString() + "<br/> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp;" + dt.Rows[0]["AddressLine2"].ToString() + "<br/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp;" + dt.Rows[0]["SuburbName"].ToString() + "</h3>";
                htmlcontent += "<h3> PATIENT CONTACTS: " + dt.Rows[0]["ContactNo"].ToString() + "</h3>";
                htmlcontent += "<h3> PATIENT EMAIL: " + dt.Rows[0]["Email"].ToString() + "</h3>";
                htmlcontent += "</div>";

            }

            htmlcontent += "<br/>";

            htmlcontent += "<table style='width:100%; border:1px solid #000; border-collapse:collapse'>";
            htmlcontent += "<thead style='font-weight:bold border:1px solid #000; border-collapse:collapse'>";
            htmlcontent += "<tr>";
            htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse; text-align:center'> Visit Date </td>";
            htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse; text-align:center'> ApproximateArriveTime </td>";
            htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse; text-align:center'> VisistArriveTime </td>";
            htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse; text-align:center'> DepartTime </td>";
            htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse; text-align:center'> WoundProgress </td>";
            htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse; text-align:center'> Notes </td>";

            htmlcontent += "</tr>";
            htmlcontent += "</thead>";


            htmlcontent += "<tbody>";
            int count = 0;
            foreach(var item in careVisits)
            {

                htmlcontent += "<tr>";
                htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse'>" + item.VisitDate.ToString("dd/MM/yyyy")+ "</td>";
                htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse'>" + item.ApproximateArriveTime.ToString("HH:mm") + "</td>";
                htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse'>" + item.VisistArriveTime?.ToString("HH:mm") + "</td>";
                htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse'>" + item.DepartTime?.ToString("HH:mm") + "</td>";
                htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse'>" + item.WoundProgress + "</td>";
                htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse'>" + item.Notes + "</td>";

                htmlcontent += "</tr>";

                count++;
            }
            htmlcontent += "<tr><td colspan='6' style='text-align: right; border:1px solid #000; border-collapse:collapse'> Total visits: " + total+"</td></tr>";

            htmlcontent += "</tbody>";

            htmlcontent += "</table>";

            

            htmlcontent += "</div>";

           

            PdfGenerator.AddPdfPages(document, htmlcontent, PageSize.A4);

            byte[] response = null;

            using(MemoryStream ms= new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }

            string FileName = dt.Rows[0]["FirstName"].ToString() + "_Carevisits.pdf";

            return File(response, "application/pdf", FileName);

        }

        [HttpGet]
        public IActionResult AddCareVisit(int id)
        {
            ViewBag.ContractID = id;

            return View();
        }

        [HttpPost]
        public IActionResult AddCareVisit(CareVisit careVisit)
        {
            if(ModelState.IsValid)
            {
                careVisit.Status = "Active";
                context.CareVisit.Add(careVisit);

                context.SaveChanges();

                return RedirectToAction("ListAssignedContracts", "CareContract", new { area = "Nurse" });
            }
            else
            {
                return View(careVisit);
            }
        }


        [HttpGet]
        public IActionResult EditContract(int id)
        {
            var cont  = context.CareContract.Find(id);

            EditContractVM contract = new EditContractVM();

            contract.CareContractID = cont.CareContractID;
            contract.AddressLine1 = cont.AddressLine1;
            contract.AddressLine2 = cont.AddressLine2;
            contract.SuburbID = cont.SuburbID;
            contract.ContractDate = DateTime.Parse(cont.ContractDate.ToString());
            contract.WoundDescription = cont.WoundDescription;
            contract.ContractStatus = cont.ContractStatus;
            contract.DeleteStatus = cont.DeleteStatus;
            contract.EndCareDate = cont.EndCareDate;
            contract.StartCareDate = cont.StartCareDate;
            contract.NurseID = cont.NurseID;
            contract.PatientID = cont.PatientID;

            return View(contract);
        }

        [HttpPost]
        public IActionResult EditContract(EditContractVM cont)
        {
            CareContract contract = new CareContract();

            contract.CareContractID = cont.CareContractID;
            contract.AddressLine1 = cont.AddressLine1;
            contract.AddressLine2 = cont.AddressLine2;
            contract.SuburbID = cont.SuburbID;
            contract.ContractDate = cont.ContractDate;
            contract.WoundDescription = cont.WoundDescription;
            contract.ContractStatus = cont.ContractStatus;
            contract.DeleteStatus = cont.DeleteStatus;
            contract.EndCareDate = cont.EndCareDate;
            contract.StartCareDate = cont.StartCareDate;
            contract.NurseID = cont.NurseID;
            contract.PatientID = cont.PatientID;

            context.CareContract.Update(contract);
            context.SaveChanges();

            return RedirectToAction("ListAssignedContracts", "CareContract", new { area = "Nurse" });
        }

        
        public IActionResult CloseContract(int id)
        {
            var contract = context.CareContract.Find(id);

            contract.ContractStatus = "C";

            context.CareContract.Update(contract);
            context.SaveChanges();

            return RedirectToAction("ListAssignedContracts", "CareContract", new { area = "Nurse" });
        }
    }
}

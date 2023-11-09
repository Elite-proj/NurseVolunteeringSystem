using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NurseVolunteeringSystem.DataAccess;
using NurseVolunteeringSystem.Models;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NurseVolunteeringSystem.Areas.Manager.Controllers
{
    [Area ("Manager")]
    
    public class PrefferedSuburbController : Controller
    {
        public readonly IConfiguration _IConfiguration;
        DataAccessLayer data;
        DataTable dt;

        private readonly AppDBContext _context;


        public PrefferedSuburbController(AppDBContext context, IConfiguration config)
        {
            _context = context;
            _IConfiguration = config;

        }

        [HttpGet]
        public IActionResult ListSuburbs()
        {
            var nurses = _context.Nurse.Include(u => u.User).Where(n=>n.User.UserType=="N" && n.User.Status=="Active").OrderBy(o => o.User.FirstName);

            ViewBag.Nurses = ViewBag.Nurses = new SelectList((from s in nurses
                                                              select new
                                                              {
                                                                  ID = s.User.UserID,
                                                                  FullName = s.User.FirstName + " " + s.User.Surname
                                                              }), "ID", "FullName", null);

            return View();
        }

        [HttpPost]
        public IActionResult ListSuburbs(NurseVolunteeringSystem.Models.Nurse nurse)
        {
            data = new DataAccessLayer(_IConfiguration);
            dt = new DataTable();

            dt = data.GetNurseByID(nurse.NurseID);
            
            var preferredSuburbs = _context.PrefferedSuburb.Where(n => n.NurseID == nurse.NurseID && n.Status=="Active").Include(s => s.Suburb).ThenInclude(c => c.City).OrderBy(o => o.Suburb.SuburbName);
            ViewBag.NurseID = nurse.NurseID;
            ViewBag.FirstName = dt.Rows[0]["FirstName"].ToString();
            ViewBag.Surname = dt.Rows[0]["Surname"].ToString();
            ViewBag.Email = dt.Rows[0]["Email"].ToString();

            return View("SuburbsResults", preferredSuburbs);
        }


        [HttpGet]
        public IActionResult DownloadSuburbsList(int id)
        {
            data = new DataAccessLayer(_IConfiguration);
            dt = new DataTable();
            var document = new PdfDocument();

            dt = data.GetNurseByID(id);

            var preferredSuburbs = _context.PrefferedSuburb.Where(n => n.NurseID == id && n.Status=="Active").Include(s => s.Suburb).ThenInclude(c => c.City).OrderBy(o => o.Suburb.SuburbName);

            int total = _context.PrefferedSuburb.Where(n => n.NurseID == id && n.Status=="Active").Count();

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
                
                htmlcontent += "<h3> PATIENT CONTACTS: " + dt.Rows[0]["ContactNo"].ToString() + "</h3>";
                htmlcontent += "<h3> PATIENT  EMAIL: " + dt.Rows[0]["Email"].ToString() + "</h3>";
                htmlcontent += "</div>";

            }

            htmlcontent += "<br/>";

            htmlcontent += "<table style='width:100%; border:1px solid #000; border-collapse:collapse'>";
            htmlcontent += "<thead style='font-weight:bold border:1px solid #000; border-collapse:collapse'>";
            htmlcontent += "<tr>";

            htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse; text-align:center'> SUBURB NAME </td>";
            htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse; text-align:center'> POSTAL CODE </td>";
            htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse; text-align:center'> CITY NAME </td>";


            htmlcontent += "</tr>";
            htmlcontent += "</thead>";


            htmlcontent += "<tbody>";




            foreach (var item in preferredSuburbs)
            {
                htmlcontent += "<tr>";
                htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse'>" + item.Suburb.SuburbName + "</td>";
                htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse'>" + item.Suburb.PostalCode + "</td>";
                htmlcontent += "<td style='border:1px solid #000; border-collapse:collapse'>" + item.Suburb.City.CityName + "</td>";


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

            string FileName = dt.Rows[0]["FirstName"].ToString() + "_preffered_suburbs.pdf";

            return File(response, "application/pdf", FileName);
        }
       
    }

}

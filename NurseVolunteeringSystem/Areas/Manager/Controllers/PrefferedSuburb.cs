using Microsoft.AspNetCore.Mvc;
using NurseVolunteeringSystem.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Areas.Manager.Controllers
{
    [Area ("Manager")]
    [Route("PrefferedSuburb")]
    public class PrefferedSuburbController : Controller
    {
        private readonly AppDBContext _context;

        public PrefferedSuburbController(AppDBContext context)
        {
            _context = context;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            var prefferedSuburbs = _context.PrefferedSuburb.ToList(); 

            return View(prefferedSuburbs);
        }

       
    }

}

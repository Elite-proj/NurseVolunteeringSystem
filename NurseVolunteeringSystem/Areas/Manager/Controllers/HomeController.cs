using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HomePage()
        {
            return View();
        }

    }
}

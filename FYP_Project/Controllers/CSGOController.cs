using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FYP_Project.Controllers
{
    public class CSGOController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

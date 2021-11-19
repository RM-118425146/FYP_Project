using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FYP_Project.ViewModels;

namespace FYP_Project.Controllers
{
    public class ValorantController : Controller
    {
        public IActionResult Teams()
        {
            TeamViewModel viewModel = new TeamViewModel();
            return View("Teams", viewModel);
        }
    }
}

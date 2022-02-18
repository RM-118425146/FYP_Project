using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FYP_Project.ViewModels;
using Microsoft.AspNetCore.Http;

namespace FYP_Project.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            string email = HttpContext.Session.GetString("emailAddress");

            PlayerViewModel viewModel = new PlayerViewModel();

            if (email == null)
            {
                return View("Index");
            }
            else
            {
                foreach (var player in viewModel.Players)
                {
                    if (email == player.emailAddress)
                    {
                        viewModel.EditablePlayer = player;
                    }
                }
                return View("Index", viewModel);
            }
        }
    }
}

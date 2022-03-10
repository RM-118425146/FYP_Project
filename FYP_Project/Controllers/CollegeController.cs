using FYP_Project.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Dapper.Contrib.Extensions;
using FYP_Project.ViewModels;
using FYP_Project.Helpers;
using Microsoft.AspNetCore.Http;

namespace FYP_Project.Controllers
{
    public class CollegeController : Controller
    {
        public IActionResult Index()
        {
            CollegeViewModel viewModel = new CollegeViewModel();

            string email = HttpContext.Session.GetString("emailAddress");
            if (email == null)
            {
                return View("Index", viewModel);
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

        public IActionResult Edit(int ID)
        {
            CollegeViewModel viewModel = new CollegeViewModel();
            viewModel.EditableCollege = viewModel.Colleges.FirstOrDefault(x => x.CollegeID == ID);
            string email = HttpContext.Session.GetString("emailAddress");
            if (email == null)
            {
                return View("Index", viewModel);
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

        public IActionResult Delete(int ID)
        {
            using (var db = DbHelper.GetConnection())
            {
                College college = db.Get<College>(ID);
                if (college != null)
                    db.Delete(college);
                return RedirectToAction("Index");
            }
        }

        public IActionResult CreateUpdate(CollegeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = DbHelper.GetConnection())
                {
                    if (viewModel.EditableCollege.CollegeID == null)
                    {
                        viewModel.EditableCollege.CollegeID = viewModel.Colleges.Count;
                        db.Insert<College>(viewModel.EditableCollege);
                    }
                    else
                    {
                        College dbItem = db.Get<College>(viewModel.EditableCollege.CollegeID);
                        TryUpdateModelAsync<College>(dbItem, "EditableCollege");
                        db.Update<College>(dbItem);
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                string email = HttpContext.Session.GetString("emailAddress");
                if (email == null)
                {
                    return View("Index", viewModel);
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
                    return View("Index", new College());
                }
            }
        }
    }
}

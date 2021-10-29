using FYP_Project.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Dapper.Contrib.Extensions;
using FYP_Project.ViewModels;
using FYP_Project.Helpers;

namespace FYP_Project.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            CollegeViewModel viewModel = new CollegeViewModel();

            return View("index", viewModel);
        }

        public IActionResult Edit(int ID)
        {
            CollegeViewModel viewModel = new CollegeViewModel();
            viewModel.EditableCollege = viewModel.Colleges.FirstOrDefault(x => x.CollegeID == ID);
            return View("Index", viewModel);
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
                using(var db = DbHelper.GetConnection())
                {
                    if(viewModel.EditableCollege.CollegeID == null)
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
                return View("Index", new College());
            }
        }
    }

}

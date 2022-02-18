using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using FYP_Project.ViewModels;
using FYP_Project.Models;
using FYP_Project.Helpers;
using Microsoft.AspNetCore.Http;

namespace FYP_Project.Controllers
{
    public class Results : Controller
    {
        public IActionResult Index()
        {
            ResultsViewModel viewModel = new ResultsViewModel();
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

        public IActionResult CreateUpdate(ResultsViewModel viewModel)
        {

            using (var db = DbHelper.GetConnection())
            {
                if (viewModel.EditableResult.ResultID == null)
                {
                    viewModel.EditableResult.ResultID = viewModel.Results.Count;
                    db.Insert<Result>(viewModel.EditableResult);
                }
                else
                {
                    Result dbItem = db.Get<Result>(viewModel.EditableResult.ResultID);
                    TryUpdateModelAsync<Result>(dbItem, "EditableResult");
                    db.Update<Result>(viewModel.EditableResult);
                }
            }
            string email = HttpContext.Session.GetString("emailAddress");
            if (email == null)
            {
                return RedirectToAction("Index", "Home");
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
                return RedirectToAction("ViewDetails", "LogIn");
            }
        }
    }
}

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
    public class ResultsController : Controller
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

        public IActionResult Accept(int ID)
        {
            ResultsViewModel viewModel = new ResultsViewModel();
            using (var db = DbHelper.GetConnection())
            {
                foreach (var result in viewModel.Results)
                {
                    if (result.ResultID == ID)
                    {
                        viewModel.EditableResult = result;
                        viewModel.EditableResult.Verified = 1;
                        Result dbItem = db.Get<Result>(viewModel.EditableResult.ResultID);
                        TryUpdateModelAsync<Result>(dbItem, "EditableResult");
                        db.Update<Result>(viewModel.EditableResult);
                    }
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
                return View("Index", viewModel);
            }
        }

        public IActionResult Reject(int ID)
        {
            ResultsViewModel viewModel = new ResultsViewModel();
            using (var db = DbHelper.GetConnection())
            {
                foreach (var result in viewModel.Results)
                {
                    if (result.ResultID == ID)
                    {
                        viewModel.EditableResult = result;
                        viewModel.EditableResult.Completed = 0;
                        Result dbItem = db.Get<Result>(viewModel.EditableResult.ResultID);
                        TryUpdateModelAsync<Result>(dbItem, "EditableResult");
                        db.Update<Result>(viewModel.EditableResult);
                    }
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
                return RedirectToAction("Index", "Results");
            }
        }

        public IActionResult Submit(int ID)
        {
            ResultsViewModel viewModel = new ResultsViewModel();
            foreach (var result in viewModel.Results)
            {
                if (result.ResultID == ID)
                {
                    viewModel.EditableResult = result;
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
                return View("Submit", viewModel);
            }
        }

        public IActionResult Verify(ResultsViewModel viewModel)
        {
            string email = HttpContext.Session.GetString("emailAddress");
            foreach (var player in viewModel.Players)
            {
                if (email == player.emailAddress)
                {
                    viewModel.EditablePlayer = player;
                }
            }

            using (var db = DbHelper.GetConnection())
            {
                if (viewModel.EditableResult.Team1Score > viewModel.EditableResult.Team2Score)
                {
                    viewModel.EditableResult.WinningTeamID = viewModel.EditableResult.Team1ID;
                }
                else
                {
                    viewModel.EditableResult.WinningTeamID = viewModel.EditableResult.Team2ID;
                }
                viewModel.EditableResult.CompletedBy = viewModel.EditablePlayer.TeamID;
                viewModel.EditableResult.Completed = 1;
                Result dbItem = db.Get<Result>(viewModel.EditableResult.ResultID);
                TryUpdateModelAsync<Result>(dbItem, "EditableResult");
                db.Update<Result>(viewModel.EditableResult);

            }

            if (email == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Results");
            }
        }

        public IActionResult CreateFixture()
        {
            ResultsViewModel viewModel = new ResultsViewModel();
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
                return View("CreateFixture", viewModel);
            }
        }

        public IActionResult Create()
        {
            ResultsViewModel viewModel = new ResultsViewModel();
            string email = HttpContext.Session.GetString("emailAddress");
            int GameID = 0;
            int? Team1ID = 0;
            int? Team2ID = 0;
            string Team1Name = Request.Form["Team1Name"].ToString();
            string Team2Name = Request.Form["Team2Name"].ToString();
            int group = Int32.Parse(Request.Form["Group"]);
            int playoff = Int32.Parse(Request.Form["Playoff"].ToString());

            foreach (var player in viewModel.Players)
            {
                if (email == player.emailAddress)
                {
                    viewModel.EditablePlayer = player;
                }
            }

            foreach(var team in viewModel.Teams)
            {
                if(team.TeamName == Team1Name)
                {
                    GameID = team.GameID;
                    Team1ID = team.TeamID;
                }
                else if(team.TeamName == Team2Name)
                {
                    Team2ID = team.TeamID;
                }
            }

            using (var db = DbHelper.GetConnection())
            {
                viewModel.EditableResult.ResultID = viewModel.Results.Count;
                viewModel.EditableResult.Team1ID = Team1ID;
                viewModel.EditableResult.Team2ID = Team2ID;
                viewModel.EditableResult.Group = group;
                viewModel.EditableResult.Playoffs = playoff;
                viewModel.EditableResult.GameID = GameID;
                viewModel.EditableResult.MatchDate = DateTime.Parse(Request.Form["EditableResult.MatchDate"]);
                db.Insert<Result>(viewModel.EditableResult);
            }

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
                return View("Confirmation", viewModel);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using FYP_Project.ViewModels;
using FYP_Project.Helpers;
using FYP_Project.Models;
using Microsoft.AspNetCore.Http;

namespace FYP_Project.Controllers
{
    public class TeamsController : Controller
    {
        public IActionResult Index()
        {
            string email = HttpContext.Session.GetString("emailAddress");

            TeamViewModel viewModel = new TeamViewModel();
            if (email == null)
            {
                return RedirectToAction("Index", "LogIn");
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
                foreach (var team in viewModel.Teams)
                {
                    if (team.TeamID == viewModel.EditablePlayer.TeamID)
                    {
                        viewModel.EditableTeam = team;
                    }
                }
                return View("Index", viewModel);
            }
        }

        public IActionResult Edit()
        {
            string email = HttpContext.Session.GetString("emailAddress");

            TeamViewModel viewModel = new TeamViewModel();
            if (email == null)
            {
                return RedirectToAction("Index", "LogIn");
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
                foreach (var team in viewModel.Teams)
                {
                    if (team.TeamID == viewModel.EditablePlayer.TeamID)
                    {
                        viewModel.EditableTeam = team;
                    }
                }
                return View("Edit", viewModel);
            }
        }

        public IActionResult CreateUpdate(TeamViewModel viewModel)
        {

            using (var db = DbHelper.GetConnection())
            {
                if (viewModel.EditableTeam.TeamID == null)
                {
                    viewModel.EditableTeam.TeamID = viewModel.Teams.Count;
                    viewModel.EditableTeam.GameID = Int32.Parse(Request.Form["TeamGame"]);
                    foreach(var college in viewModel.Colleges)
                    {
                        if(Request.Form["TeamCollege"].ToString() == college.CollegeName)
                        {
                            viewModel.EditableTeam.CollegeID = college.CollegeID;
                        }
                    }
                    switch (viewModel.EditableTeam.GameID)
                    {
                        case 0:
                            viewModel.EditableTeam.ImageURL = "Rocket-League-Emblem.png";
                            break;
                        case 1:
                            viewModel.EditableTeam.ImageURL = "Val-Logo.png";
                            break;
                        case 2:
                            viewModel.EditableTeam.ImageURL = "League-of-Legends-Logo.png";
                            break;
                        case 3:
                            viewModel.EditableTeam.ImageURL = "csgo_logo.png";
                            break;
                    }
                    db.Insert<Team>(viewModel.EditableTeam);
                }
                else
                {
                    viewModel.EditableTeam.GameID = Int32.Parse(Request.Form["TeamGame"]);
                    foreach (var college in viewModel.Colleges)
                    {
                        if (Request.Form["TeamCollege"].ToString() == college.CollegeName)
                        {
                            viewModel.EditableTeam.CollegeID = college.CollegeID;
                        }
                    }
                    Team dbItem = db.Get<Team>(viewModel.EditableTeam.TeamID);
                    TryUpdateModelAsync<Team>(dbItem, "EditableTeam");
                    db.Update<Team>(viewModel.EditableTeam);
                }
            }

            string email = HttpContext.Session.GetString("emailAddress");
            if (email == null)
            {
                return RedirectToAction("Index", "LogIn");
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
                foreach (var team in viewModel.Teams)
                {
                    if (team.TeamID == viewModel.EditablePlayer.TeamID)
                    {
                        viewModel.EditableTeam = team;
                    }
                }
                return RedirectToAction("Index", "Teams");
            }
        }
    }
}

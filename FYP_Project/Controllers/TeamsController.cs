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
using System.Diagnostics;
using System.IO;

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
                    /* Code for uploading images adapted from here: http://www.aspdotnet-pools.com/2022/01/file-upload-in-wwroot-folder-using.html */
                    if (Request.Form.Files["img"].FileName != "")
                    {
                        string fileName = Request.Form.Files["img"].FileName;
                        fileName = Path.GetFileName(fileName);
                        string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);
                        var stream = new FileStream(uploadPath, FileMode.Create);
                        Request.Form.Files["img"].CopyToAsync(stream);
                        viewModel.EditableTeam.ImageURL = fileName;
                    }
                    db.Insert<Team>(viewModel.EditableTeam);

                    viewModel.EditablePlayer.TeamID = viewModel.EditableTeam.TeamID;
                    viewModel.EditablePlayer.Captain = 1;
                    Player dbItem = db.Get<Player>(viewModel.EditablePlayer.PlayerID);
                    TryUpdateModelAsync<Player>(dbItem, "EditablePlayer");
                    db.Update<Player>(viewModel.EditablePlayer);
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
                    foreach(var team in viewModel.Teams)
                    {
                        if(team.TeamID == viewModel.EditablePlayer.TeamID)
                        {
                            viewModel.EditableTeam.ImageURL = team.ImageURL;
                        }
                    }
                    
                    Team dbItem = db.Get<Team>(viewModel.EditableTeam.TeamID);
                    TryUpdateModelAsync<Team>(dbItem, "EditableTeam");
                    db.Update<Team>(viewModel.EditableTeam);
                }
            }

            if (email == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            else
            {
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

        public IActionResult SelectGame()
        {
            TeamViewModel viewModel = new TeamViewModel();
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
                return View("SelectGame", viewModel);
            }
        }

        public IActionResult JoinTeam(TeamViewModel viewModel)
        {
            string GameName = Request.Form["GameName"].ToString();
            int gameID = 0;
            List<Team> TempTeams = new List<Team>();
            foreach (var game in viewModel.Games)
            {
                if (game.GameName == GameName)
                {
                    gameID = game.GameID;
                }
            }
            foreach (var team in viewModel.Teams)
            {
                if(team.GameID == gameID)
                {
                    TempTeams.Add(team);
                }
            }
            viewModel.GameTeams = TempTeams.OrderByDescending(team => team.TeamName).ToList();

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
                return View("SelectTeam", viewModel);
            }
        }

        public IActionResult CreateRequest(TeamViewModel viewModel)
        {
            string email = HttpContext.Session.GetString("emailAddress");
            foreach (var player in viewModel.Players)
            {
                if (email == player.emailAddress)
                {
                    viewModel.EditablePlayer = player;
                }
            }
            JoinRequestViewModel joinRequestViewModel = new JoinRequestViewModel();
            viewModel.EditableJoinRequest = new JoinRequest();
            string TeamName = Request.Form["Team"].ToString();
            using (var db = DbHelper.GetConnection())
            {
                if (viewModel.EditablePlayer.TeamID == null)
                {
                    viewModel.EditableJoinRequest.RequestID = viewModel.joinRequests.Count;
                    viewModel.EditableJoinRequest.PlayerID = viewModel.EditablePlayer.PlayerID;
                    viewModel.EditableJoinRequest.Completed = 0;
                    foreach(var team in viewModel.Teams)
                    {
                        if(team.TeamName == TeamName)
                        {
                            viewModel.EditableJoinRequest.TeamID = team.TeamID;
                        }
                    }
                    joinRequestViewModel.EditableJoinRequest = viewModel.EditableJoinRequest;
                    db.Insert<JoinRequest>(joinRequestViewModel.EditableJoinRequest);
                }
            }

            if (email == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            else
            {
                foreach (var team in viewModel.Teams)
                {
                    if (team.TeamID == viewModel.EditablePlayer.TeamID)
                    {
                        viewModel.EditableTeam = team;
                    }
                }
                return View("Confirmation", viewModel);
            }
        }

        public IActionResult Remove(int ID)
        {
            TeamViewModel viewModel = new TeamViewModel();
            viewModel.EditablePlayer = new Player();
            foreach (var player in viewModel.Players)
            {
                if (ID == player.PlayerID)
                {
                    viewModel.EditablePlayer = player;
                }
            }
            using (var db = DbHelper.GetConnection())
            {
                viewModel.EditablePlayer.TeamID = null;
                Player dbItem = db.Get<Player>(viewModel.EditablePlayer.PlayerID);
                TryUpdateModelAsync<Player>(dbItem, "EditablePlayer");
                db.Update<Player>(viewModel.EditablePlayer);
            }

            string email = HttpContext.Session.GetString("emailAddress");
            if (email == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            else
            {
                foreach (var team in viewModel.Teams)
                {
                    foreach (var player in viewModel.Players)
                    {
                        if (email == player.emailAddress)
                        {
                            viewModel.EditablePlayer = player;
                        }
                    }
                    if (team.TeamID == viewModel.EditablePlayer.TeamID)
                    {
                        viewModel.EditableTeam = team;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Delete(int ID)
        {
            TeamViewModel viewModel = new TeamViewModel();
            using (var db = DbHelper.GetConnection())
            {
                foreach (var player in viewModel.Players)
                {
                    if (player.TeamID == ID)
                    {
                        viewModel.EditablePlayer = player;
                        viewModel.EditablePlayer.TeamID = null;
                        if (player.Captain == 1)
                        {
                            viewModel.EditablePlayer.Captain = 0;
                        }
                        Player dbItem = db.Get<Player>(viewModel.EditablePlayer.PlayerID);
                        TryUpdateModelAsync<Player>(dbItem, "EditablePlayer");
                        db.Update<Player>(viewModel.EditablePlayer);
                    }
                }

                Team team = db.Get<Team>(ID);
                if (team != null)
                    db.Delete(team);

                return RedirectToAction("Index", "Home");
            }
        }
    }
}

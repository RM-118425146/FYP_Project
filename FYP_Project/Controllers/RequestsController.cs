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
    public class RequestsController : Controller
    {
        public IActionResult Index()
        {
            string email = HttpContext.Session.GetString("emailAddress");
            JoinRequestViewModel viewModel = new JoinRequestViewModel();
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

        public IActionResult Accept(int ID)
        {
            JoinRequestViewModel viewModel = new JoinRequestViewModel();
            viewModel.EditablePlayer = new Player();
            string email = HttpContext.Session.GetString("emailAddress");
            int? TeamID = 0;
            foreach (var player in viewModel.Players)
            {
                if (email == player.emailAddress)
                {
                    TeamID = player.TeamID;
                }
            }

            using (var db = DbHelper.GetConnection())
            {
                foreach(var request in viewModel.joinRequests)
                {
                    if(request.RequestID == ID)
                    {
                        viewModel.EditableJoinRequest = request;
                        viewModel.EditableJoinRequest.Completed = 1;
                    }
                    foreach(var player in viewModel.Players)
                    {
                        if(player.PlayerID == viewModel.EditableJoinRequest.PlayerID)
                        {
                            viewModel.EditablePlayer = player;
                        }
                    }
                }
                JoinRequest dbItem = db.Get<JoinRequest>(viewModel.EditableJoinRequest.RequestID);
                TryUpdateModelAsync<JoinRequest>(dbItem, "EditableJoinRequest");
                db.Update<JoinRequest>(viewModel.EditableJoinRequest);

                viewModel.EditablePlayer.TeamID = TeamID;
                Player dbItem1 = db.Get<Player>(viewModel.EditablePlayer.PlayerID);
                TryUpdateModelAsync<Player>(dbItem1, "EditablePlayer");
                db.Update<Player>(viewModel.EditablePlayer);

            }

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

        public IActionResult Decline(int ID)
        {
            JoinRequestViewModel viewModel = new JoinRequestViewModel();
            viewModel.EditablePlayer = new Player();
            string email = HttpContext.Session.GetString("emailAddress");

            using (var db = DbHelper.GetConnection())
            {
                foreach (var request in viewModel.joinRequests)
                {
                    if (request.RequestID == ID)
                    {
                        viewModel.EditableJoinRequest = request;
                        viewModel.EditableJoinRequest.Completed = 1;
                    }

                }
                JoinRequest dbItem = db.Get<JoinRequest>(viewModel.EditableJoinRequest.RequestID);
                TryUpdateModelAsync<JoinRequest>(dbItem, "EditableJoinRequest");
                db.Update<JoinRequest>(viewModel.EditableJoinRequest);

            }

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
    }
}

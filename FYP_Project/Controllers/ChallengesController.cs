using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using FYP_Project.ViewModels;
using FYP_Project.Models;
using FYP_Project.Helpers;
using Microsoft.AspNetCore.Http;

namespace FYP_Project.Controllers
{
    public class ChallengesController : Controller
    {
        public IActionResult Index()
        {
            string email = HttpContext.Session.GetString("emailAddress");

            ChallengeViewModel viewModel = new ChallengeViewModel();

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

        public IActionResult Challenges()
        {
            string email = HttpContext.Session.GetString("emailAddress");

            ChallengeViewModel viewModel = new ChallengeViewModel();

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
                return View("ChallengeTeam", viewModel);
            }
        }

        public IActionResult Create(ChallengeViewModel viewModel)
        {
            string email = HttpContext.Session.GetString("emailAddress");
            foreach (var player in viewModel.Players)
            {
                if (email == player.emailAddress)
                {
                    viewModel.EditablePlayer = player;
                }
            }

            string TeamName = Request.Form["TeamName"].ToString();
            string ChallengeType = Request.Form["ChallengeType"].ToString();
            string Amount = Request.Form["Amount"].ToString();
            ChallengeType = ChallengeType.Replace('x', char.Parse(Amount));
            using (var db = DbHelper.GetConnection())
            {
                viewModel.EditableChallenge.ChallengeID = viewModel.Challenges.Count;
                viewModel.EditableChallenge.ChallengerTeamID = viewModel.EditablePlayer.TeamID;
                viewModel.EditableChallenge.ChallengeType = ChallengeType;
                viewModel.EditableChallenge.ChallengeDate = System.DateTime.Now;
                viewModel.EditableChallenge.Accepted = 0;
                foreach(var team in viewModel.Teams)
                {
                    if(team.TeamName == TeamName)
                    {
                        viewModel.EditableChallenge.TeamID = team.TeamID;
                    }
                }
                db.Insert<Challenge>(viewModel.EditableChallenge);
            }

            if (email == null)
            {
                return View("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }          
        }

        public IActionResult Accept(int ID)
        {
            ChallengeViewModel viewModel = new ChallengeViewModel();
            using (var db = DbHelper.GetConnection())
            {
                foreach (var challenge in viewModel.Challenges)
                {
                    if (challenge.ChallengeID == ID)
                    {
                        viewModel.EditableChallenge = challenge;
                        viewModel.EditableChallenge.Accepted = 1;
                        Challenge dbItem = db.Get<Challenge>(viewModel.EditableChallenge.ChallengeID);
                        TryUpdateModelAsync<Challenge>(dbItem, "EditableChallenge");
                        db.Update<Challenge>(viewModel.EditableChallenge);
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

        public IActionResult Decline(int ID)
        {
            ChallengeViewModel viewModel = new ChallengeViewModel();
            using (var db = DbHelper.GetConnection())
            {
                foreach (var challenge in viewModel.Challenges)
                {
                    if (challenge.ChallengeID == ID)
                    {
                        viewModel.EditableChallenge = challenge;
                        viewModel.EditableChallenge.Accepted = 2;
                        Challenge dbItem = db.Get<Challenge>(viewModel.EditableChallenge.ChallengeID);
                        TryUpdateModelAsync<Challenge>(dbItem, "EditableChallenge");
                        db.Update<Challenge>(viewModel.EditableChallenge);
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
    }
}

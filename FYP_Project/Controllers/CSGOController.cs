using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FYP_Project.ViewModels;

namespace FYP_Project.Controllers
{
    [Route("/CSGO")]
    public class CSGOController : Controller
    {
        [Route("/CSGO/Home")]
        public IActionResult Index()
        {
            GameViewModel viewModel = new GameViewModel();
            foreach (var game in viewModel.Games)
            {
                if (game.GameName == "CS:GO")
                {
                    viewModel.EditableGame = game;
                }
            }
            return View("Index", viewModel);
        }

        [Route("/CSGO/Teams")]
        public IActionResult Teams()
        {
            TeamViewModel viewModel = new TeamViewModel();
            return View("Teams", viewModel);
        }

        [Route("/CSGO/Bracket")]
        public IActionResult Bracket()
        {
            return View("Bracket");
        }

        [Route("{name}")]
        public IActionResult Players(string name)
        {
            TeamViewModel viewModel = new TeamViewModel();
            foreach (var Team in viewModel.Teams)
            {
                if (Team.TeamName == name)
                {
                    viewModel.EditableTeam = Team;
                }
            }
            return View("Players", viewModel);
        }
    }
}

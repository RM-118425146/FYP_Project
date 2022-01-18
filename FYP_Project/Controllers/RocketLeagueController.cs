using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FYP_Project.ViewModels;
using FYP_Project.Models;

namespace FYP_Project.Controllers
{
    [Route("/RocketLeague")]
    public class RocketLeagueController : Controller
    {

        [Route("/RocketLeague/Home")]
        public IActionResult Index()
        {
            GameViewModel viewModel = new GameViewModel();
            foreach (var game in viewModel.Games)
            {
                if (game.GameName == "Rocket League")
                {
                    viewModel.EditableGame = game;
                }
            }
            return View("Index", viewModel);
        }

        [Route("/RocketLeague/Teams")]
        public IActionResult Teams()
        {
            TeamViewModel viewModel = new TeamViewModel();
            return View("Teams",viewModel);
        }

        [Route("/RocketLeague/Bracket")]
        public IActionResult Bracket()
        {
            ResultsViewModel viewModel = new ResultsViewModel();
            List<Record> TempA = new List<Record>();
            List<Record> TempB = new List<Record>();
            List<Record> TempC = new List<Record>();
            List<Record> TempD = new List<Record>();

            /*
            var filteredResults = viewModel.Results.Where(result => result.Group.Equals(4));
            foreach (var result in filteredResults)
            {
                if(TempD.Count == 0)
                {
                    Record temp = new Record(result.WinningTeamID, 1, 0);
                    TempD.Add(temp);
                }
                else
                {
                    foreach(var team in TempD)
                    {
                        if(result.WinningTeamID == team.TeamID)
                        {
                            int wins = team.Wins + 1;
                            Record temp = new Record(team.TeamID, wins, team.Losses);
                            TempD.RemoveAll(team => team.TeamID == result.WinningTeamID);
                            TempD.Add(temp);
                        }
                        else
                        {
                            Record temp = new Record(team.TeamID, 1, 0);
                            TempD.Add(temp);
                        }
                    }
                }
            }

            */
            foreach (var Result in viewModel.Results)
            {
                if (Result.Playoffs == 1)
                {

                    if (Result.Group == 1)
                    {

                    }
                    else if (Result.Group == 2)
                    {

                    }
                    else if (Result.Group == 3)
                    {

                    }
                    else if (Result.Group == 4)
                    {
                        if (TempD.Count == 0)
                        {
                            Record temp = new Record(Result.WinningTeamID, 1, 0);
                            Record temp2 = new Record(Result.LosingTeamID, 0, 1);
                            TempD.Add(temp);
                            TempD.Add(temp2);
                        }
                        else
                        {
                            
                            for (int j = TempD.Count - 1; j >= 0; j--)
                            {
                                if (Result.WinningTeamID == TempD[j].TeamID)
                                {
                                    TempD[j].Wins++;
                                }
                                else if (Result.LosingTeamID == TempD[j].TeamID)
                                {
                                    TempD[j].Losses++;
                                }
                                else if (j == 0 && (Result.WinningTeamID != TempD[j].TeamID || Result.LosingTeamID != TempD[j].TeamID))
                                {
                                    if (Result.WinningTeamID != TempD[j].TeamID)
                                    {
                                        Record temp = new Record(Result.WinningTeamID, 1, 0);
                                        TempD.Add(temp);
                                    }
                                    else
                                    {
                                        Record temp = new Record(Result.LosingTeamID, 1, 0);
                                        TempD.Add(temp);
                                    }
                                }
                                else
                                {
                                    Record temp = new Record(Result.WinningTeamID, 1, 0);
                                    TempD.Add(temp);
                                }
                            }
                            

                            /*
                            foreach(var team in TempD)
                            {
                                if(Result.WinningTeamID == team.TeamID)
                                {
                                    int wins = team.Wins + 1;
                                    Record temp = new Record(team.TeamID, wins, team.Losses);
                                    TempD.RemoveAll(team => team.TeamID == Result.WinningTeamID);
                                    TempD.Add(temp);
                                }
                                else
                                {
                                    Record temp = new Record(Result.WinningTeamID, 1, 0);
                                    TempD.Add(temp);
                                }
                            }
                            */
                        }
                    }
                }
            }

            viewModel.GroupD = TempD.OrderByDescending(team => team.Wins).ToList();
            return View("Bracket", viewModel);
        }

        [Route("{name}")]
        public IActionResult Players(string name)
        {
            TeamViewModel viewModel = new TeamViewModel();
            foreach(var Team in viewModel.Teams)
            {
                if(Team.TeamName == name)
                {
                    viewModel.EditableTeam = Team;
                }
            }
            return View("Players", viewModel);
        }
    }
}

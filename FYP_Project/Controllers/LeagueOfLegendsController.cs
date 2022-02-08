using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FYP_Project.ViewModels;
using FYP_Project.Models;

namespace FYP_Project.Controllers
{
    [Route("/LeagueOfLegends")]
    public class LeagueOfLegendsController : Controller
    {
        [Route("/LeagueOfLegends/Home")]
        public IActionResult Index()
        {
            GameViewModel viewModel = new GameViewModel();
            foreach (var game in viewModel.Games)
            {
                if (game.GameName == "League of Legends")
                {
                    viewModel.EditableGame = game;
                }
            }
            return View("Index", viewModel);
        }

        [Route("/LeagueOfLegends/Teams")]
        public IActionResult Teams()
        {
            TeamViewModel viewModel = new TeamViewModel();
            return View("Teams", viewModel);
        }

        [Route("/LeagueOfLegends/Bracket")]
        public IActionResult Bracket()
        {
            ResultsViewModel viewModel = new ResultsViewModel();
            List<Record> TempA = new List<Record>();
            List<Record> TempB = new List<Record>();
            List<Record> TempC = new List<Record>();
            List<Record> TempD = new List<Record>();

            foreach (var Result in viewModel.Results)
            {
                if (Result.GameID == 2)
                {

                    if (Result.Playoffs == 1)
                    {

                        if (Result.Group == 1)
                        {
                            if (TempA.Count == 0)
                            {
                                Record temp = new Record(Result.WinningTeamID, 1, 0);
                                Record temp2 = new Record(Result.LosingTeamID, 0, 1);
                                TempA.Add(temp);
                                TempA.Add(temp2);
                            }
                            else
                            {

                                for (int j = TempA.Count - 1; j >= 0; j--)
                                {
                                    if (Result.WinningTeamID == TempA[j].TeamID)
                                    {
                                        TempA[j].Wins++;
                                        j = 0;
                                        for (int k = TempA.Count - 1; k >= 0; k--)
                                        {
                                            if (TempA[k].TeamID == Result.LosingTeamID)
                                            {
                                                TempA[k].Losses++;
                                                k = 0;
                                            }
                                            else if (k == 0 && (TempA[k].TeamID != Result.LosingTeamID))
                                            {
                                                Record temp = new Record(Result.LosingTeamID, 0, 1);
                                                TempA.Add(temp);
                                            }
                                        }
                                    }
                                    else if (j == 0 && Result.WinningTeamID != TempA[j].TeamID)
                                    {
                                        if (Result.WinningTeamID != TempA[j].TeamID)
                                        {
                                            Record temp = new Record(Result.WinningTeamID, 1, 0);
                                            TempA.Add(temp);

                                            for (int k = TempA.Count - 1; k >= 0; k--)
                                            {
                                                if (TempA[k].TeamID == Result.LosingTeamID)
                                                {
                                                    TempA[k].Losses++;
                                                    k = 0;
                                                }
                                                else if (k == 0 && (TempA[k].TeamID != Result.LosingTeamID))
                                                {
                                                    Record temp2 = new Record(Result.LosingTeamID, 0, 1);
                                                    TempA.Add(temp2);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (Result.Group == 2)
                        {
                            if (TempB.Count == 0)
                            {
                                Record temp = new Record(Result.WinningTeamID, 1, 0);
                                Record temp2 = new Record(Result.LosingTeamID, 0, 1);
                                TempB.Add(temp);
                                TempB.Add(temp2);
                            }
                            else
                            {

                                for (int j = TempB.Count - 1; j >= 0; j--)
                                {
                                    if (Result.WinningTeamID == TempB[j].TeamID)
                                    {
                                        TempB[j].Wins++;
                                        j = 0;
                                        for (int k = TempB.Count - 1; k >= 0; k--)
                                        {
                                            if (TempB[k].TeamID == Result.LosingTeamID)
                                            {
                                                TempB[k].Losses++;
                                                k = 0;
                                            }
                                            else if (k == 0 && (TempB[k].TeamID != Result.LosingTeamID))
                                            {
                                                Record temp = new Record(Result.LosingTeamID, 0, 1);
                                                TempB.Add(temp);
                                            }
                                        }
                                    }
                                    else if (j == 0 && Result.WinningTeamID != TempB[j].TeamID)
                                    {
                                        if (Result.WinningTeamID != TempB[j].TeamID)
                                        {
                                            Record temp = new Record(Result.WinningTeamID, 1, 0);
                                            TempB.Add(temp);

                                            for (int k = TempB.Count - 1; k >= 0; k--)
                                            {
                                                if (TempB[k].TeamID == Result.LosingTeamID)
                                                {
                                                    TempB[k].Losses++;
                                                    k = 0;
                                                }
                                                else if (k == 0 && (TempB[k].TeamID != Result.LosingTeamID))
                                                {
                                                    Record temp2 = new Record(Result.LosingTeamID, 0, 1);
                                                    TempB.Add(temp2);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (Result.Group == 3)
                        {
                            if (TempC.Count == 0)
                            {
                                Record temp = new Record(Result.WinningTeamID, 1, 0);
                                Record temp2 = new Record(Result.LosingTeamID, 0, 1);
                                TempC.Add(temp);
                                TempC.Add(temp2);
                            }
                            else
                            {

                                for (int j = TempC.Count - 1; j >= 0; j--)
                                {
                                    if (Result.WinningTeamID == TempC[j].TeamID)
                                    {
                                        TempC[j].Wins++;
                                        j = 0;
                                        for (int k = TempC.Count - 1; k >= 0; k--)
                                        {
                                            if (TempC[k].TeamID == Result.LosingTeamID)
                                            {
                                                TempC[k].Losses++;
                                                k = 0;
                                            }
                                            else if (k == 0 && (TempC[k].TeamID != Result.LosingTeamID))
                                            {
                                                Record temp = new Record(Result.LosingTeamID, 0, 1);
                                                TempC.Add(temp);
                                            }
                                        }
                                    }
                                    else if (j == 0 && Result.WinningTeamID != TempC[j].TeamID)
                                    {
                                        if (Result.WinningTeamID != TempC[j].TeamID)
                                        {
                                            Record temp = new Record(Result.WinningTeamID, 1, 0);
                                            TempC.Add(temp);

                                            for (int k = TempC.Count - 1; k >= 0; k--)
                                            {
                                                if (TempC[k].TeamID == Result.LosingTeamID)
                                                {
                                                    TempC[k].Losses++;
                                                    k = 0;
                                                }
                                                else if (k == 0 && (TempC[k].TeamID != Result.LosingTeamID))
                                                {
                                                    Record temp2 = new Record(Result.LosingTeamID, 0, 1);
                                                    TempC.Add(temp2);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
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
                                        j = 0;
                                        for (int k = TempD.Count - 1; k >= 0; k--)
                                        {
                                            if (TempD[k].TeamID == Result.LosingTeamID)
                                            {
                                                TempD[k].Losses++;
                                                k = 0;
                                            }
                                            else if (k == 0 && (TempD[k].TeamID != Result.LosingTeamID))
                                            {
                                                Record temp = new Record(Result.LosingTeamID, 0, 1);
                                                TempD.Add(temp);
                                            }
                                        }
                                    }
                                    else if (j == 0 && Result.WinningTeamID != TempD[j].TeamID)
                                    {
                                        if (Result.WinningTeamID != TempD[j].TeamID)
                                        {
                                            Record temp = new Record(Result.WinningTeamID, 1, 0);
                                            TempD.Add(temp);

                                            for (int k = TempD.Count - 1; k >= 0; k--)
                                            {
                                                if (TempD[k].TeamID == Result.LosingTeamID)
                                                {
                                                    TempD[k].Losses++;
                                                    k = 0;
                                                }
                                                else if (k == 0 && (TempD[k].TeamID != Result.LosingTeamID))
                                                {
                                                    Record temp2 = new Record(Result.LosingTeamID, 0, 1);
                                                    TempD.Add(temp2);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            viewModel.GroupA = TempA.OrderByDescending(team => team.Wins).ToList();
            viewModel.GroupB = TempB.OrderByDescending(team => team.Wins).ToList();
            viewModel.GroupC = TempC.OrderByDescending(team => team.Wins).ToList();
            viewModel.GroupD = TempD.OrderByDescending(team => team.Wins).ToList();
            return View("Bracket", viewModel);
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

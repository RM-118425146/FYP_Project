using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using FYP_Project.Models;
using Dapper;
using FYP_Project.Helpers;

namespace FYP_Project.ViewModels
{
    public class CollegeViewModel
    {
        public CollegeViewModel()
        {
            using(var db = DbHelper.GetConnection())
            {
                this.EditableCollege = new College();
                this.Colleges = db.Query<College>("Select * FROM Colleges ORDER BY CollegeID DESC").ToList();
                this.Teams = db.Query<Team>("Select * FROM Teams ORDER BY TeamID DESC").ToList();
                this.Players = db.Query<Player>("Select * FROM Players ORDER BY PlayerID DESC").ToList();
                this.Games = db.Query<Game>("Select * FROM Games ORDER BY GameID DESC").ToList();
                this.Results = db.Query<Result>("Select * FROM Results ORDER BY ResultID DESC").ToList();
            }
        }

        public List<College> Colleges { get; set; }
        public List<Team> Teams { get; set; }
        public List<Player> Players { get; set; }
        public List<Game> Games { get; set; }
        public List<Result> Results { get; set; }

        public College EditableCollege { get; set; }
        public Player EditablePlayer { get; set; }
    }
}

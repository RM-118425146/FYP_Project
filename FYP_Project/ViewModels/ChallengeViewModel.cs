using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using FYP_Project.Models;
using Dapper;
using FYP_Project.Helpers;

namespace FYP_Project.ViewModels
{
    public class ChallengeViewModel
    {
        public ChallengeViewModel()
        {
            using (var db = DbHelper.GetConnection())
            {
                this.EditableChallenge = new Challenge();
                this.Teams = db.Query<Team>("Select * FROM Teams ORDER BY TeamID DESC").ToList();
                this.Colleges = db.Query<College>("Select * FROM Colleges ORDER BY CollegeID DESC").ToList();
                this.Players = db.Query<Player>("Select * FROM Players ORDER BY PlayerID DESC").ToList();
                this.Games = db.Query<Game>("Select * FROM Games ORDER BY GameID DESC").ToList();
                this.Results = db.Query<Result>("Select * FROM Results ORDER BY ResultID DESC").ToList();
                this.joinRequests = db.Query<JoinRequest>("Select * FROM JoinRequests ORDER BY RequestID DESC").ToList();
                this.Challenges = db.Query<Challenge>("Select * FROM Challenges ORDER BY ChallengeID DESC").ToList();
            }
        }

        public List<Team> Teams { get; set; }
        public List<College> Colleges { get; set; }
        public List<Player> Players { get; set; }
        public List<Game> Games { get; set; }
        public List<Result> Results { get; set; }
        public List<JoinRequest> joinRequests { get; set; }
        public List<Challenge> Challenges { get; set; }

        public Team EditableTeam { get; set; }
        public Player EditablePlayer { get; set; }
        public Challenge EditableChallenge { get; set; }
        public List<Team> GameTeams { get; set; }
    }
}

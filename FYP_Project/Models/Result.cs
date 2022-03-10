using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;

namespace FYP_Project.Models
{
    public class Result
    {

        [ExplicitKey]
        public int? ResultID { get; set; }

        public int? Team1ID { get; set; }

        public int? Team2ID { get; set; }

        public int Team1Score { get; set; }

        public int Team2Score { get; set; }

        public int? WinningTeamID { get; set; }

        public int? LosingTeamID { get; set; }

        public DateTime MatchDate { get; set; }

        public int GameID { get; set; }

        public string TwitchURL { get; set; }

        public int Verified { get; set; }

        public int Completed { get; set; }

        public int? CompletedBy { get; set; }

        /* 0 = true, 1 = false*/
        public int Playoffs { get; set; }

        /* 1 = a, 2 = b, 3=c, 4=d*/
        public int Group { get; set; }

        public string BallchasingLink { get; set; }

    }
}

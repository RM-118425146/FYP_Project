using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;

namespace FYP_Project.Models
{
    public class Challenge
    {

        [ExplicitKey]
        public int ChallengeID { get; set; }

        public int? TeamID { get; set; }

        public int? ChallengerTeamID { get; set; }

        public string ChallengeType { get; set; }

        public DateTime ChallengeDate { get; set; }

        public DateTime CompleteBy { get; set; }

        public int Accepted { get; set; }
    }
}

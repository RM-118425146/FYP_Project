using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FYP_Project.Models
{
    public class Record
    {
        public int? TeamID { get; set; }

        public int Wins { get; set; } 

        public int Losses { get; set; }

        public Record(int? teamID, int wins, int losses)
        {
            TeamID = teamID;
            Wins = wins;
            Losses = losses;
        }
    }
}

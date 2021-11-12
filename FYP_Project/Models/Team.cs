using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;

namespace FYP_Project.Models
{
    public class Team
    {
        [ExplicitKey]
        public int TeamID { get; set; }

        [Required]
        [MinLength(2, ErrorMessage ="Team name must be longer than 2 characters!")]
        [MaxLength(70, ErrorMessage ="Team name must be shorter than 70 characters!")]
        public string TeamName { get; set; }

        public int GameID { get; set; }

        public int  CollegeID{ get; set; }

        [Required]
        public string ImageURL { get; set; }
}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;


namespace FYP_Project.Models
{
    public class Player
    {
        [ExplicitKey]
        public int? PlayerID { get; set; }

        [Required]
        public string GamerTag { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "First name must be longer than 2 characters!")]
        [MaxLength(30, ErrorMessage = "First name must be shorter than 30 characters!")]
        public string PlayerFirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last name must be longer than 2 characters!")]
        [MaxLength(30, ErrorMessage = "Last name must be shorter than 30 characters!")]
        public string PlayerLastName { get; set; }

        public string PlayerAddress { get; set; }

        public int  PlayerTelephone { get; set; }

        public int TeamID { get; set; }

        public string? TwitchName { get; set; }

        public string emailAddress { get; set; }

        public string password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;


namespace FYP_Project.Models
{
    public class Game
    {
        [ExplicitKey]
        public int GameID { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Game name must be longer than 2 characters!")]
        [MaxLength(50, ErrorMessage = "Game name must be shorter than 50 characters!")]
        public string GameName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Name of day must be longer than 2 characters!")]
        [MaxLength(50, ErrorMessage = "Name of day must be shorter than 50 characters!")]
        public string GameDay { get; set; }
    }
}

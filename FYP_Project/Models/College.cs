using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;


namespace FYP_Project.Models
{
    public class College
    {
        [ExplicitKey]
        public int? CollegeID { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "College name must be longer than 2 characters!")]
        [MaxLength(200, ErrorMessage = "College name must be shorter than 200 characters!")]
        public string CollegeName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "College Abbreviation must be longer than 2 characters!")]
        [MaxLength(10, ErrorMessage = "College Abbreviation must be shorter than 10 characters!")]
        public string CollegeAbbv { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "College County must be longer than 2 characters!")]
        [MaxLength(50, ErrorMessage = "College County must be shorter than 50 characters!")]
        public string CollegeCounty { get; set; }
    }
}

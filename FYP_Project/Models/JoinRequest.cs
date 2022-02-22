using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;


namespace FYP_Project.Models
{
    public class JoinRequest
    {
        [ExplicitKey]
        public int? RequestID { get; set; }

        public int? PlayerID { get; set; }

        public int? TeamID { get; set; }

        public int? Completed { get; set; }
    }
}
